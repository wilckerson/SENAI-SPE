using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class EmpresaViewModel
    {
        #region Atributos

        public int IdEmpresa { get; set; }
        public Nullable<int> IdAgendaComponente { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Nome Fantasia: ")]
        public string NomeFantasia { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [RegularExpression(@"^[0-9]{2}\.[0-9]{3}\.[0-9]{3}\/[0-9]{4}\-[0-9]{2}", ErrorMessage = "CNPJ Inválido!")]
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessage = "Email Inválido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Telefone")]
        public string Tel { get; set; }
        public string Contato { get; set; }

        #endregion

        #region Métodos

        public static Empresa MapToModel(EmpresaViewModel empresaViewModel)
        {
            Empresa empresa = new Empresa()
            {
                IdEmpresa = empresaViewModel.IdEmpresa,
                IdAgendaComponente = empresaViewModel.IdAgendaComponente,
                NomeFantasia = empresaViewModel.NomeFantasia,
                CNPJ = empresaViewModel.CNPJ,
                Email = empresaViewModel.Email,
                Tel = empresaViewModel.Tel,
                Contato = empresaViewModel.Contato
            };
            return empresa;
        }

        public static EmpresaViewModel MapToViewModel(Empresa empresa)
        {
            EmpresaViewModel empresaViewModel = new EmpresaViewModel()
            {               
                NomeFantasia = "N/A",               
            };

            if (empresa != null)
            {
                empresaViewModel = new EmpresaViewModel()
                {
                    IdEmpresa = empresa.IdEmpresa,
                    IdAgendaComponente = empresa.IdAgendaComponente,
                    NomeFantasia = empresa.NomeFantasia,
                    CNPJ = empresa.CNPJ,
                    Email = empresa.Email,
                    Tel = empresa.Tel,
                    Contato = empresa.Contato
                };
            }
            return empresaViewModel;
        }


        public static List<Empresa> MapToListModel(List<EmpresaViewModel> empresasViewModel)
        {
            List<Empresa> listaEmpresa = (from empresa in empresasViewModel
                                          select new Empresa()
                                          {
                                              IdEmpresa = empresa.IdEmpresa,
                                              IdAgendaComponente = empresa.IdAgendaComponente,
                                              NomeFantasia = empresa.NomeFantasia,
                                              CNPJ = empresa.CNPJ,
                                              Email = empresa.Email,
                                              Tel = empresa.Tel,
                                              Contato = empresa.Contato
                                          }).ToList();
            return listaEmpresa;
        }

        public static List<EmpresaViewModel> MapToListViewModel(List<Empresa> empresas)
        {
            List<EmpresaViewModel> listaEmpresaViewModel = (from empresaViewModel in empresas
                                                            select new EmpresaViewModel()
                                                            {
                                                                IdEmpresa = empresaViewModel.IdEmpresa,
                                                                IdAgendaComponente = empresaViewModel.IdAgendaComponente,
                                                                NomeFantasia = empresaViewModel.NomeFantasia,
                                                                CNPJ = empresaViewModel.CNPJ,
                                                                Email = empresaViewModel.Email,
                                                                Tel = empresaViewModel.Tel,
                                                                Contato = empresaViewModel.Contato
                                                            }).ToList();
            return listaEmpresaViewModel;
        }



        #endregion
    }
}