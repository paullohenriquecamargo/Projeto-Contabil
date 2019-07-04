﻿using Model;
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
    public class CategoriaRepository : ICategoriaRepository
    {
        public bool Alterar(Categoria categoria)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE categorias SET 
nome = @NOME,
WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", categoria.Nome);
            comando.Parameters.AddWithValue("@ID", categoria.Id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
        }
                                
        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"DELETE FROM contabilidades WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;            
        }

        public int Inserir(Categoria categoria)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO (nome) 
OUTPUT INSERTED.ID VALUES (@NOME)";
            comando.Parameters.AddWithValue("@NOME", categoria.Nome);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }
                                
        public Categoria ObterpeloId(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT * FROM categorias WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if(tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            Categoria contabilidade = new Categoria();
            contabilidade.Id = Convert.ToInt32(linha["id"]);
            contabilidade.Nome = linha["nome"].ToString();
            return contabilidade;
        }

        public List<Categoria> ObterTodos()
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT * FROM categorias";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            List<Categoria> contabilidades = new List<Categoria>();
            foreach(DataRow linha in tabela.Rows)
            {
                Categoria contabilidade = new Categoria()
                {
                    Id = Convert.ToInt32(linha["id"]),
                    Nome = linha["nome"].ToString()
                };
                contabilidades.Add(contabilidade);
            }
            return contabilidades;
        }                                                               
    }
}
