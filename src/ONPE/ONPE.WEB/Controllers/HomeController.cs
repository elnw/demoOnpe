using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ONPE.WEB.ViewModel;
using ONPE.WEB.Models;

namespace ONPE.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Dashboard()
        {
            DashboardViewModel objViewModel = new DashboardViewModel();
            return View(objViewModel);
        }

        public ActionResult LstCandidato()
        {
            LstCandidatoViewModel objViewModel = new LstCandidatoViewModel();
            return View(objViewModel);
        }

        [HttpGet]
        public ActionResult AddEditCandidato(int? CandidatoId)
        {
            AddEditCandidatoViewModel objViewModel = new AddEditCandidatoViewModel();
            objViewModel.CargarDatos(CandidatoId);
            return View(objViewModel);
        }

        [HttpPost]

        public ActionResult AddEditCandidato(AddEditCandidatoViewModel objViewModel)
        {
            try
            {
                ONPEEntities context = new ONPEEntities();
                Candidato objCandidato = new Candidato();

                if (objViewModel.CandidatoId.HasValue)
                {
                    objCandidato = context.Candidato.FirstOrDefault(X => X.CandidatoId == objViewModel.CandidatoId);
                    objCandidato.Nombres = objViewModel.Nombres;
                    objCandidato.Apellidos = objViewModel.Apellidos;
                    objCandidato.DistritoId = objViewModel.DistritoId;
                    objCandidato.PartidoPoliticoId = objViewModel.PartidoPoliticoId;
                }
                else
                {
                    context.Candidato.Add(objCandidato);
                }
                objCandidato.Nombres = objViewModel.Nombres;
                objCandidato.Apellidos = objViewModel.Apellidos;
                objCandidato.DistritoId = objViewModel.DistritoId;
                objCandidato.PartidoPoliticoId = objViewModel.PartidoPoliticoId;
                objCandidato.Estado = "ACT";
                context.SaveChanges();
                TempData["Mensaje"] = "Exito! La operacion se ejecutó satisfactoriamente";
                return RedirectToAction("LstCandidato");

            }catch(Exception ex)
            {
                return View(objViewModel);
            }
        }

        public ActionResult AddEditPartidoPolitico(int? PartidoPoliticoId)
        {
            AddEditPartidoPoliticoViewModel objViewModel = new AddEditPartidoPoliticoViewModel();
            objViewModel.CargarDatos(PartidoPoliticoId);
            return View(objViewModel);
        }

        [HttpPost]
        public ActionResult AddEditPartidoPolitico(AddEditPartidoPoliticoViewModel objViewModel)
        {
            try
            {
                ONPEEntities context = new ONPEEntities();
                PartidoPolitico objPartido = new PartidoPolitico();
                if (objViewModel.PartidoPoliticoId.HasValue)
                {
                    objPartido = context.PartidoPolitico.FirstOrDefault(X => X.PartidoPoliticoId == objViewModel.PartidoPoliticoId);
                    objPartido.Nombre = objViewModel.nombre;
                }
                else
                {
                    objPartido.Nombre = objViewModel.nombre;
                    objPartido.Estado = "ACT";
                    context.PartidoPolitico.Add(objPartido);
                }
                
                context.SaveChanges();
                TempData["Mensaje"] = "Exito! La operacion se ejecutó satisfactoriamente";
                return RedirectToAction("LstPartidoPolitico");

            }
            catch(Exception ex)
            {
                return View(objViewModel);
            }


             
        }

        public ActionResult AddEditDistrito(int? DistritoId)
        {
            AddEditDistritoViewModel objViewModel = new AddEditDistritoViewModel();
            objViewModel.CargarDatos(DistritoId);
            return View(objViewModel);
        }

        [HttpPost]
        public ActionResult AddEditDistrito(AddEditDistritoViewModel objViewModel)
        {
            try
            {
                ONPEEntities context = new ONPEEntities();
                Distrito objDistrito = new Distrito();
               if (objViewModel.DistritoId.HasValue)
               {
                   objDistrito = context.Distrito.FirstOrDefault(X => X.DistritoId == objViewModel.DistritoId);
                   objDistrito.Nombre = objViewModel.nombre;
                }
               else
               {
                    objDistrito.Nombre = objViewModel.nombre;
                    objDistrito.Estado = "ACT";
                    context.Distrito.Add(objDistrito);
                }

                context.SaveChanges();
                TempData["Mensaje"] = "Exito! La operacion se ejecutó satisfactoriamente";
                return RedirectToAction("LstDistrito");

            }
            catch (Exception ex)
            {
                return View(objViewModel);
            }



        }


        public ActionResult EliminarDistrito(int? DistritoId)
        {
            AddEditDistritoViewModel objViewModel = new AddEditDistritoViewModel();
            objViewModel.CargarDatos(DistritoId);
            return View(objViewModel);
        }

        [HttpPost]
        public ActionResult EliminarDistrito(int DistritoId )
        {
            try
            {
                ONPEEntities context = new ONPEEntities();
                Distrito objDistrito = new Distrito();
           

                objDistrito = context.Distrito.FirstOrDefault(X => X.DistritoId == DistritoId);
                objDistrito.Estado = "INA";

                context.SaveChanges();
                TempData["Mensaje"] = "Exito! La operacion se ejecutó satisfactoriamente";
                return RedirectToAction("LstDistrito");

            }
            catch (Exception ex)
            {
                return RedirectToAction("LstDistrito"); //no c k poner
            }



        }


        public ActionResult LstDistrito()
        {
            LstDistritoViewModel objViewModel = new LstDistritoViewModel();
            return View(objViewModel);
        }

        public ActionResult LstPartidoPolitico()
        {
            LstPartidoPoliticoViewModel objViewModel = new LstPartidoPoliticoViewModel();
            return View(objViewModel);
        }

        public ActionResult Login()
        {
            LoginViewModel objViewModel = new LoginViewModel();
            return View(objViewModel);
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel objViewModel)
        {
            try
            {
                ONPEEntities context = new ONPEEntities();
                Usuario objUsuario = context.Usuario.FirstOrDefault(x => x.Codigo == objViewModel.codigo && x.Password == objViewModel.Password);
                if (objUsuario == null)
                {
                    TempData["Mensaje"] = "Error! Usuario y/o contraseña incorrectos >:V";
                    return View(objViewModel);
                }
                return RedirectToAction("Dashboard");
            }
            catch(Exception ex)
            {
                TempData["Mensaje"] = "Error! " + ex.Message;
                return View(objViewModel);
            }
        }

        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }





    }
}