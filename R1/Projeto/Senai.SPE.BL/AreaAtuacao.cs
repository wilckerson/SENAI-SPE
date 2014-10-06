using Common;
using Repository;
using Senai.SPE.Domain;
using SPERepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogic
{

    public class AreaAtuacaoBL : SPERepository<AreaAtuacao>
    {
        public AreaAtuacaoBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public void InserirAreaAtuacao(AreaAtuacao areaAtuacao)
        {
            //areaAtuacao.DataInclusao = DateTime.Now.AddHours(-3);
            //perfil.UsuarioInclusaoId = this.UsuarioLogado.Id;
            //perfil.Ativo = true;

            var existe = this.Get(e => string.Compare(e.Nome.ToLower(), areaAtuacao.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Área de Atuação. Já existe uma Área de Atuação cadastrada com esse Nome.");


            this.Add(areaAtuacao);
        }

        public void AtualizarAreaAtuacao(AreaAtuacao areaAtuacao)
        {
            var existe = this.Get(e => string.Compare(e.Nome.ToLower(), areaAtuacao.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    && e.IdAreaAtuacao != areaAtuacao.IdAreaAtuacao);

            if (existe.Any())
                throw new CustomException("Erro ao editar Área de Atuação. Já existe uma Área de Atuação cadastrada com esse Nome.");

            //Pegar a AreaAtuacao original
            var areaOriginal = this.Get(e => e.IdAreaAtuacao == areaAtuacao.IdAreaAtuacao, null, "Matriz,Usuario").SingleOrDefault();
            areaOriginal.Nome = areaAtuacao.Nome;

            this.Update(areaAtuacao);
        }

        public void DeletarAreaAtuacao(int id)
        {
            AreaAtuacao areaAtuacao = this.Get(e => e.IdAreaAtuacao == id, null, "Matriz,Usuario,Componente").SingleOrDefault();

            if (areaAtuacao.Matriz.Any() || areaAtuacao.Usuario.Any() || areaAtuacao.Componente.Any())
                throw new CustomException("Erro ao excluir Área de Atuação. Esta Área de Atuação já se encontra vinculada com outros membros.");

            this.Delete(areaAtuacao);
        }
    }
}
