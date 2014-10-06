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

    public class PerfilBL : SPERepository<Perfil>
    {
        public PerfilBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public Perfil Inserir(Perfil model, List<int> IdFuncionalidades)
        {
            var existe = Get(e => string.Compare(e.Nome.ToLower(), model.Nome.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0 );

            if (existe.Any()) 
                throw new CustomException("Erro ao cadastrar Perfil. Já existe um Perfil cadastrado com esse Nome.");

            using (var ctx = CreateContext())
            {
                var funcList = ctx.Set<Funcionalidade>().Where(a=> IdFuncionalidades.Contains(a.idFuncionalidade));
                funcList.ToList().ForEach(a => model.Funcionalidade.Add(a));
                ctx.Set<Perfil>().Add(model);
                ctx.SaveChanges();
            }

            return model;
        }
        

        public void Atualizar(Perfil model, List<int> IdFuncionalidades)
        {
            this.Update(model);
            this.Save();
            var ctx = new SPEContext();
            ctx.Configuration.LazyLoadingEnabled = false;
            ctx.Configuration.ProxyCreationEnabled = false;
            List<Funcionalidade> lista = new List<Funcionalidade>();
            foreach (var item in IdFuncionalidades)
            {
                lista.Add(ctx.Funcionalidade.Where(a => a.idFuncionalidade == item).FirstOrDefault());
            }
            var PerfilFuncionalidade = (from u in ctx.Perfil.Include("Funcionalidade")
                                        where u.idPerfil == model.idPerfil
                                        select u).SingleOrDefault();

            if (PerfilFuncionalidade != null && PerfilFuncionalidade.Funcionalidade.Count > 0)
            {
                foreach (var item in PerfilFuncionalidade.Funcionalidade.ToList())
                {
                    PerfilFuncionalidade.Funcionalidade.Remove(item);
                }

            }
            PerfilFuncionalidade.Funcionalidade = lista;
            ctx.SaveChanges();
            ctx.Dispose();
            Save();
        }

        public void Apagar(int id)
        {
            //Perfil model = Get(a => a.idPerfil == id, null, "Funcionalidade,Usuario").SingleOrDefault();
            var model = this.Context.Perfil.Include("Funcionalidade")
               .Include("usuario")               
               .Where(a => a.idPerfil == id).FirstOrDefault();
            
            if (model.Usuario.Any()) 
                throw new CustomException("Erro ao excluir Perfil. Já existe um Usuário vinculado a esse Perfil.");

            this.Context.SaveChanges();
            this.Context.Dispose();
            Delete(model);
        }
    }
}
