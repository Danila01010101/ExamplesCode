using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

public class NumbersGeneration : MonoBehaviour
{
    private int numbersAmount = 1000;

    private async void Start()
    {
        int task = await UniTask.RunOnThreadPool(Generate);
        Debug.Log(task);
    }

    private int Generate()
    {
        int result = 0;
        Random rand = new ();
        
        for (int i = 0; i < numbersAmount; i++)
        {
            int newNumber = rand.Next(0, 1000);
            result += newNumber;
            result /= 2;
        }

        return result;
    }
}
