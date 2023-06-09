using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        //public ActionResult Login(string userName, string password)
        //{
        //    ML.Result result = BL.Usuario.GetByUserName(userName);

        //    if (result.Correct)//si el usuario existe
        //    {
        //        ML.Usuario usuario = (ML.Usuario)result.Object;
        //        if (password == usuario.Password)
        //        {
        //            return RedirectToAction("Index", "Home");

        //            //return View("./Usuario"); //co devolver a una vista diferente en MVC .net core
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "Contraseña invalida";
        //            return PartialView("ModalLogin");
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Mensaje = "Usuario invalido";
        //        return PartialView("ModalLogin");
        //    }
        //}
    }
}
