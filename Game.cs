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
    bool Done = true, cClicked = false;
    public Text announcer;
    public Text[] lostText = new Text[5];
    public Sprite[] Logo = new Sprite[25];
    public GameObject[] mycards = new GameObject[2];
    public GameObject[] cpu1cards = new GameObject[2];
    public GameObject[] cpu2cards = new GameObject[2];
    public GameObject[] cpu3cards = new GameObject[2];
    public GameObject pannel, coupOff, coupCanvas;
    public GameObject[] coupCircle = new GameObject[3];
    string mali = "banker", ertebat = "director", attack = "cherik", uniqe4 = "solh", uniqe5 = "siasat";
    int whoCoup;

    /// <Mali>
    public Text[] cointxt = new Text[4];
    //   public GameObject maliOff;
    ///

    /// <ertebat>///////////////////////////////
    public GameObject midField;
    public GameObject[] midFiledIcon = new GameObject[4];
    public GameObject[] midFiledCards = new GameObject[4];
    public Text[] midFiledText = new Text[4];

    //(Me)
    int ertebatClick=0;
    int[] Firstchoice = new int[2];
    int[] secondchoice = new int[2];
    //(cpu)

    //////////////////////////////////////////////

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

    void printLost()
    {
        int funanc = 0, comnu = 0, atk = 0, uq4 = 0, uq5 = 0;
        for (int i = 0; i < lost.Length; i++)
        {
            if (lost[i] == 1)
                funanc++;
            if (lost[i] == 2)
                comnu++;
            if (lost[i] == 3)
                atk++;
            if (lost[i] == 4)
                uq4++;
            if (lost[i] == 5)
                uq5++;
        }

        lostText[0].text = funanc.ToString();
        lostText[1].text = comnu.ToString();
        lostText[2].text = atk.ToString();
        lostText[3].text = uq4.ToString();
        lostText[4].text = uq5.ToString();
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

        //testing
        Me.coin = 7;
        //


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
        else if (whichAction == "ertebat")
        {
            if (ertebat == "director")
            {
                announcer.text = "ﻢﻧﺍﺩﺮﮔﺭﺎﮐ";
            }

            yield return new WaitForSeconds(1.5f);

            if (cpu1.Alive)
            {
                result[0] = cpu1.Chalesh(2, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu1Name;
            }
            yield return new WaitForSeconds(1);
            if (cpu2.Alive)
            {
                result[1] = cpu2.Chalesh(2, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu2Name;
            }
            yield return new WaitForSeconds(1);
            if (cpu3.Alive)
            {
                result[2] = cpu3.Chalesh(2, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu3Name;
            }
            yield return new WaitForSeconds(1);
        }

        //testing
        bool permision = true;
        //

        for (int i = 0; i < result.Length; i++)
        {
            if (result[i])
            {
                permision = false;
                break;
            }
        }



        if (permision)
        {
            if (whichAction == "mali")
                StartCoroutine(Mali());
            else if (whichAction == "ertebat")
                StartCoroutine(ertebatat());

        }

    }


    public void selectCoup(int who)
    {
        cClicked = true;
        whoCoup = who;
    }

    public void coup()
    {
        StartCoroutine(coupy());
    }

    IEnumerator coupy()
    {
        if (myturn)
        {
            Me.coin -= 7;
            cointxt[0].text = Me.coin.ToString();
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

            yield return new WaitUntil(() => cClicked == true);
            cClicked = false;
        }

        if (whoCoup == 1)
        {
            int ran;
            do
            {
                ran = Random.Range(1, 3);
            } while ((ran == 1 && cpu1.card1 == -1) || (ran == 2 && cpu1.card2 == -1));

            if (ran == 1)
            {
                if (cpu1.card1 == 1)
                {
                    if (mali == "banker")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                }
                else if (cpu1.card1 == 2)
                {
                    if (ertebat == "director")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                }
                else if (cpu1.card1 == 3)
                {
                    if (attack == "cherik")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                }
                else if (cpu1.card1 == 4)
                {
                    if (uniqe4 == "solh")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                }
                else if (cpu1.card1 == 5)
                {
                    if (uniqe5 == "siasat")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu1Name;
                }

                for (int i = 0; i < lost.Length; i++)
                {
                    if (lost[i] == -1)
                    {
                        lost[i] = cpu1.card1;
                        cpu1.card1 = -1;
                        break;
                    }
                }

                cpu1cards[0].SetActive(false);
            }
            else
            {
                if (cpu1.card2 == 1)
                {
                    if (mali == "banker")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                }
                else if (cpu1.card2 == 2)
                {
                    if (ertebat == "director")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                }
                else if (cpu1.card2 == 3)
                {
                    if (attack == "cherik")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                }
                else if (cpu1.card2 == 4)
                {
                    if (uniqe4 == "solh")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                }
                else if (cpu1.card2 == 5)
                {
                    if (uniqe5 == "siasat")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu1Name;
                }

                for (int i = 0; i < lost.Length; i++)
                {
                    if (lost[i] == -1)
                    {
                        lost[i] = cpu1.card2;
                        cpu1.card2 = -1;
                        break;
                    }
                }

                cpu2cards[0].SetActive(false);
            }

            printLost();
            yield return new WaitForSeconds(3);

        }
        else if (whoCoup == 2)
        {
            int ran;
            do
            {
                ran = Random.Range(1, 3);
            } while ((ran == 1 && cpu2.card1 == -1) || (ran == 2 && cpu2.card2 == -1));

            if (ran == 1)
            {
                if (cpu2.card1 == 1)
                {
                    if (mali == "banker")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                }
                else if (cpu2.card1 == 2)
                {
                    if (ertebat == "director")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                }
                else if (cpu2.card1 == 3)
                {
                    if (attack == "cherik")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                }
                else if (cpu2.card1 == 4)
                {
                    if (uniqe4 == "solh")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                }
                else if (cpu2.card1 == 5)
                {
                    if (uniqe5 == "siasat")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu2Name;
                }

                for (int i = 0; i < lost.Length; i++)
                {
                    if (lost[i] == -1)
                    {
                        lost[i] = cpu2.card1;
                        cpu2.card1 = -1;
                        break;
                    }
                }
                cpu2cards[0].SetActive(false);
            }
            else
            {
                if (cpu2.card2 == 1)
                {
                    if (mali == "banker")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                }
                else if (cpu2.card2 == 2)
                {
                    if (ertebat == "director")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                }
                else if (cpu2.card2 == 3)
                {
                    if (attack == "cherik")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                }
                else if (cpu2.card2 == 4)
                {
                    if (uniqe4 == "solh")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                }
                else if (cpu2.card2 == 5)
                {
                    if (uniqe5 == "siasat")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu2Name;
                }

                for (int i = 0; i < lost.Length; i++)
                {
                    if (lost[i] == -1)
                    {
                        lost[i] = cpu2.card2;
                        cpu2.card2 = -1;
                        break;
                    }
                }
                cpu2cards[1].SetActive(false);
            }

            printLost();
            yield return new WaitForSeconds(3);
        }
        else if (whoCoup == 3)
        {
            int ran;
            do
            {
                ran = Random.Range(1, 3);
            } while ((ran == 1 && cpu3.card1 == -1) || (ran == 2 && cpu3.card2 == -1));

            if (ran == 1)
            {
                if (cpu3.card1 == 1)
                {
                    if (mali == "banker")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                }
                else if (cpu3.card1 == 2)
                {
                    if (ertebat == "director")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                }
                else if (cpu3.card1 == 3)
                {
                    if (attack == "cherik")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                }
                else if (cpu3.card1 == 4)
                {
                    if (uniqe4 == "solh")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                }
                else if (cpu3.card1 == 5)
                {
                    if (uniqe5 == "siasat")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu3Name;
                }

                for (int i = 0; i < lost.Length; i++)
                {
                    if (lost[i] == -1)
                    {
                        lost[i] = cpu3.card1;
                        cpu3.card1 = -1;
                        break;
                    }
                }
                cpu3cards[0].SetActive(false);
            }
            else
            {
                if (cpu3.card2 == 1)
                {
                    if (mali == "banker")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                }
                else if (cpu3.card2 == 2)
                {
                    if (ertebat == "director")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                }
                else if (cpu3.card2 == 3)
                {
                    if (attack == "cherik")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                }
                else if (cpu3.card2 == 4)
                {
                    if (uniqe4 == "solh")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                }
                else if (cpu3.card2 == 5)
                {
                    if (uniqe5 == "siasat")
                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu3Name;
                }

                for (int i = 0; i < lost.Length; i++)
                {
                    if (lost[i] == -1)
                    {
                        lost[i] = cpu3.card2;
                        cpu3.card2 = -1;
                        break;
                    }
                }
                cpu3cards[1].SetActive(false);
            }

            printLost();
            yield return new WaitForSeconds(3);
        }
        else if (whoCoup == 0)
        {
            // me being attacked
        }
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

    IEnumerator ertebatat()
    {
        announcer.text = "";
        int Role1=0, Role2=0, index1, index2;
        int t = 0;

        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] != -1)
            {
                if (t == 0)
                {
                    Role1 = numbers[i];
                    index1 = i;
                }
                else
                {
                    Role2 = numbers[i];
                    index2 = i;
                }
                t++;
            }

            if (t == 2)
                break;

        }

        print(Me.card1 + " -- " + Me.card2);
        midIconCheck(Role1,Role2);
        midField.SetActive(true);

        if (myturn)
        {
            int lives = 0;
            if (Me.card1 != -1)
                lives++;
            if (Me.card2 != -1)
                lives++;

            yield return new WaitUntil(() => ertebatClick == lives); 

            


        }
        


    }

    void midIconCheck(int role1 , int role2)
    {
        if (Me.card1 == 1)
        {
            if (mali == "banker")
            {
                midFiledIcon[0].GetComponent<Image>().sprite = Logo[0];
                midFiledText[0].text = "ﺭﺍﺪﮑﻧﺎﺑ";
            }
        }
        else if (Me.card1 == 2)
        {
            if (ertebat == "director")
            {
                midFiledIcon[0].GetComponent<Image>().sprite = Logo[1];
                midFiledText[0].text = "ﻥﺍﺩﺮﮔﺭﺎﮐ";
            }
        }
        else if (Me.card1 == 3)
        {
            if (attack == "cherik")
            {
                midFiledIcon[0].GetComponent<Image>().sprite = Logo[2];
                midFiledText[0].text = "ﮏﯾﺮﭼ";
            }
        }
        else if (Me.card1 == 4)
        {
            if (uniqe4 == "solh")
            {
                midFiledIcon[0].GetComponent<Image>().sprite = Logo[3];
                midFiledText[0].text = "ﺐﻠﻃ ﺢﻠﺻ";
            }
        }
        else if (Me.card1 == 5)
        {
            if (uniqe5 == "siasat")
            {
                midFiledIcon[0].GetComponent<Image>().sprite = Logo[4];
                midFiledText[0].text = "ﺭﺍﺪﻤﺘﺳﺎﯿﺳ";
            }
        }
        else if (Me.card1 == -1)
        {
            midFiledCards[0].SetActive(false);
        }

//////////////////////////////////////////

        if (Me.card2 == 1)
        {
            if (mali == "banker")
            {
                midFiledIcon[1].GetComponent<Image>().sprite = Logo[0];
                midFiledText[1].text = "ﺭﺍﺪﮑﻧﺎﺑ";
            }
        }
        else if (Me.card2 == 2)
        {
            if (ertebat == "director")
            {
                midFiledIcon[1].GetComponent<Image>().sprite = Logo[1];
                midFiledText[1].text = "ﻥﺍﺩﺮﮔﺭﺎﮐ";
            }
        }
        else if (Me.card2 == 3)
        {
            if (attack == "cherik")
            {
                midFiledIcon[1].GetComponent<Image>().sprite = Logo[2];
                midFiledText[1].text = "ﮏﯾﺮﭼ";
            }
        }
        else if (Me.card2 == 4)
        {
            if (uniqe4 == "solh")
            {
                midFiledIcon[1].GetComponent<Image>().sprite = Logo[3];
                midFiledText[1].text = "ﺐﻠﻃ ﺢﻠﺻ";
            }
        }
        else if (Me.card2 == 5)
        {
            if (uniqe5 == "siasat")
            {
                midFiledIcon[1].GetComponent<Image>().sprite = Logo[4];
                midFiledText[1].text = "ﺭﺍﺪﻤﺘﺳﺎﯿﺳ";
            }
        }else if (Me.card2 == -1)
        {
            midFiledCards[1].SetActive(false);
        }

        
      /////////////////////////////////////////  

        if (role1 == 1)
        {
            if (mali == "banker")
            {
                midFiledIcon[2].GetComponent<Image>().sprite = Logo[0];
                midFiledText[2].text = "ﺭﺍﺪﮑﻧﺎﺑ";
            }
        }
        else if (role1 == 2)
        {
            if (ertebat == "director")
            {
                midFiledIcon[2].GetComponent<Image>().sprite = Logo[1];
                midFiledText[2].text = "ﻥﺍﺩﺮﮔﺭﺎﮐ";
            }
        }
        else if (role1 == 3)
        {
            if (attack == "cherik")
            {
                midFiledIcon[2].GetComponent<Image>().sprite = Logo[2];
                midFiledText[2].text = "ﮏﯾﺮﭼ";
            }
        }
        else if (role1 == 4)
        {
            if (uniqe4 == "solh")
            {
                midFiledIcon[2].GetComponent<Image>().sprite = Logo[3];
                midFiledText[2].text = "ﺐﻠﻃ ﺢﻠﺻ";
            }
        }
        else if (role1 == 5)
        {
            if (uniqe5 == "siasat")
            {
                midFiledIcon[2].GetComponent<Image>().sprite = Logo[4];
                midFiledText[2].text = "ﺭﺍﺪﻤﺘﺳﺎﯿﺳ";
            }
        }
        //////////////////////////////
        if (role2 == 1)
        {
            if (mali == "banker")
            {
                midFiledIcon[3].GetComponent<Image>().sprite = Logo[0];
                midFiledText[3].text = "ﺭﺍﺪﮑﻧﺎﺑ";
            }
        }
        else if (role2 == 2)
        {
            if (ertebat == "director")
            {
                midFiledIcon[3].GetComponent<Image>().sprite = Logo[1];
                midFiledText[3].text = "ﻥﺍﺩﺮﮔﺭﺎﮐ";
            }
        }
        else if (role2 == 3)
        {
            if (attack == "cherik")
            {
                midFiledIcon[3].GetComponent<Image>().sprite = Logo[2];
                midFiledText[3].text = "ﮏﯾﺮﭼ";
            }
        }
        else if (role2 == 4)
        {
            if (uniqe4 == "solh")
            {
                midFiledIcon[3].GetComponent<Image>().sprite = Logo[3];
                midFiledText[3].text = "ﺐﻠﻃ ﺢﻠﺻ";
            }
        }
        else if (role2 == 5)
        {
            if (uniqe5 == "siasat")
            {
                midFiledIcon[3].GetComponent<Image>().sprite = Logo[4];
                midFiledText[3].text = "ﺭﺍﺪﻤﺘﺳﺎﯿﺳ";
            }
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
