using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class ImageExtentions
{
    public static async Task ChangeAlfa(this Image image, float endValue, float duration)
    {
        float animationDuration = 0;
        float startValue = image.color.a;

        while (animationDuration < duration)
        {
            animationDuration += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, endValue, animationDuration / duration);
            Debug.Log(animationDuration / duration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, newValue);
            
            await Task.Yield();
        }
        
        image.color = new Color(image.color.r, image.color.g, image.color.b, endValue);
    }
}
