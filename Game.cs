using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    int[] numbers = { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 };
    int[] lost = { -1, -1, -1, -1, -1, -1, -1 };
    bool myturn=false, cpu1turn=false, cpu2turn=false, cpu3turn=false;
    Player Me = new Player();Player cpu1 = new Player();Player cpu2 = new Player();Player cpu3= new Player();
    int endgame = 6;
    bool Done = false;

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
   
    IEnumerator Robot()
    {
        yield return new WaitUntil(() => Done == true);

        while (endgame != 0 && Me.Alive)
        {
         
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        ShuffleArray(numbers);




        StartCoroutine(Robot());
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class Player
{
    public bool Alive;
    public int coin;

    public Player()
    {
        coin = 2;
        Alive = true;
    }
}
