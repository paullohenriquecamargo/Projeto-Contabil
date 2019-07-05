using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContabilidadeController : Controller
    {
        private ContabilidadeRepository repositorio;

        public ContabilidadeController()
        {
            repositorio = new ContabilidadeRepository();            
        }

        // GET: Contabilidade
        public ActionResult Index()
        {
            List<Contabilidade> contabilidades = repositorio.ObterTodos();
            ViewBag.Contabilidades = contabilidades;
            return View();
        }
        public ActionResult Cadastro()
        {
            ContabilidadeRepository contabilidadeRepository = new ContabilidadeRepository();
            List<Contabilidade> contabilidades = contabilidadeRepository.ObterTodos();
            ViewBag.Contabilidade = contabilidades;

            return View();
        }

        public ActionResult Store(string nome)
        {
            Contabilidade contabilidades = new Contabilidade();
            contabilidades.Nome = nome;
            repositorio.Inserir(contabilidades);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repositorio.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Contabilidade contabilidade = repositorio.ObterPeloId(id);
            ViewBag.Contabilidade = contabilidade;                                    
            return View();
        }

        public ActionResult Update(int id, string nome)
        {
            Contabilidade contabilidade = new Contabilidade();
            contabilidade.Id = id;
            contabilidade.Nome = nome;
            repositorio.Alterar(contabilidade);

            return RedirectToAction("Index");
        }
    }
}