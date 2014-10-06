using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class TipoAtividadeViewModel
    {

        #region Átributos

        [Key]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int IdTipoAtividade { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }
        public bool Interna { get; set; }

        public virtual ICollection<AgendaDocente> AgendaDocente { get; set; }

        #endregion

        #region Métodos

        public static TipoAtividade MapToModel(TipoAtividadeViewModel TipoAtividadeViewModel)
        {
            TipoAtividade TipoAtividade = new TipoAtividade()
            {
                IdTipoAtividade = TipoAtividadeViewModel.IdTipoAtividade,
                Nome = TipoAtividadeViewModel.Nome,
                Interna = TipoAtividadeViewModel.Interna,
                AgendaDocente = TipoAtividadeViewModel.AgendaDocente
            };
            return TipoAtividade;
        }

        public static TipoAtividadeViewModel MapToViewModel(TipoAtividade TipoAtividade)
        {
            TipoAtividadeViewModel TipoAtividadeViewModel = new TipoAtividadeViewModel()
            {
                IdTipoAtividade = TipoAtividade.IdTipoAtividade,
                Nome = TipoAtividade.Nome,
                Interna = TipoAtividade.Interna,
                AgendaDocente = TipoAtividade.AgendaDocente
            };
            return TipoAtividadeViewModel;
        }

        public static List<TipoAtividade> MapToListModel(List<TipoAtividadeViewModel> TipoAtividadeViewModel)
        {
            List<TipoAtividade> listaTipoAtividade = (from TipoAtividade in TipoAtividadeViewModel
                                                    select new TipoAtividade()
                                                    {
                                                        IdTipoAtividade = TipoAtividade.IdTipoAtividade,
                                                        Nome = TipoAtividade.Nome,
                                                        Interna = TipoAtividade.Interna,
                                                        AgendaDocente = TipoAtividade.AgendaDocente
                                                    }).ToList();
            return listaTipoAtividade;
        }

        public static List<TipoAtividadeViewModel> MapToListViewModel(List<TipoAtividade> TipoAtividade)
        {
            List<TipoAtividadeViewModel> listaTipoAtividadeViewModel = (from TipoAtividadeViewModel in TipoAtividade
                                                                      select new TipoAtividadeViewModel()
                                                                      {
                                                                          IdTipoAtividade = TipoAtividadeViewModel.IdTipoAtividade,
                                                                          Nome = TipoAtividadeViewModel.Nome,
                                                                          Interna = TipoAtividadeViewModel.Interna,
                                                                          AgendaDocente = TipoAtividadeViewModel.AgendaDocente
                                                                      }).ToList();
            return listaTipoAtividadeViewModel;
        }

        #endregion
    }
}