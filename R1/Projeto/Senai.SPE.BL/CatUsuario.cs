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

    public class CatUsuarioBL : SPERepository<CatUsuario>
    {
        public CatUsuarioBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public CatUsuario InserirCatUsuario(CatUsuario model)
        {
            var existe = Get(e => string.Compare(e.NomeCatUsuario.ToLower(), model.NomeCatUsuario.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0 );

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Categoria de Usuario. Já existe uma Categoria cadastrado com esse Nome.");

            return Add(model);
        }

        public void AtualizarCatUsuario(CatUsuario model)
        {
            var existe = Get(e => string.Compare(e.NomeCatUsuario.ToLower(), model.NomeCatUsuario.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Categoria de Usuario. Já existe uma Categoria cadastrado com esse Nome.");

            Update(model);
        }

        //public void ApagarCatUsuario(int id)
        //{
        //    CatUsuario model = Get(a => a.IdCatUsuario== id, null, "Usuario").SingleOrDefault();

        //    if (model.Usuario.Any() )
        //        throw new CustomException("Erro ao excluir Categoria de Usuario. Esta Categoria já encontra-se vinculada a um ou mais usuários.");

        //    Delete(model);
        //}
    }
}
