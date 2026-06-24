using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CircleAnimation : MonoBehaviour
{
    private Image circle;
    private float currentAlpha = 0;
    private bool shouldBeVisible;
    private bool animate;
    private CancellationTokenSource currentAnimationToken;
    
    private int clicksCount;
    
    private void Start()
    {
        circle = GetComponent<Image>();
        
        currentAnimationToken = new CancellationTokenSource();
        
        Animate(currentAnimationToken.Token);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            circle.ChangeAlpha(0, 1, new CancellationTokenRegistration().Token);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            circle.ChangeAlpha(1, 1, new CancellationTokenRegistration().Token);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            circle.transform.MoveTo(circle.transform.position + Vector3.right * 100, 1, new CancellationTokenRegistration().Token);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            circle.transform.MoveTo(circle.transform.position - Vector3.right * 100, 1, new CancellationTokenRegistration().Token);
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            circle.transform.MoveTo(circle.transform.position + Vector3.up * 100, 1, new CancellationTokenRegistration().Token);
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            circle.transform.MoveTo(circle.transform.position - Vector3.up * 100, 1, new CancellationTokenRegistration().Token);
        }
        
        if (Input.GetMouseButtonDown(0))
            clicksCount++;
        
        if (clicksCount == 3)
            currentAnimationToken.Cancel();
    }

    private async Task Animate(CancellationToken token)
    {
        while (true)
        {
            await Task.Yield();

            if (shouldBeVisible)
            {
                currentAlpha += Time.deltaTime;
                
                if (currentAlpha >= 1f)
                {
                    shouldBeVisible = false;
                    
                    if (token.IsCancellationRequested)
                        return;
                }
            }
            else
            {
                currentAlpha -= Time.deltaTime;
                
                if (currentAlpha <= 0)
                {
                    shouldBeVisible = true;
                    
                    if (token.IsCancellationRequested)
                        return;
                }
            }
            
            circle.color = new Color(1, 1, 1, currentAlpha);
        }
    }
}
