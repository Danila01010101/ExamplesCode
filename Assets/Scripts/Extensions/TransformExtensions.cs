using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class TransformExtensions
{
    public static async UniTask MoveTo(this Transform transform, Vector3 position, float duration, CancellationToken token)
    {
        float animationDuration = 0;
        Vector3 startPosition = transform.position;

        while (animationDuration < duration)
        {
            animationDuration += Time.deltaTime;
            Vector3 newPosition = Vector3.Lerp(startPosition, position, animationDuration / duration);
            transform.position = newPosition;
            
            if (token.IsCancellationRequested)
                return;
            
            await UniTask.Yield();
        }
        
        transform.position = position;
    }
}
