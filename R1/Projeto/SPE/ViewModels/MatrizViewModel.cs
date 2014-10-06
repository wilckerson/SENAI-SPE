using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class MatrizViewModel
    {
        #region Atributos

        public int IdMatriz { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }

        public List<ModalidadeViewModel> Modalidades { get; set; }
        [Display(Name = "Modalidade")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<int> IdModalidade { get; set; }

        [Display(Name = "Área de Atuação")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public Nullable<int> IdAreaAtuacao { get; set; }

        [Display(Name = "CBO")]
        public Nullable<int> IdCBO { get; set; }

        [Display(Name = "Responsável")]
        public Nullable<int> IdResponsavel { get; set; }

        [Display(Name = "Custo")]
        public string Preco { get; set; }

        [Display(Name = "Carga Horária")]
        public Nullable<int> CH { get; set; }

        [Display(Name = "Status")]
        public Nullable<short> Status { get; set; }

        [Display(Name = "Aprovação")]
        public Nullable<short> Aprovado { get; set; }
        
        [Display(Name = "Data Expiração")]
        public Nullable <System.DateTime> DataFim { get; set; }
        public string DataFimView { get; set; }

        public virtual AreaAtuacao AreaAtuacao { get; set; }
        public virtual CBO CBO { get; set; }
        public virtual ICollection<Turma> Turma { get; set; }
        public virtual Modalidade Modalidade { get; set; }
        public virtual ICollection<Componente> Componente { get; set; }
        public virtual ICollection<Material> Material { get; set; }
        public virtual ICollection<Servico> Servico { get; set; }
        public virtual ReprovacaoMatrizViewModel ReprovacaoMatriz { get; set; }

        //Listas Utilizadas na view para popular dropdown
        public List<ModalidadeViewModel> ListaModalidades { get; set; }
        public List<AreaAtuacaoViewModel> ListaAreaAtuacao { get; set; }
        public List<CBOViewModel> ListaCBO { get; set; }
        public List<ReprovacaoMatriz> ListaReprovacaoMatriz { get; set; }
        public virtual ICollection<Modulo> Modulo { get; set; }


        //Lista de Componentes referentes ao vincular componente

        public MatrizListarComponentesViewModel ListaComponente { get; set; }



        #region Atributtes Table Function

        public string ModalidadeNome { get; set; }
        public string AreaAtuacaoNome { get; set; }

        #endregion


        public string Json { get; set; }
        #endregion

        #region Métodos

        public static Matriz MapToModel(MatrizViewModel matrizViewModel)
        {
            Matriz matriz = new Matriz()
            {
                IdMatriz = matrizViewModel.IdMatriz,
                IdModalidade = matrizViewModel.IdModalidade,
                IdAreaAtuacao = matrizViewModel.IdAreaAtuacao,
                Nome = matrizViewModel.Nome,
                IdCBO = matrizViewModel.IdCBO,
                Preco = matrizViewModel.Preco == null ? 0 : decimal.Parse(matrizViewModel.Preco.Replace(".", "")),
                CH = matrizViewModel.CH,
                Status = matrizViewModel.Status,
                Aprovado = matrizViewModel.Aprovado,
                IdResponsavel = matrizViewModel.IdResponsavel,
                ReprovacaoMatriz = matrizViewModel.ListaReprovacaoMatriz,
                //DataInicial = DateTime.Parse(matrizViewModel.DataInicialView),

            };
            if (matrizViewModel.DataFimView == null)
            {

            }
            else { matriz.DataFim = DateTime.Parse(matrizViewModel.DataFimView); }
            return matriz;
        }

        public static MatrizViewModel MapToViewModel(Matriz matriz)
        {
            MatrizViewModel matrizViewModel = new MatrizViewModel()
            {
                
                IdMatriz = matriz.IdMatriz,
                IdModalidade = matriz.IdModalidade,
                IdAreaAtuacao = matriz.IdAreaAtuacao,
                IdCBO = matriz.IdCBO,
                Preco = matriz.Preco.GetValueOrDefault(0).ToString("#0.00"),
                Nome = matriz.Nome,
                AreaAtuacao = matriz.AreaAtuacao,
                CBO = matriz.CBO,
                Turma = matriz.Turma,
                Material = matriz.Material,
                Modalidade = matriz.Modalidade,
                Servico = matriz.Servico,
                Modulo = matriz.Modulo,
                CH = matriz.CH,
                Status = matriz.Status == null ? short.Parse("1") : matriz.Status,
                Aprovado = matriz.Aprovado,
                IdResponsavel = matriz.IdResponsavel,
                ListaReprovacaoMatriz = matriz.ReprovacaoMatriz.ToList(),
                DataFimView = matriz.DataFim ==null ? "" : matriz.DataFim.Value.ToShortDateString()

            };
            return matrizViewModel;
        }

        public static List<Matriz> MapToListModel(List<MatrizViewModel> matrizViewModel)
        {
            List<Matriz> listaMatriz = (from matriz in matrizViewModel
                                        select new Matriz()
                                        {
                                            IdMatriz = matriz.IdMatriz,
                                            IdModalidade = matriz.IdModalidade,
                                            IdAreaAtuacao = matriz.IdAreaAtuacao,
                                            IdCBO = matriz.IdCBO,
                                            Preco = matriz.Preco == null ? 0 : decimal.Parse(matriz.Preco.Replace(".", "")),
                                            Nome = matriz.Nome,
                                            CH = matriz.CH,
                                            Status = matriz.Status,
                                            Aprovado = matriz.Aprovado,
                                            IdResponsavel = matriz.IdResponsavel,
                                            DataFim = matriz.DataFim == null ? DateTime.Parse(null) : matriz.DataFim.Value
                                        }).ToList();
            return listaMatriz;
        }

        public static List<MatrizViewModel> MapToListViewModel(List<Matriz> matriz)
        {
            List<MatrizViewModel> listaMatrizViewModel = (from matrizViewModel in matriz
                                                          select new MatrizViewModel()
                                                          {
                                                              IdMatriz = matrizViewModel.IdMatriz,
                                                              IdModalidade = matrizViewModel.IdModalidade,
                                                              IdAreaAtuacao = matrizViewModel.IdAreaAtuacao,
                                                              IdCBO = matrizViewModel.IdCBO,
                                                              Preco = matrizViewModel.Preco.GetValueOrDefault(0).ToString("#0.00"),
                                                              AreaAtuacao = matrizViewModel.AreaAtuacao,
                                                              CBO = matrizViewModel.CBO,
                                                              Turma = matrizViewModel.Turma,
                                                              Material = matrizViewModel.Material,
                                                              Modalidade = matrizViewModel.Modalidade,
                                                              Servico = matrizViewModel.Servico,
                                                              Nome = matrizViewModel.Nome,
                                                              Modulo = matrizViewModel.Modulo,
                                                              CH = matrizViewModel.CH,
                                                              Status = matrizViewModel.Status == null ? short.Parse("1") : matrizViewModel.Status.Value,
                                                              Aprovado = matrizViewModel.Aprovado,
                                                              IdResponsavel = matrizViewModel.IdResponsavel,
                                                              DataFimView = matrizViewModel.DataFim == null ? "" : matrizViewModel.DataFim.Value.ToShortTimeString()
                                                          }).ToList();
            return listaMatrizViewModel;
        }

        #endregion

        public static List<MatrizViewModel> MapToListViewModel(IEnumerable<ufnListarMatrizes1_Result> listaMatriz)
        {
            List<MatrizViewModel> listaMatrizViewModel = (from matrizViewModel in listaMatriz
                                                          select new MatrizViewModel()
                                                          {
                                                              IdMatriz = matrizViewModel.MATRIZID.GetValueOrDefault(0),
                                                              Nome = matrizViewModel.NOMEMATRIZ,
                                                              Preco = matrizViewModel.PRECO.GetValueOrDefault(0).ToString("#0.00"),
                                                              IdModalidade = matrizViewModel.MODALIDADEID,
                                                              ModalidadeNome = matrizViewModel.NOMEMODALIDADE,
                                                              IdAreaAtuacao = matrizViewModel.AREAATUACAOID,
                                                              AreaAtuacaoNome = matrizViewModel.AREAATUACAO,
                                                              IdCBO = matrizViewModel.CBOID
                                                          }).ToList();
            return listaMatrizViewModel;
        }
    }
}