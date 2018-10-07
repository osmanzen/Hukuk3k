using Hukuk3K.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Mapping
{
    public class DavaDosyaMap : EntityTypeConfiguration<DavaDosya>
    {
        public DavaDosyaMap()
        {
            ToTable("DavaDosya");
            HasKey(x => x.DavaDosyaId);

            Property(x => x.Url).IsRequired();

            Property(x => x.DavaDosyaAdi).IsRequired().HasMaxLength(50);

            Property(x => x.EklemeTarihi).IsRequired();

            //NAV

            HasRequired(x => x.DavaBaslik).WithMany(x => x.DavaDosyalari).HasForeignKey(x => x.DavaBaslikId);
        }
    }
}
