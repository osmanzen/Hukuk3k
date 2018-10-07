using Hukuk3K.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Mapping
{
    public class DavaMap : EntityTypeConfiguration<Dava>
    {
        public DavaMap()
        {
            ToTable("Dava");
            HasKey(x => x.DavaId);

            Property(x => x.AcilisTarihi).IsRequired();

            Property(x => x.DosyaNo).IsRequired().HasMaxLength(50);

            //NAV

            HasRequired(x => x.BirimDaire).WithMany(x => x.Davalar).HasForeignKey(x => x.BirimDaireId);

            HasRequired(x => x.DavaDurum).WithMany(x => x.Davalar).HasForeignKey(x => x.DavaDurumId);

            HasRequired(x => x.Kullanici).WithMany(x => x.Davalar).HasForeignKey(x => x.TCKimlikNo);

            //HasMany(x => x.DavaBasliklari).WithRequired(x => x.Dava).HasForeignKey(x => x.DavaBaslikId);
        }
    }
}
