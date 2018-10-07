using Hukuk3K.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Mapping
{
    public class DavaTipiMap : EntityTypeConfiguration<DavaTipi>
    {
        public DavaTipiMap()
        {
            ToTable("DavaTipi");
            HasKey(x => x.DavaTipiId);

            Property(x => x.DavaTipiAdi).IsRequired().HasMaxLength(50);

            //NAV

            HasRequired(x => x.Adliye).WithMany(x => x.DavaTipleri).HasForeignKey(x => x.AdliyeId);

            //HasMany(x => x.BirimDaireler).WithRequired(x => x.DavaTipi).HasForeignKey(x => x.BirimDaireId);
        }
    }
}
