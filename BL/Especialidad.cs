using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Especialidad
    {
        public static Dictionary<string, object> GetAll()
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", false}, {"Excepcion", ""}, {"Especialidad", null} };
            ML.Especialidad especialidade = new ML.Especialidad();

            try
            {
                using (DL.ArodriguezHospitalContext context = new DL.ArodriguezHospitalContext())
                {
                    var registros = (from especialidad in context.Especialidads
                                     select new
                                     {
                                         IdEspecialidad=especialidad.IdEspecialidad,
                                         Nombre = especialidad.Nombre
                                     }).ToList();

                    if(registros != null)
                    {
                        especialidade.Especialidades = new List<object>();
                        foreach(var registro in registros)
                        {
                            ML.Especialidad special = new ML.Especialidad();
                            special.IdEspecialidad = registro.IdEspecialidad;
                            special.Nombre = registro.Nombre;

                            especialidade.Especialidades.Add(special);
                        }

                        diccionario["Especialidad"] = especialidade;
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
                diccionario["Excepcion"] = ex.Message;
                diccionario["Resultado"] = false;
            }

            return diccionario;
        }

        public static Dictionary<string, object> GetById(int IdEspecialidad)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", false}, {"Excepcion", ""}, {"Especialidad", null} };

            try
            {
                using (DL.ArodriguezHospitalContext context = new DL.ArodriguezHospitalContext())
                {
                    var registro = (from especialidad in context.Especialidads
                                    where especialidad.IdEspecialidad == IdEspecialidad
                                    select new
                                    {
                                        IdEspecialidad = especialidad.IdEspecialidad,
                                        Nombre = especialidad.Nombre
                                    }).SingleOrDefault();

                    if(registro != null)
                    {
                        ML.Especialidad special = new ML.Especialidad();
                        special.IdEspecialidad = registro.IdEspecialidad;
                        special.Nombre = registro.Nombre;

                        diccionario["Resultado"] = true;
                        diccionario["Especialidad"] = special;
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
    }
}
