using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcilYardimVeritabani.Models
{
    public class EkipGeriDonus
    {
        public int Id { get; set; }
        public int EkipId { get; set; }
        public string GeriDonusMesaji { get; set; }
        public DateTime TarihSaat { get; set; }

        public Ekip Ekip { get; set; }
    }
}
