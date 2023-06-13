﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;

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
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"UsuarioAdd '{cine.Nombre}', '{cine.ApellidoPaterno}', '{cine.ApellidoMaterno}','{cine.Email}','{cine.UserName}', @Password", new SqlParameter("@Password", cine.Password));

                    if (RowsAfected > 0)
                    {
                        result.Correct = true; 
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
                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.Nombre = objUsuario.Nombre;
                        usuario.UserName = objUsuario.UserName;
                        usuario.Email = objUsuario.Email;
                        usuario.Password = objUsuario.Contrasena;



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
        public static ML.Result GetByEmail(string email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CescalonaCineContext context = new DL.CescalonaCineContext())
                {
                    var obj = context.Usuarios.FromSqlRaw($"UsuarioGetById '{email}' ").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = obj.IdUsuario;
                        usuario.UserName = obj.UserName;
                        usuario.Email = obj.Email;
                        usuario.Password = obj.Contrasena; 

                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo realizar la consulta";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
