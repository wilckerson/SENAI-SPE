using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class CBOViewModel
    {
        public int IdCBO { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Descrricao { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [RegularExpression("^.{1,8}$", ErrorMessage = "O número máximo de caracteres é 8.")]
        public string Codigo { get; set; }

        public Nullable<int> Tipo { get; set; }


        public virtual ICollection<Matriz> Matriz { get; set; }

        #region Métodos

        public static CBO MapToModel(CBOViewModel cboViewModel)
        {
            CBO cbo = new CBO()
            {
                IdCBO = cboViewModel.IdCBO,
                Descrricao = cboViewModel.Descrricao,
                Codigo = cboViewModel.Codigo,
                Tipo = cboViewModel.Tipo
            };
            return cbo;
        }

        public static CBOViewModel MapToViewModel(CBO cbo)
        {
            CBOViewModel cboViewModel = new CBOViewModel()
            {
                IdCBO = cbo.IdCBO,
                Descrricao = cbo.Descrricao,
                Codigo = cbo.Codigo,
                Tipo = cbo.Tipo
            };
            return cboViewModel;
        }


        public static List<CBO> MapToListModel(List<CBOViewModel> cboViewModel)
        {
            List<CBO> listaCBO = (from cbo in cboViewModel
                                  select new CBO()
                                  {
                                      IdCBO = cbo.IdCBO,
                                      Descrricao = cbo.Descrricao,
                                      Codigo = cbo.Codigo,
                                      Tipo = cbo.Tipo
                                  }).ToList();
            return listaCBO;
        }

        public static List<CBOViewModel> MapToListViewModel(List<CBO> cbo)
        {
            List<CBOViewModel> listaCBOViewModel = (from cboViewModel in cbo
                                                    select new CBOViewModel()
                                                    {
                                                        IdCBO = cboViewModel.IdCBO,
                                                        Descrricao = cboViewModel.Descrricao,
                                                        Codigo = cboViewModel.Codigo,
                                                        Tipo = cboViewModel.Tipo
                                                    }).ToList();
            return listaCBOViewModel;
        }



        #endregion
    }
}