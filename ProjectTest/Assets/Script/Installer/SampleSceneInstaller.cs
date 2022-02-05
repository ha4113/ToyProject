using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class SampleSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.LogError("SampleSceneInstall");
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
        Container.BindFactory<long, Task<Player>, PlayerFactory>().AsSingle();
        Container.Bind<OutputEventSubscribe>().AsSingle().NonLazy();
        
        var ctrl = Container.Resolve<PlayerController>();
        ctrl.AddPlayer(1);
    }
}
