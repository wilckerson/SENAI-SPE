using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senai.SPE.Domain;
using Common;
using Senai.SPE.Domain.Enum;
using Common.Extensions.StringEx;
using SPE.ViewModel;

namespace SPE.Controllers
{
    public class DocenteController : BaseController
    {
        //
        // GET: /DocenteViewModels/
        public ViewResult Index()
        {
            List<DocenteViewModel> docentesViewModel = null;
            try
            {
                //var listaDocente = this.speDominioService.GetDocenteAll();
                var listaDocente = BL.Docente.Get(null,null,"TipoContrato,Empresa");
                docentesViewModel = DocenteViewModel.MapToListViewModel(listaDocente.ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página de Index da Docente", ex);
            }
            return View(docentesViewModel);
        }

        //
        // GET: /DocenteViewModels/Details/5
        public ViewResult DetalharDocente(int id)
        {
            DocenteViewModel docenteViewModel = null;
            try
            {
                //var docente = this.speDominioService.GetDocenteById(id);
                var docente = BL.Docente.GetById(id);
                docenteViewModel = DocenteViewModel.MapToViewModel(docente);
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página DetalharDocente", ex);
                return View();
            }
            return View(docenteViewModel);
        }

        //
        // GET: /DocenteViewModels/Create
        public ActionResult CadastrarDocente()
        {
            try
            {
                //var lista = this.speDominioService.GetAreaAtuacaoAll();
               
                

                //var lista = AreaAtuacaoViewModel.MapToListViewModel((from u in this.Context.AreaAtuacao
                //                                                     select u).ToList());
                DocenteViewModel docenteViewModel = new DocenteViewModel();
                docenteViewModel.listaAreaAtuacao = AreaAtuacaoViewModel.MapToListViewModel(BL.AreaAtuacao.Get().ToList());
                docenteViewModel.AgendaComponente = new List<AgendaComponenteViewModel>();
                docenteViewModel.TipoContrato = TipoContratoViewModel.MapToListViewModel( BL.TipoContrato.Get().ToList() );
                docenteViewModel.Empresa = EmpresaViewModel.MapToListViewModel(BL.Empresa.Get().ToList());
                
                docenteViewModel.ListaComponentes = new List<ComponenteViewModel>();

                //var listaCompoente = ComponenteViewModel.MapToListViewModel(this.speDominioService.BuscarComponentesPor("", 1, 1000));
                var listaCompoente = ComponenteViewModel.MapToListViewModel(BL.Componente.BuscarComponentesPor("", 1, 1000));

                foreach (ComponenteViewModel item in listaCompoente)
                {
                    if (!docenteViewModel.ListaComponentes.Any(a => StringExtension.GenerateSlug(a.Nome).Contains(StringExtension.GenerateSlug(item.Nome))) && docenteViewModel.ListaComponentes.Count < 10)
                    {
                        docenteViewModel.ListaComponentes.Add(item);
                    }
                }

                return View(docenteViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página CadastrarDocente", ex);
                return View();
            }
        }

        //
        // POST: /DocenteViewModels/Create
        [HttpPost]
        public ActionResult CadastrarDocente(DocenteViewModel docenteviewmodel, FormCollection collection)
        {
            Docente model = null;
            try
            {
                model = DocenteViewModel.MapToModel(docenteviewmodel);
                var arrayId = Array.ConvertAll(collection["idComponentes"].Split(';'), t => int.Parse(t));
                var arrayIdAreasAtuacao = Array.ConvertAll(collection["idsAreaAtuacao"].Split(';'), t => int.Parse(t));
                //var componentes = this.speDominioService.GetFilteredComponente(a => arrayId.Contains(a.IdComponente), null, "").ToList();
                var componentes = BL.Componente.Get(a => arrayId.Contains(a.IdComponente), null, "").ToList();
                var areas = BL.AreaAtuacao.Get(a => arrayIdAreasAtuacao.Contains(a.IdAreaAtuacao), null, "").ToList();
                //model.Componente = componentes;
                //this.speDominioService.InserirDocente(model, componentes);
                var docente = BL.Docente.InserirDocente(model, componentes);
                BL.Docente.AtualizarAreAtuacaoDocente(docente, areas);
                
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Docente realizado com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Docente.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /DocenteViewModels/Edit/5
        public ActionResult EditarDocente(int id)
        {
            DocenteViewModel docenteViewModel = null;

            try
            {
                //var lista = this.speDominioService.GetAreaAtuacaoAll();
                

                //var docente = this.speDominioService.GetFilteredDocente(a => a.IdDocente == id, null, "Componente").SingleOrDefault();
                var docente = BL.Docente.Get(a => a.IdDocente == id, null, "Componente,AreaAtuacao").SingleOrDefault();
                docenteViewModel = DocenteViewModel.MapToViewModel(docente);
                docenteViewModel.listaAreaAtuacao = AreaAtuacaoViewModel.MapToListViewModel(BL.AreaAtuacao.Get().ToList());
                //docenteViewModel.listaAreaAtuacaoDocente = 
                docenteViewModel.AgendaComponente = new List<AgendaComponenteViewModel>();
                docenteViewModel.TipoContrato = TipoContratoViewModel.MapToListViewModel(BL.TipoContrato.Get().ToList());
                docenteViewModel.Empresa = EmpresaViewModel.MapToListViewModel(BL.Empresa.Get().ToList());
                

                docenteViewModel.ListaComponentes = new List<ComponenteViewModel>();

                //var listaCompoente = ComponenteViewModel.MapToListViewModel(this.speDominioService.BuscarComponentesPor("", 1, 1000));
                var listaCompoente = ComponenteViewModel.MapToListViewModel(BL.Componente.BuscarComponentesPor("", 1, 1000));

                foreach (ComponenteViewModel item in listaCompoente)
                {
                    if (!docenteViewModel.ListaComponentes.Any(a => StringExtension.GenerateSlug(a.Nome).Contains(StringExtension.GenerateSlug(item.Nome))) && docenteViewModel.ListaComponentes.Count < 10)
                    {
                        docenteViewModel.ListaComponentes.Add(item);
                    }
                }

                return View(docenteViewModel);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página EditarDocente", ex);
            }
            return View(docenteViewModel);
        }

        //
        // POST: /DocenteViewModels/Edit/5
        [HttpPost]
        public ActionResult EditarDocente(DocenteViewModel docenteviewmodel, FormCollection collection)
        {
            try
            {
                var model = DocenteViewModel.MapToModel(docenteviewmodel);

                var arrayIdComponentes = Array.ConvertAll(collection["idComponentes"].Split(';'), t => int.Parse(t));
                var arrayIdAreasAtuacao = Array.ConvertAll(collection["idsAreaAtuacao"].Split(';'), t => int.Parse(t));
                //var componentes = this.speDominioService.GetFilteredComponente(a => arrayIdComponentes.Contains(a.IdComponente), null, "").ToList();
                var componentes = BL.Componente.Get(a => arrayIdComponentes.Contains(a.IdComponente), null, "").ToList();
                var areas = BL.AreaAtuacao.Get(a => arrayIdAreasAtuacao.Contains(a.IdAreaAtuacao), null, "").ToList();


                var lista = BL.AreaAtuacao.Get();
                List<AreaAtuacaoViewModel> listaAreaAtuacao = AreaAtuacaoViewModel.MapToListViewModel(lista.ToList());

                BL.Docente.AtualizarAgendaDocente(model, componentes);
                BL.Docente.AtualizarAreAtuacaoDocente(model, areas);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Docente realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Docente.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /DocenteViewModels/Delete/5
        public ActionResult ExcluirDocente(int id)
        {
            try
            {
                //this.speDominioService.ApagarDocente(id);
                BL.Docente.ApagarDocente(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Docente excluído com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Docente.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");

        }

        public ActionResult ModalExcluir()
        {
            return PartialView("_ModalConfirmacaoExcluir");
        }

        public ActionResult BuscarComponentes(string filtro)
        {
            List<Componente> viewModel = new List<Componente>();
            //var lista = ComponenteViewModel.MapToListViewModel(this.speDominioService.BuscarComponentesPor(filtro, 1, 1000));

            var procedureResult = BL.Componente.BuscarComponentesPor(filtro, 1, 1000);

            var componentes = procedureResult.Select(p => new Componente()
            {
                IdComponente = p.IdComponente.GetValueOrDefault(),
                Nome = p.NomeComponente,
                CH = (short)p.CH.GetValueOrDefault()
            }).ToList();

            //var lista = ComponenteViewModel.MapToListViewModel(componentes);

            foreach (var item in componentes)
            {
                if (!viewModel.Any(a => StringExtension.GenerateSlug(a.Nome).Contains(StringExtension.GenerateSlug(item.Nome))) && viewModel.Count < 10)
                {
                    
                    viewModel.Add(item);
                }
            }

            return View("~/Views/Docente/_BuscarComponentes.cshtml", viewModel);
        }


    }
}