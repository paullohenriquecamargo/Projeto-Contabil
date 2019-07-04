using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ContaReceber
    {
        public int Id;
        public int IdCliente;
        public int IdCategoria;
        public string Nome;
        public DateTime DataPagamento;
        public decimal Valor;

        public Categoria Categoria;
    }
}
