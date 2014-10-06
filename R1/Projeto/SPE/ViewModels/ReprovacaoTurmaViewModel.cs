using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class ReprovacaoTurmaViewModel
    {

        #region Atributos

        public int IdReprovacaoTurma { get; set; }
        public Nullable<int> IdTurma { get; set; }
        [Display(Name = "Observação: ")]
        public string Observacao { get; set; }


        #endregion

        #region Métodos

        public static ReprovacaoTurmaViewModel MapToModel(ReprovacaoTurma reprovacaoTurma)
        {
            ReprovacaoTurmaViewModel reprovacao = new ReprovacaoTurmaViewModel()
            {
                IdReprovacaoTurma = reprovacaoTurma.IdReprovacaoTurma,
                IdTurma = reprovacaoTurma.IdTurma,
                Observacao = reprovacaoTurma.Observacao
            };
            return reprovacao;
        }

        public static ReprovacaoTurmaViewModel MapToViewModel(ReprovacaoTurmaViewModel modulo)
        {
            ReprovacaoTurmaViewModel reprovacaoViewModel = new ReprovacaoTurmaViewModel()
            {
                IdReprovacaoTurma = modulo.IdReprovacaoTurma,
                IdTurma = modulo.IdTurma,
                Observacao = modulo.Observacao
            };
            return reprovacaoViewModel;
        }


        public static List<ReprovacaoTurmaViewModel> MapToListModel(List<ReprovacaoTurmaViewModel> reprovacaoTurmaViewModel)
        {
            List<ReprovacaoTurmaViewModel> listaReprovacaoTurma = (from modulo in reprovacaoTurmaViewModel
                                                                   select new ReprovacaoTurmaViewModel()
                                                            {
                                                                IdReprovacaoTurma = modulo.IdReprovacaoTurma,
                                                                Observacao = modulo.Observacao,
                                                                IdTurma = modulo.IdTurma
                                                            }).ToList();
            return listaReprovacaoTurma;
        }

        public static List<ReprovacaoTurmaViewModel> MapToListViewModel(List<ReprovacaoTurma> reprovacao)
        {
            List<ReprovacaoTurmaViewModel> listaReprovacaoTurmaViewModel = (from moduloViewModel in reprovacao
                                                                   select new ReprovacaoTurmaViewModel()
                                                                              {
                                                                                  IdReprovacaoTurma = moduloViewModel.IdReprovacaoTurma,
                                                                                  Observacao = moduloViewModel.Observacao,
                                                                                  IdTurma = Convert.ToInt32(moduloViewModel.IdTurma)
                                                                              }).ToList();
            return listaReprovacaoTurmaViewModel;
        }



        #endregion       
    }
}