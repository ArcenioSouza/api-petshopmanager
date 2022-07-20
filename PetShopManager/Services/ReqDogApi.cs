using System.Runtime.Serialization.Json;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopManager.Services
{
    public class ReqDogApi
    {
        public async Task<string> GetInfoDogs(string raca)
        {
            //Configura a requisição
            string url = $"https://api.thedogapi.com/v1/breeds/search?q={raca}";
            var req = WebRequest.Create(url);
            req.Method = "GET";
            req.ContentType = "application/json";
            req.Headers["x-api-key"] = "83056c37-0343-48e2-b201-8d5b05e14ae4";

            //Configura a resposta
            var response = (HttpWebResponse)req.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);            
            return reader.ReadToEnd();
        }    

        public async Task<string> GetRandomDogs()
        {
            //Configura a requisição
            string url = $"https://api.thedogapi.com/v1/breeds?limit=20";
            var req = WebRequest.Create(url);
            req.Method = "GET";
            req.ContentType = "application/json";
            req.Headers["x-api-key"] = "83056c37-0343-48e2-b201-8d5b05e14ae4";

            //Configura a resposta
            var response = (HttpWebResponse)req.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);            
            return reader.ReadToEnd();
        }     
    }
}