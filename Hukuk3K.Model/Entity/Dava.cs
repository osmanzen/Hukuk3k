using Hukuk3K.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Entity
{
    public class Dava : OrtakProperty, IEntity
    {
        public Dava()
        {
            DavaBasliklari = new HashSet<DavaBaslik>();
        }

        public Guid DavaId { get; set; }

        public string DosyaNo { get; set; }

        public Guid BirimDaireId { get; set; }

        public string TCKimlikNo { get; set; }

        public Guid DavaDurumId { get; set; }

        public DateTime AcilisTarihi { get; set; }

        //Navigation Properties

        public virtual BirimDaire BirimDaire { get; set; }

        public virtual DavaDurum DavaDurum { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual ICollection<DavaBaslik> DavaBasliklari { get; set; }

    }
}
