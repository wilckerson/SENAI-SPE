using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Common;

using Senai.SPE.Domain;
using SPE.ViewModel;

namespace SPE.Controllers
{
    public class ComponenteController : BaseController
    {
        //
        // GET: /Componente/
        public ActionResult Index(FiltrosComponente filtro = null)
        {
            filtro = filtro == null ? new FiltrosComponente() : filtro;
            try
            {
                filtro.Componentes = ComponenteViewModel.MapToListViewModel(BL.Componente.Get(null, null, "TipoAmbiente").ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página de componentes", ex);

                return View();
            }

            return View(filtro);
        }

        public ViewResult DetalharComponente(int id)
        {
            ComponenteViewModel componenteViewModel = null;
            try
            {
                var componente = BL.Componente.GetById(id);
                componenteViewModel = ComponenteViewModel.MapToViewModel(componente);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                TempData["Error"] = true;
                Logging.getInstance().Error("Erro ao carregar página a página DetalharComponente", ex);

                return View();
            }

            return View(componenteViewModel);
        }

        public PartialViewResult GetListaComponenteAreaAtuacao(string IdsAreaAtuacao, string IdsModulosFiltrar = null)
        {
            
            List<Componente> componentes = BL.Componente.BuscarComponentesPorAreaAtuacao(IdsAreaAtuacao).ToList();

            if (!String.IsNullOrWhiteSpace(IdsModulosFiltrar))
            {
                List<string> arrayIdsModulosFiltrar = IdsModulosFiltrar.Split(';').Distinct().ToList();
                componentes.RemoveAll(a => arrayIdsModulosFiltrar.Contains(a.IdComponente.ToString()));
            }

            return PartialView("_BuscarComponentes", componentes);
        }
        public ActionResult CadastrarComponente()
        {
            ComponenteViewModel viewModel = null;
            try
            {
                viewModel = new ComponenteViewModel();
                viewModel.ListaTipoAmbiente = TipoAmbienteViewModel.MapToListViewModel(BL.TipoAmbiente.Get(a => true, b => b.OrderBy(c => c.Descr), "").ToList());
                viewModel.ListaAreaAtuacao = AreaAtuacaoViewModel.MapToListViewModel(BL.AreaAtuacao.Get().ToList());

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                TempData["Error"] = true;
                Logging.getInstance().Error("Erro ao carregar página a página CadastrarComponente", ex);
            }

            return View(viewModel);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ModalComponente()
        {
            ComponenteViewModel viewModel = null;
            try
            {
                viewModel = new ComponenteViewModel();
                viewModel.ListaAreaAtuacao = AreaAtuacaoViewModel.MapToListViewModel(BL.AreaAtuacao.Get().ToList());
                viewModel.ListaTipoAmbiente = TipoAmbienteViewModel.MapToListViewModel(BL.TipoAmbiente.Get(a => true, b => b.OrderBy(c => c.Descr), "").ToList());

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                TempData["Error"] = true;
                Logging.getInstance().Error("Erro ao carregar página a página CadastrarComponente", ex);
            }

            return PartialView(viewModel);
        }
        [HttpPost]
        public ActionResult ModalCadastrarComponente(ComponenteViewModel viewModel)
        {
            Componente inserido = new Componente();
            Componente model = null;
            ComponenteViewModel retornoComponente = new ComponenteViewModel();
            try
            {
                var idTipoAmbiente = Array.ConvertAll(viewModel.TipoAmbienteId.Split(','), a => int.Parse(a));
                var idAreaAtuacao = Array.ConvertAll(viewModel.AreaAtuacaoId.Split(','), a => int.Parse(a));

                List<AreaAtuacao> listaAreaAtuacao = BL.AreaAtuacao.Get(a => idAreaAtuacao.Contains(a.IdAreaAtuacao)).ToList();

                model = ComponenteViewModel.MapToModel(viewModel);
                inserido = BL.Componente.InserirComponente(model, idTipoAmbiente);
                BL.Componente.AtualizarAreAtuacaoComponente(inserido, listaAreaAtuacao);

                inserido = BL.Componente.GetById(inserido.IdComponente);

                retornoComponente = ComponenteViewModel.MapToViewModel(inserido);
                retornoComponente.TipoAmbiente = null;
                retornoComponente.AreaAtuacao = null;


                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Componente realizado com sucesso.";
            }
            catch (Exception ex)
            {
                bool validation = (ex.GetType().Name == "CustomException");
                if (!validation)
                {
                    TempData["Error"] = true;
                }
                TempData["ErrorMessage"] = validation ? ex.Message : "Erro ao cadastrar Componente.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);

                return Json("false", JsonRequestBehavior.AllowGet);
            }

            return Json(retornoComponente, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult CadastrarComponente(ComponenteViewModel viewModel, FormCollection collection)
        {
            Componente model = null;
            try
            {
                if (BL.Componente.Get(a => a.CH == viewModel.CH && a.Nome == viewModel.Nome).Any())
                {
                    TempData["Error"] = true;
                    TempData["ErrorMessage"] = "Erro ao cadastrar Componente. Já existe um componente com o mesmo nome e carga horária.";
                    return RedirectToAction("Index");
                }
                var teste = collection["tipoAmbiente"].Split(',');
                var arrayIdAreasAtuacao = Array.ConvertAll(collection["idsAreaAtuacao"].Split(';'), t => int.Parse(t));
                int[] idTipoAmbiente = Array.ConvertAll(teste, s => int.Parse(s));
                model = ComponenteViewModel.MapToModel(viewModel);
                var areas = BL.AreaAtuacao.Get(a => arrayIdAreasAtuacao.Contains(a.IdAreaAtuacao), null, "").ToList();
                var componente = BL.Componente.InserirComponente(model, idTipoAmbiente);
                BL.Componente.AtualizarAreAtuacaoComponente(componente, areas);

                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Componente realizado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Componente.";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarComponente(int id)
        {
            ComponenteViewModel componenteViewModel = null;
            try
            {
                var componente = BL.Componente.Get(a => a.IdComponente == id, null, "TipoAmbiente,AreaAtuacao").SingleOrDefault();

                componenteViewModel = ComponenteViewModel.MapToViewModel(componente);
                componenteViewModel.ListaTipoAmbiente = TipoAmbienteViewModel.MapToListViewModel(BL.TipoAmbiente.Get(a => true, b => b.OrderBy(c => c.Descr), "").ToList());
                componenteViewModel.ListaAreaAtuacao = AreaAtuacaoViewModel.MapToListViewModel(BL.AreaAtuacao.Get().ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                TempData["Error"] = true;
                Logging.getInstance().Error("Erro ao carregar página a página EditarComponente", ex);
            }

            return View(componenteViewModel);
        }

        [HttpPost]
        public ActionResult EditarComponente(ComponenteViewModel viewModel, FormCollection collection)
        {
            try
            {
                var tiposAmbientesId = collection["tipoAmbiente"].Split(',');
                int[] idTipoAmbiente = Array.ConvertAll(tiposAmbientesId, s => int.Parse(s));
                var arrayIdAreasAtuacao = Array.ConvertAll(collection["idsAreaAtuacao"].Split(';'), t => int.Parse(t));

                var model = ComponenteViewModel.MapToModel(viewModel);
                var areas = BL.AreaAtuacao.Get(a => arrayIdAreasAtuacao.Contains(a.IdAreaAtuacao), null, "").ToList();
                BL.Componente.AtualizarComponente(model, idTipoAmbiente);
                BL.Componente.AtualizarAreAtuacaoComponente(model, areas);

                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Edição de Componente realizada com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Componente.";
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ExcluirComponente(int id)
        {
            try
            {
                BL.Componente.ApagarComponente(id);
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Componente excluído com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Componente";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ModelAlert(AlertViewModel model)
        {
            try
            {
                return PartialView("ModalAlert", model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar model";
                Logging.getInstance().Error("Erro ao carregar model", ex);
            }

            AlertViewModel viewModel = new AlertViewModel();
            model.Titulo = "Informação";
            viewModel.Descricao = "Erro ao carregar model";
            return PartialView("ModalAlert", viewModel);
        }
    }
}
