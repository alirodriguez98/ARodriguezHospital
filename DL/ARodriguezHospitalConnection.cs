using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class ARodriguezHospitalConnection
    {
        public static string GetConnectionString()
        {
            return "Server=.; Database=ARodriguezHospital; Trusted_Connection=True; TrustServerCertificate=True; User ID=sa; Password=pass@word1;";
        }
    }
}
