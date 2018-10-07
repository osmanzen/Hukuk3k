using Hukuk3K.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Entity
{
    public class Kullanici : OrtakProperty, IEntity
    {
        public Kullanici()
        {
            Davalar = new HashSet<Dava>();
        }

        public string TCKimlikNo { get; set; }

        public string Sifre { get; set; }

        public string AdSoyad { get; set; }

        public string EMail { get; set; }

        public string Tel { get; set; }

        public string Adres { get; set; }

        public Boolean Admin { get; set; }

        //Navigation Properties

        public virtual ICollection<Dava> Davalar { get; set; }
    }
}
