using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class ModalidadeViewModel
    {
        #region Atributos

        [Key]
        public int? IdModalidade { get; set; }

        [Display(Name = "Nome: ")]
        [Required(ErrorMessage= "Campo Obrigatório")]
        public string Nome { get; set; }

     

        #endregion

        #region Métodos

        public static Modalidade MapToModel(ModalidadeViewModel modalidadeViewModel)
        {
            Modalidade modalidade = new Modalidade()
            {
                IdModalidade = modalidadeViewModel.IdModalidade.GetValueOrDefault(0),
                Nome = modalidadeViewModel.Nome
            };
               
            return modalidade;
        }

        public static ModalidadeViewModel MapToViewModel(Modalidade modalidade)
        {
            ModalidadeViewModel modalidadeViewModel = new ModalidadeViewModel()
            {
                IdModalidade = modalidade.IdModalidade,
                Nome = modalidade.Nome
            };
            return modalidadeViewModel;
        }


        public static List<Modalidade> MapToListModel(List<ModalidadeViewModel> modalidadesViewModel)
        {
            List<Modalidade> listaModalidade = (from modalidade in modalidadesViewModel
                                                select new Modalidade()
                                                {
                                                    IdModalidade = modalidade.IdModalidade.GetValueOrDefault(0),
                                                    Nome = modalidade.Nome
                                                }).ToList();
            return listaModalidade;
        }

        public static List<ModalidadeViewModel> MapToListViewModel(List<Modalidade> modalidades)
        {
            List<ModalidadeViewModel> listaModalidadeViewModel = (from modalidadeViewModel in modalidades
                                                                  select new ModalidadeViewModel()
                                                                  {
                                                                      IdModalidade = modalidadeViewModel.IdModalidade,
                                                                      Nome = modalidadeViewModel.Nome
                                                                  }).ToList();
            return listaModalidadeViewModel;
        }



        #endregion
    }
}