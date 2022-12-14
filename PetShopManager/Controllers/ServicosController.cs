using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopManager.Data;
using PetShopManager.DTO;
using PetShopManager.Models;

namespace PetShopManager.Controllers
{
    [ApiController]
    [Route("api/v1/servicos")]
    [Authorize(Roles = "Funcionario")]
    public class ServicosController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public ServicosController(ApplicationDbContext database)
        {
            _database = database;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ServicoDTO servicoTemp)
        {
            try
            {
                Servico ServicoParaSalvar = new()
                {
                    Tipo = servicoTemp.Tipo,
                    Valor = servicoTemp.Valor,
                    IsActive = true
                };

                await _database.Servicos.AddAsync(ServicoParaSalvar);
                await _database.SaveChangesAsync();
                return Created("", ServicoParaSalvar);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
             try
            {
                List<Servico> ListaDeServicos = await _database.Servicos.Where(servico => servico.IsActive == true).ToListAsync();
                if (ListaDeServicos.Count == 0) return NotFound("Nenhum serviço encontrado");
                return Ok(ListaDeServicos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                Servico ServicoPesquisado = await _database.Servicos.FirstAsync(servico => servico.Id == id);
                if(ServicoPesquisado.IsActive == false) return Ok("Esse serviço não está mais disponível");
                return Ok(ServicoPesquisado);
            }
            catch (Exception ex)
            {
                return NotFound(new { msg = "Nenhum serviço encontrado com esse id", error = ex.Message });
            }
        }

        [HttpPatch("atualizar/{id}")]
        public async Task<ActionResult> Patch(int id, ServicoDTO servicoTemp)
        {
            try
            {
                Servico ServicoParaAtualizar = await _database.Servicos.FirstAsync(servico => servico.Id == id);
                if (ServicoParaAtualizar is null) return NotFound("Não foi encontrado serviço com esse Id");
                ServicoParaAtualizar.Tipo = servicoTemp.Tipo ?? ServicoParaAtualizar.Tipo;
                if(ServicoParaAtualizar.Valor != 0) ServicoParaAtualizar.Valor = servicoTemp.Valor;

                _database.Servicos.Update(ServicoParaAtualizar);
                await _database.SaveChangesAsync();
                return Created("", ServicoParaAtualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
        
        [HttpDelete("deletar/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Servico ServicoParaDeletar = await _database.Servicos.FirstAsync(servico => servico.Id == id);
                if (ServicoParaDeletar is null) return NotFound("Não foi encontrado cliente com esse Id");

                _database.Servicos.Remove(ServicoParaDeletar);
                await _database.SaveChangesAsync();
                return Ok(ServicoParaDeletar.Tipo + " removido com sucesso");

            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}