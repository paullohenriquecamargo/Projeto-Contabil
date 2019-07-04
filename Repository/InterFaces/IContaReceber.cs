using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.InterFaces
{
    interface IContaReceber
    {
        int Inserir(ContaReceber contaReceber);

        List<ContaReceber> ObterTodos();

        bool Alterar(ContaReceber contaReceber);

        ContaReceber ObterPeloid(int id);

        bool Apagar(int id);
    }
}
