using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, IProgressCounter
{
    private AsyncOperation _sceneLoad;

    public float Progress { get; private set; }

    public async Task PreloadScene(string sceneName)
    {
        _sceneLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        _sceneLoad.allowSceneActivation = false;

        while (_sceneLoad.progress < 0.9f)
        {
            Progress = _sceneLoad.progress;
            await Task.Yield();
        }

        Progress = 1f;

        Debug.Log("Scene loaded and waiting for activation");
    }

    public void ActivateScene()
    {
        if (_sceneLoad != null)
            _sceneLoad.allowSceneActivation = true;
    }
}
