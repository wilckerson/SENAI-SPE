using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class ComponenteViewModel
    {
        #region Atributos

        public int? IdComponente { get; set; }

        [Display(Name = "Nome: ")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Carga Horária: ")]
        [RegularExpression("(^0?[1-9][0-9]*$)", ErrorMessage = "A Carga Horária deve ser maior que 0")]

        [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<short> CH { get; set; }

        public string TipoAmbienteId { get; set; }
        public string AreaAtuacaoId { get; set; }
        public virtual ICollection<TipoAmbiente> TipoAmbiente { get; set; }
        public List<TipoAmbienteViewModel> ListaTipoAmbiente { get; set; }
        public List<AreaAtuacao> AreaAtuacao { get; set; }
        public List<AreaAtuacaoViewModel> ListaAreaAtuacao { get; set; }

        [Display(Name = "Área de Atuação: ")]
        public int IdAreaAtuacao { get; set; }

        #endregion

        #region Métodos

        public static Componente MapToModel(ComponenteViewModel componenteViewModel)
        {
            Componente componente = new Componente()
            {
                IdComponente = componenteViewModel.IdComponente.GetValueOrDefault(0),
                Nome = componenteViewModel.Nome,
                CH = componenteViewModel.CH.GetValueOrDefault(0),
            };
            return componente;
        }

        public static ComponenteViewModel MapToViewModel(Componente componente)
        {
            ComponenteViewModel componenteViewModel = new ComponenteViewModel()
            {
                IdComponente = componente.IdComponente,
                Nome = componente.Nome,
                CH = componente.CH,
                TipoAmbiente = componente.TipoAmbiente,

                AreaAtuacao = componente.AreaAtuacao.ToList()
            };
            return componenteViewModel;
        }


        public static List<Componente> MapToListModel(List<ComponenteViewModel> componentesViewModel)
        {
            List<Componente> listaComponente = new List<Componente>();
            if (componentesViewModel != null)
            {
                listaComponente = (from componente in componentesViewModel
                                   select new Componente()
{
    IdComponente = componente.IdComponente.GetValueOrDefault(0),
    Nome = componente.Nome,
    CH = componente.CH.GetValueOrDefault(0),

}).ToList();
            }
            return listaComponente;
        }

        public static List<ComponenteViewModel> MapToListViewModel(List<Componente> componentes)
        {
            List<ComponenteViewModel> listaComponenteViewModel = (from componenteViewModel in componentes
                                                                  select new ComponenteViewModel()
                                                         {
                                                             IdComponente = componenteViewModel.IdComponente,
                                                             Nome = componenteViewModel.Nome,
                                                             CH = componenteViewModel.CH.GetValueOrDefault(0),
                                                             TipoAmbiente = componenteViewModel.TipoAmbiente,
                                                             AreaAtuacao = componenteViewModel.AreaAtuacao.ToList()
                                                         }).ToList();
            return listaComponenteViewModel;
        }




        public static List<ComponenteViewModel> MapToListViewModel(List<ufnListarComponentesPorModulo1_Result> ufnComponentes)
        {
            List<ComponenteViewModel> listaComponente = (from componente in ufnComponentes
                                                         select new ComponenteViewModel()
                                                {
                                                    IdComponente = componente.IdComponente.GetValueOrDefault(0),
                                                    Nome = componente.NomeComponente,
                                                    CH = (short)componente.CH.GetValueOrDefault(0)

                                                }).ToList();
            return listaComponente;
        }



        #endregion
    }
}