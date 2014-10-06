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

    public class TurnoBL : SPERepository<Turno>
    {
        public TurnoBL(TransactionRepository transaction)
            : base(transaction) { }

        public Turno Inserir(Turno turno)
        {
            var existe = Get(e => string.Compare(e.Descr.ToLower(), turno.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Turno. Já existe um Turno cadastrado com essa Descrição.");

            return Add(turno);
        }

        public Turno Atualizar(Turno turno)
        {
            var existe = Get(e => string.Compare(e.Descr.ToLower(), turno.Descr.ToLower(), CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0
                                                    && e.IdTurno != turno.IdTurno);

            if (existe.Any())
                throw new CustomException("Erro ao cadastrar Turno. Já existe um Turno cadastrado com essa Descrição.");


            return Update(turno);
        }

        public bool Deletar(int id)
        {
            Turno tipoRecurso = Get(a => a.IdTurno == id, null, "Turma").FirstOrDefault();
            if (tipoRecurso.Turma.Any())
                throw new CustomException("Erro ao excluir Turno. Este Turno encontra-se vinculado a uma Turma");

            return Delete(tipoRecurso);
        }

    }
}
