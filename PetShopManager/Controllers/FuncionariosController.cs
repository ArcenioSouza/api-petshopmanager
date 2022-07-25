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
    [Route("api/v1/funcionarios")]
    [Authorize(Roles = "Funcionario")]
    public class FuncionariosController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public FuncionariosController(ApplicationDbContext database)
        {
            _database = database;
        }

        [HttpPost]
        public async Task<ActionResult> Post(FuncionarioDTO funcionarioTemp)
        {
            try
            {
                var funcionarios = _database.Funcionarios.ToList();
                if(funcionarios.Exists(funcionario => funcionario.Cpf.Equals(funcionarioTemp.Cpf))) return StatusCode(StatusCodes.Status422UnprocessableEntity, "Funcionario já cadastrado");
                Funcionario FuncionarioParaSalvar = new()
                {
                    Nome = funcionarioTemp.Nome,
                    Cpf = funcionarioTemp.Cpf,
                    Cargo = _database.Cargos.First(cargo => cargo.Id == funcionarioTemp.CargoId),
                    Email = funcionarioTemp.Email,
                    Senha = funcionarioTemp.Senha,
                    IsActive = true
                };

                await _database.Funcionarios.AddAsync(FuncionarioParaSalvar);
                await _database.SaveChangesAsync();
                return Ok(FuncionarioParaSalvar);
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
                List<Funcionario> ListaDeFuncionarios = await _database.Funcionarios.Where(funcionario => funcionario.IsActive == true).ToListAsync();
                if (ListaDeFuncionarios.Count == 0) return NotFound("Nenhum funcionario encontrado");
                return Ok(ListaDeFuncionarios);
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
                Funcionario FuncionarioPesquisado = await _database.Funcionarios.FirstAsync(funcionario => funcionario.Id == id);

                return Ok(FuncionarioPesquisado);
            }
            catch (Exception ex)
            {
                return NotFound(new { msg = "Nenhum funcionario encontrado com esse id", error = ex.Message });
            }
        }

        [HttpPatch("atualizar/{id}")]
        public async Task<ActionResult> Patch(int id, FuncionarioDTO funcionarioTemp)
        {
            try
            {
                Funcionario FuncionarioParaAtualizar = await _database.Funcionarios.FirstAsync(funcionario => funcionario.Id == id);
                if (FuncionarioParaAtualizar is null) return NotFound("Não foi encontrado Funcionario com esse Id");
                FuncionarioParaAtualizar.Nome = funcionarioTemp.Nome ?? FuncionarioParaAtualizar.Nome;
                FuncionarioParaAtualizar.Cargo = _database.Cargos.First(cargo => cargo.Id == funcionarioTemp.CargoId) ?? FuncionarioParaAtualizar.Cargo;
                FuncionarioParaAtualizar.Email = funcionarioTemp.Email ?? FuncionarioParaAtualizar.Email;
                FuncionarioParaAtualizar.Senha = funcionarioTemp.Senha ?? FuncionarioParaAtualizar.Senha;

                _database.Funcionarios.Update(FuncionarioParaAtualizar);
                await _database.SaveChangesAsync();
                return Ok(FuncionarioParaAtualizar);
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
                Funcionario FuncionarioParaDeletar = await _database.Funcionarios.FirstAsync(funcionario => funcionario.Id == id);
                if (FuncionarioParaDeletar is null) return NotFound("Não foi encontrado funcionario com esse Id");
                FuncionarioParaDeletar.IsActive = false;
                _database.Funcionarios.Update(FuncionarioParaDeletar);
                await _database.SaveChangesAsync();
                return Ok(FuncionarioParaDeletar.Nome + " deletado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}