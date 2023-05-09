using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDominio
{
    public enum TwoFactorType
    {
        None = 0,
        App = 1,
        Email = 2,
        SMS = 3,
        Call = 4
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set;}
        public string Apellido { get; set; }
        public DateTime Fecha { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public bool TwoFactor { get; set; }
        public TwoFactorType TwoFactorType { get; set; }
        public bool Premium { get; set; }

    }
}
