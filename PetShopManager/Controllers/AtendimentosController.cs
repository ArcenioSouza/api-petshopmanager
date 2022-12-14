using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopManager.Data;
using PetShopManager.DTO;
using PetShopManager.Models;

namespace PetShopManager.Controllers
{
    [ApiController]
    [Route("api/v1/atendimentos")]
    public class AtendimentosController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public AtendimentosController(ApplicationDbContext database)
        {
            _database = database;
        }

        [Authorize(Roles = "Funcionario")]
        [HttpPost]
        public async Task<ActionResult> Post(AtendimentoDTO atendimentoTemp)
        {
            try
            {
                Animal AnimalTemp = await _database.Animais.FirstAsync(animal => animal.Id == atendimentoTemp.AnimalId);
                Funcionario FuncionarioTemp = await _database.Funcionarios.Include(cargo => cargo.Cargo).FirstAsync(funcionario => funcionario.Id == atendimentoTemp.FuncionarioId);
                Servico ServicoTemp = await _database.Servicos.FirstAsync(servico => servico.Id == atendimentoTemp.ServicoId);
                Cliente ClienteTemp = await _database.Clientes.FirstAsync(cliente => cliente.Id == atendimentoTemp.ClienteId);

                Atendimento AtendimentoParaSalvar = new()
                {
                    Cliente = ClienteTemp,
                    Servico = ServicoTemp,
                    Animal = AnimalTemp,
                    Funcionario = FuncionarioTemp,

                    PesoDoAnimalAtualizado = atendimentoTemp.PesoDoAnimalAtualizado == 0 ? AnimalTemp.PesoAtual : atendimentoTemp.PesoDoAnimalAtualizado,

                    AlturaDoAnimalAtualizado = atendimentoTemp.AlturaDoAnimalAtualizado == 0 ? AnimalTemp.AlturaAtual : atendimentoTemp.PesoDoAnimalAtualizado,

                    DataAtendimento = DateTime.Now,
                    Diagnostico = atendimentoTemp.Diagnostico,
                    Observacoes = atendimentoTemp.Observacoes
                };

                await _database.Atendimentos.AddAsync(AtendimentoParaSalvar);
                await _database.SaveChangesAsync();

                return Created("", AtendimentoParaSalvar);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [Authorize(Roles = "Funcionario")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                List<Atendimento> ListaRegistrosDeAtendimentos = await _database.Atendimentos.Include(animal => animal.Animal).Include(cliente => cliente.Cliente).Include(funcionario => funcionario.Funcionario).Include(servico => servico.Servico).ToListAsync();

                if (ListaRegistrosDeAtendimentos.Count == 0) return NotFound("Nenhum registro de atendimento encontrado");

                return Ok(ListaRegistrosDeAtendimentos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [Authorize(Roles = "Cliente, Funcionario")]
        [HttpGet("cliente/busca/{id}")]
        public async Task<ActionResult> GetByIdCliente(int id)
        {
            try
            {
                List<Atendimento> ListaRegistrosAtendimentos = await _database.Atendimentos.Include(animal => animal.Animal).Include(cliente => cliente.Cliente).Where(atendimento => atendimento.Cliente.Id == id).Include(funcionario => funcionario.Funcionario).Include(servico => servico.Servico).ToListAsync();

                if (ListaRegistrosAtendimentos.Count == 0) return NotFound("Nenhum registro de atendimento encontrado");

                return Ok(ListaRegistrosAtendimentos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [Authorize(Roles = "Cliente, Funcionario")]
        [HttpGet("cliente/busca/{nome}/{cpf}")]
        public async Task<ActionResult> GetByNomeCliente(string nome, string cpf)
        {
            try
            {
                Cliente ValidarCliente = _database.Clientes.First(cliente => cliente.Nome == nome);
                if (ValidarCliente is null) return NotFound("Cliente n??o encontrado");
                if (ValidarCliente.Cpf != cpf) return StatusCode(StatusCodes.Status401Unauthorized, "Informa????es do cliente n??o coincidem com o banco de dados");

                List<Atendimento> ListaRegistrosAtendimentos = await _database.Atendimentos.Include(animal => animal.Animal).Include(cliente => cliente.Cliente).Where(atendimento => atendimento.Cliente.Nome == nome).Include(funcionario => funcionario.Funcionario).Include(servico => servico.Servico).ToListAsync();

                return Ok(ListaRegistrosAtendimentos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [Authorize(Roles = "Cliente, Funcionario")]
        [HttpGet("animal/busca/{id}")]
        public async Task<ActionResult> GetByIdAnimal(int id)
        {
            try
            {
                List<Atendimento> ListaRegistros = await _database.Atendimentos.Include(animal => animal.Animal).Where(animal => animal.Animal.Id == id).Include(cliente => cliente.Cliente).Include(funcionario => funcionario.Funcionario).Include(servico => servico.Servico).ToListAsync();

                if (ListaRegistros.Count == 0) return NotFound("Nenhum registro de atendimento encontrado");

                return Ok(ListaRegistros);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [Authorize(Roles = "Cliente, Funcionario")]
        [HttpGet("animal/busca/{nome}/{nascimento}")]
        public async Task<ActionResult> GetByNomeAnimal(string nome, string nascimento)
        {
            try
            {
                var dataNascimento = Convert.ToDateTime(nascimento);

                List<Atendimento> ListaRegistros = await _database.Atendimentos.Include(animal => animal.Animal).Where(animal => animal.Animal.Nome == nome && animal.Animal.DataNascimento == dataNascimento).Include(cliente => cliente.Cliente).Include(funcionario => funcionario.Funcionario).Include(servico => servico.Servico).ToListAsync();

                if (ListaRegistros.Count == 0) return NotFound("Nenhum registro de atendimento encontrado");

                return Ok(ListaRegistros);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [Authorize(Roles = "Funcionario")]
        [HttpPatch("atualizar/{id}")]
        public async Task<ActionResult> Patch(int id, AtendimentoPatchDTO atendimentoTemp)
        {
            try
            {
                Atendimento AtendimentoParaAtualizar = await _database.Atendimentos.FirstAsync(animal => animal.Id == id);
                if (AtendimentoParaAtualizar is null) return NotFound("N??o foi encontrado Funcionario com esse Id");

                AtendimentoParaAtualizar.Diagnostico = atendimentoTemp.Diagnostico ?? AtendimentoParaAtualizar.Diagnostico;
                AtendimentoParaAtualizar.Observacoes = atendimentoTemp.Observacoes ?? AtendimentoParaAtualizar.Diagnostico;

                _database.Atendimentos.Update(AtendimentoParaAtualizar);
                await _database.SaveChangesAsync();

                return Created("", AtendimentoParaAtualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}