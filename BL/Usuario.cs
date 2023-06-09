using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario cine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CescalonaCineContext contex = new DL.CescalonaCineContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"UsuarioAdd '{cine.Nombre}', '{cine.ApellidoPaterno}', '{cine.ApellidoMaterno}','{cine.Email}','{cine.UserName}','{cine.PassWord}' ");

                    if (RowsAfected > 0)
                    {
                        result.Correct = true; ;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al ingresar el cine";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result GetByUserName(string userName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CescalonaCineContext context = new DL.CescalonaCineContext())
                {
                    var objUsuario = context.Usuarios.FromSqlRaw($"UsuarioGetByUserName {userName} ").AsEnumerable().FirstOrDefault();

                    //result.Objects = new List<object>();

                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.UserName = objUsuario.UserName;
                        //usuario.PassWord = objUsuario.PassWord;



                        result.Object = usuario; //boxing


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = true;
                        result.ErrorMessage = "Ocurrio un erro al obtener los regristro en la tabla";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = !false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
