using System.Threading.Tasks;
using UnityEngine;

public class PrefabsLoader: IProgressCounter
{
    public float Progress { get; private set; }
    
    public async Task LoadPrefab(string prefabPath)
    {
        Debug.Log("Start loading prefab " + prefabPath);
        var task = Resources.LoadAsync<GameObject>(prefabPath);
        
        while (!task.isDone)
        {
            Progress = task.progress;
            await Task.Yield();
        }
        
        Debug.Log("Loaded prefab " + prefabPath);
    }
}
