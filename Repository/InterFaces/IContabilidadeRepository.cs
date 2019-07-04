using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.InterFaces
{
    interface IContabilidadeRepository
    {
        int Inserir(Contabilidade contabilidade);

        bool Alterar(Contabilidade contabilidade);

        bool Apagar(int id);

        List<Contabilidade> ObterTodos(string pesquisa);

        Contabilidade ObterPeloId(int id);
    }
}
