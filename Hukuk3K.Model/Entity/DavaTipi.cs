using Hukuk3K.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Entity
{
    public class DavaTipi : OrtakProperty, IEntity
    {
        public DavaTipi()
        {
            BirimDaireler = new HashSet<BirimDaire>();
        }

        public Guid DavaTipiId { get; set; }

        public string DavaTipiAdi { get; set; }

        public Guid AdliyeId { get; set; }

        //Navigation Properties

        public virtual Adliye Adliye { get; set; }

        public virtual ICollection<BirimDaire> BirimDaireler { get; set; }
    }
}
