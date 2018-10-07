using Hukuk3K.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Entity
{
    public class Adliye : OrtakProperty, IEntity
    {
        public Adliye()
        {
            DavaTipleri = new HashSet<DavaTipi>();
        }

        public Guid AdliyeId { get; set; }

        public string AdliyeAdi { get; set; }

        public Guid SehirId { get; set; }

        //Navigation Properties

        public virtual Sehir Sehir { get; set; }

        public virtual ICollection<DavaTipi> DavaTipleri { get; set; }
    }
}