using Senai.SPE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPE.ViewModel
{
    public class RecursoAmbienteViewModel
    {

        #region Átributos

        [Key]
        public int IdRecursoAmbiente { get; set; }
        public Nullable<int> IdRecurso { get; set; }
        public Nullable<int> IdAmbiente { get; set; }
        public short Status { get; set; }

        public virtual Ambiente Ambiente { get; set; }
        public virtual Recurso Recurso { get; set; }

        #endregion

        #region Métodos

        public static RecursoAmbiente MapToModel(RecursoAmbienteViewModel recursoAmbienteViewModel)
        {
            RecursoAmbiente recursoAmbiente = new RecursoAmbiente()
            {
                IdRecursoAmbiente = recursoAmbienteViewModel.IdRecursoAmbiente,
                IdRecurso = recursoAmbienteViewModel.IdRecurso,
                IdAmbiente = recursoAmbienteViewModel.IdAmbiente,
                Status = recursoAmbienteViewModel.Status,
                Ambiente = recursoAmbienteViewModel.Ambiente,
                Recurso = recursoAmbienteViewModel.Recurso
            };
            return recursoAmbiente;
        }

        public static RecursoAmbienteViewModel MapToViewModel(RecursoAmbiente recursoAmbiente)
        {
            RecursoAmbienteViewModel recursoAmbienteViewModel = new RecursoAmbienteViewModel()
            {
                IdRecursoAmbiente = recursoAmbiente.IdRecursoAmbiente,
                IdRecurso = recursoAmbiente.IdRecurso,
                IdAmbiente = recursoAmbiente.IdAmbiente,
                Status = recursoAmbiente.Status
            };
            return recursoAmbienteViewModel;
        }

        public static List<RecursoAmbiente> MapToListModel(List<RecursoAmbienteViewModel> recursoAmbienteViewModel)
        {
            List<RecursoAmbiente> listaRecursoAmbiente = (from recursoAmbiente in recursoAmbienteViewModel
                                                  select new RecursoAmbiente()
                                                  {
                                                      IdRecursoAmbiente = recursoAmbiente.IdRecursoAmbiente,
                                                      IdRecurso = recursoAmbiente.IdRecurso,
                                                      IdAmbiente = recursoAmbiente.IdAmbiente,
                                                      Status = recursoAmbiente.Status
                                                  }).ToList();
            return listaRecursoAmbiente;
        }

        public static List<RecursoAmbienteViewModel> MapToListViewModel(List<RecursoAmbiente> recursoAmbiente)
        {
            List<RecursoAmbienteViewModel> listaRecursoAmbienteViewModel = (from recursoAmbienteViewModel in recursoAmbiente
                                                                    select new RecursoAmbienteViewModel()
                                                                    {
                                                                        IdRecursoAmbiente = recursoAmbienteViewModel.IdRecursoAmbiente,
                                                                        IdRecurso = recursoAmbienteViewModel.IdRecurso,
                                                                        IdAmbiente = recursoAmbienteViewModel.IdAmbiente,
                                                                        Status = recursoAmbienteViewModel.Status,
                                                                        Ambiente = recursoAmbienteViewModel.Ambiente,
                                                                        Recurso = recursoAmbienteViewModel.Recurso
                                                                    }).ToList();
            return listaRecursoAmbienteViewModel;
        }

        #endregion
    }
}