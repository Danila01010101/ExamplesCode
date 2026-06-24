using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

public class NumbersGeneration : MonoBehaviour
{
    private int numbersAmount = 1000;

    private async void Start()
    {
        var task = Task.Run(() => Generate());
        await task;
        Debug.Log(task.Result);
    }

    private int Generate()
    {
        int result = 0;
        Random rand = new Random();
        
        for (int i = 0; i < numbersAmount; i++)
        {
            int newNumber = rand.Next(0, 1000);
            result += newNumber;
            result /= 2;
        }

        return result;
    }
}
