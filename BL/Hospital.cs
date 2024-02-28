using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Hospital
    {
        public static Dictionary<string, object> GetAll()
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", false}, {"Excepcion", ""}, {"Hospital", null} };
            ML.Hospital hosp = new ML.Hospital();

            try
            {
                using (DL.ArodriguezHospitalContext context = new DL.ArodriguezHospitalContext())
                {
                    var registros = (from hospital in context.Hospitals
                                     join especialidad in context.Especialidads on hospital.IdEspecialidad equals especialidad.IdEspecialidad
                                     select new
                                     {
                                         IdHospital = hospital.IdHospital,
                                         Nombre = hospital.Nombre,
                                         Direccion = hospital.Direccion,
                                         AnioConstruccion = hospital.AnioConstruccion,
                                         Capacidad = hospital.Capacidad,
                                         IdEspecialidad = especialidad.IdEspecialidad,
                                         NombreEspecialidad = especialidad.Nombre
                                     }).ToList();

                    if(registros != null)
                    {
                        hosp.Hospitales = new List<object>();
                        foreach (var registro in registros)
                        {
                            ML.Hospital hospitali = new ML.Hospital();
                            hospitali.IdHospital = registro.IdHospital;
                            hospitali.Nombre = registro.Nombre;
                            hospitali.Direccion = registro.Direccion;
                            hospitali.AnioConstruccion = registro.AnioConstruccion;
                            hospitali.Capacidad = registro.Capacidad;
                            hospitali.Especialidad = new ML.Especialidad();
                            hospitali.Especialidad.IdEspecialidad = registro.IdEspecialidad;
                            hospitali.Especialidad.Nombre = registro.NombreEspecialidad;

                            hosp.Hospitales.Add(hospitali);
                        }

                        diccionario["Resultado"] = true;
                        diccionario["Hospital"] = hosp;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Excepcion"] = ex.Message;
                diccionario["Resultado"] = false;
            }

            return diccionario;
        }

        public static Dictionary<string, object> GetById(int idHospital)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", false}, {"Excepcion", ""}, {"Hospital", null} };

            try
            {
                using (DL.ArodriguezHospitalContext context = new DL.ArodriguezHospitalContext())
                {
                    var registro = (from hospital in context.Hospitals
                                    join especialidad in context.Especialidads on hospital.IdEspecialidad equals especialidad.IdEspecialidad
                                    where hospital.IdHospital == idHospital
                                    select new
                                    {
                                        IdHospital = hospital.IdHospital,
                                        Nombre = hospital.Nombre,
                                        Direccion = hospital.Direccion,
                                        AnioConstruccion = hospital.AnioConstruccion,
                                        Capacidad = hospital.Capacidad,
                                        IdEspecialidad = especialidad.IdEspecialidad,
                                        NombreEspecialidad = especialidad.Nombre
                                    }).SingleOrDefault();

                    if(registro != null)
                    {
                        ML.Hospital hospitali = new ML.Hospital();
                        hospitali.IdHospital = registro.IdHospital;
                        hospitali.Nombre = registro.Nombre;
                        hospitali.Direccion = registro.Direccion;
                        hospitali.AnioConstruccion = registro.AnioConstruccion;
                        hospitali.Capacidad = registro.Capacidad;
                        hospitali.Especialidad = new ML.Especialidad();
                        hospitali.Especialidad.IdEspecialidad = registro.IdEspecialidad;
                        hospitali.Especialidad.Nombre = registro.NombreEspecialidad;

                        diccionario["Resultado"] = true;
                        diccionario["Hospital"] = hospitali;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Excepcion"] = ex.Message;
                diccionario["Resultado"] = false;
            }

            return diccionario;
        }

        public static Dictionary<string, object> Add(ML.Hospital hospital)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", false}, {"Excepcion", ""} };

            try
            {
                using (DL.ArodriguezHospitalContext context = new DL.ArodriguezHospitalContext())
                {
                    var filasAfectadas = context.Database.ExecuteSqlRaw($"HospitalAdd '{hospital.Nombre}', '{hospital.Direccion}', '{hospital.AnioConstruccion.ToString("dd-MM-yyyy")}', " +
                        $"{hospital.Capacidad}, {hospital.Especialidad.IdEspecialidad}");

                    if(filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }

            return diccionario;
        }

        public static Dictionary<string, object> Delete(int idHospital)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Resultado", false }, { "Excepcion", "" } };

            try
            {
                using (DL.ArodriguezHospitalContext context = new DL.ArodriguezHospitalContext())
                {
                    var filasAfectadas = context.Database.ExecuteSqlRaw($"HospitalDelete {idHospital}");

                    if(filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }

            return diccionario;
        }

        public static Dictionary<string, object> Update(ML.Hospital hospital)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Resultado", false }, { "Excepcion", "" } };

            try
            {
                using (SqlConnection context = new SqlConnection(DL.ARodriguezHospitalConnection.GetConnectionString()))
                {
                    string sentencia = "HospitalUpdate";

                    SqlParameter[] parametros = new SqlParameter[6];
                    parametros[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    parametros[0].Value = hospital.Nombre;
                    parametros[1] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                    parametros[1].Value = hospital.Direccion;
                    parametros[2] = new SqlParameter("@AnioConstruccion", SqlDbType.DateTime);
                    parametros[2].Value = hospital.AnioConstruccion.ToString("dd-MM-yyyy");
                    parametros[3] = new SqlParameter("@Capacidad", SqlDbType.Int);
                    parametros[3].Value = hospital.Capacidad;
                    parametros[4] = new SqlParameter("@IdEspecialidad", SqlDbType.Int);
                    parametros[4].Value = hospital.Especialidad.IdEspecialidad;
                    parametros[5] = new SqlParameter("@IdHospital", SqlDbType.Int);
                    parametros[5].Value = hospital.IdHospital;

                    SqlCommand command = new SqlCommand(sentencia, context);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddRange(parametros);

                    command.Connection.Open();

                    int filasAfectadas = command.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }

            return diccionario;
        }
    }
}
