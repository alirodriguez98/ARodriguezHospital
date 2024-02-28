using BL;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class HospitalController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            Dictionary<string, object> result = BL.Hospital.GetAll();
            bool resultado = (bool)result["Resultado"];

            if (resultado)
            {
                ML.Hospital hospital = (ML.Hospital)result["Hospital"];
                return View(hospital);
            }

            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Form(int? IdHospital)
        {
            ML.Hospital hospital = new ML.Hospital();

            if(IdHospital != null)
            {
                Dictionary<string, object> resultHospital = BL.Hospital.GetById(IdHospital.Value);
                bool resultadoHospital = (bool)resultHospital["Resultado"];

                if (resultadoHospital)
                {
                    Dictionary<string, object> resultEspecialidad = BL.Especialidad.GetAll();
                    bool resultadoEspecialidad = (bool)resultEspecialidad["Resultado"];

                    if (resultadoEspecialidad)
                    {
                        hospital = (ML.Hospital)resultHospital["Hospital"];
                        hospital.Especialidad = (ML.Especialidad)resultEspecialidad["Especialidad"];
                        return View(hospital);
                    }
                    else
                    {
                        string excepcion = (string)resultHospital["Excepcion"];
                        ViewBag.Mensaje = "Ha ocurrido una excepcion: " + excepcion;
                        return PartialView("Modal");
                    }
                }
                else
                {
                    string excepcion = (string)resultHospital["Excepcion"];
                    ViewBag.Mensaje = "Ha ocurrido una excepcion: " + excepcion;
                    return PartialView("Modal");
                }
            }
            else
            {
                Dictionary<string, object> resultEspecialidad = BL.Especialidad.GetAll();
                hospital.Especialidad = (ML.Especialidad)resultEspecialidad["Especialidad"];
                return View(hospital);
            }
        }


        [HttpPost]
        public IActionResult Form(ML.Hospital hospital)
        {
            if(hospital.IdHospital > 0)
            {
                Dictionary<string, object> result = BL.Hospital.Update(hospital);
                bool resultado = (bool)result["Resultado"];
                if (resultado)
                {
                    ViewBag.Mensaje = "Se ha actualizado con exito el registro";
                    return PartialView("Modal");
                }
                else
                {
                    string excepcion = (string)result["Excepcion"];
                    ViewBag.Mensaje = "Ha ocurrido una excepcion: " + excepcion;
                    return PartialView("Modal");
                }
            }
            else
            {
                Dictionary<string, object> result = BL.Hospital.Add(hospital);
                bool resultado = (bool)result["Resultado"];
                if (resultado)
                {
                    ViewBag.Mensaje = "Se ha agregado con exito el registro";
                    return PartialView("Modal");
                }
                else
                {
                    string excepcion = (string)result["Excepcion"];
                    ViewBag.Mensaje = "Ha ocurrido una excepcion: " + excepcion;
                    return PartialView("Modal");
                }
            }
        }

        [HttpGet]
        public IActionResult Delete(int IdHospital)
        {
            Dictionary<string, object> result = BL.Hospital.Delete(IdHospital);
            bool resultado = (bool)result["Resultado"];

            if (resultado)
            {
                ViewBag.Mensaje = "Se ha eliminado con exito el registro";
                return PartialView("Modal");
            }
            else
            {
                string excepcion = (string)result["Excepcion"];
                ViewBag.Mensaje = "Ha ocurrido una excepcion: " + excepcion;
                return PartialView("Modal");
            }
        }
    }
}
