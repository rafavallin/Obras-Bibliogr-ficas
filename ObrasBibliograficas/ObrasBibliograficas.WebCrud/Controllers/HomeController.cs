using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ObrasBibliograficas.WebCrud.Models;
using ObrasBibliograficas.WebCrud.Services;
using System.Linq;

namespace ObrasBibliograficas.WebCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly AutoresApi _api;
        public HomeController(AutoresApi api)
        {
            _api = api;
        }
        
        public IActionResult Index()
        {
            var result = _api.GetAll();

            return View( result);
        }

        public IActionResult Delete(int id)
        {
            var result = _api.Delete(id);

            return RedirectToAction("Create", "Home");

        }

        [HttpGet]
        public IActionResult Create()
        {
            var autor = new Autor();
            return View(autor);
        }

        public IActionResult Create([FromBody]string[] listaNomes)
        {

            var retorno = _api.Create(listaNomes.ToList());


            return Json(retorno);

        }



    }
}
