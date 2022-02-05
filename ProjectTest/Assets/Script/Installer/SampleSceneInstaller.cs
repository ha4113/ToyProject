using System.Threading.Tasks;
using Zenject;

public class SampleSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
        Container.BindFactory<long, Task<Player>, PlayerFactory>().AsSingle();
        
        Container.BindInterfacesTo<OutputSubscribe>().AsSingle().NonLazy();
    }
}
