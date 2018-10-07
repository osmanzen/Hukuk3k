using Hukuk3K.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Entity
{
    public class DavaDosya : OrtakProperty, IEntity
    {
        public Guid DavaDosyaId { get; set; }

        public string Url { get; set; }

        public string DavaDosyaAdi { get; set; }

        public Guid DavaBaslikId { get; set; }

        public DateTime EklemeTarihi { get; set; }

        //Navigation Properties

        public virtual DavaBaslik DavaBaslik { get; set; }
    }
}
