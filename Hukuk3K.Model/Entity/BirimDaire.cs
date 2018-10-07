using Hukuk3K.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Entity
{
    public class BirimDaire : OrtakProperty, IEntity
    {
        public BirimDaire()
        {
            Davalar = new HashSet<Dava>();
        }

        public Guid BirimDaireId { get; set; }

        public string BirimDaireAdi { get; set; }

        public Guid DavaTipiId { get; set; }

        //Navigation Properties

        public virtual ICollection<Dava> Davalar { get; set; }

        public virtual DavaTipi DavaTipi { get; set; }
    }
}
