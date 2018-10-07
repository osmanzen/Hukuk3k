using Hukuk3K.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.Model.Mapping
{
    public class DavaDurumMap : EntityTypeConfiguration<DavaDurum>
    {
        public DavaDurumMap()
        {
            ToTable("DavaDurum");
            HasKey(x => x.DavaDurumId);

            Property(x => x.DavaDurumAdi).IsRequired().HasMaxLength(50);

            //NAV

            //HasMany(x => x.Davalar).WithRequired(x => x.DavaDurum).HasForeignKey(x => x.DavaDurumId);
        }
    }
}
