using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObrasBibliograficas.WebCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ObrasBibliograficas.WebCrud.Services
{
    public class AutoresApi 
    {
        private readonly HttpRequestMessage _request;
        private readonly IConfiguration _config;

        public AutoresApi(HttpRequestMessage request, IConfiguration config)
        {
            _request = request;
            _config = config;
        }

        public List<Autor> GetAll()
        {
            _request.Method = HttpMethod.Get;
            
            _request.RequestUri = new Uri(_config.GetSection("UrlBase")["value"]);

            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(_request).Result;

            return JsonConvert.DeserializeObject<List<Autor>>(response.Content.ReadAsStringAsync().Result);
        }

        public string Delete(int id)
        {
            try
            {
                _request.Method = HttpMethod.Delete;

                _request.RequestUri = new Uri(_config.GetSection("UrlBase")["value"] + id);

                HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(_request).Result;

                if (response.IsSuccessStatusCode)
                    return "Sucesso";
                else
                   return "Falha ao cadastrar";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Create(List<string> nomeCompleto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(nomeCompleto);

                _request.Method = HttpMethod.Post;

                _request.RequestUri = new Uri(_config.GetSection("UrlBase")["value"]);
                _request.Content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(_request).Result;



                if (response.IsSuccessStatusCode)
                {
                    return "Autores cadastrado com sucesso";
                }
                else
                {
                    return "Falha ao cadastrar";
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
