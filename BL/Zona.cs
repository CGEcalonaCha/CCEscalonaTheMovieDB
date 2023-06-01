using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.CescalonaCineContext context = new DL.CescalonaCineContext())
                {
                    var query = context.Zonas.FromSqlRaw($"ZonaGetAll");

                    result.Objects = new List<object>();

                    foreach (var obj in query)
                    {

                        ML.Zona rol = new ML.Zona();

                        rol.IdZona = obj.IdZona;
                        rol.Nombre = obj.Nombre;

                        result.Objects.Add(obj);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
