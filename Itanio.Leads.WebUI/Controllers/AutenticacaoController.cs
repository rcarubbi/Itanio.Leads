﻿using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Servicos;
using Itanio.Leads.WebUI.Models;
using Itanio.Leads.WebUI.Servicos;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace Itanio.Leads.WebUI.Controllers
{
    [AllowAnonymous]
    public class AutenticacaoController : BaseController
    {
        public AutenticacaoController(IContexto contexto)
            : base(contexto)
        {

        }

        // GET: Autenticacao
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                ServicoAutenticacao autenticacao = new ServicoAutenticacao(_contexto);
                if (autenticacao.Login(viewModel.Conta, viewModel.Senha))
                {
                    FormsAuthentication.RedirectFromLoginPage(viewModel.Conta, viewModel.MantenhaMeConectado);
                    return new EmptyResult();
                }
                else
                {
                    ModelState.AddModelError("UsuarioOuSenhaInvalidos", "Usuário ou Senha inválidos");
                }
            }

            return View(viewModel);
        }

        public ActionResult EsqueciMinhaSenha()
        {
            EsqueciMinhaSenhaViewModel viewModel = new EsqueciMinhaSenhaViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EsqueciMinhaSenha(EsqueciMinhaSenhaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ServicoAutenticacao servico = new ServicoAutenticacao(_contexto);
                var senha = servico.RecuperarSenha(viewModel.Email);
                if (senha != null)
                {
                    ServicoNotificacao servicoNotificacao = new ServicoNotificacao(_contexto);
                    await Task.Run(() => servicoNotificacao.NotificarRecuperacaoSenha(viewModel.Email, senha, this.Url.Action("Login", "Autenticacao")));
                    ViewBag.Mensagem = "Enviamos um e-mail para você com uma senha temporária, utilize-a no seu próximo acesso.";
                }
                else
                {
                    ViewBag.Mensagem = "";
                    ModelState.AddModelError("EmailNaoEncontrado", "E-mail não encontrado");
                }
            }
            return View(viewModel);
        }


        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return Redirect("Login");
        }
    }

}