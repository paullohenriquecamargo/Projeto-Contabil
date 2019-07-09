using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.InterFaces
{
    interface IClienteRepository
    {
        int Inserir(Cliente cliente);

        bool Alterar(Cliente cliente);

        bool Apagar(int id);

        List<Cliente> ObterTodos();

        Cliente ObterPeloId(int id);
    }
}
