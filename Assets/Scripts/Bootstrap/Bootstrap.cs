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
    
    private void Awake()
    {
        Initialize();
    }

    private async void Initialize()
    {
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
        
        await Task.WhenAll(currentTasks);
        
        Debug.Log("All tasks completed");
        
        bootstrapCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateProgress();
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

        int result = (int)(Math.Round(allTasksProgress, 2));
        loadingProgressSlider.value = result;
    }
}