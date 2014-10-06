using Common.DataAnnotations;
using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SPE.ViewModel
{
    public class AmbienteViewModel
    {

        #region Atributos
        [Key]
        public int? IdAmbiente { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Localização")] 
        public Nullable<int> IdLoc { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Tipo de Ambiente")]  
        public Nullable<int> IdTipoAmbiente { get; set; }
        public Nullable<int> IdAgendaComponente { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [RegularExpression("(^0?[1-9][0-9]*$)", ErrorMessage = "A Capacidade deve ser maior que 0")]
        [Display(Name = "Capacidade")]  
        public Nullable<short> Capac { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<short> Status { get; set; }
        [Display(Name = "Observação")]
        [DataType(DataType.MultilineText)]
        [StringLength(255)]
        [HtmlAttributes( MaxLength = 255)]
        public string Observacao { get; set; }

        public string  NomeLocal { get; set; }

        public virtual ICollection<AgendaAmbiente> AgendaAmbiente { get; set; }
        public virtual AgendaComponente AgendaComponente { get; set; }
        public virtual LocalAmbiente LocalAmbiente { get; set; }
        public virtual TipoAmbiente TipoAmbiente { get; set; }
        public virtual ICollection<RecursoAmbiente> RecursoAmbiente { get; set; }


        //Listas para popular o DropDownList e CHeckbox
        public List<TipoAmbienteViewModel> ListaTipoAmbiente { get; set; }
        public List<LocalAmbienteViewModel> ListaLocalidade { get; set; }
        public List<RecursoViewModel> ListaRecurso { get; set; }
        public string HiddenListaRecurso { get; set; }

        #endregion

        #region Métodos

        public static Ambiente MapToModel(AmbienteViewModel ambienteViewModel)
        {
            Ambiente ambiente = new Ambiente()
            {
                IdAmbiente = ambienteViewModel.IdAmbiente.GetValueOrDefault(0),
                IdLoc = ambienteViewModel.IdLoc,
                IdTipoAmbiente = ambienteViewModel.IdTipoAmbiente,
                IdAgendaComponente = ambienteViewModel.IdAgendaComponente,
                Capac = ambienteViewModel.Capac,
                Nome = ambienteViewModel.Nome,
                Status = ambienteViewModel.Status,
                Observacao = ambienteViewModel.Observacao,
                RecursoAmbiente = ambienteViewModel.RecursoAmbiente
            };
            return ambiente;
        }

        public static AmbienteViewModel MapToViewModel(Ambiente ambiente)
        {
            AmbienteViewModel ambienteViewModel = new AmbienteViewModel()
            {
                IdAmbiente = ambiente.IdAmbiente,
                IdLoc = ambiente.IdLoc,
                IdTipoAmbiente = ambiente.IdTipoAmbiente,
                IdAgendaComponente = ambiente.IdAgendaComponente,
                Capac = ambiente.Capac,
                Nome = ambiente.Nome,
                Status = ambiente.Status,
                Observacao = ambiente.Observacao,
                AgendaAmbiente = ambiente.AgendaAmbiente,
                AgendaComponente = ambiente.AgendaComponente,
                LocalAmbiente = ambiente.LocalAmbiente,
                TipoAmbiente = ambiente.TipoAmbiente,
                RecursoAmbiente = ambiente.RecursoAmbiente


            };
            return ambienteViewModel;
        }


        public static List<Ambiente> MapToListModel(List<AmbienteViewModel> ambienteViewModel)
        {
            List<Ambiente> listaAmbiente = (from ambiente in ambienteViewModel
                                        select new Ambiente()
                                        {
                                            IdAmbiente = ambiente.IdAmbiente.GetValueOrDefault(0),
                                            IdLoc = ambiente.IdLoc,
                                            IdTipoAmbiente = ambiente.IdTipoAmbiente,
                                            IdAgendaComponente = ambiente.IdAgendaComponente,
                                            Capac = ambiente.Capac,
                                            Nome = ambiente.Nome,
                                            Status = ambiente.Status,
                                            Observacao = ambiente.Observacao,
                                            RecursoAmbiente = ambiente.RecursoAmbiente
                                        }).ToList();
            return listaAmbiente;
        }

        public static List<AmbienteViewModel> MapToListViewModel(List<Ambiente> ambiente)
        {
            List<AmbienteViewModel> listaAmbienteViewModel = (from ambienteViewModel in ambiente
                                                          select new AmbienteViewModel()
                                                          {
                                                              IdAmbiente = ambienteViewModel.IdAmbiente,
                                                              IdLoc = ambienteViewModel.IdLoc,
                                                              IdTipoAmbiente = ambienteViewModel.IdTipoAmbiente,
                                                              IdAgendaComponente = ambienteViewModel.IdAgendaComponente,
                                                              Capac = ambienteViewModel.Capac,
                                                              Nome = ambienteViewModel.Nome,
                                                              Status = ambienteViewModel.Status,
                                                              Observacao = ambienteViewModel.Observacao,
                                                              AgendaAmbiente = ambienteViewModel.AgendaAmbiente,
                                                              AgendaComponente = ambienteViewModel.AgendaComponente,
                                                              LocalAmbiente = ambienteViewModel.LocalAmbiente,
                                                              TipoAmbiente = ambienteViewModel.TipoAmbiente,
                                                              RecursoAmbiente = ambienteViewModel.RecursoAmbiente
                                                          }).ToList();
            return listaAmbienteViewModel;
        }

        public static List<AmbienteViewModel> MapToListViewModel(List<ufnListarAmbientes1_Result> ambiente)
        {
            List<AmbienteViewModel> listaAmbienteViewModel = (from ambienteViewModel in ambiente
                                                              select new AmbienteViewModel()
                                                              {
                                                                  IdAmbiente = ambienteViewModel.IdAmbiente,
                                                                  Nome = ambienteViewModel.NomeAmbiente,
                                                                  IdLoc = ambienteViewModel.IdLocal,
                                                                  NomeLocal = ambienteViewModel.NomeLocal,
                                                                  Capac = ambienteViewModel.Capacidade                                                                 
                                                              }).ToList();
            return listaAmbienteViewModel;
        }

        #endregion

       
    }
}