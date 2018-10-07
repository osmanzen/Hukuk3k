using Hukuk3K.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Mapping
{
    public class KullaniciMap : EntityTypeConfiguration<Kullanici>
    {
        public KullaniciMap()
        {
            ToTable("Kullanici");
            HasKey(c => c.TCKimlikNo);

            Property(x => x.TCKimlikNo).HasMaxLength(11);
            Property(c => c.Adres).IsRequired();
            Property(c => c.Sifre).IsRequired().HasMaxLength(20);
            Property(c => c.AdSoyad).IsRequired().HasMaxLength(50);
            Property(c => c.EMail).IsRequired().HasMaxLength(50);
            Property(c => c.Tel).IsRequired().HasMaxLength(20);
            Property(c => c.Admin).IsRequired();

            //NAV

            //HasMany(x => x.Davalar).WithRequired(x => x.Kullanici).HasForeignKey(x => x.DavaId);
        }
    }
}
