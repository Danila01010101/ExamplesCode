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
    private bool shouldBeVisible = true;
    private CancellationTokenSource currentAnimationToken;
    
    private int clicksCount;
    
    private async void Start()
    {
        circle = GetComponent<Image>();
        
        currentAnimationToken = new CancellationTokenSource();
        
        await Animate(currentAnimationToken.Token);
        
        Debug.Log("Animation completed.");
    }

    private async void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            await circle.ChangeAlpha(0, 1, currentAnimationToken.Token);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            await circle.ChangeAlpha(1, 1, currentAnimationToken.Token);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            await circle.transform.MoveTo(circle.transform.position + Vector3.right * 100, 1, currentAnimationToken.Token);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            await circle.transform.MoveTo(circle.transform.position - Vector3.right * 100, 1, currentAnimationToken.Token);
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            await circle.transform.MoveTo(circle.transform.position + Vector3.up * 100, 1, currentAnimationToken.Token);
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            await circle.transform.MoveTo(circle.transform.position - Vector3.up * 100, 1, currentAnimationToken.Token);
        }
        
        if (Input.GetMouseButtonDown(0))
            clicksCount++;
        
        if (clicksCount == 3)
            currentAnimationToken.Cancel();
    }

    private async Task Animate(CancellationToken token)
    {
        circle.color = new Color(1, 1, 1, 0);
        
        while (true)
        {
            await Task.Yield();
                    
            if (token.IsCancellationRequested)
            {
                if (shouldBeVisible)
                {
                    shouldBeVisible = true;
                    SetColor(0);
                }
                
                break;
            }
            
            SetColor(currentAlpha);
            
            if (shouldBeVisible)
            {
                currentAlpha += Time.deltaTime;
                
                if (currentAlpha >= 1f)
                {
                    shouldBeVisible = false;
                    SetColor(1);
                }
            }
            else
            {
                currentAlpha -= Time.deltaTime;
                
                if (currentAlpha <= 0)
                {
                    shouldBeVisible = true;
                    SetColor(0);
                    break;
                }
            }

            Debug.Log(currentAlpha);
        }
    }

    private void SetColor(float newValue)
    {
        if (circle != null)
        {
            circle.color = new Color(1, 1, 1, newValue);
        }
    }

    private void OnDestroy()
    {
        currentAnimationToken.Cancel();
        currentAnimationToken.Dispose();
    }
}
