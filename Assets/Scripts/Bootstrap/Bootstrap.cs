using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Canvas bootstrapCanvas;
    [SerializeField] private ImageLoader loaderScreenPrefab;
    [SerializeField] private string imageUrl;
    
    [SerializeField] private Slider loadingProgressSlider;

    private List<Task> currentTasks = new List<Task>();
    private List<IProgressCounter> currentProgressCounters = new List<IProgressCounter>();
    
    private bool loading;
    
    private async void Start()
    {
        await Initialize();
    }

    private async Task Initialize()
    {
        loading = true;
        ImageLoader imageLoader = Instantiate(loaderScreenPrefab, bootstrapCanvas.transform);
        Task imageLoadingTask = imageLoader.Load(imageUrl);
        AddLoader(imageLoadingTask, imageLoader);
        imageLoader.transform.SetAsFirstSibling();

        PrefabsLoader prefabsLoader = new PrefabsLoader();
        Task prefabLoadingTask = prefabsLoader.LoadPrefab("Cube");
        AddLoader(prefabLoadingTask, prefabsLoader);
        
        SceneLoader sceneLoader = new SceneLoader();
        Task sceneLoadingTask = sceneLoader.PreloadScene("EmptyScene");
        AddLoader(sceneLoadingTask, sceneLoader);
        
        await imageLoadingTask;
        
        await Task.WhenAll(currentTasks);
        
        loading = false;
        loadingProgressSlider.value = 1;
        
        Debug.Log("All tasks completed loading scene.");
        
        sceneLoader.ActivateScene();
    }

    private void Update()
    {
        if (loading)
        {
            UpdateProgress();
        }
    }

    private void AddLoader(Task task, IProgressCounter progressCounter)
    {
        currentProgressCounters.Add(progressCounter);
        currentTasks.Add(task);
    }

    private void UpdateProgress()
    {
        float allTasksProgress = 0;

        foreach (var progressCounter in currentProgressCounters)
        {
            allTasksProgress += progressCounter.Progress;
        }

        float result = (float)(Math.Round(allTasksProgress / currentProgressCounters.Count, 2));
        Debug.Log(result);
        loadingProgressSlider.value = result;
    }
}