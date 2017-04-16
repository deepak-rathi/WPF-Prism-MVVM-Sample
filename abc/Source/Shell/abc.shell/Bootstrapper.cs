using abc.Domain;
using abc.Domain.Interfaces;
using abc.infrastructure.Constants;
using abc.infrastructure.Interfaces;
using abc.infrastructure.Services;
using abc.Persistence;
using abc.shell.Views;
using Prism.Logging;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace abc.shell
{
    class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// The shell object
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<MainWindow>();
        }

        /// <summary>
        /// Initialize shell (MainWindow)
        /// </summary>
        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Configure the container
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //Localizer - Service
            Container.RegisterInstance(typeof(ILocalizerService),
                ServiceNames.LocalizerService,
                new LocalizerService("en-US"),
                new Microsoft.Practices.Unity.ContainerControlledLifetimeManager());

            Container.RegisterInstance(typeof(IStockService),
                ServiceNames.StockService,
                new StockService(new StockRepository()),
                new Microsoft.Practices.Unity.ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Configure the module catalog
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            // Register ModuleSection1
            moduleCatalog.AddModule(typeof(ModuleSection1.ModuleSection1));
            // Register ModuleSection2
            moduleCatalog.AddModule(typeof(ModuleSection2.ModuleSection2));
        }

        /// <summary>
        /// Create logger
        /// </summary>
        /// <returns></returns>
        protected override ILoggerFacade CreateLogger()
        {
            //return base.CreateLogger();
            return new Logging.NLogLogger();
        }
    }
}
