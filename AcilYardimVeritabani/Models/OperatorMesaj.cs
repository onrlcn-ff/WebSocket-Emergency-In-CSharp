using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcilYardimVeritabani.Models
{
    public class OperatorMesaj
    {
        public int Id { get; set; }
        public int OperatorId { get; set; }
        public int EkipId { get; set; }
        public string Mesaj { get; set; }
        public DateTime TarihSaat { get; set; }

        public Operator Operator { get; set; }
        public Ekip Ekip { get; set; }
    }
}
