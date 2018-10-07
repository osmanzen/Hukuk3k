using Hukuk3K.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Mapping
{
    public class DavaBaslikMap : EntityTypeConfiguration<DavaBaslik>
    {
        public DavaBaslikMap()
        {
            ToTable("DavaBaslik");
            HasKey(x => x.DavaBaslikId);

            Property(x => x.DavaBaslikAdi).IsRequired().HasMaxLength(50);

            //NAV

            HasRequired(x => x.Dava).WithMany(x => x.DavaBasliklari).HasForeignKey(x => x.DavaId);

            //HasMany(x => x.DavaDosyalari).WithRequired(x => x.DavaBaslik).HasForeignKey(x => x.DavaDosyaId);
        }
    }
}
