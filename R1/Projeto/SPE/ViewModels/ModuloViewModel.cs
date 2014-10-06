using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class ModuloViewModel
    {

        #region Atributos

        public int IdModulo { get; set; }
        public int IdMatriz { get; set; }
        public string Nome { get; set; }


        #endregion

        #region Métodos

        public static Modulo MapToModel(ModuloViewModel moduloViewModel)
        {
            Modulo modulo = new Modulo()
            {
                IdModulo = moduloViewModel.IdModulo,
                Nome = moduloViewModel.Nome,
                IdMatriz = moduloViewModel.IdMatriz
            };
            return modulo;
        }

        public static ModuloViewModel MapToViewModel(Modulo modulo)
        {
            ModuloViewModel moduloViewModel = new ModuloViewModel()
            {
                IdModulo = modulo.IdModulo,
                Nome = modulo.Nome,
                IdMatriz = modulo.IdMatriz
            };
            return moduloViewModel;
        }


        public static List<Modulo> MapToListModel(List<ModuloViewModel> modulosViewModel)
        {
            List<Modulo> listaModulo = (from modulo in modulosViewModel
                                                            select new Modulo()
                                                            {
                                                                IdModulo = modulo.IdModulo,
                                                                Nome = modulo.Nome,
                                                                IdMatriz = modulo.IdMatriz
                                                            }).ToList();
            return listaModulo;
        }

        public static List<ModuloViewModel> MapToListViewModel(List<Modulo> modulos)
        {
            List<ModuloViewModel> listaModuloViewModel = (from moduloViewModel in modulos
                                                                              select new ModuloViewModel()
                                                                              {
                                                                                  IdModulo = moduloViewModel.IdModulo,
                                                                                  Nome = moduloViewModel.Nome,
                                                                                  IdMatriz = moduloViewModel.IdMatriz
                                                                              }).ToList();
            return listaModuloViewModel;
        }



        #endregion

        internal static List<Modulo> MapToModel(List<ModuloComponenteViewModel> modulosViewModel)
        {
            List<Modulo> listaModulo = (from modulo in modulosViewModel
                                        select new Modulo()
                                        {                                            
                                            Nome = modulo.Nome,
                                            IdMatriz = modulo.IdMatriz,
                                            Componente = ComponenteViewModel.MapToListModel(modulo.Componentes)
                                        }).ToList();
            return listaModulo;
        }
    }
}