using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ImageLoader : MonoBehaviour, IProgressCounter
{
    public float Progress { get; private set; }

    public async Task Load(string url)
    {
        Debug.Log("Start loading image " + url);
            
        var image = GetComponent<RawImage>();
        image.color = new Color(1, 1, 1, 0);
        
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            await request.SendWebRequest();

            while (!request.isDone)
            {
                Progress = request.downloadProgress;
                await Task.Yield();
            }
        
            if(request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                image.color = Color.white;
                image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                
                Debug.Log("Image loaded " + url);
            }
        }

        Progress = 1f;
    }
}