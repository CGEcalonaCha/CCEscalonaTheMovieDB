using Microsoft.AspNetCore.Mvc;
using ML;
using System.Text.Json.Nodes;

namespace PL.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            ML.Movie resultMovi = new ML.Movie();
            resultMovi.Objects = new List<object>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                var responceTask = cliente.GetAsync("movie/popular?api_key=401526372779a487928a18f653d2ee6d&language=en-US&page=2");
                responceTask.Wait();
                var result = responceTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Movie>();

                    readTask.Wait();
                    foreach (var resulItem in readTask.Result.results)
                    {
                        ML.Movie resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Movie>(resulItem.ToString());
                        resultMovi.Objects.Add(resultItemList);


                    }
                    ML.Movie movie = new ML.Movie();
                    movie.Movies = resultMovi.Objects;
                    return View(movie);
                }

                return View(resultMovi);
            }
        }
        [HttpPost] //Añadir a Favoritos
        public ActionResult Form(ML.Movie idPelicula)
        {
            if (idPelicula != null)
            {
                ML.Favorite favorite = new ML.Favorite();
                favorite.media_id = idPelicula.id;
                favorite.media_type = "movie";
                favorite.favorite = true;

                ML.Movie resultWebApi = new ML.Movie();
                resultWebApi.Objects = new List<object>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.themoviedb.org/3/account/19729267/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Favorite>("favorite?session_id=968c620908c4ff2f89b1adc20c5dfddb53112139&api_key=401526372779a487928a18f653d2ee6d", favorite);
                    postTask.Wait();

                    var resultUsuario = postTask.Result;
                    if (resultUsuario.IsSuccessStatusCode)
                    {

                        return RedirectToAction("GetAll");

                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al insertar el registro" + " ";
                    }
                    ViewBag.Message = "El resigistro de Usuario a sido agrgado con exito";
                }
            }

            return View();
        }
        public IActionResult GetFavorite()
        {
            ML.Movie resultMovi = new ML.Movie();
            resultMovi.Objects = new List<object>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                var responceTask = cliente.GetAsync("account/19728058/favorite/movies?api_key=c0387e98c8c3342904d47ecf8e243d5c&language=en-US&page=1&session_id=ab17b03821f2d09b97d85e2472d40f86a3fe8c2f&sort_by=created_at.asc");
                responceTask.Wait();
                var result = responceTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Movie>();

                    readTask.Wait();
                    foreach (var resulItem in readTask.Result.results)
                    {
                        ML.Movie resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Movie>(resulItem.ToString());
                        resultMovi.Objects.Add(resultItemList);


                    }
                    ML.Movie movie = new ML.Movie();
                    movie.Movies = resultMovi.Objects;
                    return View(movie);
                }

                return View(resultMovi);
            }
        }
        [HttpPost] //Añadir a Favoritos
        public ActionResult FormDelete(ML.Movie idPelicula)
        {
            if (idPelicula != null)
            {
                ML.Favorite favorite = new ML.Favorite();
                favorite.media_id = idPelicula.id;
                favorite.media_type = "movie";
                favorite.favorite = false;

                ML.Movie resultWebApi = new ML.Movie();
                resultWebApi.Objects = new List<object>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.themoviedb.org/3/account/19729267/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Favorite>("favorite?session_id=968c620908c4ff2f89b1adc20c5dfddb53112139&api_key=401526372779a487928a18f653d2ee6d", favorite);
                    postTask.Wait();

                    var resultUsuario = postTask.Result;
                    if (resultUsuario.IsSuccessStatusCode)
                    {

                        return RedirectToAction("GetAllFavorites");

                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al insertar el registro" + " ";
                    }
                    ViewBag.Message = "El resigistro de Usuario a sido agrgado con exito";
                }
            }

            return View();
        }
        //public IActionResult GetFavorite()
        //{
        //    ML.Movie resultMovi = new ML.Movie();
        //    resultMovi.Objects = new List<object>();

        //    using (var cliente = new HttpClient())
        //    {
        //        cliente.BaseAddress = new Uri("https://api.themoviedb.org/3/");
        //        var responceTask = cliente.GetAsync("account/19728058/favorite/movies?api_key=401526372779a487928a18f653d2ee6d&language=en-US&page=2");
        //        responceTask.Wait();
        //        var result = responceTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsAsync<ML.Movie>();

        //            readTask.Wait();
        //            foreach (var resulItem in readTask.Result.results)
        //            {
        //                ML.Movie resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Movie>(resulItem.ToString());
        //                resultMovi.Objects.Add(resultItemList);


        //            }
        //            ML.Movie movie = new ML.Movie();
        //            movie.Movies = resultMovi.Objects;
        //            return View(movie);
        //        }

        //        return View(resultMovi);
        //    }
        //}



        //public IActionResult GetFavoritos()
        //{
        //    ML.Movie movi = new ML.Movie();

        //    using (var cliente = new HttpClient())
        //    {
        //        cliente.BaseAddress = new Uri("https://api.themoviedb.org/3/");
        //        var responceTask = cliente.GetAsync("account/19728058/favorite/movies?api_key=c0387e98c8c3342904d47ecf8e243d5c");
        //        responceTask.Wait();
        //        var result = responceTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsStringAsync();
        //            dynamic resultJson = JsonObject.Parse(readTask.Result.ToString());
        //            readTask.Wait();
        //            movi.Movies = new List<object>();
        //            foreach (var resulItem in readTask.Result.)
        //            {


        //                movi.Movies.Add(resulItem);
        //            }
        //        }
        //    }

        //    return View(movi);

    } 
}
