using Common.Core.Util;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Log.InitLog(new Logger());
        Container.BindInterfacesTo<TableLoader>().AsSingle().NonLazy();
        Container.BindInterfacesTo<PrefabManager>().AsSingle();
        Container.BindInterfacesTo<PacketManager>().AsSingle();
        Container.BindInterfacesTo<ErrorHandler>().AsSingle();
        Container.BindInterfacesTo<EventHandler>().AsSingle();
    }
}
