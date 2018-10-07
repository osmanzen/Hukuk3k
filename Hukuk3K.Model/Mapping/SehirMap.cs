using Hukuk3K.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Mapping
{
    public class SehirMap : EntityTypeConfiguration<Sehir>
    {
        public SehirMap()
        {
            ToTable("Sehir");
            HasKey(x => x.SehirId);

            Property(x => x.SehirAdi).HasMaxLength(50).IsRequired();
            //NAV

            //HasMany(x => x.Adliyeler).WithRequired(x => x.Sehir).HasForeignKey(x => x.AdliyeId);
        }
    }
}
