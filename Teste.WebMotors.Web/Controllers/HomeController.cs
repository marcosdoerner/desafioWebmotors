using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Teste.WebMotors.Web.Models;

namespace Teste.WebMotors.Web.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly string urlApi;
        private readonly string urlDesafio;

        public HomeController()
        {
            urlApi = "http://localhost:2925/";
            urlDesafio = "https://desafioonline.webmotors.com.br/api/OnlineChallenge/";
        }

        public IActionResult Index()
        {
            IEnumerable<AnuncioViewModel> anuncios = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.urlApi);
                var responseTask = client.GetAsync("api/ListarAnuncios");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    anuncios = JsonConvert.DeserializeObject<List<AnuncioViewModel>>(readTask.Result);
                }
                else
                {
                    anuncios = Enumerable.Empty<AnuncioViewModel>();
                    ModelState.AddModelError(string.Empty, "Consulta Vazia");
                }
            }
            return View(anuncios);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult Create()
        {
            var marcas = this.BuscarMarcas();
            ViewBag.marcas = marcas.ToList();
            ViewBag.modelos = Enumerable.Empty<ModeloViewModel>();
            ViewBag.versoes = Enumerable.Empty<VersaoViewModel>();

            return View();
        }

        private IEnumerable<MarcaViewModel> BuscarMarcas()
        {
            var marcas = Enumerable.Empty<MarcaViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.urlDesafio);
                var responseTask = client.GetAsync("Make");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    marcas = JsonConvert.DeserializeObject<List<MarcaViewModel>>(readTask.Result);
                }
            }

            return marcas.ToList();
        }

        private IEnumerable<ModeloViewModel> BuscarModelos(int marcaId)
        {
            var modelos = Enumerable.Empty<ModeloViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.urlDesafio);
                var responseTask = client.GetAsync("Model?MakeID=" + marcaId.ToString());
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    modelos = JsonConvert.DeserializeObject<List<ModeloViewModel>>(readTask.Result);
                }
            }
            ViewBag.modelos = modelos.ToList();
            return modelos.ToList();
        }

        private IEnumerable<VersaoViewModel> BuscarVersoes(int modelId)
        {
            var versoes = Enumerable.Empty<VersaoViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.urlDesafio);
                var responseTask = client.GetAsync("Version?ModelID=" + modelId.ToString());
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    versoes = JsonConvert.DeserializeObject<List<VersaoViewModel>>(readTask.Result);
                }
            }

            ViewBag.versoes = versoes.ToList();

            return versoes.ToList();
        }

        public IActionResult SelecionarMarca()
        {
            var marcas = this.BuscarMarcas();

            ViewBag.marcas = marcas.ToList();
            return Json(marcas);
        }

        public IActionResult SelecionarModeloPorMarca(int marcaId)
        {
            var modelos = this.BuscarModelos(marcaId).ToList();
            return Json(modelos);
        }

        public IActionResult SelecionarVersaoPorModelo(int modelId)
        {
            var versoes = this.BuscarVersoes(modelId);

            return Json(versoes);
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Create(AnuncioViewModel anuncio)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.urlApi);

                    var postTask = client.PostAsync("api/IncluirAnuncio", new StringContent(JsonConvert.SerializeObject(anuncio), Encoding.UTF8, "application/json"));
                    postTask.Wait();
                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Erro no Servidor. Contacte o Administrador.");
            }
            return View(anuncio);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult Edit(int? id)
        {
            AnuncioViewModel anuncio = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.urlApi);

                var responseTask = client.GetAsync("api/ConsultarAnuncio?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    anuncio = JsonConvert.DeserializeObject<AnuncioViewModel>(readTask.Result);
                }
            }

            var marcas = this.BuscarMarcas();
            ViewBag.marcas = marcas.ToList();
            ViewBag.modelos = Enumerable.Empty<ModeloViewModel>();
            ViewBag.versoes = Enumerable.Empty<VersaoViewModel>();

            var editMarca = marcas.FirstOrDefault(x => x.Name.ToLower() == anuncio.Marca.ToLower());
            if (editMarca != null)
            {
                anuncio.MarcaId = editMarca.ID;
                var modelos = this.BuscarModelos(anuncio.MarcaId);
                ViewBag.modelos = modelos.ToList();

                var editModelo = modelos.FirstOrDefault(x => x.Name.ToLower() == anuncio.Modelo.ToLower());
                if (editModelo != null)
                {
                    anuncio.ModeloId = editModelo.ID;

                    var versoes = this.BuscarVersoes(anuncio.ModeloId);
                    ViewBag.versoes = versoes.ToList();
                    var editVersao = versoes.FirstOrDefault(x => x.Name.ToLower() == anuncio.Versao.ToLower());
                    if (editVersao != null)
                        anuncio.VersaoId = editVersao.ID;
                }
            }
            return View(anuncio);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Edit(AnuncioViewModel anuncio)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.urlApi);

                    var putTask = client.PostAsync("api/AtualizaAnuncio", new StringContent(JsonConvert.SerializeObject(anuncio), Encoding.UTF8, "application/json"));
                    putTask.Wait();
                    var result = putTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(anuncio);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult Delete(int? id)
        {
            AnuncioViewModel anuncio = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.urlApi);

                //HTTP GET
                var responseTask = client.GetAsync("api/ConsultarAnuncio?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    anuncio = JsonConvert.DeserializeObject<AnuncioViewModel>(readTask.Result);
                }
            }
            return View(anuncio);
        }

        public IActionResult Remove(int id)
        {
            AnuncioViewModel anuncio = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.urlApi);
                
                var deleteTask = client.DeleteAsync("api/RemoverAnuncio?id=" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(anuncio);
        }

        public IActionResult Details(int? id)
        {
            AnuncioViewModel anuncio = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.urlApi);

                //HTTP GET
                var responseTask = client.GetAsync("api/ConsultarAnuncio?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    anuncio = JsonConvert.DeserializeObject<AnuncioViewModel>(readTask.Result);
                }
            }
            return View(anuncio);
        }

    }
}
