using Hukuk3K.Core.DataAccess.EntityFramework;
using Hukuk3K.DAL.Abstract;
using Hukuk3K.DAL.Concrete.EntityFramework.Context;
using Hukuk3K.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.DAL.Concrete.EntityFramework.Entities
{
    public class EFKullaniciDAL : EFEntityRepositoryBase<Kullanici, Hukuk3KDBContext>, IKullaniciDAL
    {
        public EFKullaniciDAL(DbContext context) : base(context)
        {

        }
    }
}
