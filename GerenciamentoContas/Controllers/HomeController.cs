using GerenciamentoContas.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciamentoContas.Controllers
{
    public class HomeController : Controller
    {

        ClienteModel metodosCli = new ClienteModel();
        Conexao con = new Conexao();
        ContasModel metodosContas = new ContasModel();
        ContasAdmModel metodosAdm = new ContasAdmModel();

        public int recordsPerPage = 8;


        public ActionResult Index(string sortOn, string orderBy,string pSortOn, string keyword, int? page)
        {
                var Listar = metodosCli.Listar();
                if (!page.HasValue)
                {
                    page = 1; // set initial page value
                    if (string.IsNullOrWhiteSpace(orderBy) || orderBy.Equals("asc"))
                    {
                        orderBy = "desc";
                    }
                    else
                    {
                        orderBy = "asc";
                    }
                }

                // override the sort order if the previous sort order and current
                if (!string.IsNullOrWhiteSpace(sortOn) && !sortOn.Equals(pSortOn,StringComparison.CurrentCultureIgnoreCase))
                {
                    orderBy = "asc";
                }

                var list = Listar.AsQueryable();
                switch (sortOn)
                {
                    case "NomeCliente":
                        if (orderBy.Equals("desc"))
                        {
                            list = list.OrderByDescending(p => p.NomeCliente);
                        }
                        else
                        {
                            list = list.OrderBy(p => p.NomeCliente);
                        }
                        break;
                    case "CodCliente":
                        if (orderBy.Equals("desc"))
                        {
                            list = list.OrderByDescending(p => p.CodCliente);
                        }
                        else
                        {
                            list = list.OrderBy(p => p.CodCliente);
                        }
                    break;
                    default:
                        list = list.OrderBy(p => p.CodCliente);
                    break;
                }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                list = list.Where(f => f.NomeCliente.ToUpper().ToLower().StartsWith(keyword));
            }
            var finalList = list.ToPagedList(page.Value, recordsPerPage);
            return View(finalList);
        }



        public ActionResult ClientesVencidos(string orderBy, int? page)
        {
            var Listar = metodosCli.ListarVencidos();
            var list = Listar.AsQueryable();
            ViewBag.nada = "Nada para mostrar";
            if (!page.HasValue)
            {
                page = 1; // set initial page value
                if (string.IsNullOrWhiteSpace(orderBy) || orderBy.Equals("asc"))
                {
                    orderBy = "desc";
                }
                else
                {
                    orderBy = "asc";
                }
            }

            var finalList = list.ToPagedList(page.Value, recordsPerPage);

            return View(finalList);
        }

        public ActionResult Inicio()
        {
            return View();
        }
        public ActionResult Inicio2()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            LoginModel log = new LoginModel();
            if (login.Adm == "Admin" && login.Senha == "admin")
            {
                return RedirectToAction("Inicio2");
            }
            ViewBag.Message = "Login ou senha incorretos";
            return View(login);
        }

        public ActionResult PagContasCli(int id)
        {
            var conta = metodosContas.ListarIdConta(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            return View(conta);
        }

        [HttpPost]
        public ActionResult PagContasCli(ContasModel conta, int id)
        {
            conta.CodConta = id;
            metodosContas.SituacaoPag(conta);
            return RedirectToAction("Index");
        }

        public ActionResult PagContasCliVencido(int id)
        {
            var conta = metodosContas.ListarIdConta(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            return View(conta);
        }

        [HttpPost]
        public ActionResult PagContasCliVencido(ContasModel conta, int id)
        {
            conta.CodConta = id;
            metodosContas.SituacaoPag(conta);
            return RedirectToAction("ClientesVencidos");
        }
        public ActionResult CadastroCli()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroCli(ClienteModel cliente)
        {
            if (ModelState.IsValid)
            {
                metodosCli.Insert(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public ActionResult ExcluirCli(int id)
        {
            var cliente = metodosCli.ListarId(id);
            if(cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost,ActionName("ExcluirCli")]
        public ActionResult ExcluirConfCli(int id)
        {
            ClienteModel cli = new ClienteModel();
            cli.CodCliente = id;
            metodosCli.Excluir(cli);
            return RedirectToAction("Index");
        }

        public ActionResult AlterarCli(int id)
        {
            var cliente = metodosCli.ListarId(id);
            if(cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public ActionResult AlterarCli(ClienteModel cliente)
        {
            if (ModelState.IsValid)
            {
                metodosCli.Atualizar(cliente);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult ListarCliente(int id)
        {
            var cliente = metodosCli.ListarId(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        ////////////////////////////////////////////////////////

        public ActionResult ContasAdm(string orderBy, int? page)
        {
            var Listar = metodosAdm.Listar();
            if (!page.HasValue)
            {
                page = 1;
                if (string.IsNullOrWhiteSpace(orderBy) || orderBy.Equals("asc"))
                {
                    orderBy = "desc";
                }
                else
                {
                    orderBy = "asc";
                }
            }

            var list = Listar.AsQueryable();

            var finalList = list.ToPagedList(page.Value, recordsPerPage);
            return View(finalList);
        }

        public ActionResult PagContasAdm(int id)
        {  
            var adm = metodosAdm.ListarIdConta(id);
            if (adm == null)
            {
                return HttpNotFound();
            }
            return View(adm);
        }

        [HttpPost]
        public ActionResult PagContasAdm(ContasAdmModel adm, int id)
        {
            adm.CodConta = id;
            metodosAdm.SituacaoPag(adm);
            return RedirectToAction("ContasAdm");
        }

        public ActionResult CadastrarContasAdm()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CadastrarContasAdm(ContasAdmModel contas)
        {
            if (ModelState.IsValid)
            {
                metodosAdm.InsertContasAdm(contas);
                return RedirectToAction("ContasAdm");
            }
            return View(contas);
        }

        public ActionResult ExcluirContaAdm(int id)
        {
            var conta = metodosAdm.ListarIdConta(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            return View(conta);
        }

        [HttpPost, ActionName("ExcluirContaAdm")]
        public ActionResult ExcluirConfContaAdm(int id)
        {
            ContasAdmModel conta = new ContasAdmModel();
            conta.CodConta = id;
            metodosAdm.Excluir(conta);
            return RedirectToAction("ContasAdm");
        }

        public ActionResult AlterarContasAdm(int id)
        {
            var adm = metodosAdm.ListarIdConta(id);
            if (adm == null)
            {
                return HttpNotFound();
            }
            return View(adm);
        }

        [HttpPost]
        public ActionResult AlterarContasAdm(ContasAdmModel adm)
        {
            if (ModelState.IsValid)
            {
                metodosAdm.Atualizar(adm);
                return RedirectToAction("ContasAdm");
            }
            return View();
        }

        //////////////////////////////////////////////////////////
        public static int valorCliente;
        public ActionResult ContasCli(int id, string orderBy, int? page)
        {
            valorCliente = id;
            var contas = metodosContas.Listar(id);
            if (!page.HasValue)
            {
                page = 1;
                if (string.IsNullOrWhiteSpace(orderBy) || orderBy.Equals("asc"))
                {
                    orderBy = "desc";
                }
                else
                {
                    orderBy = "asc";
                }
            }

            var list = contas.AsQueryable();

            var finalList = list.ToPagedList(page.Value, recordsPerPage);
            return View(finalList);
        }

        public ActionResult CadastrarContas()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CadastrarContas(ContasModel contas)
        {
            contas.CodClienteConta = valorCliente;
            if (ModelState.IsValid)
            {
                metodosContas.Insert(contas);
                metodosContas.AtualizarQtd();
                return RedirectToAction("Index");
            }
            return View(contas);
        }

        public ActionResult ExcluirConta(int id)
        {
            var conta = metodosContas.ListarIdConta(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            return View(conta);
        }

        [HttpPost, ActionName("ExcluirConta")]
        public ActionResult ExcluirConfConta(int id)
        {
            ContasModel conta = new ContasModel();
            conta.CodConta = id;
            metodosContas.Excluir(conta);
            metodosContas.AtualizarQtd();
            return RedirectToAction("Index");
        }

        public ActionResult AlterarContas(int id)
        {
            var contas = metodosContas.ListarIdConta(id);
            if (contas == null)
            {
                return HttpNotFound();
            }
            return View(contas);
        }

        [HttpPost]
        public ActionResult AlterarContas(ContasModel contas)
        {
            if (ModelState.IsValid)
            {
                metodosContas.Atualizar(contas);
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}