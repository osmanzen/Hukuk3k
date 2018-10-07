using Hukuk3K.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Mapping
{
    public class AdliyeMap : EntityTypeConfiguration<Adliye>
    {
        public AdliyeMap()
        {
            ToTable("Adliye");
            HasKey(x => x.AdliyeId);

            Property(x => x.AdliyeAdi).IsRequired().HasMaxLength(50);

            //NAV

            HasRequired(x => x.Sehir).WithMany(x => x.Adliyeler).HasForeignKey(x => x.SehirId);
            //HasMany(x => x.DavaTipleri).WithRequired(x => x.Adliye).HasForeignKey(x => x.DavaTipiId);
        }
    }
}
