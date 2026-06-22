using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CircleAnimation : MonoBehaviour
{
    private Image circle;
    private float currentAlfa = 0;
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
            circle.ChangeAlfa(0, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            circle.ChangeAlfa(1, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            circle.transform.MoveTo(circle.transform.position + Vector3.right * 100, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            circle.transform.MoveTo(circle.transform.position - Vector3.right * 100, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            circle.transform.MoveTo(circle.transform.position + Vector3.up * 100, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            circle.transform.MoveTo(circle.transform.position - Vector3.up * 100, 1);
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
                currentAlfa += Time.deltaTime;
                
                if (currentAlfa >= 1f)
                {
                    shouldBeVisible = false;
                    
                    if (token.IsCancellationRequested)
                        return;
                }
            }
            else
            {
                currentAlfa -= Time.deltaTime;
                
                if (currentAlfa <= 0)
                {
                    shouldBeVisible = true;
                    
                    if (token.IsCancellationRequested)
                        return;
                }
            }
            
            circle.color = new Color(1, 1, 1, currentAlfa);
        }
    }
}
