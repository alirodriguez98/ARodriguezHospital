using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Hospital
    {
        public int IdHospital { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public DateTime AnioConstruccion { get; set; }
        public int Capacidad { get; set; }
        public ML.Especialidad Especialidad { get; set; }
        public List<object> Hospitales { get; set; }
    }
}
