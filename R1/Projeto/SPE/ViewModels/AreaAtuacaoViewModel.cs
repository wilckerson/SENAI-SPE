using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class AreaAtuacaoViewModel
    {
        #region Atributos

        [Key]
        public int? IdAreaAtuacao { get; set; }

        [Display(Name = "Nome: ")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }


        #endregion

        #region Métodos

        public static AreaAtuacao MapToModel(AreaAtuacaoViewModel areasAtuacaoViewModel)
        {
            AreaAtuacao areaAtuacao = new AreaAtuacao()
            {
                IdAreaAtuacao = areasAtuacaoViewModel.IdAreaAtuacao.GetValueOrDefault(0),
                Nome = areasAtuacaoViewModel.Nome
            };

            return areaAtuacao;
        }

        public static AreaAtuacaoViewModel MapToViewModel(AreaAtuacao areaAtuacao)
        {
            AreaAtuacaoViewModel areaAtuacaoViewModel = new AreaAtuacaoViewModel()
            {
                IdAreaAtuacao = areaAtuacao.IdAreaAtuacao,
                Nome = areaAtuacao.Nome
            };
            return areaAtuacaoViewModel;
        }


        public static List<AreaAtuacao> MapToListModel(List<AreaAtuacaoViewModel> areasAtuacaoViewModel)
        {
            List<AreaAtuacao> listaAreaAtuacao = (from areaAtuacao in areasAtuacaoViewModel
                                                  select new AreaAtuacao()
                                                  {
                                                      IdAreaAtuacao = areaAtuacao.IdAreaAtuacao.GetValueOrDefault(0),
                                                      Nome = areaAtuacao.Nome
                                                  }).ToList();
            return listaAreaAtuacao;
        }

        public static List<AreaAtuacaoViewModel> MapToListViewModel(List<AreaAtuacao> areasAtuacao)
        {
            List<AreaAtuacaoViewModel> listaAreaAtuacaoViewModel = (from areaAtuacaoViewModel in areasAtuacao
                                                                    select new AreaAtuacaoViewModel()
                                                                    {
                                                                        IdAreaAtuacao = areaAtuacaoViewModel.IdAreaAtuacao,
                                                                        Nome = areaAtuacaoViewModel.Nome
                                                                    }).ToList();
            return listaAreaAtuacaoViewModel;
        }



        #endregion
    }
}