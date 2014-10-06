using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class ReprovacaoMatrizViewModel
    {

        #region Atributos

        public int IdReprovacaoMatriz { get; set; }
        public Nullable<int> IdMatriz { get; set; }
        public string observacao { get; set; }
        
        public Nullable<System.DateTime> Data { get; set; }
        public string DataView { get; set; }



        #endregion

        #region Métodos

        public static ReprovacaoMatrizViewModel MapToModel(ReprovacaoMatriz reprovacaoMatriz)
        {
            ReprovacaoMatrizViewModel reprovacao = new ReprovacaoMatrizViewModel()
            {
                IdReprovacaoMatriz = reprovacaoMatriz.IdReprovacaoMatriz,
                IdMatriz = reprovacaoMatriz.IdMatriz,
                observacao = reprovacaoMatriz.Observacao,
                Data = reprovacaoMatriz.Data
            };
            return reprovacao;
        }

        public static ReprovacaoMatrizViewModel MapToViewModel(ReprovacaoMatrizViewModel modulo)
        {
            ReprovacaoMatrizViewModel reprovacaoViewModel = new ReprovacaoMatrizViewModel()
            {
                IdReprovacaoMatriz = modulo.IdReprovacaoMatriz,
                IdMatriz = modulo.IdMatriz,
                observacao = modulo.observacao,
                DataView = modulo.Data == null ? "" : modulo.Data.Value.ToString()
            };
            return reprovacaoViewModel;
        }


        public static List<ReprovacaoMatrizViewModel> MapToListModel(List<ReprovacaoMatrizViewModel> reprovacaoMatrizViewModel)
        {
            List<ReprovacaoMatrizViewModel> listaReprovacaoMatriz = (from modulo in reprovacaoMatrizViewModel
                                                                   select new ReprovacaoMatrizViewModel()
                                                            {
                                                                IdReprovacaoMatriz = modulo.IdReprovacaoMatriz,
                                                                observacao = modulo.observacao,
                                                                IdMatriz = modulo.IdMatriz
                                                            }).ToList();
            return listaReprovacaoMatriz;
        }

        public static List<ReprovacaoMatrizViewModel> MapToListViewModel(List<ReprovacaoMatriz> reprovacao)
        {
            List<ReprovacaoMatrizViewModel> listaReprovacaoMatrizViewModel = (from moduloViewModel in reprovacao
                                                                   select new ReprovacaoMatrizViewModel()
                                                                              {
                                                                                  IdReprovacaoMatriz = moduloViewModel.IdReprovacaoMatriz,
                                                                                  observacao = moduloViewModel.Observacao,
                                                                                  IdMatriz = Convert.ToInt32(moduloViewModel.IdMatriz),
                                                                                  DataView = moduloViewModel.Data == null ? "" : moduloViewModel.Data.Value.ToString()                                                                              }).ToList();
            return listaReprovacaoMatrizViewModel;
        }



        #endregion       
    }
}
