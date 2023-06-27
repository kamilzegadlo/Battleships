using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BattleShips.Domain.Plumbing
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPlacementValidator>().ImplementedBy<PlacementValidator>());
            container.Register(Component.For<IPlacementAllocator>().ImplementedBy<RandomPlacementAllocator>());
            container.Register(Component.For<IGameFactory>().ImplementedBy<GameFactory>());
        }
    }
}
