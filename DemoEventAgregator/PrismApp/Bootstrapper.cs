using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using PrismApp.ViewModels;
using PrismApp.Views;

namespace PrismApp
{
    internal class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            // Modules defined in code
            return new ModuleCatalog();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            // Add your modules here
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();

            Container.RegisterType<PublisherViewModel>();
            Container.RegisterType<SubscriberViewModel>();

            IRegionManager regionManager  = this.Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("PublisherRegion", typeof(PublisherView));
            regionManager.RegisterViewWithRegion("SubscriberRegion", typeof(SubscriberView));
        }
    }
}
