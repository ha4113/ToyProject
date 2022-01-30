using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class PrefabManager : IInitializable
{
    public void Initialize() { }

    public async Task<T> Get<T>(string path)
        where T : MonoBehaviour, new()
    {
        return new GameObject().AddComponent<T>();
    }
}
