using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcilYardimVeritabani.Models
{
    public class Operator
    {
        public int Id { get; set; }
        public string Adi { get; set; }

        public ICollection<OperatorMesaj> OperatorMesajlari { get; set; }
    }
}
