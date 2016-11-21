using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SPGenerator.AddinWeb.ViewModels.Home;
using SPGenerator.Generator;
using SPGenerator.Generator.ColumnDataGenerator.Boolean;
using SPGenerator.Generator.ColumnDataGenerator.Choice;
using SPGenerator.Generator.ColumnDataGenerator.Currency;
using SPGenerator.Generator.ColumnDataGenerator.DateTime;
using SPGenerator.Generator.ColumnDataGenerator.MultilineText;
using SPGenerator.Generator.ColumnDataGenerator.Number;
using SPGenerator.Generator.ColumnDataGenerator.Shared;
using SPGenerator.Generator.ColumnDataGenerator.Text;
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
            ConfigureAddinWeb(container);
            ConfigureSharePoint(container);
            ConfigureDataGenertator(container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void ConfigureAddinWeb(Container container)
        {
            container.Register<IIndexVMFactory, IndexVMFactory>();
        }

        private static void ConfigureDataGenertator(Container container)
        {
            container.Register<IDataGenerator, DataGenerator>();
            container.Register<IColumnDataGeneratorFactory, ColumnDataGeneratorFactory>();
            container.RegisterCollection<IBooleanDataGenerator>(new[] {
                typeof(FalseBooleanDataGenerator),
                typeof(TrueBooleanDataGenerator),
                typeof(RandomBooleanDataGenerator),
                typeof(NullDataGenerator),
            });
            container.RegisterCollection<IChoiceDataGenerator>(new[] {
                typeof(RandomChoiceDataGenerator),
                typeof(NullDataGenerator),
            });
            container.RegisterCollection<ICurrencyDataGenerator>(new[] {
                typeof(BoundaryDoubleDataGenerator),
                typeof(BoundaryIntegerDataGenerator),
                typeof(NegativeRandomDoubleDataGenerator),
                typeof(NegativeRandomIntegerDataGenerator),
                typeof(PositiveRandomDoubleDataGenerator),
                typeof(PositiveRandomIntegerDataGenerator),
                typeof(RandomDoubleDataGenerator),
                typeof(RandomIntegerDataGenerator),
                typeof(NullDataGenerator),
            });
            container.RegisterCollection<IDateTimeDataGenerator>(new[] {
                typeof(BoundaryDateTimeDataGenerator),
                typeof(RandomDateTimeDataGenerator),
                typeof(NullDataGenerator),
            });
            container.RegisterCollection<IMultilineTextDataGenerator>(new[] {
                typeof(DbPlainMultilineTextDataGenerator),
                typeof(NullDataGenerator),
            });
            container.RegisterCollection<INumberDataGenerator>(new[] {
                typeof(BoundaryDoubleDataGenerator),
                typeof(BoundaryIntegerDataGenerator),
                typeof(NegativeRandomDoubleDataGenerator),
                typeof(NegativeRandomIntegerDataGenerator),
                typeof(PositiveRandomDoubleDataGenerator),
                typeof(PositiveRandomIntegerDataGenerator),
                typeof(RandomDoubleDataGenerator),
                typeof(RandomIntegerDataGenerator),
                typeof(NullDataGenerator),
            });
            container.RegisterCollection<ITextDataGenerator>(new[] {
                typeof(DbPlainTextDataGenerator),
                typeof(NullDataGenerator),
            });
        }

        private static void ConfigureSharePoint(Container container)
        {
            container.Register<ISharePointContextHelper, SharePointContextHelper>();
            container.Register<ISharePointService, SharePointService>();
            container.Register<IColumnMappingResolver, ColumnMappingResolver>();
        }
    }
}