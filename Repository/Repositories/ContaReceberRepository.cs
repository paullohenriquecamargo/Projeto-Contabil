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
    public class ContaReceberRepository : IContaReceber
    {
        public bool Alterar(ContaReceber contaReceber)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE contas_receber SET 
nome = @NOME,
data_pagamento = @DATA_PAGAMENTO,
valor = @VALOR,
id_categoria = @ID_CATEGORIA,
id_cliente = @ID_CLIENTE
WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", contaReceber.Nome);
            comando.Parameters.AddWithValue("@DATA_PAGAMENTO", contaReceber.DataPagamento);
            comando.Parameters.AddWithValue("@VALOR", contaReceber.Valor);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaReceber.IdCategoria);
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaReceber.IdCliente);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"DELETE FROM contas_receber WHERE id = @ID ";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(ContaReceber contaReceber)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO contas_receber 
            (nome, data_pagamento, valor, id_categoria, id_cliente)
            OUTPUT INSERTED.ID
            VALUES
            (@NOME, @DATA_PAGAMENTO, @VALOR, @ID_CATEGORIA, @ID_CLIENTE)";
            comando.Parameters.AddWithValue("@NOME", contaReceber.Nome);
            comando.Parameters.AddWithValue("@DATA_PAGAMENTO", contaReceber.DataPagamento);
            comando.Parameters.AddWithValue("@VALOR", contaReceber.Valor);
            comando.Parameters.AddWithValue("@ID_CATEGORIA", contaReceber.IdCategoria);
            comando.Parameters.AddWithValue("@ID_CLIENTE", contaReceber.IdCliente);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public ContaReceber ObterPeloid(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT * FROM contas_receber WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Id = Convert.ToInt32(linha["id"]);
            contaReceber.Nome = linha["nome"].ToString();
            contaReceber.DataPagamento = Convert.ToDateTime(linha["data_pagamento"]);
            contaReceber.Valor = Convert.ToDecimal(linha["valor"]);
            return contaReceber;
        }


        public List<ContaReceber> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT 
clientes.id AS 'ClienteId',
clientes.nome AS 'ClienteNome',
clientes.cpf AS 'ClienteCpf',
categorias.id AS 'CategoriasId',
categorias.nome AS 'CategoriasNome',
contas_receber.id AS 'ContaReceberId',
contas_receber.nome AS 'ContaReceberNome',
contas_receber.data_pagamento AS 'ContaReceberDataPagamento',
contas_receber.valor AS 'ContaReceberValor'
FROM contas_receber
INNER JOIN clientes ON (contas_receber.id_cliente = clientes.id)
INNER JOIN categorias ON (contas_receber.id_categoria = categorias.id)";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<ContaReceber> contasReceber = new List<ContaReceber>();
            foreach (DataRow linha in tabela.Rows)
            {
                ContaReceber contaReceber = new ContaReceber();
                contaReceber.Id = Convert.ToInt32(linha["id"]);
                contaReceber.Nome = linha["nome"].ToString();
                contaReceber.DataPagamento = Convert.ToDateTime(linha["data_pagamento"]);
                contaReceber.Valor = Convert.ToDecimal(linha["valor"]);                
                contaReceber.Categoria = new Categoria();
                contaReceber.Categoria.Id = Convert.ToInt32(linha["CategoriaId"]);
                contaReceber.Categoria.Nome = linha["CategoriaNome"].ToString();
                contasReceber.Add(contaReceber);                
            }
            return contasReceber;
        }
    }
}
