using Hukuk3K.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Entity
{
    public class DavaDurum : OrtakProperty, IEntity
    {
        public DavaDurum()
        {
            Davalar = new HashSet<Dava>();
        }

        public Guid DavaDurumId { get; set; }

        public string DavaDurumAdi { get; set; }

        //Navigation Properties

        public virtual ICollection<Dava> Davalar { get; set; }
    }
}
