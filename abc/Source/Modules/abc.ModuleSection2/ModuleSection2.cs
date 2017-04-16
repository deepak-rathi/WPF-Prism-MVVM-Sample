using abc.Domain;
using abc.Domain.DTO;
using abc.infrastructure.Base;
using abc.infrastructure.Constants;
using abc.ModuleSection2.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace abc.ModuleSection2
{
    public class ModuleSection2 : PrismBaseModule
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unityContainer">The Unity container.</param>
        /// <param name="regionManager">The region manager.</param>
        public ModuleSection2(IUnityContainer unityContainer, IRegionManager regionManager) :
            base(unityContainer, regionManager)
        {
            AutoMapper.Mapper.CreateMap<StockDTO, Stock>();
            AutoMapper.Mapper.CreateMap<Stock, StockDTO>();

            //// Region
            regionManager.RegisterViewWithRegion(RegionNames.RegionSection2, typeof(StockDetail));

        }
    }
}
