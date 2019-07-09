using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteRepository repository;

        public ClienteController()
        {
            repository = new CategoriaRepository();
        }

        // GET: Cliente
        public ActionResult Index()
        {
            List<Cliente> clientes = repository.ObterTodos();
            ViewBag.Cliente = clientes;
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Store(string nome)
        {
            Cliente cliente = new Cliente();
            cliente.Nome = nome;
            return RedirectToAction("Index");
        }

        
    }
}