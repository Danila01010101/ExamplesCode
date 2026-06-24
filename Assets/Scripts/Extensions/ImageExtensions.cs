using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class ImageExtensions
{
    public static async Task ChangeAlpha(this Image image, float endValue, float duration, CancellationToken token)
    {
        float animationDuration = 0;
        float startValue = image.color.a;

        while (animationDuration < duration)
        {
            animationDuration += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, endValue, animationDuration / duration);
            Debug.Log(animationDuration / duration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, newValue);
            
            if (token.IsCancellationRequested)
                return;
            
            await Task.Yield();
        }
        
        image.color = new Color(image.color.r, image.color.g, image.color.b, endValue);
    }
}
