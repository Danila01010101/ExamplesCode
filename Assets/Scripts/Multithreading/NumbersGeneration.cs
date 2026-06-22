using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

public class NumbersGeneration : MonoBehaviour
{
    private int numbersAmount = 1000;

    private void Start()
    {
        Task.Run(() => Generate());
    }

    private void Generate()
    {
        int result = 0;
        Random rand = new Random();
        
        for (int i = 0; i < numbersAmount; i++)
        {
            int newNumber = rand.Next(0, 1000);
            result += newNumber;
            result /= 2;
            Debug.Log(i + " in thread" + Thread.CurrentThread.ManagedThreadId);
        }
        
        Debug.Log(result + " in thread" + Thread.CurrentThread.ManagedThreadId);
    }
}
