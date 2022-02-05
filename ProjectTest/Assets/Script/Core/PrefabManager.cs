using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public interface IPrefabManager
{
    Task<T> Get<T>(string path)
        where T : MonoBehaviour, new();
}
public class PrefabManager : IPrefabManager, IInitializable
{
    public void Initialize() { }

    public async Task<T> Get<T>(string path)
        where T : MonoBehaviour, new()
    {
        await Task.Delay(1000);
        return new GameObject().AddComponent<T>();
    }
}
