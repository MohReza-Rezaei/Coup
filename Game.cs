using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public name name_script;
    int[] numbers = { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 };
    int[] lost = { -1, -1, -1, -1, -1, -1, -1 };
    bool myturn = false, cpu1turn = false, cpu2turn = false, cpu3turn = false;
    Player Me = new Player(); Player cpu1 = new Player(); Player cpu2 = new Player(); Player cpu3 = new Player();
    int endgame = 6;
    bool Done = true;
    public Text announcer;
    public GameObject pannel, coupOff , coupCanvas;
    public GameObject[] coupCircle = new GameObject[3];
    string mali = "banker", ertebat = "", attack = "cherik", uniqe4 = "solh", uniqe5 = "siasat";

    /// <Mali>
    public Text[] cointxt = new Text[4];
    //   public GameObject maliOff;
    ///

    /// <ertebat>
    //  public GameObject maliOff;
    ///

    /// <attack>
    public GameObject attackOff;
    ///
    /// <uniqe4>
    // public GameObject maliOff;
    ///

    /// /// <uniqe5>
    // public GameObject maliOff;
    ///
    void ShuffleArray(int[] array, bool check)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int j = Random.Range(i, array.Length);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        if (check)
        {

            //testing
            int ran = 1;//Random.Range(1, 5);
            //
            switch (ran)
            {
                case 1:
                    myturn = true;
                    break;
                case 2:
                    cpu1turn = true;
                    break;
                case 3:
                    cpu1turn = true;
                    break;
                case 4:
                    cpu3turn = true;
                    break;
            }


            Me.card1 = numbers[0];
            cpu1.card1 = numbers[1];
            cpu2.card1 = numbers[2];
            cpu3.card1 = numbers[3];
            Me.card2 = numbers[4];
            cpu1.card2 = numbers[5];
            cpu2.card2 = numbers[6];
            cpu3.card2 = numbers[7];

            for (int i = 0; i < 8; i++)
            {
                numbers[i] = -1;
            }
        }

    }

    void Meoffcheck()
    {
        //coup
        if (Me.coin < 7)
        {
            coupOff.SetActive(true);
        }
        else
        {
            coupOff.SetActive(false);
        }

        //attack
        if (attack == "cherik")
        {
            if (Me.coin < 4)
            {
                attackOff.SetActive(true);
            }
            else
            {
                attackOff.SetActive(false);
            }
        }

    }

    IEnumerator Robot()
    {
        //testing



        while (endgame != 0 && Me.Alive)
        {
            yield return new WaitUntil(() => Done == true);
            Done = false;
            if (myturn)
            {

                Meoffcheck();
                pannel.SetActive(true);

            }
            else
            {

            }
            yield return new WaitForSeconds(1);
        }

        // point
    }


    void Start()
    {
        ShuffleArray(numbers, true);




        StartCoroutine(Robot());
    }

    public void Action(string whichAction)
    {
        StartCoroutine(Actiony(whichAction));
    }
    IEnumerator Actiony(string whichAction)
    {
        bool[] result = { false, false, false };
        pannel.SetActive(false);
        if (whichAction == "mali")
        {
            if (mali == "banker")
            {
                announcer.text = "ﻡﺭﺍﺪﮑﻧﺎﺑ";
            }
            yield return new WaitForSeconds(1.5f);

            if (cpu1.Alive)
            {
                result[0] = cpu1.Chalesh(1, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu1Name;
            }
            yield return new WaitForSeconds(1);
            if (cpu2.Alive)
            {
                result[1] = cpu2.Chalesh(1, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu2Name;
            }
            yield return new WaitForSeconds(1);
            if (cpu3.Alive)
            {
                result[2] = cpu3.Chalesh(1, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu3Name;
            }
            yield return new WaitForSeconds(1);
        }

        bool permision = true;

        for (int i = 0; i < result.Length; i++)
        {
            if (result[i])
            {
                permision = false;
                break;
            }
        }

        //testing
        permision = true;
        //

        if (permision)
        {
            if (whichAction == "mali")
                StartCoroutine(Mali());


        }

    }

    public void coup()
    {
        StartCoroutine(coupy());
    }

    IEnumerator coupy()
    {
        if (!cpu1.Alive)
            coupCircle[0].SetActive(false);
        else
            coupCircle[0].SetActive(true);

        if (!cpu2.Alive)
            coupCircle[1].SetActive(false);
        else
            coupCircle[1].SetActive(true);

        if (!cpu3.Alive)
            coupCircle[2].SetActive(false);
        else
            coupCircle[2].SetActive(true);

        coupCanvas.SetActive(true);

        yield return new WaitForSeconds(1);
    }



    public void earn()
    {
        StartCoroutine(earny());
    }
    IEnumerator earny()
    {
        if (myturn)
        {
            pannel.SetActive(false);
            Me.coin += 1;
            cointxt[0].text = Me.coin.ToString();
        }
        else if (cpu1turn)
        {
            cpu1.coin += 1;
            cointxt[1].text = cpu1.coin.ToString();
        }
        else if (cpu2turn)
        {
            cpu2.coin += 1;
            cointxt[2].text = cpu2.coin.ToString();
        }
        else if (cpu3turn)
        {
            cpu3.coin += 1;
            cointxt[3].text = cpu3.coin.ToString();
        }
        announcer.text = "ﻪﮑﺳ " + "+1";
        yield return new WaitForSeconds(4);
        announcer.text = "";
    }

    IEnumerator Mali()
    {
        if (mali == "banker")
        {
            if (myturn)
            {
                Me.coin += 3;
                announcer.text = "ﻪﮑﺳ " + "+3";
                cointxt[0].text = Me.coin.ToString();
            }
            else if (cpu1turn)
            {
                cpu1.coin += 3;
                announcer.text = "ﻪﮑﺳ " + "+3";
                cointxt[1].text = cpu1.coin.ToString();

            }
            else if (cpu2turn)
            {
                cpu2.coin += 3;
                announcer.text = "ﻪﮑﺳ " + "+3";
                cointxt[2].text = cpu2.coin.ToString();

            }
            else if (cpu3turn)
            {
                cpu3.coin += 3;
                announcer.text = "ﻪﮑﺳ " + "+3";
                cointxt[3].text = cpu3.coin.ToString();

            }
            yield return new WaitForSeconds(1.5f);
            announcer.text = "";
        }

    }
    


}

public class Player
{
    public int card1;
    public int card2;
    public bool Alive;
    public int coin;

    public Player()
    {
        coin = 2;
        Alive = true;
    }

    public bool Chalesh(int what, int[] lost)
    {   int burn = 0;
        int res = 0;
        int hand = 0;
        Debug.Log(card1+"--"+card2);
        if (card1 == what)
            res++;
        if (card2 == what)
            res++;

        if (card1 != -1)
            hand++;
        if (card2 != -1)
            hand++;

        for (int i = 0; i < lost.Length; i++)
            {
                if (lost[i] == what)
                {
                    res++;
                }
                if (lost[i] != -1)
                    burn++;
            }


        if (res == 3)
        {
            return true;
        }
        else
        {
            float formula;
            formula = (float)(3 - res) / (15 - burn - hand);
            Debug.Log(formula);
            formula *= 300;
            Debug.Log(formula);
            formula = 100 - formula;
            formula /= 4;
            Debug.Log(formula);
            int go = Random.Range(1, 101);
            go *= 2;
            Debug.Log(go);
            if (go < formula)
            {
                return true;
            } else {
                return false;
            }
            
        }
        
   
    }
}
