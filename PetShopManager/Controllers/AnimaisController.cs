using System.IO;
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
using PetShopManager.DTO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

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
        
        [Authorize(Roles = "Funcionario")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                List<Animal> ListaDeAnimais = await _database.Animais.Include(cliente => cliente.Cliente).Where(animal => animal.IsActive == true).ToListAsync();
                if (ListaDeAnimais.Count == 0) return NotFound("Nenhum animal encontrado");
                return Ok(ListaDeAnimais);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [Authorize(Roles = "Cliente, Funcionario")]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                Animal AnimalPesquisado = await _database.Animais.Include(cliente => cliente.Cliente).FirstAsync(animal => animal.Id == id);
                if(AnimalPesquisado.IsActive == false) return Ok("O animal com esse id não está ativo");
                return Ok(AnimalPesquisado);
            }
            catch (Exception ex)
            {
                return NotFound(new { msg = "Nenhum animal encontrado com esse id", error = ex.Message });
            }
        }

        [Authorize(Roles = "Cliente, Funcionario")]
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
                return BadRequest(new { erro = ex.Message });
            }
        }

        [Authorize(Roles = "Cliente, Funcionario")]
        [HttpGet("random/dogs")]
        public async Task<ActionResult> GetDogsRandom()
        {
            try
            {
                var req = new ReqDogApi();
                var response = await req.GetRandomDogs(); 

                var jsonList = JsonConvert.DeserializeObject<List<Cachorros>>(response);               
                Console.WriteLine(jsonList);                
                return Ok(jsonList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
        
        [Authorize(Roles = "Cliente, Funcionario")]
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
                        DataNascimento = Convert.ToDateTime(animalTemp.DataNascimento),
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
                        DataNascimento = Convert.ToDateTime(animalTemp.DataNascimento),
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

        [Authorize(Roles = "Cliente, Funcionario")]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, AnimalDTO animalTemp)
        {
            try
            {
                Animal AnimalParaAtualizar = _database.Animais.First(animal => animal.Id == id);
                if (AnimalParaAtualizar is null) return NotFound("Não foi encontrado animal com esse Id");
                AnimalParaAtualizar.Nome = animalTemp.Nome ?? AnimalParaAtualizar.Nome;
                AnimalParaAtualizar.Sexo = animalTemp.Sexo ?? AnimalParaAtualizar.Sexo;
                AnimalParaAtualizar.Cliente = _database.Clientes.First(cliente => cliente.Id == animalTemp.ClienteID) ?? AnimalParaAtualizar.Cliente;
                AnimalParaAtualizar.PesoAtual = animalTemp.PesoAtual == 0 ? AnimalParaAtualizar.PesoAtual : animalTemp.PesoAtual;
                AnimalParaAtualizar.AlturaAtual = animalTemp.AlturaAtual == 0 ? AnimalParaAtualizar.AlturaAtual : animalTemp.AlturaAtual;
                if(animalTemp.DataNascimento != null) AnimalParaAtualizar.DataNascimento = Convert.ToDateTime(animalTemp.DataNascimento);
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

        [Authorize(Roles = "Funcionario")]
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