using System.Runtime.Serialization.Json;
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
            //Depois mudar para aparecer apenas os que estão ativos.
            List<Animal> ListaDeAnimais = await _database.Animais.ToListAsync();
            return Ok(ListaDeAnimais);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            Animal AnimalPesquisado = await _database.Animais.FirstAsync(animal => animal.Id == id);
            return Ok(AnimalPesquisado);
        }

        [HttpGet("search/{raca}")]
        public async Task<ActionResult> GetRaca(string raca)
        {
            var req = new ReqDogApi();
            var response = await req.GetInfoDogs(raca);
            JArray json = JArray.Parse(response);
            List<string> racas = new();
            foreach (var item in json)
            {
                racas.Add((string)item["name"]);                
            }
            return Ok(new {racas});
        }

        [HttpPost]
        public async Task<ActionResult> Post(Animal animalTemp)
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

        [HttpPatch]
        public async Task<ActionResult> Patch(Animal animalTemp)
        {
            Animal AnimalParaAtualizar = _database.Animais.First(animal => animal.Id == animalTemp.Id);

            AnimalParaAtualizar.Nome = animalTemp.Nome ?? AnimalParaAtualizar.Nome;
            AnimalParaAtualizar.Sexo = animalTemp.Sexo ?? AnimalParaAtualizar.Sexo;
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Animal AnimalParaDeletar = _database.Animais.First(animal => animal.Id == id);
            AnimalParaDeletar.IsActive = false;
            _database.Animais.Update(AnimalParaDeletar);
            await _database.SaveChangesAsync();
            return Ok(AnimalParaDeletar.Nome + " removido com sucesso");
        }
    }
}