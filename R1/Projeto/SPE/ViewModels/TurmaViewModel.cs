using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class TurmaViewModel
    {
        [Key]
        public int IdTurma { get; set; }
        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<int> IdUnidade { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Turno")]
        public Nullable<int> IdTurno { get; set; }
        [Display(Name = "Centro de Responsabilidade")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<int> IdCR { get; set; }
        [Display(Name = "Matriz")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<int> IdMatriz { get; set; }
        [Display(Name = "Orientador")]
        public Nullable<int> IdUsuario { get; set; }
        [Display(Name = "Nº de Vagas")]

        [RegularExpression("(^0?[1-9][0-9]*$)", ErrorMessage = "O número de vagas deve ser maior que 0")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<short> QtdeVagas { get; set; }
        [Display(Name = "Data Final")]
        public Nullable<System.DateTime> DataFim { get; set; }
        [Display(Name = "Data Inicio")]
        public Nullable<System.DateTime> DataInicio { get; set; }

        [Display(Name = "C.H.")]
        public Nullable<int> CHView { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Status")]
        public Nullable<short> Status { get; set; }
        [Display(Name = "Oferta")]
        public Nullable<short> TipoOferta { get; set; }
        [Display(Name = "Preço")]
        public Nullable<decimal> Preco { get; set; }
        [Display(Name = "Preço")]
        public string PrecoView { get; set; }
        [Display(Name = "Evento")]
        public Nullable<int> Evento { get; set; }
      
        public Nullable<int> Aprovado { get; set; }
        public int? IdResponsavel { get; set; }
        public virtual CR CR { get; set; }
        public virtual Matriz Matriz { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual Unidade Unidade { get; set; }
        public virtual Usuario Usuario { get; set; }
        public ReprovacaoTurmaViewModel ReprovacaoTurma = new ReprovacaoTurmaViewModel();

        [Required (ErrorMessage="Campo obrigatório")]
        public string Observacao { 
            get { return ReprovacaoTurma.Observacao; }
            set { ReprovacaoTurma.Observacao = value; }
        }

        public virtual ICollection<Material> Material { get; set; }
        public virtual ICollection<Servico> Servico { get; set; }


        public IEnumerable<MatrizViewModel> ListaMatrizes { get; set; }
        public List<ReprovacaoTurma> ListaReprovacaoTurma { get; set; }

        public IEnumerable<TurnoViewModel> ListaTurnos { get; set; }

        public IEnumerable<UnidadeViewModel> ListaUnidades { get; set; }

        public IEnumerable<CRViewModel> ListaCR { get; set; }

        public IEnumerable<UsuarioViewModel> ListaUsuario { get; set; }

        public MatrizViewModel ListaMatrizModulo { get; set; }

        public string HoraIniView { get; set; }

        public string HoraFimView { get; set; }

        public string DataFimView { get; set; }

        public string DataIniView { get; set; }


        public virtual ICollection<AgendaAmbiente> AgendaAmbiente { get; set; }
        public virtual ICollection<AgendaComponente> AgendaComponente { get; set; }
        public virtual ICollection<AgendaDocente> AgendaDocente { get; set; }
        #region Métodos

        public static Turma MapToModel(TurmaViewModel turmaViewModel)
        {
            Turma turma = new Turma()
            {
                IdTurma = turmaViewModel.IdTurma,
                IdUnidade = turmaViewModel.IdUnidade,
                IdTurno = turmaViewModel.IdTurno,
                IdUsuario = turmaViewModel.IdUsuario,
                IdMatriz = turmaViewModel.IdMatriz,
                IdCR = turmaViewModel.IdCR,
                DataFim = turmaViewModel.DataFimView == null ? turmaViewModel.DataFim : DateTime.Parse(turmaViewModel.DataFimView),
                Preco = turmaViewModel.Preco,
                QtdeVagas = turmaViewModel.QtdeVagas,
                TipoOferta = turmaViewModel.TipoOferta,
                Status = turmaViewModel.Status,
                DataInicio = turmaViewModel.DataIniView == null ? turmaViewModel.DataInicio : DateTime.Parse(turmaViewModel.DataIniView),
                Evento = turmaViewModel.Evento,
                Aprovado = turmaViewModel.Aprovado,
                ReprovacaoTurma = turmaViewModel.ListaReprovacaoTurma,
                IdResponsavel = turmaViewModel.IdResponsavel

            };
            return turma;
        }

        public static TurmaViewModel MapToViewModel(Turma turma)
        {
            TurmaViewModel turmaViewModel = new TurmaViewModel()
            {
                IdTurma = turma.IdTurma,
                IdUnidade = turma.IdUnidade,
                IdTurno = turma.IdTurno,
                IdUsuario = turma.IdUsuario,
                IdMatriz = turma.IdMatriz,
                IdCR = turma.IdCR,
                DataFimView = turma.DataFim == null ? "" : turma.DataFim.Value.ToShortDateString(),
                Preco = turma.Preco,
                QtdeVagas = turma.QtdeVagas,
                TipoOferta = turma.TipoOferta,
                Status = turma.Status,
                DataIniView = turma.DataInicio == null ? "" : turma.DataInicio.Value.ToShortDateString(),
                CR = turma.CR,
                Matriz = turma.Matriz,
                Turno = turma.Turno,
                Unidade = turma.Unidade,
                Usuario = turma.Usuario,
                Material = turma.Material,
                CHView = turma.Matriz == null ? 0 : turma.Matriz.CH,
                Servico = turma.Servico,
                PrecoView = turma.Preco.HasValue ? turma.Preco.Value.ToString("0.00") :  "",
                AgendaAmbiente = turma.AgendaAmbiente,
                AgendaComponente = turma.AgendaComponente,
                AgendaDocente = turma.AgendaDocente,
                Evento = turma.Evento,
                Aprovado = turma.Aprovado,
                ListaReprovacaoTurma = turma.ReprovacaoTurma.OrderByDescending(a=>a.IdReprovacaoTurma).ToList(),
                IdResponsavel = turma.IdResponsavel

            };
            return turmaViewModel;
        }


        public static List<Turma> MapToListModel(List<TurmaViewModel> turmaViewModel)
        {
            List<Turma> listaTurma = (from turma in turmaViewModel
                                      select new Turma()
                                      {
                                          IdTurma = turma.IdTurma,
                                          IdUnidade = turma.IdUnidade,
                                          IdTurno = turma.IdTurno,
                                          IdUsuario = turma.IdUsuario,
                                          IdMatriz = turma.IdMatriz,
                                          IdCR = turma.IdCR,
                                          DataFim = DateTime.Parse(turma.DataFimView),
                                          Preco = turma.Preco,
                                          QtdeVagas = turma.QtdeVagas,
                                          TipoOferta = turma.TipoOferta,
                                          Status = turma.Status,
                                          Evento = turma.Evento,

                                          DataInicio = DateTime.Parse(turma.DataIniView),
                                          Aprovado = turma.Aprovado
                                      }).ToList();
            return listaTurma;
        }

        public static List<TurmaViewModel> MapToListViewModel(List<Turma> turmas)
        {
            List<TurmaViewModel> listaTurmaViewModel = (from turma in turmas
                                                        select new TurmaViewModel()
                                                        {
                                                            IdTurma = turma.IdTurma,
                                                            IdUnidade = turma.IdUnidade,
                                                            IdTurno = turma.IdTurno,
                                                            CR = turma.CR,
                                                            IdUsuario = turma.IdUsuario,
                                                            IdMatriz = turma.IdMatriz,
                                                            IdCR = turma.IdCR,
                                                            Nome = turma.Matriz != null ? turma.Matriz.Nome : "",
                                                            DataFimView = turma.DataFim == null || turma.DataInicio == DateTime.MinValue ? "" : turma.DataFim.Value.ToShortDateString(),
                                                            Preco = turma.Preco,
                                                            QtdeVagas = turma.QtdeVagas,
                                                            TipoOferta = turma.TipoOferta,
                                                            Status = turma.Status,
                                                            Evento = turma.Evento,
                                                            AgendaAmbiente = turma.AgendaAmbiente,
                                                            AgendaComponente = turma.AgendaComponente,
                                                            AgendaDocente = turma.AgendaDocente,
                                                            CHView = turma.Matriz == null ? 0 : turma.Matriz.CH,
                                                            PrecoView = turma.Preco.GetValueOrDefault().ToString("0.00"),
                                                            DataIniView = turma.DataInicio == null || turma.DataInicio == DateTime.MinValue ? "" : turma.DataInicio.Value.ToShortDateString(),
                                                            Aprovado = Convert.ToInt32(turma.Aprovado)
                                                        }).ToList();
            return listaTurmaViewModel;

        }

        public static List<TurmaViewModel> MapToListViewModel(List<ufnListarTurmas1_Result> turmas)
        {
            List<TurmaViewModel> lista = new List<TurmaViewModel>();

            lista = (from turma in turmas
                     select new TurmaViewModel()
                     {
                         IdTurma = turma.IdTurma.GetValueOrDefault(0),
                         Nome = turma.NomeTurma,
                         DataIniView = turma.DataInicio == null || turma.DataInicio == DateTime.MinValue ? "" : turma.DataInicio.Value.ToShortDateString()
                     }).ToList();

            return lista;
        }

        #endregion



        
    }
}