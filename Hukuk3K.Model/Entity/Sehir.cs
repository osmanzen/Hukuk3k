using Hukuk3K.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Entity
{
    public class Sehir : OrtakProperty, IEntity
    {
        public Sehir()
        {
            Adliyeler = new HashSet<Adliye>();
        }

        public Guid SehirId { get; set; }

        public string SehirAdi { get; set; }

        //Navigation Properties

        public virtual ICollection<Adliye> Adliyeler { get; set; }
    }
}
