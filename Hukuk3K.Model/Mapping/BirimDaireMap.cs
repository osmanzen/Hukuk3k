using Hukuk3K.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Mapping
{
    public class BirimDaireMap : EntityTypeConfiguration<BirimDaire>
    {
        public BirimDaireMap()
        {
            ToTable("BirimDaire");
            HasKey(x => x.BirimDaireId);

            Property(x => x.BirimDaireAdi).IsRequired().HasMaxLength(50);

            //NAV

            HasRequired(x => x.DavaTipi).WithMany(x => x.BirimDaireler).HasForeignKey(x => x.DavaTipiId);
            //HasMany(x => x.Davalar).WithRequired(x => x.BirimDaire).HasForeignKey(x => x.DavaId);
        }
    }
}
