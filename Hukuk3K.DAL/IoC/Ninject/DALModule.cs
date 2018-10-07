using Hukuk3K.DAL.Concrete.EntityFramework.Context;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hukuk3K.DAL.IoC.Ninject
{
    public class DALModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<DbContext>().To<Hukuk3KDBContext>().InSingletonScope();
        }
    }
}
