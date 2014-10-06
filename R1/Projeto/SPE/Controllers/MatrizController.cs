using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Senai.SPE.Domain;
using System.Data.Entity;
using Senai.SPE.Domain.Enum;
using SPE.ViewModel;
using Common.Email;

namespace SPE.Controllers
{
    public class MatrizController : BaseController
    {
        //
        // GET: /Matriz/
        public ActionResult Index(FiltrosMatriz filtro = null)
        {

            try
            {
                var listaMatriz = BL.Matriz.Get(null, null, "CBO, AREAATUACAO, MODALIDADE").ToList();
                filtro.Matriz = MatrizViewModel.MapToListViewModel(listaMatriz);

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página de Index da Matriz", ex);
            }
            return View(filtro);
        }

        //
        // GET: /Matriz/Details/5

        public ActionResult DetalharMatriz(int id)
        {
            MatrizViewModel matrizViewModel = null;
            try
            {
                var item = BL.Matriz.GetById(id);
                matrizViewModel = MatrizViewModel.MapToViewModel(item);
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página DetalharMatriz", ex);
                return View();
            }
            return View(matrizViewModel);
        }

        public ActionResult CadastrarMatriz()
        {
            MatrizViewModel viewModel = null;
            try
            {
                viewModel = new MatrizViewModel();

                viewModel.ListaModalidades = ModalidadeViewModel.MapToListViewModel((BL.Modalidade.Get()).ToList());
                viewModel.ListaAreaAtuacao = AreaAtuacaoViewModel.MapToListViewModel((BL.AreaAtuacao.Get()).ToList());
                viewModel.ListaCBO = CBOViewModel.MapToListViewModel((BL.CBO.Get()).ToList());

                MatrizListarComponentesViewModel model = new MatrizListarComponentesViewModel();
                model.Matriz = 0;
                model.Filtro = "";

                //var lista = BL.Componente.BuscarComponentesPor(model.Filtro, 1, (int)PaginacaoEnum.NumeroElementos);
                //model.ListaViewModel = ComponenteViewModel.MapToListViewModel(lista);

                viewModel.ListaComponente = model;

                viewModel.Componente = new List<Componente>();
                viewModel.Modulo = new List<Modulo>();
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página CadastrarMatriz", ex);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarMatriz(MatrizViewModel matrizviewmodel)
        {
            Matriz model = null;
            matrizviewmodel.IdResponsavel = LoggedUser().Id;
            try
            {
                if (Session["Modulos"] != null)
                {
                    model = MatrizViewModel.MapToModel(matrizviewmodel);
                    var matriz = BL.Matriz.InserirMatriz(model);
                    foreach (var item in (List<ModuloComponenteViewModel>)Session["Modulos"])
                    {
                        item.IdMatriz = matriz.IdMatriz;

                        var teste = (Session["Modulos"] as List<ModuloComponenteViewModel>).Where(e => e.Nome == item.Nome);
                        if (teste.Count() > 1)
                            throw new CustomException("Erro ao cadastrar Matriz. Existem módulos com o mesmo nome", 1);
                    }

                    var modulo = ModuloViewModel.MapToModel((List<ModuloComponenteViewModel>)Session["Modulos"]);
                     BL.Modulo.VinculoModuloComponentes(modulo, matrizviewmodel.Nome, matrizviewmodel.CH.ToString());

                }
                else
                {
                    model = MatrizViewModel.MapToModel(matrizviewmodel);
                    var matriz = BL.Matriz.InserirMatriz(model);
                }

                matrizviewmodel.IdResponsavel = (int)Session["usuarioId"];
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro de Matriz realizado com sucesso.";
                Session["Modulos"] = null;
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Matriz.";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);

                if (ex.GetType().Name == "CustomException")
                {
                    if ((ex as CustomException).ErrorCode == 1)
                        return RedirectToAction("CadastrarMatriz");
                }

            }

            return RedirectToAction("Index");
        }

        public ActionResult EditarMatriz(int id)
        {
            MatrizViewModel matrizViewModel = null;
            try
            {
                var item = BL.Matriz.Get(a => a.IdMatriz == id, null, "Modulo, Modulo.Componente").FirstOrDefault();
                matrizViewModel = MatrizViewModel.MapToViewModel(item);

                matrizViewModel.ListaModalidades = ModalidadeViewModel.MapToListViewModel((BL.Modalidade.Get()).ToList());
                matrizViewModel.ListaAreaAtuacao = AreaAtuacaoViewModel.MapToListViewModel((BL.AreaAtuacao.Get()).ToList());
                matrizViewModel.ListaCBO = CBOViewModel.MapToListViewModel((BL.CBO.Get()).ToList());
                // Listar componentes          
                matrizViewModel.ListaReprovacaoMatriz = BL.ReprovacaoMatriz.Get(a => a.IdMatriz == matrizViewModel.IdMatriz).ToList();
                MatrizListarComponentesViewModel viewModel = new MatrizListarComponentesViewModel();
                viewModel.Matriz = id;
                viewModel.Filtro = Request.QueryString["filtro"];

                var lista = BL.Componente.BuscarComponentesPor(viewModel.Filtro, 1, (int)PaginacaoEnum.NumeroElementos);
                viewModel.ListaViewModel = ComponenteViewModel.MapToListViewModel(lista);

                matrizViewModel.ListaComponente = viewModel;
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página EditarMatriz", ex);
            }
            matrizViewModel.ListaReprovacaoMatriz.OrderByDescending(a=>a.IdReprovacaoMatriz);
            return View(matrizViewModel);
        }

        //
        // POST: /MatrizViewModels/Edit/5
        [HttpPost]
        public ActionResult EditarMatriz(MatrizViewModel matrizviewmodel)
        {
            try
            {
                var usuario =  BL.Usuario.Get(a=>a.IdUsuario == matrizviewmodel.IdResponsavel).FirstOrDefault();
                //matrizviewmodel.ReprovacaoMatriz.Observacao = Request.Form["descricao"];
                matrizviewmodel.Aprovado = (!string.IsNullOrEmpty(Request.Form["aprovarMatriz"])) ? Convert.ToInt16(Request.Form["aprovarMatriz"]) : new Nullable<short>();

                var modulo = ModuloViewModel.MapToModel((List<ModuloComponenteViewModel>)Session["Modulos"]);
                BL.Modulo.VinculoModuloComponentes(modulo, matrizviewmodel.Nome, matrizviewmodel.CH.ToString());

                if (matrizviewmodel.Aprovado == 0 && matrizviewmodel.ReprovacaoMatriz.observacao != null)
                {
                    BL.Matriz.AtualizarReprovacaoMatriz(MatrizViewModel.MapToModel(matrizviewmodel), DateTime.Now, matrizviewmodel.ReprovacaoMatriz.observacao);
                    EmailHelper.DispararEmail(usuario.Nome, "Reprovação de matriz", usuario.Email, "" , "A Matriz foi reprovada pelo seguinte motivo: " + matrizviewmodel.ReprovacaoMatriz.observacao);
                }
                else if (matrizviewmodel.Aprovado != 1 )
                {
                    matrizviewmodel.Aprovado = null;
                    BL.Matriz.AtualizarMatriz(MatrizViewModel.MapToModel(matrizviewmodel));
                    
                }
                else if (matrizviewmodel.Aprovado == 1 )
                {
                    if (string.IsNullOrEmpty(matrizviewmodel.ReprovacaoMatriz.observacao))
                    {
                        matrizviewmodel.ReprovacaoMatriz.observacao = "";
                    }
                    BL.Matriz.AtualizarReprovacaoMatriz(MatrizViewModel.MapToModel(matrizviewmodel), DateTime.Now, matrizviewmodel.ReprovacaoMatriz.observacao);
                }
                
                

                if (Session["Sucesso"] != null)
                {
                    if ((bool)Session["Sucesso"])
                    {
                        var model = MatrizViewModel.MapToModel(matrizviewmodel);
                        BL.Matriz.AtualizarMatriz(model);

                        TempData["Sucesso"] = true;
                        TempData["SucessoMessage"] = "Edição de Matriz realizada com sucesso.";
                    }
                    Session["Sucesso"] = null;
                }
                else
                {
                    TempData["Error"] = true;
                    TempData["ErrorMessage"] = Session["ErrorMessage"];
                    Session["ErrorMessage"] = null;
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao editar Matriz.";

                Logging.getInstance().Error("Erro ao editar Matriz", ex);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /MatrizViewModels/Delete/5
        public ActionResult ExcluirMatriz(int id)
        {
            try
            {
                BL.Matriz.ApagarMatriz(id);

                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Matriz excluída com sucesso.";

            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao excluir Matriz. Esse Matriz está associado a outro(s) itens.";
                Logging.getInstance().Error("Erro ao excluir Matriz", ex);
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao excluir Matriz.";
                Logging.getInstance().Error("Erro ao excluir Matriz", ex);
            }

            return RedirectToAction("Index");
        }

        public ActionResult AdicionarComponentes(string nome)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarComponentes(string filtro,int idAreaAtuacao)
        {
            MatrizViewModel matrizViewModel = new MatrizViewModel();
            MatrizListarComponentesViewModel model = new MatrizListarComponentesViewModel();

            //#refatorar: Codigo duplicado
            var componentes = BL.Componente.BuscarComponentesPorAreaAtuacao(idAreaAtuacao.ToString());
            componentes = componentes.Where(c => c.Nome.ToLower().Contains(filtro.ToLower())).ToList();

            model.ListaViewModel = ComponenteViewModel.MapToListViewModel(componentes);
            model.Filtro = filtro;
            matrizViewModel.ListaComponente = model;
            return PartialView("_BuscarModuloComponentes", matrizViewModel);
        }

        public ActionResult Teste()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ModuloTeste(List<ModuloComponenteViewModel> lista, string i, string Nome, string CH)
        {
            try
            {
                if (lista == null)
                {
                    BL.Modulo.RemoverModulos(int.Parse(i), Nome, CH);
                }
                else
                {
                    if (lista.FirstOrDefault().IdMatriz != 0)
                    {
                        var modulo = ModuloViewModel.MapToModel(lista);
                        BL.Modulo.VinculoModuloComponentes(modulo, Nome, CH);

                    }
                    else
                    {

                    }
                }
                Session["Modulos"] = lista;
                Session["Sucesso"] = true;
                TempData["SucessoMessage"] = "Matriz editada com sucesso.";
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                Session["Error"] = true;
                Session["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao editar Matriz.";

                Logging.getInstance().Error(Session["ErrorMessage"].ToString(), ex);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}
