using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Carubbi.Datatables;
using Carubbi.GenericRepository;
using DataTables.Mvc;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Entidades;
using Itanio.Leads.Domain.Repositorios;
using Itanio.Leads.WebUI.Models;

namespace Itanio.Leads.WebUI.Controllers
{
    [Authorize]
    public class ProjetoController : BaseController
    {
        public ProjetoController(IContexto contexto)
            : base(contexto)
        {
        }

        // GET: Projeto
        public ActionResult Index()
        {
            return View(new ProjetoViewModel());
        }

        public ActionResult Editar(string id)
        {
            var viewModel = new ProjetoViewModel();
            var projetoRepo = new RepositorioProjeto(_contexto);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var projeto = projetoRepo.ObterPorId(new Guid(id));
                viewModel = ProjetoViewModel.FromEntity(projeto);
            }

            var modalViewModel = new ModalFormViewModel
            {
                Id = "EditarProjetoForm",
                PartialViewName = "_EditarProjeto",
                Title = string.Format("{0} de Projeto", !string.IsNullOrWhiteSpace(id) ? "Alteração" : "Inclusão"),
                ViewModel = viewModel,
                Size = ModalSize.Medium
            };
            return PartialView("_ModalFormDialog", modalViewModel);
        }

        [HttpPost]
        public ActionResult Editar(ProjetoViewModel viewModel)
        {
            var projetoRepo = new RepositorioProjeto(_contexto);
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(viewModel.Id))
                {
                    var projeto = projetoRepo.ObterPorId(new Guid(viewModel.Id));
                    var projetoAlterado = ProjetoViewModel.ToEntity(viewModel, projeto.Arquivos);
                    projetoRepo.Atualizar(projetoAlterado);
                }
                else
                {
                    var novoProjeto = ProjetoViewModel.ToEntity(viewModel, new List<Arquivo>());
                    projetoRepo.Adicionar(novoProjeto);
                }

                return PartialView("_projetoGrid");
            }

            return PartialView("_validationSummary", viewModel);
        }

        public ActionResult Excluir(string id)
        {
            var viewModel = new ProjetoViewModel();
            var projetoRepo = new RepositorioProjeto(_contexto);
            var projeto = projetoRepo.ObterPorId(new Guid(id));
            viewModel = ProjetoViewModel.FromEntity(projeto);

            var modalViewModel = new ModalQuestionViewModel
            {
                Id = "ConfirmacaoExclusaoProjeto",
                Title = "Exclusão de Projeto",
                Body = string.Format("Tem certeza que deseja excluir o projeto {0}?", viewModel.Nome),
                YesButtonAction = Url.Action("Excluir", "Projeto"),
                CloseNoButton = true,
                KeyProperties = string.Format("Id={0}", id),
                YesButtonUpdateContainerId = "projeto-grid-container",
                YesButtonNotification = new NotificationViewModel
                {
                    Type = NotificationType.Error,
                    Message = "Projeto excluído com sucesso!",
                    Title = "Exclusão de Projeto"
                }
            };
            return PartialView("_ModalQuestionDialog", modalViewModel);
        }

        [HttpPost]
        public ActionResult Excluir(ProjetoViewModel viewModel)
        {
            var projetoRepo = new RepositorioProjeto(_contexto);
            var projeto = projetoRepo.ObterPorId(new Guid(viewModel.Id));

            projetoRepo.Excluir(projeto);


            return PartialView("_projetoGrid");
        }

        public JsonResult Projetos([ModelBinder(typeof(DataTablesBinder))]
            IDataTablesRequest requestModel)
        {
            var repo = new GenericRepository<Projeto>(_contexto);
            var query = new SearchQuery<Projeto>();

            var sortedColumns = requestModel
                .Columns
                .GetSortedColumns()
                .ToDynamicExpression<ProjetoViewModel>();

            if (sortedColumns.Length > 0) query.AddSortCriteria(new DynamicFieldSortCriteria<Projeto>(sortedColumns));

            query.Take = requestModel.Length;
            query.Skip = requestModel.Start;

            var result = repo.Search(query);

            return Json(new DataTablesResponse(requestModel.Draw,
                ProjetoViewModel.FromEntityCollection(result.Entities), result.Count, result.Count));
        }
    }
}