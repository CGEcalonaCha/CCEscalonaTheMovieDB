using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Cine cine = new ML.Cine();
            cine.Cines = new List<object>();
            ML.Result result = new ML.Result();
            cine.Zona = new ML.Zona();
            cine.Zona.Zonas = new List<object>();
            cine.Zona.Zonas = result.Objects;
            result = BL.Cine.GetAll();
            if (result.Correct)
            {
                cine.Cines = result.Objects;
            }
            return View(cine);
        }
        [HttpGet]
        public IActionResult Form(int? idCine)
        {
            ML.Cine cine = new ML.Cine();
            ML.Result result = new ML.Result();
            cine.Zona = new ML.Zona();
            cine.Zona.Zonas = new List<object>();
            result = BL.Zona.GetAll();
            cine.Zona = new ML.Zona();
            cine.Zona.Zonas = result.Objects;
            if (result.Correct)
            {

                cine.Zona.Zonas = result.Objects;
            }
            if (idCine == null)
            {

                return View(cine);
            }
            else
            {
                ML.Result resultGetById = new ML.Result();
                resultGetById = BL.Cine.GetById(idCine.Value);
                if (resultGetById.Correct)
                {
                    cine = (ML.Cine)resultGetById.Object;
                    cine.Zona = new ML.Zona();
                    cine.Zona.Zonas = result.Objects;
                }
                return View(cine);
            }
        }
        [HttpPost]
        public IActionResult Form(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            if (cine.IdCine == 0)
            {
                result = BL.Cine.Add(cine);
                if (result.Correct)
                {
                    ViewBag.Message = "El cine Se Agrego Correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio Un Error Al Agregar El Cine" + result.ErrorMessage;
                }
            }
            else
            {
                result = BL.Cine.Update(cine);
                if (result.Correct)
                {
                    ViewBag.Message = "El cine Se Actualizo Correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio Un Error Al Actualizar El Cine" + result.ErrorMessage;
                }
            }
            return View("Modal");
        }
        public IActionResult Delete(int idCine)
        {
            ML.Result result = new ML.Result();
            result = BL.Cine.Delete(idCine);
            if (result.Correct)
            {
                ViewBag.Message = "El cine Se Elimino Correctamente";
            }
            else
            {
                ViewBag.Message = "Ocurrio Un Error Al Elimar El Cine" + result.ErrorMessage;
            }
            return View("Modal");
        }
        //public IActionResult GetGraphs()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult GetGraphs(ML.Cine cineEstadisticas)
        {
            //ML.Cine cineVentas = new ML.Cine();
            ML.Cine Estadisticas = new ML.Cine();
            Estadisticas.EstadisticaCine = new ML.Estadistica();
            cineEstadisticas.Cines = new List<object>();
            cineEstadisticas.Cines = new List<object>();
            decimal total = 0;
            decimal sur = 0;
            decimal oriente = 0;
            decimal poniente = 0;
            decimal norte = 0;
            ML.Result resultCines = BL.Cine.GetAll();
            if (resultCines.Correct)
            {
                cineEstadisticas.Cines = resultCines.Objects;
                foreach (ML.Cine cine in cineEstadisticas.Cines)
                {

                    total = (total + cine.Ventas);
                }
                foreach (ML.Cine Zona in cineEstadisticas.Cines)
                {
                    if (Zona.Zona.IdZona == 1)
                    {
                        sur = (sur + Zona.Ventas);
                    }
                    else
                    {
                        if (Zona.Zona.IdZona == 2)
                        {
                            oriente = (oriente + Zona.Ventas);

                        }
                        else
                        {
                            if (Zona.Zona.IdZona == 3)
                            {
                                poniente = (poniente + Zona.Ventas);

                            }
                            else
                            {
                                norte = (norte + Zona.Ventas);

                            }
                        }
                    }


                }
                Estadisticas.EstadisticaCine.Sur = ((sur / total) * 100);
                Estadisticas.EstadisticaCine.Norte = ((oriente / total) * 100);
                Estadisticas.EstadisticaCine.Poniete = ((poniente / total) * 100);
                Estadisticas.EstadisticaCine.Oriente = ((norte / total) * 100);



            }
            return View(Estadisticas);
        }
    }

}
