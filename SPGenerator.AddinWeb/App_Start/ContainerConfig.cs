using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SPGenerator.AddinWeb.ViewModels.Home;
using SPGenerator.Generator;
using SPGenerator.SharePoint;
using SPGenerator.SharePoint.ColumnMapping;
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
            container.Register<IIndexVMFactory, IndexVMFactory>();
            container.Register<ISharePointContextHelper, SharePointContextHelper>();
            container.Register<ISharePointService, SharePointService>();
            container.Register<IDataGenerator, DataGenerator>();
            container.Register<IColumnDataGeneratorFactory, ColumnDataGeneratorFactory>();
            container.Register<IColumnMappingResolver, ColumnMappingResolver>();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}