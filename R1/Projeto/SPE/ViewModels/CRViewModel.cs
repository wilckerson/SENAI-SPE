using Common.DataAnnotations;
using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class CRViewModel
    {

        #region Atributos

        [Key]
        public int? IdCR { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Modalidade")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<int> IdModalidade { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [RegularExpression("^.{1,12}$", ErrorMessage = "O número máximo de caracteres é 12.")]
        public string Codigo { get; set; }


        public virtual Modalidade Modalidade { get; set; }
        public virtual ICollection<Turma> Turma { get; set; }
        public virtual ICollection<Unidade> Unidade { get; set; }

        public string NomeModalidade { get; set; }

        public List<ModalidadeViewModel> ListaModalidades { get; set; }

        #endregion

        #region Métodos

        public static CR MapToModel(CRViewModel crViewModel)
        {
            CR cr = new CR()
            {
                IdCR = crViewModel.IdCR.GetValueOrDefault(0),
                Nome = crViewModel.Nome,
                IdModalidade = crViewModel.IdModalidade,
                Codigo = crViewModel.Codigo,
                Modalidade = crViewModel.Modalidade,
                Turma = crViewModel.Turma,
                Unidade = crViewModel.Unidade
                //Modalidade = crViewModel.Modalidade,
                //Unidade = crViewModel.Unidade
            };

            return cr;
        }

        public static CRViewModel MapToViewModel(CR cr)
        {
            CRViewModel crViewModel = new CRViewModel()
            {
                IdCR = cr.IdCR,
                Nome = cr.Nome,
                IdModalidade = cr.IdModalidade,
                Codigo = cr.Codigo,
                Modalidade = cr.Modalidade,
                Turma = cr.Turma.ToList(),
                Unidade = cr.Unidade,
                NomeModalidade = cr.Modalidade != null? cr.Modalidade.Nome : "Sem Modalidade"
                
            };
            return crViewModel;
        }


        public static List<CR> MapToListModel(List<CRViewModel> crsViewModel)
        {
            List<CR> listaCR = crsViewModel.Select(item => CRViewModel.MapToModel(item)).ToList();
            return listaCR;
        }

        public static List<CRViewModel> MapToListViewModel(List<CR> crs)
        {
            List<CRViewModel> listaCRViewModel = crs.Select(item => CRViewModel.MapToViewModel(item)).ToList();
                //(from crViewModel in crs select CRViewModel.MapToViewModel(crViewModel)).ToList();
            return listaCRViewModel;
        }

        public static List<CRViewModel> MapToListViewModel(List<ufnListarCR1_Result> crs)
        {
            List<CRViewModel> listaCRViewModel = (from crViewModel in crs
                                                  select new CRViewModel()
                                                  {
                                                      IdCR = crViewModel.IdCr,
                                                      Nome = crViewModel.Nome,
                                                      Codigo = crViewModel.Codigo,
                                                      NomeModalidade = crViewModel.Modalidade,


                                                  }).ToList();
            return listaCRViewModel;
        }

        #endregion



    }
}