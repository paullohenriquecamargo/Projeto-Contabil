using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ContaPagar
    {
        public int Id;
        public int IdCliente;
        public Cliente Cliente;
  
        public int IdCategoria;
        public Categoria Categoria;
        public string Nome;
        public DateTime DataVencimento;
        public DateTime DataPagamento;
        public decimal Valor;        
    }
}
