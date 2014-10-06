using Repository;
using Senai.SPE.Domain;
using SPERepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogic
{

    public class FuncionalidadeBL : SPERepository<Funcionalidade>
    {
        public FuncionalidadeBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        //public void Inserir(Funcionalidade model)
        //{
        //    //var existe = Get(e => string.Compare(e., model..ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);
        //    var existe = Get(e=> e
        //    if (existe.Any())
        //        throw new CustomException("Erro ao cadastrar Empresa. Já existe uma Emrpesa cadastrada com esse Nome Fantasia.");

        //    return Add(model);
        //}
    }
}
