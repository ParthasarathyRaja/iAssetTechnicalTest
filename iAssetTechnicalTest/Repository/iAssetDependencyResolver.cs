using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Ninject.Web.Common;


namespace iAssetTechnicalTest.Repository.DependencyResolution
{
    public class iAssetDependencyResolver : NinjectModule
    {
        public override void Load()
        {
            Bind<IWeatherRepository>().To<WeatherRepository>();
        }
    }
}