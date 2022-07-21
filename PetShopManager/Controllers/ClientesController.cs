using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopManager.Data;
using PetShopManager.DTO;
using PetShopManager.Models;

namespace PetShopManager.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public ClientesController(ApplicationDbContext database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                List<Cliente> ListaDeClientes = await _database.Clientes.Where(cliente => cliente.IsActive == true).ToListAsync();
                if (ListaDeClientes.Count == 0) return NotFound("Nenhum cliente encontrado");
                return Ok(ListaDeClientes);
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
                Cliente ClientePesquisado = await _database.Clientes.FirstAsync(cliente => cliente.Id == id);

                return Ok(ClientePesquisado);
            }
            catch (Exception ex)
            {
                return NotFound(new { msg = "Nenhum cliente encontrado com esse id", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(ClienteDTO clienteTemp)
        {
            try
            {
                Cliente ClienteParaSalvar = new()
                {
                    Nome = clienteTemp.Nome,
                    Cpf = clienteTemp.Cpf,
                    Telefone = clienteTemp.Telefone,
                    Email = clienteTemp.Email,
                    Senha = clienteTemp.Senha,
                    IsActive = true
                };

                await _database.Clientes.AddAsync(ClienteParaSalvar);
                await _database.SaveChangesAsync();
                return Ok(ClienteParaSalvar);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, ClienteDTO clienteTemp)
        {
            try
            {
                Cliente ClienteParaAtualizar = await _database.Clientes.FirstAsync(cliente => cliente.Id == id);
                if (ClienteParaAtualizar is null) return NotFound("Não foi encontrado cliente com esse Id");
                ClienteParaAtualizar.Nome = clienteTemp.Nome ?? ClienteParaAtualizar.Nome;
                ClienteParaAtualizar.Cpf = clienteTemp.Cpf ?? ClienteParaAtualizar.Cpf;
                ClienteParaAtualizar.Telefone = clienteTemp.Telefone ?? ClienteParaAtualizar.Telefone;
                ClienteParaAtualizar.Email = clienteTemp.Email ?? ClienteParaAtualizar.Email;
                ClienteParaAtualizar.Senha = clienteTemp.Senha ?? ClienteParaAtualizar.Senha;

                _database.Clientes.Update(ClienteParaAtualizar);
                await _database.SaveChangesAsync();
                return Ok(ClienteParaAtualizar);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Cliente ClienteParaDeletar = await _database.Clientes.FirstAsync(cliente => cliente.Id == id);
                if (ClienteParaDeletar is null) return NotFound("Não foi encontrado cliente com esse Id");
                ClienteParaDeletar.IsActive = false;
                _database.Clientes.Update(ClienteParaDeletar);
                await _database.SaveChangesAsync();
                return Ok(ClienteParaDeletar.Nome + " deletado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}