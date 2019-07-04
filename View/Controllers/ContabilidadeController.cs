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
            repositorio.ObterTodos("");
        }

        // GET: Contabilidade
        public ActionResult Index()
        {
            List<Contabilidade> contabilidades = repositorio.ObterTodos("");
            ViewBag.Contabilidades = contabilidades;
            return View();
        }
        public ActionResult Cadastro()
        {
            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categoria = categoriaRepository.ObterTodos();
            ViewBag.Categorias = categoria;

            return View();
        }

        public ActionResult Store(int idCategoria, string nome)
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

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos();
            ViewBag.Categorias = categorias;

            return View();
        }

        public ActionResult Update(int id, string nome, int idCategoria)
        {
            Contabilidade contabilidade = new Contabilidade();
            contabilidade.Id = id;
            contabilidade.Nome = nome;
            repositorio.Alterar(contabilidade);

            return RedirectToAction("Index");
        }
    }
}