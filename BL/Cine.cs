using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cine
    {

            public static ML.Result Add(ML.Cine cine)
            {
                ML.Result result = new ML.Result();
                try
                {
                    using (DL.CescalonaCineContext contex = new DL.CescalonaCineContext())
                    {
                        int RowsAfected = contex.Database.ExecuteSqlRaw($"CineAdd '{cine.Complejo}', '{cine.Direccion}', '{cine.Ventas}', {cine.Zona.IdZona}");

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
            public static ML.Result Update(ML.Cine cine)
            {
                ML.Result result = new ML.Result();
                try
                {
                    using (DL.CescalonaCineContext contex = new DL.CescalonaCineContext())
                    {
                        int RowsAfected = contex.Database.ExecuteSqlRaw($"CineUpdate {cine.IdCine}, '{cine.Complejo}', '{cine.Direccion}', '{cine.Ventas}', {cine.Zona.IdZona} ");

                        if (RowsAfected > 0)
                        {
                            result.Correct = true; 
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrio un error al Actualizar el cine";
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
            public static ML.Result Delete(int idCine)
            {
                ML.Result result = new ML.Result();
                try
                {
                    using (DL.CescalonaCineContext contex = new DL.CescalonaCineContext())
                    {
                        int RowsAfected = contex.Database.ExecuteSqlRaw($"CineDelete {idCine}");

                        if (RowsAfected > 0)
                        {
                            result.Correct = true; ;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrio un error al Elimar el cine";
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
            public static ML.Result GetAll()
            {
                ML.Result result = new ML.Result();
                try
                {
                    using (DL.CescalonaCineContext contex = new DL.CescalonaCineContext())
                    {
                        var RowsAfected = contex.Cines.FromSqlRaw("CineGetAll").ToList();

                        result.Objects = new List<object>();

                        if (contex != null)
                        {
                            foreach (var obj in RowsAfected)
                            {
                                ML.Cine cine = new ML.Cine();
                                cine.IdCine = obj.IdCine;
                                cine.Complejo = obj.Complejo;
                                cine.Direccion = obj.Direccion;
                                cine.Ventas = obj.Venta.Value;

                                ML.Zona zona = new ML.Zona();
                                zona.IdZona = obj.IdZona.Value;
                                zona.Nombre = obj.NombreZona;



                                result.Objects.Add(cine);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontraron registros.";
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
            public static ML.Result GetById(int idCine)
            {
                ML.Result result = new ML.Result();
                try
                {
                    using (DL.CescalonaCineContext contex = new DL.CescalonaCineContext())
                    {
                        var RowsAfected = contex.Cines.FromSqlRaw($"CineGetById {idCine}").AsEnumerable().FirstOrDefault();

                        result.Object = new object();

                        if (RowsAfected != null)
                        {
                            ML.Cine cine = new ML.Cine();
                            cine.IdCine = RowsAfected.IdCine;
                            cine.Complejo = RowsAfected.Complejo;
                            cine.Direccion = RowsAfected.Direccion;
                            cine.Ventas = RowsAfected.Venta.Value;

                            ML.Zona zona = new ML.Zona();
                            zona.IdZona = RowsAfected.IdZona.Value;
                            zona.Nombre = RowsAfected.NombreZona;

                            result.Object = cine;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al obtener el registros en la tabla Cine";
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
        }
}
