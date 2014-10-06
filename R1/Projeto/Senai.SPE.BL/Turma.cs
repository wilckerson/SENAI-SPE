using Repository;
using Senai.SPE.Domain;
using SPERepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogic
{

    public class TurmaBL : SPERepository<Turma>
    {
        public TurmaBL(TransactionRepository transaction)
            : base(transaction)
        {

        }

        public void AtualizarTurmaPorAgendamento(string idTurma)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this.Context.Database.Connection.ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandText = "exec AtualizarDataTurma " + idTurma;

                // Open the connection and execute the reader.
                sqlConnection.Open();
                command.ExecuteNonQuery();
                //sqlConnection.Close();
            }
        }

        public IQueryable<ufnPegarMinMaxDatas1_Result> GetDatasMinMax(int id)
        {
            return Context.ufnPegarMinMaxDatas1(id);
        }

        public void AtualizarReprovacaoTurma(Turma turma,  DateTime date, string observ = "")
        {
            
            //perfil.DataAlteracao = DateTime.Now.AddHours(-3);
            //perfil.UsuarioAlteracaoId = this.UsuarioLogado.Id;
            if (turma.Aprovado != null)
            {

                //this.Update(turma);
                //this.Save();
                var ctx = new SPEContext();
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;
                ReprovacaoTurma RepTurma = new ReprovacaoTurma                
                {
                    IdTurma = turma.IdTurma,
                    Observacao = observ,
                    Data = date,
                    Tipo = turma.Aprovado
                };
                ctx.ReprovacaoTurma.Add(RepTurma);

                var turmax = GetById(turma.IdTurma);
                turmax.Aprovado = turma.Aprovado;
               
                Update(turmax);
                ctx.SaveChanges();
                ctx.Dispose();
                Save();
                

                //turmaOriginal = turma;
               
                
            
            }
            else
            {
                var turmaOriginal = this.Get(e => e.IdTurma == turma.IdTurma, null, "AgendaAmbiente,AgendaComponente,AgendaDocente").SingleOrDefault();

                if (turmaOriginal.IdMatriz != turma.IdMatriz)
                {
                    var ListaAgendaAmbiente = turmaOriginal.AgendaAmbiente.ToList();
                    var ListaAgendaComponente = turmaOriginal.AgendaComponente.ToList();
                    var ListaAgendaDocente = turmaOriginal.AgendaDocente.ToList();
                    AgendaAmbienteBL agendaAmbienteBL = new AgendaAmbienteBL();
                    ComponenteBL agendaComponenteBL = new ComponenteBL();
                    AgendaDocenteBL agendaDocenteBL = new AgendaDocenteBL();

                    turma.DataFim = null;
                    turma.DataInicio = null;

                    foreach (var item in ListaAgendaAmbiente)
                    {
                        agendaAmbienteBL.Delete(item);
                    }

                    foreach (var item in ListaAgendaComponente)
                    {
                        agendaComponenteBL.Delete(item.IdAgendaComponente);

                    }

                    foreach (var item in ListaAgendaDocente)
                    {
                        agendaDocenteBL.Delete(item);
                    }

                    //turmaOriginal.AgendaAmbiente.Clear();
                    //turmaOriginal.AgendaComponente.Clear();
                    //turmaOriginal.AgendaDocente.Clear();
                }

                this.Update(turma);
                this.Save();
                var ctx = new SPEContext();
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;
                ReprovacaoTurma lista = new ReprovacaoTurma
                {
                    IdTurma = turma.IdTurma,
                    Observacao = observ,
                    Data = date,
                    Tipo = turma.Aprovado
                };
                ctx.ReprovacaoTurma.Add(lista);
                ctx.SaveChanges();
                ctx.Dispose();
                Save();

                //turmaOriginal = turma;
                Update(turma);

            }
            

        }

        public void AtualizarTurma(Turma turma)
        {

            //perfil.DataAlteracao = DateTime.Now.AddHours(-3);
            //perfil.UsuarioAlteracaoId = this.UsuarioLogado.Id;

            if (turma.Aprovado != null)
            {
                var ctx = new SPEContext();
                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

                var turmaOriginal = GetById(turma.IdTurma);
                turmaOriginal.Aprovado = turma.Aprovado;
               
                Update(turmaOriginal);
                ctx.SaveChanges();
                ctx.Dispose();
                Save();
            }
            else
            {
                var turmaOriginal = this.Get(e => e.IdTurma == turma.IdTurma, null, "AgendaAmbiente,AgendaComponente,AgendaDocente").SingleOrDefault();

                if (turmaOriginal.IdMatriz != turma.IdMatriz)
                {
                    var ListaAgendaAmbiente = turmaOriginal.AgendaAmbiente.ToList();
                    var ListaAgendaComponente = turmaOriginal.AgendaComponente.ToList();
                    var ListaAgendaDocente = turmaOriginal.AgendaDocente.ToList();
                    AgendaAmbienteBL agendaAmbienteBL = new AgendaAmbienteBL();
                    ComponenteBL agendaComponenteBL = new ComponenteBL();
                    AgendaDocenteBL agendaDocenteBL = new AgendaDocenteBL();

                    turma.DataFim = null;
                    turma.DataInicio = null;

                    foreach (var item in ListaAgendaAmbiente)
                    {
                        agendaAmbienteBL.Delete(item);
                    }

                    foreach (var item in ListaAgendaComponente)
                    {
                        agendaComponenteBL.Delete(item.IdAgendaComponente);

                    }

                    foreach (var item in ListaAgendaDocente)
                    {
                        agendaDocenteBL.Delete(item);
                    }

                    //turmaOriginal.AgendaAmbiente.Clear();
                    //turmaOriginal.AgendaComponente.Clear();
                    //turmaOriginal.AgendaDocente.Clear();
                }

                //turmaOriginal = turma;
                Update(turma);

            }

        }

        public List<ufnListarTurmas1_Result> RetornarTurmas(int pageIndex = 1, int pageNumber = 10, int? idCR = null, string nome = "", int? codigo = null, int? modalidade = null)
        {
            return (from u in Context.ufnListarTurmas1(idCR, nome, codigo, modalidade, pageIndex, pageNumber) select u).ToList();
        }
    }
}
