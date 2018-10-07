using Hukuk3K.Model.Entity;
using Hukuk3K.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.DAL.Concrete.EntityFramework.Context
{
    public class Hukuk3KDBContext : DbContext
    {
        public Hukuk3KDBContext()
            : base(nameOrConnectionString: "Data Source=184.168.194.70;Integrated Security=False;User ID=3khukuk;Password=3khukuk@;Connect Timeout=15;Encrypt=False;Packet Size=4096;MultipleActiveResultSets=true")
        {

        }

        public DbSet<Adliye> Adliyeler { get; set; }

        public DbSet<BirimDaire> BirimDaireler { get; set; }

        public DbSet<Dava> Davalar { get; set; }

        public DbSet<DavaBaslik> DavaBasliklari { get; set; }

        public DbSet<DavaDosya> DavaDosyalari { get; set; }

        public DbSet<DavaDurum> DavaDurum { get; set; }

        public DbSet<DavaTipi> DavaTipleri { get; set; }

        public DbSet<Kullanici> Kullanicilar { get; set; }

        public DbSet<Sehir> Sehirler { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdliyeMap());
            modelBuilder.Configurations.Add(new BirimDaireMap());
            modelBuilder.Configurations.Add(new DavaMap());
            modelBuilder.Configurations.Add(new DavaBaslikMap());
            modelBuilder.Configurations.Add(new DavaDosyaMap());
            modelBuilder.Configurations.Add(new DavaDurumMap());
            modelBuilder.Configurations.Add(new DavaTipiMap());
            modelBuilder.Configurations.Add(new KullaniciMap());
            modelBuilder.Configurations.Add(new SehirMap());
        }
    }
}
