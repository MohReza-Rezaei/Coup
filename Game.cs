using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{ 
   int[] numbers = {1,2,3,4,5,1,2,3,4,5,1,2,3,4,5};
   int[] lost ={-1,-1,-1,-1,-1,-1,-1};
   

   void ShuffleArray(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int j = Random.Range(i, array.Length);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ShuffleArray(numbers);
        for(int i = 0;i<numbers.Length;i++){
            print(numbers[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
