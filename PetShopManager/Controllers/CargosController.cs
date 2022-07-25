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
    [Route("api/v1/cargos")]
    [Authorize(Roles = "Funcionario")]
    public class CargosController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public CargosController(ApplicationDbContext database)
        {
            _database = database;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CargoDTO cargoTemp)
        {
            try
            {
                Cargo CargoParaSalvar = new()
                {
                    NomeCargo = cargoTemp.NomeCargo
                };

                await _database.Cargos.AddAsync(CargoParaSalvar);
                await _database.SaveChangesAsync();
                return Created("", CargoParaSalvar);
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
                List<Cargo> ListaDeCargos = await _database.Cargos.ToListAsync();
                if (ListaDeCargos.Count == 0) return NotFound("Nenhum Cargo encontrado");
                return Ok(ListaDeCargos);
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
                Cargo CargoPesquisado = await _database.Cargos.FirstAsync(cargo => cargo.Id == id);
                return Ok(CargoPesquisado);
            }
            catch (Exception ex)
            {
                return NotFound(new { msg = "Nenhum cliente encontrado com esse id", error = ex.Message });
            }
        }

        [HttpPatch("atualizar/{id}")]
        public async Task<ActionResult> Patch(int id, CargoDTO cargoTemp)
        {
            try
            {
                Cargo CargoParaAtualizar = await _database.Cargos.FirstAsync(cargo => cargo.Id == id);
                if (CargoParaAtualizar is null) return NotFound("Não foi encontrado cliente com esse Id");
                CargoParaAtualizar.NomeCargo = cargoTemp.NomeCargo ?? CargoParaAtualizar.NomeCargo;

                _database.Cargos.Update(CargoParaAtualizar);
                await _database.SaveChangesAsync();
                return Created("", CargoParaAtualizar);
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
                Cargo CargoParaDeletar = await _database.Cargos.FirstAsync(cargo => cargo.Id == id);
                if (CargoParaDeletar is null) return NotFound("Não foi encontrado cliente com esse Id");

                _database.Cargos.Remove(CargoParaDeletar);
                await _database.SaveChangesAsync();
                return Ok(CargoParaDeletar.NomeCargo + " removido com sucesso");

            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }

        }
    }
}