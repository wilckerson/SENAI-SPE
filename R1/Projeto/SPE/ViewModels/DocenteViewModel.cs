using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class DocenteViewModel
    {
        #region Atributos

        [Key]
        public int IdDocente { get; set; }

        [Display(Name = "Agenda Componente")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<int> IdAgendaComponente { get; set; }

        [Display(Name = "Tipo de Contrato")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<int> IdTipoContrato { get; set; }

        [Display(Name = "Empresa")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<int> IdEmpresa { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string CPF { get; set; }

        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        public Nullable<byte> Sexo { get; set; }
        public string Tel { get; set; }

        [Display(Name = "Nível de Função")]
        [RegularExpression(@"\d", ErrorMessage = "Favor, digite um numero válido.")]
        public Nullable<short> NivelFuncao { get; set; }

        [Display(Name = "Área de Atuação")]
        public Nullable<int> idAreaAtuacao { get; set; }
        public List<AreaAtuacaoViewModel> listaAreaAtuacao { get; set; }

        public string DescricaoContrato { get; set; }

        public EmpresaViewModel EmpresaAtual { get; set; }
        public TipoContratoViewModel TipoContratoAtual { get; set; }
        
        public virtual ICollection<Componente> Componente { get; set; }

        public virtual List<AgendaComponenteViewModel> AgendaComponente { get; set; }
        public virtual List<TipoContratoViewModel> TipoContrato { get; set; }
        public virtual List<EmpresaViewModel> Empresa { get; set; }


        public List<AreaAtuacao> AreaAtuacao { get; set; }
        public List<ComponenteViewModel> ListaComponentes { get; set; }


        #endregion

        #region Métodos

        public static Docente MapToModel(DocenteViewModel docenteViewModel)
        {
            Docente docente = new Docente()
            {
                IdDocente = docenteViewModel.IdDocente,
                IdAgendaComponente = docenteViewModel.IdAgendaComponente,
                IdTipoContrato = docenteViewModel.IdTipoContrato,
                IdEmpresa = docenteViewModel.IdEmpresa,
                CPF = docenteViewModel.CPF,
                Email = docenteViewModel.Email,
                Sexo = docenteViewModel.Sexo,
                Tel = docenteViewModel.Tel,
                NivelFuncao = docenteViewModel.NivelFuncao,
                Nome = docenteViewModel.Nome,
               
            };
            return docente;
        }

        public static DocenteViewModel MapToViewModel(Docente docente)
        {
            DocenteViewModel docenteViewModel = new DocenteViewModel()
            {
                IdDocente = docente.IdDocente,
                IdAgendaComponente = docente.IdAgendaComponente,
                IdTipoContrato = docente.IdTipoContrato,
                IdEmpresa = docente.IdEmpresa,
                CPF = docente.CPF,
                Email = docente.Email,
                Sexo = docente.Sexo,
                Tel = docente.Tel,
                NivelFuncao = docente.NivelFuncao,
                Nome = docente.Nome,
                Componente = docente.Componente,
                AreaAtuacao  = docente.AreaAtuacao.ToList()
            };
            return docenteViewModel;
        }


        public static List<Docente> MapToListModel(List<DocenteViewModel> docentesViewModel)
        {
            List<Docente> listaDocente = (from docente in docentesViewModel
                                          select new Docente()
                                          {
                                              IdDocente = docente.IdDocente,
                                              IdAgendaComponente = docente.IdAgendaComponente,
                                              IdTipoContrato = docente.IdTipoContrato,
                                              IdEmpresa = docente.IdEmpresa,
                                              CPF = docente.CPF,
                                              Email = docente.Email,
                                              Sexo = docente.Sexo,
                                              Tel = docente.Tel,
                                              NivelFuncao = docente.NivelFuncao,
                                              Nome = docente.Nome
                                             
                                          }).ToList();
            return listaDocente;
        }

        public static List<DocenteViewModel> MapToListViewModel(List<Docente> docentes)
        {
            List<DocenteViewModel> listaDocenteViewModel = (from docenteViewModel in docentes
                                                            select new DocenteViewModel()
                                                            {
                                                                IdDocente = docenteViewModel.IdDocente,
                                                                IdAgendaComponente = docenteViewModel.IdAgendaComponente,
                                                                IdTipoContrato = docenteViewModel.IdTipoContrato,
                                                                IdEmpresa = docenteViewModel.IdEmpresa,
                                                                CPF = docenteViewModel.CPF,
                                                                Email = docenteViewModel.Email,
                                                                Sexo = docenteViewModel.Sexo,
                                                                Tel = docenteViewModel.Tel,
                                                                NivelFuncao = docenteViewModel.NivelFuncao,
                                                                Nome = docenteViewModel.Nome,
                                                                AreaAtuacao = docenteViewModel.AreaAtuacao.ToList(),
                                                                EmpresaAtual = EmpresaViewModel.MapToViewModel( docenteViewModel.Empresa),
                                                                TipoContratoAtual = TipoContratoViewModel.MapToViewModel(docenteViewModel.TipoContrato)

                                                            }).ToList();
            return listaDocenteViewModel;
        }


        public static List<DocenteViewModel> MapToListViewModel(List<ufnListarDocentes1_Result> docentes)
        {
            List<DocenteViewModel> listaDocenteViewModel = (from docenteViewModel in docentes
                                                            select new DocenteViewModel()
                                                            {
                                                                IdDocente = docenteViewModel.IdDocente.GetValueOrDefault(0),
                                                                IdTipoContrato = docenteViewModel.IdTipoContrato,
                                                                CPF = docenteViewModel.CPF,
                                                                Email = docenteViewModel.Email,
                                                                Tel = docenteViewModel.Telefone,
                                                                Nome = docenteViewModel.Nome,
                                                                DescricaoContrato = docenteViewModel.DescricaoContrato
                                                            }).ToList();
            return listaDocenteViewModel;
        }



        #endregion

    }
}