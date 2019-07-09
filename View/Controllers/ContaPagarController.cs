using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContaPagarController : Controller
    {
        private ContaPagarRepository repository;

        public ContaPagarController()
        {
            repository = new ContaPagarRepository();
        }
        // GET: ContaPagar
        public ActionResult Index()
        {
            List<ContaPagar> contasPagar = repository.ObterTodos();
            ViewBag.ContasPagar = contasPagar;
            return View();
        }

        public ActionResult Cadastro()
        {
            ContaPagarRepository contaPagarRepository = new ContaPagarRepository();
            List<ContaPagar> contasPagar = contaPagarRepository.ObterTodos();
            ViewBag.ContasPagar = contasPagar;
            return View();
        }

        public ActionResult Store(string nome, DateTime dataVencimento, DateTime dataPagamento, decimal valor, int idCliente, int idCategoria)
        {
            ContaPagar contaPagar = new ContaPagar();
            contaPagar.Nome = nome;
            contaPagar.DataPagamento = dataPagamento;
            contaPagar.Valor = valor;
            contaPagar.IdCliente = idCliente;
            contaPagar.IdCategoria = idCategoria;
            repository.Inserir(contaPagar);
            return RedirectToACtion("Index");
        }

        public ActionResult Apagar(int id)
        {

        }
    }
}