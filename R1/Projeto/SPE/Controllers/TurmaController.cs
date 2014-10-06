using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senai.SPE.Domain;
using BusinessLogic;
using Common;
using Senai.SPE.Domain.Enum;
using SPE.ViewModel;
using Common.Email;

namespace SPE.Controllers
{
    public class TurmaController : BaseController
    {
        private SPEContext context = new SPEContext();

        //
        // GET: /TurmaViewModels/
        public ViewResult Index(FiltrosTurma filtro = null)
        {
            try
            {
                var turmas = BL.Turma.Get(null, null, "MATRIZ, CR.MODALIDADE").ToList();
                filtro.Turmas = TurmaViewModel.MapToListViewModel(turmas);
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";
                Logging.getInstance().Error("Erro ao carregar página de turmas", ex);
            }
            return View(filtro);
        }

        //
        // GET: /TurmaViewModels/Details/5
        public ViewResult DetalharTurma(int id)
        {
            return View();
        }

        //
        // GET: /TurmaViewModels/Create
        public ActionResult CadastrarTurma()
        {
            TurmaViewModel viewModel = null;
            var dataAtual =  DateTime.Today;

            try
            {

                viewModel = new TurmaViewModel();
                viewModel.ListaCR = CRViewModel.MapToListViewModel(BL.CR.Get().ToList());
                viewModel.ListaMatrizes = MatrizViewModel.MapToListViewModel(BL.Matriz.Get(a => (a.Aprovado == 1 && a.Status == 1 && a.Modalidade.CR.Any()) && (a.DataFim >= dataAtual || a.DataFim == null)).ToList());

                viewModel.ListaTurnos = TurnoViewModel.MapToListViewModel(BL.Turno.Get().ToList());
                viewModel.ListaUnidades = UnidadeViewModel.MapToListViewModel(BL.Unidade.Get().ToList());

                viewModel.ListaUsuario = new List<UsuarioViewModel>();
                viewModel.Status = 0;
                viewModel.TipoOferta = 0;

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = "Erro ao carregar a página";

                Logging.getInstance().Error("Erro ao carregar página CadastrarModalidade", ex);
            }
            return View(viewModel);

        }

        //
        // POST: /TurmaViewModels/Create
        [HttpPost]
        public ActionResult CadastrarTurma(TurmaViewModel turmaviewmodel)
        {
            Turma turmaCadastrada = new Turma();
            turmaviewmodel.IdResponsavel = LoggedUser().Id;
            try
            {
                turmaviewmodel.PrecoView = turmaviewmodel.PrecoView == null ? "0" : turmaviewmodel.PrecoView;
                turmaviewmodel.Preco = Decimal.Parse(turmaviewmodel.PrecoView.Replace("R$", "").Replace(".", ""));

                turmaCadastrada = BL.Turma.Add(TurmaViewModel.MapToModel(turmaviewmodel));
                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro da Turma" + turmaCadastrada.IdTurma.ToString("0000") + " realizado com sucesso.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao cadastrar Turma";

                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
            }

            return RedirectToAction("EditarTurma", new { id = turmaCadastrada.IdTurma });
        }

        //
        // GET: /TurmaViewModels/Edit/5
        public ActionResult EditarTurma(int id)
        {
            var dataAtual = DateTime.Today;
            ViewBag.User = LoggedUser().Id;

            var turma = BL.Turma.Get(a => a.IdTurma == id, null, "Matriz, Matriz.Modulo , Matriz.Modulo.Componente,AgendaComponente,ReprovacaoTurma").SingleOrDefault();

            TurmaViewModel viewModel = TurmaViewModel.MapToViewModel(turma);

            PopularListasTurmaVM(id, dataAtual, viewModel);
            return View(viewModel);
        }

        private void PopularListasTurmaVM(int id, DateTime dataAtual, TurmaViewModel viewModel)
        {
            viewModel.ListaCR = CRViewModel.MapToListViewModel(BL.CR.Get().ToList());

            var matrizes = BL.Matriz.Get(a => a.Aprovado == 1 && a.Status == 1 && a.Modalidade.CR.Any()).ToList();
            viewModel.ListaMatrizes = MatrizViewModel.MapToListViewModel(matrizes);
            viewModel.ListaTurnos = TurnoViewModel.MapToListViewModel(BL.Turno.Get().ToList());

            viewModel.ListaUnidades = UnidadeViewModel.MapToListViewModel(BL.Unidade.Get().ToList());
            viewModel.ListaUsuario = new List<UsuarioViewModel>();

            if (viewModel.ListaReprovacaoTurma ==  null)
            viewModel.ListaReprovacaoTurma = BL.ReprovacaoTurma.Get(r => r.IdTurma == id).OrderByDescending(a => a.IdReprovacaoTurma).ToList();          

            //viewModel.ListaMatrizModulo = MatrizViewModel.MapToViewModel(viewModel.Matriz);

            var datas = BL.Turma.GetDatasMinMax(id).FirstOrDefault();
            if (datas.DataFim != null && datas.Dataini != null)
            {
                viewModel.DataIniView = datas.Dataini.Value == null ? "" : datas.Dataini.Value.ToShortDateString();
                viewModel.DataFimView = datas.DataFim.Value == null ? "" : datas.DataFim.Value.ToShortDateString();
            }
            else
            {
                viewModel.DataIniView = "";
                viewModel.DataFimView = "";
            }
        }

        //
        // POST: /TurmaViewModels/Edit/5
        [HttpPost]
        public ActionResult EditarTurma(TurmaViewModel turmaviewmodel, FormCollection collection)
        {

            try
            {                

                turmaviewmodel.PrecoView = turmaviewmodel.PrecoView == null ? "0" : turmaviewmodel.PrecoView;
                turmaviewmodel.Preco = Decimal.Parse(turmaviewmodel.PrecoView.Replace("R$", "").Replace(".", ""));
                turmaviewmodel.Aprovado = (!string.IsNullOrEmpty(Request.Form["aprovacaoTurma"])) ?  Convert.ToUInt16(Request.Form["aprovacaoTurma"]) : new Nullable<int>();
                turmaviewmodel.IdResponsavel = LoggedUser().Id;                
                if (turmaviewmodel.ReprovacaoTurma.Observacao == null && turmaviewmodel.Aprovado != 1)
                {
                    turmaviewmodel.Aprovado = null;
                }
                if (turmaviewmodel.Aprovado == 0)
                {
                    var usuario = BL.Usuario.Get(a => a.IdUsuario == turmaviewmodel.IdResponsavel).FirstOrDefault();
                    BL.Turma.AtualizarReprovacaoTurma(TurmaViewModel.MapToModel(turmaviewmodel), DateTime.Now, turmaviewmodel.ReprovacaoTurma.Observacao);
                    EmailHelper.DispararEmail(usuario.Nome, "Reprovação de Turma", usuario.Email, "", "A Turma foi reprovada pelo seguinte motivo: " + turmaviewmodel.ReprovacaoTurma.Observacao);
                }
                else if (turmaviewmodel.Aprovado == 1 && turmaviewmodel.ReprovacaoTurma.Observacao != null)
                {                    
                    BL.Turma.AtualizarReprovacaoTurma(TurmaViewModel.MapToModel(turmaviewmodel), DateTime.Now, turmaviewmodel.ReprovacaoTurma.Observacao);
                    
                }
                else if (turmaviewmodel.Aprovado == 1 && turmaviewmodel.ReprovacaoTurma.Observacao == null)
                {
                    turmaviewmodel.Aprovado = null;
                    BL.Turma.AtualizarTurma(TurmaViewModel.MapToModel(turmaviewmodel));
                }
                else
                {
                    BL.Turma.AtualizarTurma(TurmaViewModel.MapToModel(turmaviewmodel));
                } 
               
                
                //BL.ReprovacaoTurma.AtualizarTurma(ReprovacaoTurmaViewModel.MapToModel(turmaviewmodel.ListaReprovacaoTurma.ToList()));

                TempData["Sucesso"] = true;
                TempData["SucessoMessage"] = "Cadastro da Turma" + turmaviewmodel.IdTurma.ToString("0000") + " foi atualizado com sucesso.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = true;
                TempData["ErrorMessage"] = (ex.GetType().Name == "CustomException") ? ex.Message : "Erro ao Editar Turma";

                PopularListasTurmaVM(turmaviewmodel.IdTurma, DateTime.Now, turmaviewmodel);
                Logging.getInstance().Error(TempData["ErrorMessage"].ToString(), ex);
                return View(turmaviewmodel);
            }


            
        }

        //
        // GET: /TurmaViewModels/Delete/5
        public ActionResult ExcluirTurma(int id)
        {
            return View();
        }

        //
        // POST: /TurmaViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
        }

        public ActionResult RetornaTurmasBy(int idCR)
        {
            var viewModel = TurmaViewModel.MapToListViewModel(BL.Turma.RetornarTurmas(idCR, 1, (int)PaginacaoEnum.NumeroElementos));
            return PartialView("_RetornaTurmas", viewModel);
        }

        public ActionResult ListarCR(int idMatriz)
        {
            MatrizViewModel matrizSelecionada = MatrizViewModel.MapToViewModel(BL.Matriz.GetById(idMatriz));
            int idModalidadeDaMatriz = (matrizSelecionada.IdModalidade.HasValue) ? matrizSelecionada.IdModalidade.Value : 0;
            List<CRViewModel> crViewModel = CRViewModel.MapToListViewModel(BL.CR.Get(e => e.IdModalidade == idModalidadeDaMatriz).ToList());


            return PartialView("_ListarCR", crViewModel);
        }

    }
}