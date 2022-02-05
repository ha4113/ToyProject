using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<PrefabManager>().AsSingle();
        Container.BindInterfacesTo<PacketManager>().AsSingle();
        Container.BindInterfacesTo<ErrorHandler>().AsSingle();
        Container.BindInterfacesTo<EventHandler>().AsSingle();
    }
}
