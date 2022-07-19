using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopManager.Data;
using PetShopManager.Models;
using PetShopManager.Services;
using Newtonsoft.Json.Linq;

namespace PetShopManager.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnimaisController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        public AnimaisController(ApplicationDbContext database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                List<Animal> ListaDeAnimais = await _database.Animais.Where(animal => animal.IsActive == true).ToListAsync();
                if (ListaDeAnimais.Count == 0) return NotFound("Nenhum animal encontrado");
                return Ok(ListaDeAnimais);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                Animal AnimalPesquisado = await _database.Animais.FirstAsync(animal => animal.Id == id);
                return Ok(AnimalPesquisado);
            }
            catch (Exception ex)
            {
                return NotFound(new { msg = "Nenhum animal encontrado com esse id", error = ex.Message });
            }
        }

        [HttpGet("search/{raca}")]
        public async Task<ActionResult> GetRaca(string raca)
        {
            try
            {
                var req = new ReqDogApi();
                var response = await req.GetInfoDogs(raca);
                JArray json = JArray.Parse(response);
                if (json.Count == 0) return NotFound("Nenhuma raça encontrada");
                List<string> racas = new();
                foreach (var item in json)
                {
                    racas.Add((string)item["name"]);
                }
                return Ok(new { racas });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(AnimalDTO animalTemp)
        {
            try
            {
                Animal AnimalParaSalvar;
                var req = new ReqDogApi();
                var response = await req.GetInfoDogs(animalTemp.Raca);
                JArray json = JArray.Parse(response);

                if (json.Count == 0)
                {
                    AnimalParaSalvar = new()
                    {
                        Nome = animalTemp.Nome,
                        Sexo = animalTemp.Sexo,
                        Cliente = _database.Clientes.First(cliente => cliente.Id == animalTemp.ClienteID),
                        PesoAtual = animalTemp.PesoAtual,
                        AlturaAtual = animalTemp.AlturaAtual,
                        Raca = animalTemp.Raca,
                        TempoDeVida = "",
                        Temperamento = "",
                        PesoMedio = "",
                        AlturaMedia = "",
                        IsActive = true
                    };
                }
                else
                {
                    AnimalParaSalvar = new()
                    {
                        Nome = animalTemp.Nome,
                        Sexo = animalTemp.Sexo,
                        Cliente = _database.Clientes.First(cliente => cliente.Id == animalTemp.ClienteID),
                        PesoAtual = animalTemp.PesoAtual,
                        AlturaAtual = animalTemp.AlturaAtual,
                        Raca = animalTemp.Raca,
                        TempoDeVida = (string)json[0]["life_span"],
                        Temperamento = (string)json[0]["temperament"],
                        PesoMedio = (string)json[0]["weight"]["metric"],
                        AlturaMedia = (string)json[0]["height"]["metric"],
                        IsActive = true
                    };
                }

                await _database.Animais.AddAsync(AnimalParaSalvar);
                await _database.SaveChangesAsync();
                return Created("", AnimalParaSalvar);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPatch]
        public async Task<ActionResult> Patch(AnimalDTO animalTemp)
        {
            try
            {
                Animal AnimalParaAtualizar = _database.Animais.First(animal => animal.Id == animalTemp.ClienteID);
                if (AnimalParaAtualizar is null) return NotFound("Não foi encontrado animal com esse Id");
                AnimalParaAtualizar.Nome = animalTemp.Nome ?? AnimalParaAtualizar.Nome;
                AnimalParaAtualizar.Sexo = animalTemp.Sexo ?? AnimalParaAtualizar.Sexo;
                AnimalParaAtualizar.Cliente = _database.Clientes.First(cliente => cliente.Id == animalTemp.ClienteID) ?? AnimalParaAtualizar.Cliente;
                AnimalParaAtualizar.PesoAtual = animalTemp.PesoAtual == 0 ? AnimalParaAtualizar.PesoAtual : animalTemp.PesoAtual;
                AnimalParaAtualizar.AlturaAtual = animalTemp.AlturaAtual == 0 ? AnimalParaAtualizar.AlturaAtual : animalTemp.AlturaAtual;
                AnimalParaAtualizar.Raca = animalTemp.Raca ?? AnimalParaAtualizar.Raca;

                if (AnimalParaAtualizar.Raca != null || AnimalParaAtualizar.Raca != "")
                {
                    var req = new ReqDogApi();
                    var response = await req.GetInfoDogs(AnimalParaAtualizar.Raca);
                    JArray json = JArray.Parse(response);

                    if (json.Count != 0)
                    {
                        AnimalParaAtualizar.TempoDeVida = (string)json[0]["life_span"];
                        AnimalParaAtualizar.Temperamento = (string)json[0]["temperament"];
                        AnimalParaAtualizar.PesoMedio = (string)json[0]["weight"]["metric"];
                        AnimalParaAtualizar.AlturaMedia = (string)json[0]["height"]["metric"];
                    }
                }
                AnimalParaAtualizar.IsActive = true;

                _database.Animais.Update(AnimalParaAtualizar);
                await _database.SaveChangesAsync();
                return Created("", AnimalParaAtualizar);
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
                Animal AnimalParaDeletar = _database.Animais.First(animal => animal.Id == id);
                if (AnimalParaDeletar is null) return NotFound("Não foi encontrado animal com esse Id");
                AnimalParaDeletar.IsActive = false;
                _database.Animais.Update(AnimalParaDeletar);
                await _database.SaveChangesAsync();
                return Ok(AnimalParaDeletar.Nome + " removido com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}