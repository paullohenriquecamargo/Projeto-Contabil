using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContaReceberController : Controller
    {
        private ContaReceberRepository repository;

        public ContaReceberController()
        {
            repository = new ContaReceberRepository();
        }

        // GET: ContaReceber
        public ActionResult Index()
        {
            List<ContaReceber> contasReceber = repository.ObterTodos();
            ViewBag.ContasReceber = contasReceber;
            return View();
        }

        public ActionResult Cadastro()
        {
            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos();
            ViewBag.Categorias = categorias;

            return View();
        }

        public ActionResult Store(string nome, DateTime data)
        {
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Nome = nome;
            repository.Inserir(contaReceber);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repository.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ContaReceber contaReceber = repository.ObterPeloid(id);
            ViewBag.ContaReceber = contaReceber;
            return View();
        }

        public ActionResult Update(int id, string nome)
        {
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Id = id;
            contaReceber.Nome = nome;
            repository.Alterar(contaReceber);
            return RedirectToAction("Index");
        }
    }
}