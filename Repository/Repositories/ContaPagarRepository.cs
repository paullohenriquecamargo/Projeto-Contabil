using Model;
using Repository.DataBase;
using Repository.InterFaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ContaPagarRepository : IContaPagarRepository
    {
        public bool Alterar(ContaPagar contaPagar)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE contas_pagar SET 
nome = @NOME,
data_vencimento = @DATA_VENCIMENTO,
data_pagamento = @DATA_PAGAMENTO,
valor = @VALOR,
id_cliente = @ID_CLIENTE,
id_categoria = @ID_CATEGORIA
WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", contaPagar.Nome);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", contaPagar.DataVencimento);
            comando.Parameters.AddWithValue("@DATA_PAGAMENTO", contaPagar.DataPagamento);
            comando.Parameters.AddWithValue("@VALOR", contaPagar.Valor);
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaPagar.IdCliente);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaPagar.IdCategoria);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"DELETE FROM contas_pagar WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;                
        }

        public int Inserir(ContaPagar contaPagar)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO contas_pagar 
(nome, data_vencimento, data_pagamento, valor, id_cliente, id_categoria)
OUTPUT INSERTED.ID
VALUES
(@NOME, @DATA_VENCIMENTO, @DATA_PAGAMENTO, @VALOR, @ID_CLIENTE, @ID_CATEGORIA)";
            comando.Parameters.AddWithValue("@NOME", contaPagar.Nome);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", contaPagar.DataVencimento);
            comando.Parameters.AddWithValue("@DATA_PAGAMENTO", contaPagar.DataPagamento);
            comando.Parameters.AddWithValue("@VALOR", contaPagar.Valor);
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaPagar.IdCliente);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaPagar.IdCategoria);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public ContaPagar ObterPeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT * FROM contas_pagar WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if(tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            ContaPagar contaPagar = new ContaPagar();
            contaPagar.Id = Convert.ToInt32(linha["id"]);
            contaPagar.Nome = linha["nome"].ToString();
            contaPagar.DataVencimento = Convert.ToDateTime(linha["data_vencimento"]);
            contaPagar.DataPagamento = Convert.ToDateTime(linha["data_pagamento"]);
            contaPagar.Valor = Convert.ToDecimal(linha["valor"]);
            contaPagar.IdCliente = Convert.ToInt32(linha["id_cliente"]);
            contaPagar.IdCategoria = Convert.ToInt32(linha["id_categoria"]);
            return contaPagar;
        }

        public List<ContaPagar> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT  
clientes.id AS 'ClienteId',
clientes.nome AS 'ClienteNome',
clientes.cpf AS 'ClienteCpf',
categorias.id AS 'CategoriasId',
categorias.nome AS 'CategoriasNome',
contas_pagar.id AS 'ContaPagarId',
contas_pagar.nome AS 'ContaPagarNome',
contas_pagar.data_pagamento AS 'ContaPagarDataPagamento',
contas_pagar.data_vencimento AS 'ContaPagarDataVencimento',
contas_pagar.valor AS 'ContaPagarValor'
FROM contas_pagar
INNER JOIN clientes ON (contas_pagar.id_cliente = clientes.id)
INNER JOIN categorias ON (contas_pagar.id_categoria = categorias.id)";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<ContaPagar> contasPagar = new List<ContaPagar>();
            foreach(DataRow linha in tabela.Rows)
            {
                ContaPagar contaPagar = new ContaPagar();
                contaPagar.Id = Convert.ToInt32(linha["id"]);
                contaPagar.Nome = linha["nome"].ToString();
                contaPagar.DataPagamento = Convert.ToDateTime(linha["data_pagamento"]);
                contaPagar.DataVencimento = Convert.ToDateTime(linha["data_vencimento"]);
                contaPagar.Valor = Convert.ToDecimal(linha["valor"]);
                contaPagar.Categoria = new Categoria();
                contaPagar.Categoria.Id = Convert.ToInt32(linha["CategoriaId"]);
                contaPagar.Categoria.Nome = linha["CategoriaNome"].ToString();
                contaPagar.Cliente = new Cliente();
                contaPagar.Cliente.Id = Convert.ToInt32(linha["ClienteId"]);
                contaPagar.Cliente.Nome = linha["ClienteId"].ToString();
                contaPagar.Cliente.Cpf = linha["ClienteCpf"].ToString();
                contasPagar.Add(contaPagar);
            }
            return contasPagar;
        }
    }
}
