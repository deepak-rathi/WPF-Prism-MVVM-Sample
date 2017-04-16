using abc.infrastructure.Base;
using abc.infrastructure.Constants;
using abc.infrastructure.Interfaces;
using Prism.Logging;
using Prism.Unity;

namespace abc.shell.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        //LocalizerService Instance to change current culture or do any other localization work
        private ILocalizerService localizerService = null;

        #region Constructor
        public MainWindowViewModel()
        {
            Container.TryResolve<ILoggerFacade>().Log("MainViewModel created", Category.Info, Priority.None);
            localizerService = (ILocalizerService)Container.Resolve(typeof(ILocalizerService), ServiceNames.LocalizerService);
        }
        #endregion
    }
}
