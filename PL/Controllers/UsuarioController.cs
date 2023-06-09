﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private IHostingEnvironment environment;
        private IConfiguration configuration;
        public UsuarioController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }
        [HttpGet]
        public ActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Login(ML.Usuario usuario, string password)
        {
            // Crear una instancia del algoritmo de hash bcrypt
            var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);
            // Obtener el hash resultante para la contraseña ingresada 
            var passwordHash = bcrypt.GetBytes(20);

            if (usuario.UserName != null)
            {
                // Insertar usuario en la base de datos
                usuario.Password = passwordHash;
                ML.Result result = BL.Usuario.Add(usuario);
                return View();
            }
            else
            {
                // Proceso de login
                // Proceso de login
                ML.Result result = BL.Usuario.GetByEmail(usuario.Email);
                usuario = (ML.Usuario)result.Object;

                if (usuario.Password.SequenceEqual(passwordHash))
                {

                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult CambiarPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CambiarPassword(string email)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetByEmail(email);
            usuario = (ML.Usuario)result.Object;
            if (result.Correct)
            {
                string emailOrigen = "cescalonacha@gmail.com";

                MailMessage mailMessage = new MailMessage(emailOrigen, email, "Recuperar Contraseña", "<p>Correo para recuperar contraseña</p>");
                mailMessage.IsBodyHtml = true;

                //string contenidoHTML = System.IO.File.ReadAllText(@"C:\Users\digis\OneDrive\Documents\ESCALONA CHAVARRIA CECILIA GABRIELA\Repositorio\CGEscalonaCha\CCEscalonaTheMovieDB\PL\PL\Views\Usuario\Email.html");
                string contenidoHTML = System.IO.File.ReadAllText(Path.Combine(environment.ContentRootPath, "wwwroot", "Templates", "Email.html"));

                mailMessage.Body = contenidoHTML;

                string url = configuration["NewPassword"] + HttpUtility.UrlEncode(email);


                mailMessage.Body = mailMessage.Body.Replace("{Url}", url);
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, configuration["Contrasenia"]);

                smtpClient.Send(mailMessage);
                smtpClient.Dispose();

                ViewBag.Modal = "show";
                ViewBag.Mensaje = "Se ha enviado un correo de confirmación a tu correo electronico";
                return View();
            }
            else
            {
                ViewBag.Mensaje = " No Se ha enviado un correo ";

                return View("Login");
            }

            //validar que exista el email en la bd

            //    string emailOrigen = "cescalonacha@gmail.com";

            //    MailMessage mailMessage = new MailMessage(emailOrigen, email, "Recuperar Contraseña", "<p>Correo para recuperar contraseña</p>");
            //    mailMessage.IsBodyHtml = true;
            //    string contenidoHTML = System.IO.File.ReadAllText(@"C:\Users\digis\OneDrive\Documents\ESCALONA CHAVARRIA CECILIA GABRIELA\Repositorio\CGEscalonaCha\CCEscalonaTheMovieDB\PL\PL\Views\Usuario\Email.html");
            //    mailMessage.Body = contenidoHTML;
            //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            //    smtpClient.EnableSsl = true;
            //    smtpClient.UseDefaultCredentials = false;
            //    smtpClient.Port = 587;
            //    smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, "ratgrwdndsvbtqnp");

            //    smtpClient.Send(mailMessage);
            //    smtpClient.Dispose();

            //    ViewBag.Modal = "show";
            //    ViewBag.Mensaje = "Se ha enviado un correo de confirmación a tu correo electronico";
            //    return RedirectToAction("NewPassword", "Usuario");
        }
        [HttpGet]
        public ActionResult NewPassword(string email)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Email = email;

            return View(usuario);
        }

        [HttpPost]
        public ActionResult NewPassword(ML.Usuario usuario, string password)
        {

            var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);

            var passwordHash = bcrypt.GetBytes(20);
            usuario.Password = passwordHash;

            ML.Result result = BL.Usuario.Update(usuario);

            if (result.Correct)
            {
                ViewBag.Modal = "show";
                ViewBag.Message = "Se ha actualizado correctamente";
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                ViewBag.Modal = "show";
                ViewBag.Mensaje = "Error al actualizar la contraseña";
                return View("Modal");
            }
        }

    }
}
