using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.LogError("ProjectInstall");
        Container.BindInterfacesAndSelfTo<GlobalEvent>().AsSingle();
        Container.BindInterfacesAndSelfTo<PrefabManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<PacketManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<ErrorHandler>().AsSingle();
        
    }
}
