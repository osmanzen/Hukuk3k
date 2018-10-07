using Hukuk3K.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Entity
{
    public class DavaBaslik : OrtakProperty, IEntity
    {
        public DavaBaslik()
        {
            DavaDosyalari = new HashSet<DavaDosya>();
        }

        public Guid DavaBaslikId { get; set; }

        public string DavaBaslikAdi { get; set; }

        public Guid DavaId { get; set; }

        //Navigation Properties

        public virtual Dava Dava { get; set; }

        public virtual ICollection<DavaDosya> DavaDosyalari { get; set; }
    }
}
