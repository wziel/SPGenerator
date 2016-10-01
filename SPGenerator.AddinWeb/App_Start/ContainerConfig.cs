using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SPGenerator.Generator;
using SPGenerator.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPGenerator.AddinWeb.App_Start
{
    /// <summary>
    /// SimpleInjector container configuration.
    /// </summary>
    public class ContainerConfig
    {
        public static void ConfigureContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<ModelTranslator>();
            container.Register<SharePointContextHelper>();
            container.Register<SharePointService>();
            container.Register<DataGenerator>();
            container.Register<ColumnDataGeneratorFactory>();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}