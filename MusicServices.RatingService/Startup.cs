
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using MusicServices.Data;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using MusicPlayerWithStars.Domain.Data;

namespace MusicServices.RatingService
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var kernel = new StandardKernel();
            var resolver = new NinjectSignalRDependencyResolver(kernel);
            
            kernel.Bind<IMusicRatingRepository>().To<MongoDbMusicRatingRepository>().InSingletonScope();

            var config = new HubConfiguration();
            config.Resolver = resolver;

            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR(config);
        }
    }

    internal class NinjectSignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectSignalRDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
        }
    }
 
}
