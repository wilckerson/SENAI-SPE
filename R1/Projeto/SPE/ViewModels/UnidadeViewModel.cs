using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class UnidadeViewModel
    {
        public int IdUnidade { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Descr { get; set; }
        [Display(Name = "Habilitada")]
        public Nullable<short> Status { get; set; }
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Range(0, 2147483640, ErrorMessage = "Atingido o máximo para o campo")] //2.147.483.647 Valor máximo para tipo INT no SQL Server
        public Nullable<int> Codigo { get; set; }

        public virtual ICollection<AgendaComponente> AgendaComponente { get; set; }
        public virtual ICollection<CR> CR { get; set; }
        public virtual ICollection<Modalidade> Modalidade { get; set; }
        public virtual ICollection<Turma> Turma { get; set; }

        public static Unidade MapToModel(UnidadeViewModel unidadeViewModel)
        {
            Unidade unidade = new Unidade()
            {
                IdUnidade = unidadeViewModel.IdUnidade,
                Descr = unidadeViewModel.Descr,
                Status = unidadeViewModel.Status,
                Codigo = unidadeViewModel.Codigo,

                AgendaComponente = unidadeViewModel.AgendaComponente,
                CR = unidadeViewModel.CR,
                Modalidade = unidadeViewModel.Modalidade,
                Turma = unidadeViewModel.Turma
            };
            return unidade;
        }

        public static UnidadeViewModel MapToViewModel(Unidade unidade)
        {
            UnidadeViewModel unidadeViewModel = new UnidadeViewModel()
            {
                IdUnidade = unidade.IdUnidade,
                Descr = unidade.Descr,
                Status = unidade.Status,
                Codigo = unidade.Codigo,

                AgendaComponente = unidade.AgendaComponente,
                CR = unidade.CR,
                Modalidade = unidade.Modalidade,
                Turma = unidade.Turma
            };
            return unidadeViewModel;
        }


        public static List<Unidade> MapToListModel(List<UnidadeViewModel> unidadeViewModel)
        {
            List<Unidade> listaUnidade = (from unidade in unidadeViewModel
                                          select new Unidade()
                                          {
                                              IdUnidade = unidade.IdUnidade,
                                              Descr = unidade.Descr,
                                              Status = unidade.Status,
                                              Codigo = unidade.Codigo,

                                              AgendaComponente = unidade.AgendaComponente,
                                              CR = unidade.CR,
                                              Modalidade = unidade.Modalidade,
                                              Turma = unidade.Turma
                                          }).ToList();
            return listaUnidade;
        }

        public static List<UnidadeViewModel> MapToListViewModel(List<Unidade> unidade)
        {
            List<UnidadeViewModel> listaUnidadeViewModel = (from unidadeViewModel in unidade
                                                            select new UnidadeViewModel()
                                                            {
                                                                IdUnidade = unidadeViewModel.IdUnidade,
                                                                Descr = unidadeViewModel.Descr,
                                                                Status = unidadeViewModel.Status,
                                                                Codigo = unidadeViewModel.Codigo,

                                                                AgendaComponente = unidadeViewModel.AgendaComponente,
                                                                CR = unidadeViewModel.CR,
                                                                Modalidade = unidadeViewModel.Modalidade,
                                                                Turma = unidadeViewModel.Turma
                                                            }).ToList();
            return listaUnidadeViewModel;
        }


    }
}