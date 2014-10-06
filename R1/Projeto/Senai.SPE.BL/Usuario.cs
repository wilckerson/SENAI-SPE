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

    public class UsuarioBL : SPERepository<Usuario>
    {
        public UsuarioBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public UsuarioBL()
        {
            // TODO: Complete member initialization
        }

        public Usuario Inserir(Usuario model)
        {
            var existe = Get(e => string.Compare(e.Nome.ToLower(), model.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0 );

            if (existe.Any()) 
                throw new CustomException("Erro ao cadastrar Usuario. Já existe um Usuário cadastrado com esse Nome.");

            return Add(model);
        }

        public void Atualizar(Usuario model)
        {
            var existe = Get(e => e.IdUsuario != model.IdUsuario && string.Compare(e.Nome.ToLower(), model.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (existe.Any())
            {
                throw new CustomException("Erro ao editar Usuario. Já existe um Usuário cadastrado com esse Nome.");
            }
            else
            {
                existe = Get(e => e.IdUsuario != model.IdUsuario && string.Compare(e.Email.ToLower(), model.Email.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);
                if (existe.Any())
                {
                    throw new CustomException("Erro ao editar Usuario. já existe um usuário com o email cadastrado.");
                }
            } 
            Update(model);
        }

        public void ApagarUsuario(int id)
        {
            Usuario model = Get(a => a.IdUsuario == id, null).SingleOrDefault();

            Delete(model);
        }

        public void Modificar(Usuario model)
        {
            Update(model);
        }
    }
}
