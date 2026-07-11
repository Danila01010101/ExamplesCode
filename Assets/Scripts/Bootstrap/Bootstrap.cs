using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Canvas _bootstrapCanvas;
    [SerializeField] private ImageLoader _loaderScreenPrefab;
    [SerializeField] private string _imageUrl;
    
    [SerializeField] private Slider _loadingProgressSlider;

    private List<UniTask> _currentTasks = new ();
    private List<IProgressCounter> _currentProgressCounters = new ();
    
    private bool _loading;
    
    private async void Start()
    {
        await Initialize();
    }

    private async UniTask Initialize()
    {
        _loading = true;
        ImageLoader imageLoader = Instantiate(_loaderScreenPrefab, _bootstrapCanvas.transform);
        UniTask imageLoadingTask = imageLoader.Load(_imageUrl);
        AddLoader(imageLoadingTask, imageLoader);
        imageLoader.transform.SetAsFirstSibling();

        PrefabsLoader prefabsLoader = new ();
        ResourceRequest prefabLoadingRequest = Resources.LoadAsync<GameObject>("Cube");
        AddLoader(prefabLoadingRequest.ToUniTask(), prefabsLoader);
        
        SceneLoader sceneLoader = new ();
        UniTask sceneLoadingTask = sceneLoader.PreloadScene("EmptyScene");
        AddLoader(sceneLoadingTask, sceneLoader);
        
        await UniTask.WhenAll(_currentTasks);
        
        _loading = false;
        _loadingProgressSlider.value = 1;
        
        Debug.Log("All tasks completed loading scene.");
        
        sceneLoader.ActivateScene();
    }

    private void Update()
    {
        if (_loading)
        {
            UpdateProgress();
        }
    }

    private void AddLoader(UniTask task, IProgressCounter progressCounter)
    {
        _currentProgressCounters.Add(progressCounter);
        _currentTasks.Add(task);
    }

    private void UpdateProgress()
    {
        float allTasksProgress = 0;
        
        if (_currentProgressCounters.Count == 0)
            return;

        foreach (var progressCounter in _currentProgressCounters)
        {
            allTasksProgress += progressCounter.Progress;
        }

        float result = (float)(Math.Round(allTasksProgress / _currentProgressCounters.Count, 2));
        Debug.Log(result);
        _loadingProgressSlider.value = result;
    }
}