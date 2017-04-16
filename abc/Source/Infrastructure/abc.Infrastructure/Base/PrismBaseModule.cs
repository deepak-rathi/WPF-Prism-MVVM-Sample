using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace abc.infrastructure.Base
{
    public abstract class PrismBaseModule : IModule
    {
        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unityContainer">The Unity container.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="regionViewRegistry">The region view registry.</param>
        public PrismBaseModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            UnityContainer = unityContainer;
            RegionManager = regionManager;
        }

        #endregion Constructor

        #region Interface IModule

        /// <summary>
        /// Initialize module
        /// </summary>
        public virtual void Initialize()
        {

        }

        #endregion Interface IModule

        #region Properties

        /// <summary>
        /// The Unity container
        /// </summary>
        public IUnityContainer UnityContainer { get; private set; }

        /// <summary>
        /// The region manager
        /// </summary>
        public IRegionManager RegionManager { get; private set; }

        #endregion Properties
    }
}
