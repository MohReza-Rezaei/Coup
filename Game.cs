using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.MPE;
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
    bool Done = true, cClicked = false , losingClick = false;
    public Text announcer;
    public Text[] mytext = new Text[2];
    public Text[] lostText = new Text[5];
    public Sprite[] Logo = new Sprite[25];
    public GameObject chalesh , Reaction;
    public GameObject[] myicon = new GameObject[2];
    public GameObject[] mycards = new GameObject[2];
    public GameObject[] cpu1cards = new GameObject[2];
    public GameObject[] cpu2cards = new GameObject[2];
    public GameObject[] cpu3cards = new GameObject[2];
    public GameObject pannel, coupOff, coupCanvas , siasatOff;
    public GameObject[] coupCircle = new GameObject[3];
    public GameObject[] losingCircle = new GameObject[2];
    public GameObject[] edea = new GameObject[3];
    public GameObject lostSection;
    string mali = "banker", ertebat = "director", attack = "cherik", uniqe4 = "solh", uniqe5 = "siasat";
    int whoCoup;
    bool mychallange = false , myReaction = false , losetwice = false;
    public Color[] roleColor = new Color[5];

    bool stop = false , robotWait = false , meWait = false;

    // info for passing waitUntil
    // cClicked = is for doing continueing Action and ActionRob functions -> end of ertbatat , attack , uniqe5 and . . 
    // losingClick = for you to lose one cart then continue -- losingy function -> end of burn function
    // Mewait = for continue after chalsh  ->  end of chaleshBtn and DischaleshBtn
    // Robwait = for continue after reaction -> end of reactionBtn



    /// <Mali>
    public Text[] cointxt = new Text[4];
    //   public GameObject maliOff;
    

    /// <ertebat>///////////////////////////////
    public GameObject midField;
    public GameObject[] midFiledIcon = new GameObject[4];
    public GameObject[] midFiledCards = new GameObject[4];
    public Text[] midFiledText = new Text[4];
    public Text joontitle;
    //(Me)
    int ertebatClick = 0;
    int Firstchoice = -1,secondchoice = -1;
    //(cpu)

    //////////////////////////////////////////////

    /// <attack>
    public GameObject attackOff;
    public GameObject[] attackCircle = new GameObject[3];
    public GameObject attackCanvas;

    int whoAttacked;
    ///
    


    /// <uniqe4>
    // public GameObject maliOff;
    int WhoSolh = -1;
    public GameObject[] solhIcon = new GameObject[4];
    ///
    


    /// /// <uniqe5>
    public GameObject[] politicCircle = new GameObject[3];
    public GameObject politicCanvas;
    int whoPolitic;
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
            if ((!cpu1.Alive && !cpu2.Alive && WhoSolh == 3) || (!cpu2.Alive && !cpu3.Alive && WhoSolh == 1) || (!cpu1.Alive && !cpu3.Alive && WhoSolh == 2))
        {
            attackOff.SetActive(true);
        }
        else
        {
            attackOff.SetActive(false);
        }


            if (Me.coin < 4)
            {
                attackOff.SetActive(true);
            }
            else
            {
                attackOff.SetActive(false);
            }
        }


        //siasat
        if ((!cpu1.Alive && !cpu2.Alive && WhoSolh == 3) || (!cpu2.Alive && !cpu3.Alive && WhoSolh == 1) || (!cpu1.Alive && !cpu3.Alive && WhoSolh == 2))
        {
            siasatOff.SetActive(true);
        }
        else
        {
            siasatOff.SetActive(false);
        }
        

    }

    bool RobotOffCheck(Player temp, int action)
    {
        if (action == 1)
        {
            // mali
            return true;
        }
        else if (action == 2)
        {
            // ertebat
            return true;
        }
        else if (action == 3)
        {
            //attack
            int three = 0;
            if (temp == cpu1)
            {
                if (!cpu2.Alive || WhoSolh == 2)
                    three++;
                if (!cpu3.Alive || WhoSolh == 3)
                    three++;
                if (WhoSolh == 0)
                    three++;

                if (three == 3)
                    return false;
            }
            else if (temp == cpu2)
            {
                if (!cpu1.Alive || WhoSolh == 1)
                    three++;
                if (!cpu3.Alive || WhoSolh == 3)
                    three++;
                if (WhoSolh == 0)
                    three++;

                if (three == 3)
                    return false;
            }
            else if (temp == cpu3)
            {
                if (!cpu1.Alive || WhoSolh == 1)
                    three++;
                if (!cpu2.Alive || WhoSolh == 2)
                    three++;
                if (WhoSolh == 0)
                    three++;

                if (three == 3)
                    return false;
            }

            if (attack == "cherik")
            {
                if (temp.coin < 4)
                    return false;
            }
            return true;
        }
        else if (action == 4)
        {
            // solh
            return true;
        }
        else if (action == 5)
        {
            //siasat 
            if (uniqe5 == "siasat")
            {
                int three = 0;
            if (temp == cpu1)
            {
                if (!cpu2.Alive || WhoSolh == 2)
                    three++;
                if (!cpu3.Alive || WhoSolh == 3)
                    three++;
                if (WhoSolh == 0)
                    three++;

                if (three == 3)
                    return false;
            }
            else if (temp == cpu2)
            {
                if (!cpu1.Alive || WhoSolh == 1)
                    three++;
                if (!cpu3.Alive || WhoSolh == 3)
                    three++;
                if (WhoSolh == 0)
                    three++;

                if (three == 3)
                    return false;
            }
            else if (temp == cpu3)
            {
                if (!cpu1.Alive || WhoSolh == 1)
                    three++;
                if (!cpu2.Alive || WhoSolh == 2)
                    three++;
                if (WhoSolh == 0)
                    three++;

                if (three == 3)
                    return false;
            }
            }
        }
        return false;
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

    void MeIconCheck()
    {
        if (Me.card1 == 1)
        {
            if (mali == "banker")
            {
                mycards[0].GetComponent<Image>().color = roleColor[0];
                myicon[0].GetComponent<Image>().sprite = Logo[0];
                mytext[0].text = "ﺭﺍﺪﮑﻧﺎﺑ";
            }
        }
        else if (Me.card1 == 2)
        {
            if (ertebat == "director")
            {   
                mycards[0].GetComponent<Image>().color = roleColor[1];
                myicon[0].GetComponent<Image>().sprite = Logo[1];
                mytext[0].text = "ﻥﺍﺩﺮﮔﺭﺎﮐ";
            }
        }
        else if (Me.card1 == 3)
        {
            if (attack == "cherik")
            {   
                mycards[0].GetComponent<Image>().color = roleColor[2];
                myicon[0].GetComponent<Image>().sprite = Logo[2];
                mytext[0].text = "ﮏﯾﺮﭼ";
            }
        }
        else if (Me.card1 == 4)
        {
            if (uniqe4 == "solh")
            {   
                mycards[0].GetComponent<Image>().color = roleColor[3];
                myicon[0].GetComponent<Image>().sprite = Logo[3];
                mytext[0].text = "ﺐﻠﻃ ﺢﻠﺻ";
            }
        }
        else if (Me.card1 == 5)
        {
            if (uniqe5 == "siasat")
            {   
                mycards[0].GetComponent<Image>().color = roleColor[4];
                myicon[0].GetComponent<Image>().sprite = Logo[4];
                mytext[0].text = "ﺭﺍﺪﻤﺘﺳﺎﯿﺳ";
            }
        }

        //
        
        if (Me.card2 == 1)
        {
            if (mali == "banker")
            {   
                mycards[1].GetComponent<Image>().color = roleColor[0];
                myicon[1].GetComponent<Image>().sprite = Logo[0];
                mytext[1].text = "ﺭﺍﺪﮑﻧﺎﺑ";
            }
        }
        else if (Me.card2 == 2)
        {
            if (ertebat == "director")
            {   
                mycards[1].GetComponent<Image>().color = roleColor[1];
                myicon[1].GetComponent<Image>().sprite = Logo[1];
                mytext[1].text = "ﻥﺍﺩﺮﮔﺭﺎﮐ";
            }
        }
        else if (Me.card2 == 3)
        {
            if (attack == "cherik")
            {   
                mycards[1].GetComponent<Image>().color = roleColor[2];
                myicon[1].GetComponent<Image>().sprite = Logo[2];
                mytext[1].text = "ﮏﯾﺮﭼ";
            }
        }
        else if (Me.card2 == 4)
        {
            if (uniqe4 == "solh")
            {   
                mycards[1].GetComponent<Image>().color = roleColor[3];
                myicon[1].GetComponent<Image>().sprite = Logo[3];
                mytext[1].text = "ﺐﻠﻃ ﺢﻠﺻ";
            }
        }
        else if (Me.card2 == 5)
        {
            if (uniqe5 == "siasat")
            {   
                mycards[1].GetComponent<Image>().color = roleColor[4];
                myicon[1].GetComponent<Image>().sprite = Logo[4];
                mytext[1].text = "ﺭﺍﺪﻤﺘﺳﺎﯿﺳ";
            }
        }
    }

    public void PAUSE()
    {
        if (!stop)
        {
            Time.timeScale = 0;
            stop = true;
            print("stop");
        }
        else
        {
            Time.timeScale = 1;
            stop = false;
            print("continue");
        } 
    }

    

    

    public void ChaleshBtn()
    {
        mychallange = true;
        meWait = true;
    }

    public void DisChaleshBtn()
    {
        mychallange = false;
        meWait = true;
    }

    public void reactionBtn()
    {
        myReaction = true;
        robotWait = true;
    }

    public void DisreactionBtn()
    {
        myReaction = false;
        robotWait = true;
    }

    void losingy()
    {
        StartCoroutine(losing());
    }

    public void burn(int num)
    {
        if (num == 1)
        {
            int box = Me.card1;
            Me.card1 = -1;
            mycards[0].SetActive(false);
            for (int i = 0; i < lost.Length; i++)
            {
                if (lost[i] == -1)
                {
                    lost[i] = box;
                    break;
                }
            }
            printLost();
        }
        else if (num == 2)
        {
            int box = Me.card2;
            Me.card2 = -1;
            mycards[1].SetActive(false);
            for (int i = 0; i < lost.Length; i++)
            {
                if (lost[i] == -1)
                {
                    lost[i] = box;
                    break;
                }
            }
            printLost();
        }
        losingClick = true;   
    }

    IEnumerator losing()
    {
        if (Me.card1 == -1)
        {
            losingCircle[0].SetActive(false);
        }
        if (Me.card2 == -1)
        {
            losingCircle[1].SetActive(false);
        }
        lostSection.SetActive(true);
        yield return new WaitUntil(() => losingClick == true);
        lostSection.SetActive(false);
        losetwice = true;
    }

    void AllCheckAlive()
    {
        if (Me.card1 == -1 && Me.card2 == -1)
            Me.Alive = false;

        if (cpu1.card1 == -1 && cpu1.card2 == -1)
            cpu1.Alive = false;

        if (cpu2.card1 == -1 && cpu2.card2 == -1)
            cpu2.Alive = false;

        if (cpu3.card1 == -1 && cpu3.card2 == -1)
            cpu3.Alive = false;
    }

    void next()
    {
        if (myturn)
        {
            myturn = false;
            if (cpu1.Alive)
                cpu1turn = true;
            else if (cpu2.Alive)
                cpu2turn = true;
            else if (cpu3.Alive)
                cpu3turn = true;
        }
        else if (cpu1turn)
        {
            cpu1turn = false;
            if (cpu2.Alive)
                cpu2turn = true;
            else if (cpu3.Alive)
                cpu3turn = true;
            else if (Me.Alive)
                myturn = true;
        }
        else if (cpu2turn)
        {
            cpu2turn = false;
            if (cpu3.Alive)
                cpu3turn = true;
            else if (Me.Alive)
                myturn = true;
            else if (cpu1.Alive)
                cpu1turn = true;
        }else if (cpu3turn)
        {
            cpu3turn = false;
            if (Me.Alive)
                myturn = true;
            else if (cpu1.Alive)
                cpu1turn = true;
            else if (cpu2.Alive)
                cpu2turn = true;
        }

       
    }

    void backToDeck(int naghsh)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] == -1)
            {
                numbers[i] = naghsh;
                break;
            }
        }
    }

    int getFromDeck()
    {
        int select, res;
        do
        {
            select = Random.Range(0, numbers.Length);
        } while (numbers[select] == -1);

        res = numbers[select];
        numbers[select] = -1;
        return res;
    }

    void Point()
    {
        print("yesssssss!");
        if (Me.Alive)
        {  // win

            int trophy = PlayerPrefs.GetInt("Trophy");
            trophy += Random.Range(15, 25);
            PlayerPrefs.SetInt("Trophy", trophy);

            int win = PlayerPrefs.GetInt("Win");
            win++;
            PlayerPrefs.SetInt("Win", win);

            int TheHighestTrophy = PlayerPrefs.GetInt("HighestTrophy");
            if (trophy > TheHighestTrophy)
                PlayerPrefs.SetInt("HighestTrophy", trophy);

        }
        else
        {  // lose
            int trophy = PlayerPrefs.GetInt("Trophy");
            trophy -= Random.Range(15, 25);
            if (trophy <= 0)
                PlayerPrefs.SetInt("Trophy", 0);
            else
                PlayerPrefs.SetInt("Trophy", trophy);

            int lose = PlayerPrefs.GetInt("Lose");
            lose++;
            PlayerPrefs.SetInt("lose", lose);
        }

        int playTimes = PlayerPrefs.GetInt("PlayTimes");
        playTimes++;
        PlayerPrefs.SetInt("PlayTimes",playTimes);

    }

    IEnumerator Robot()
    {
        //testing



        while (endgame != 0 && Me.Alive)
        {
            yield return new WaitUntil(() => Done == true);
            Done = false;
        //    yield return new WaitForSeconds(2f);
            if (myturn)
            {

                Meoffcheck();
                pannel.SetActive(true);

            }
            else
            {
                StartCoroutine(ActionRob());
            }
            yield return new WaitForSeconds(1);
        }

        print("endgame");
        // point
        Point();
    }


    void Start()
    {
        ShuffleArray(numbers, true);
        MeIconCheck();

        //testing
        
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
                announcer.color = Color.white;
                announcer.text = "ﻡﺭﺍﺪﮑﻧﺎﺑ";
            }
            yield return new WaitForSeconds(1.5f);
            announcer.color = Color.black;

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
        }else if (whichAction == "attack")
        {
            if (attack == "cherik")
            {   
                announcer.color = Color.white;
                announcer.text = "ﻢﮑﯾﺮﭼ";
            }
             yield return new WaitForSeconds(1.5f);
            announcer.color = Color.black;


            if (cpu1.Alive)
            {
                result[0] = cpu1.Chalesh(3, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu1Name;
            }
            yield return new WaitForSeconds(1);
            if (cpu2.Alive)
            {
                result[1] = cpu2.Chalesh(3, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu2Name;
            }
            yield return new WaitForSeconds(1);
            if (cpu3.Alive)
            {
                result[2] = cpu3.Chalesh(3, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu3Name;
            }
            yield return new WaitForSeconds(1);
        }else if (whichAction == "ertebat")
        {
            if (ertebat == "director")
            {   announcer.color = Color.white;
                announcer.text = "ﻢﻧﺍﺩﺮﮔﺭﺎﮐ";
            }
            yield return new WaitForSeconds(1.5f);
            announcer.color = Color.black;


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
        }  else if (whichAction == "uniqe4")
        {
            if (uniqe4 == "solh")
            {   announcer.color = Color.white;
                announcer.text = "ﻢﺒﻠﻃ ﺢﻠﺻ";
            }
            yield return new WaitForSeconds(1.5f);
            announcer.color = Color.black;


            if (cpu1.Alive)
            {
                result[0] = cpu1.Chalesh(4, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu1Name;
            }
            yield return new WaitForSeconds(1);
            if (cpu2.Alive)
            {
                result[1] = cpu2.Chalesh(4, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu2Name;
            }
            yield return new WaitForSeconds(1);
            if (cpu3.Alive)
            {
                result[2] = cpu3.Chalesh(4, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu3Name;
            }
            yield return new WaitForSeconds(1);
        }else if (whichAction == "uniqe5")
        {
            if (uniqe5 == "siasat")
            {   announcer.color = Color.white;
                announcer.text ="ﻡﺭﺍﺪﻤﺘﺳﺎﯿﺳ";
            }

           yield return new WaitForSeconds(1.5f);
            announcer.color = Color.black;

            if (cpu1.Alive)
            {
                result[0] = cpu1.Chalesh(5, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu1Name;
            }
            yield return new WaitForSeconds(1);
            if (cpu2.Alive)
            {
                result[1] = cpu2.Chalesh(5, lost);
                announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu2Name;
            }
            yield return new WaitForSeconds(1);
            if (cpu3.Alive)
            {
                result[2] = cpu3.Chalesh(5, lost);
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
        
       
        //

        if (permision)
        {
            if (whichAction == "mali")
                StartCoroutine(Mali());
            else if (whichAction == "ertebat")
            {
                StartCoroutine(ertebatat());
                yield return new WaitUntil(() => cClicked == true);
                cClicked = false;
            }
            else if (whichAction == "attack")
            {
                StartCoroutine(Attack());
                cClicked = false;
                yield return new WaitUntil(() => cClicked == true);
                cClicked = false;
            }
            else if (whichAction == "uniqe4")
                StartCoroutine(uniqe4y());
            else if (whichAction == "uniqe5")
            {   StartCoroutine(uniqe5y());
                cClicked = false;
                yield return new WaitUntil(() => cClicked == true);
                cClicked = false;
            }
                
        }
        else
        {

            Player temp = new Player();
            announcer.color = Color.gray;
            if (result[0])
            {
                announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ ﺍﺭ ﺎﻤﺷ " + name_script.cpu1Name;
                temp = cpu1;
            }
            else if (result[1])
            {
                announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ ﺍﺭ ﺎﻤﺷ " + name_script.cpu2Name;
                temp = cpu2;
            }
            else if (result[2])
            {
                announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ ﺍﺭ ﺎﻤﺷ " + name_script.cpu3Name;
                temp = cpu3;
            }


            yield return new WaitForSeconds(2);
            announcer.color = Color.black;

            int operation = 0;
            if (whichAction == "mali")
            {
                operation = 1;
            }
            else if (whichAction == "ertebat")
            {
                operation = 2;
            }
            else if (whichAction == "uniqe4")
            {
                operation = 4;
            }
            else if (whichAction == "uniqe5")
            {
                operation = 5;
            }
            else if (whichAction == "attack")
            {
                operation = 3;
            }

            if (Me.card1 == operation || Me.card2 == operation)
            {
                announcer.color = Color.green;
                announcer.text = "ﯼﺪﺷ ﻩﺪﻧﺮﺑ ﺍﺭ ﺶﻟﺎﭼ";
                yield return new WaitForSeconds(1.5f);
                announcer.color = Color.black;

                announcer.text = "ﺪﯾﺪﺟ ﺕﺭﺎﮐ ﺏﺎﺨﺘﻧﺍ ﻝﺎﺣ ﺭﺩ";


                if (Me.card1 == operation)
                {
                    myicon[0].GetComponent<Image>().sprite = null;
                    mycards[0].GetComponent<Image>().color = Color.white;
                    mytext[0].text = "";
                }
                else
                {
                    myicon[1].GetComponent<Image>().sprite = null;
                    mycards[1].GetComponent<Image>().color = Color.white;
                    mytext[1].text = "";
                }
                yield return new WaitForSeconds(2);

                backToDeck(operation);
                ShuffleArray(numbers, false);

                if (Me.card1 == operation)
                    Me.card1 = getFromDeck();
                else
                    Me.card2 = getFromDeck();

                MeIconCheck();
                announcer.text = "";
                yield return new WaitForSeconds(1.5f);

                int ran;
                do
                {
                    ran = Random.Range(1, 3);
                } while ((ran == 1 && temp.card1 == -1) || (ran == 2 && temp.card2 == -1));

                if (ran == 1)
                {
                    if (temp == cpu1)
                        cpu1cards[0].SetActive(false);
                    else if (temp == cpu2)
                        cpu2cards[0].SetActive(false);
                    else if (temp == cpu3)
                        cpu3cards[0].SetActive(false);

                    int box = temp.card1;
                    if (temp == cpu1)
                        cpu1.card1 = -1;
                    else if (temp == cpu2)
                        cpu2.card1 = -1;
                    else if (temp == cpu3)
                        cpu3.card1 = -1;

                    if (box == 1)
                    {
                        if (mali == "banker")
                        {
                            if (temp == cpu1)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                            else if (temp == cpu2)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                            else if (temp == cpu3)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                        }

                    }
                    else if (box == 2)
                    {
                        if (ertebat == "director")
                        {
                            if (temp == cpu1)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                            else if (temp == cpu2)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                            else if (temp == cpu3)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                        }

                    }
                    else if (box == 3)
                    {
                        if (attack == "cherik")
                        {
                            if (temp == cpu1)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                            else if (temp == cpu2)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                            else if (temp == cpu3)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                        }

                    }
                    else if (box == 4)
                    {
                        if (uniqe4 == "solh")
                        {
                            if (temp == cpu1)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                            else if (temp == cpu2)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                            else if (temp == cpu3)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                        }

                    }
                    else if (box == 5)
                    {
                        if (uniqe5 == "siasat")
                        {
                            if (temp == cpu1)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                            else if (temp == cpu2)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                            else if (temp == cpu3)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                        }

                    }

                    for (int i = 0; i < lost.Length; i++)
                    {
                        if (lost[i] == -1)
                        {
                            lost[i] = box;
                            break;
                        }
                    }
                    printLost();
                    yield return new WaitForSeconds(2);

                }
                else
                {
                    if (temp == cpu1)
                        cpu1cards[1].SetActive(false);
                    else if (temp == cpu2)
                        cpu2cards[1].SetActive(false);
                    else if (temp == cpu3)
                        cpu3cards[1].SetActive(false);
                    int box = temp.card2;

                    if (temp == cpu1)
                        cpu1.card2 = -1;
                    else if (temp == cpu2)
                        cpu2.card2 = -1;
                    else if (temp == cpu3)
                        cpu3.card2 = -1;

                    if (box == 1)
                    {
                        if (mali == "banker")
                        {
                            if (temp == cpu1)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                            else if (temp == cpu2)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                            else if (temp == cpu3)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                        }
                    }
                    else if (box == 2)
                    {
                        if (ertebat == "director")
                        {
                            if (temp == cpu1)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                            else if (temp == cpu2)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                            else if (temp == cpu3)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                        }
                    }
                    else if (box == 3)
                    {
                        if (attack == "cherik")
                        {
                            if (temp == cpu1)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                            else if (temp == cpu2)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                            else if (temp == cpu3)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                        }
                    }
                    else if (box == 4)
                    {
                        if (uniqe4 == "solh")
                        {
                            if (temp == cpu1)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                            else if (temp == cpu2)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                            else if (temp == cpu3)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                        }
                    }
                    else if (box == 5)
                    {
                        if (uniqe5 == "siasat")
                        {
                            if (temp == cpu1)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                            else if (temp == cpu2)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                            else if (temp == cpu3)
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                        }
                    }

                    for (int i = 0; i < lost.Length; i++)
                    {
                        if (lost[i] == -1)
                        {
                            lost[i] = box;
                            break;
                        }
                    }
                    printLost();
                    yield return new WaitForSeconds(2);
                }

                if (whichAction == "mali")
                    StartCoroutine(Mali());
                else if (whichAction == "ertebat")
                {
                    StartCoroutine(ertebatat());
                    yield return new WaitUntil(() => cClicked == true);
                    cClicked = false;
                }
                else if (whichAction == "attack")
                {
                    StartCoroutine(Attack());
                    cClicked = false;
                    yield return new WaitUntil(() => cClicked == true);
                    cClicked = false;
                }
                else if (whichAction == "uniqe4")
                    StartCoroutine(uniqe4y());
                else if (whichAction == "uniqe5")
                {
                    StartCoroutine(uniqe5y());
                    cClicked = false;
                    yield return new WaitUntil(() => cClicked == true);
                    cClicked = false;
                }
                    

                endgame--;
            }
            else
            {
                announcer.color = Color.red;
                announcer.text = "ﯼﺩﺭﻮﺧ ﺖﺴﮑﺷ";
                yield return new WaitForSeconds(1.5f);
                announcer.color = Color.black;
                announcer.text = "";

                losingy();
                yield return new WaitUntil(() => losingClick == true);
                losingClick = false;
            }

        }

        yield return new WaitForSeconds(1);
        AllCheckAlive();
        next();
        Done = true;
    }

    bool canAttack(int who) {
        if (who == 1)
        {
            if (cpu2.Alive && WhoSolh != 2)
                return true;
            else if (cpu3.Alive && WhoSolh != 3)
                return true;
            else if (Me.Alive && WhoSolh != 0)
                return true;
            else
                return false;
        }
        else if (who == 2)
        {
            if (cpu1.Alive && WhoSolh != 1)
                return true;
            else if (cpu3.Alive && WhoSolh != 3)
                return true;
            else if (Me.Alive && WhoSolh != 0)
                return true;
            else
                return false;
        }
        else if (who == 3)
        {
            if (cpu1.Alive && WhoSolh != 1)
                return true;
            else if (cpu2.Alive && WhoSolh != 2)
                return true;
            else if (Me.Alive && WhoSolh != 0)
                return true;
            else
                return false;
        }

        return false;
    }

    IEnumerator RobAttack() {
        yield return new WaitForSeconds(2);

        if (cpu1turn)
        {
            if (attack == "cherik")
            {
                cpu1.coin -= 4;
                cointxt[1].text = cpu1.coin.ToString();
            }
        

            int ran;
            do
            {
                ran = Random.Range(0, 4); 
            } while ((ran == 1) || (ran == 2 && !cpu2.Alive) || (ran == 3 && !cpu3.Alive) || (ran == WhoSolh));

            //testing
           

            if (ran == 2)
            {
                
                announcer.text = " ﺩﺮﮐ ﻪﻠﻤﺣ " + name_script.cpu2Name + " ﻪﺑ " + name_script.cpu1Name;
                yield return new WaitForSeconds(4);
                if (cpu2.card1 == 3 || cpu2.card2 == 3)
                {
                    announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu2Name;
                    yield return new WaitForSeconds(2);

                    int ran2 = Random.Range(1, 4);
                    if (ran2 == 1)
                    {
                        announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu2Name + " " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);
                        announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);

                        int ran3;
                        do
                        {
                            ran3 = Random.Range(1, 3);
                        } while ((ran3 == 1 && cpu1.card1 == -1) || (ran3 == 2 && cpu1.card2 == -1));

                        int box = 0;
                        if (ran3 == 1)
                        {
                            box = cpu1.card1;
                            cpu1.card1 = -1;
                            cpu1cards[0].SetActive(false);
                        }
                        else if (ran3 == 2)
                        {
                            box = cpu1.card2;
                            cpu1.card2 = -1;
                            cpu1cards[1].SetActive(false);
                        }




                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);

                      endgame--;
                    }
                    else
                    {
                        announcer.text = " ﺩﺮﮐ ﻝﻮﺒﻗ " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);
                        //done;
                    }
                }
                else
                {
                    int ran4 = Random.Range(1, 5);
                    //testing
                   
                    //
                    if (ran4 == 1)
                    {
                        print("BLOF");
                        announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu2Name;
                        yield return new WaitForSeconds(2);

                        int ran2 = Random.Range(1, 4);
                        if (ran2 == 1)
                        {
                            announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu2Name + " " + name_script.cpu1Name;
                            yield return new WaitForSeconds(2);
                            announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu1Name;
                            yield return new WaitForSeconds(2);

                            int box = 0;
                            if (cpu2.card1 != -1)
                            {
                                box = cpu2.card1;
                                cpu2.card1 = -1;
                                cpu2cards[0].SetActive(false);


                                if (box == 1)
                                {
                                    if (mali == "banker")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                                    }
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                            }
                            if (cpu2.card2 != -1)
                            {
                                box = cpu2.card2;
                                cpu2.card2 = -1;
                                cpu2cards[1].SetActive(false);

                                if (box == 1)
                                {
                                    if (mali == "banker")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                                    }
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                            }



                         endgame-=2;
                        }
                        else
                        {
                            announcer.text = " ﺩﺮﮐ ﻝﻮﺒﻗ " + name_script.cpu1Name;
                            yield return new WaitForSeconds(2);
                            //done;
                        }
                    }
                    else
                    {
                        int ran3;
                        do
                        {
                            ran3 = Random.Range(1, 3);
                        } while ((ran3 == 1 && cpu2.card1 == -1) || (ran3 == 2 && cpu2.card2 == -1));

                        int box = 0;
                        if (ran3 == 1)
                        {
                            box = cpu2.card1;
                            cpu2.card1 = -1;
                            cpu2cards[0].SetActive(false);
                        }
                        else if (ran3 == 2)
                        {
                            box = cpu2.card2;
                            cpu2.card2 = -1;
                            cpu2cards[1].SetActive(false);
                        }




                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);
                     endgame--;
                    }
                }
            }
            else if (ran == 3)
            {print("here3");
                

                announcer.text = " ﺩﺮﮐ ﻪﻠﻤﺣ " + name_script.cpu3Name + " ﻪﺑ " + name_script.cpu1Name;
                yield return new WaitForSeconds(4);
                if (cpu3.card1 == 3 || cpu3.card2 == 3)
                {
                    announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu3Name;
                    yield return new WaitForSeconds(2);

                    int ran2 = Random.Range(1, 4);
                    if (ran2 == 1)
                    {
                        announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu3Name + " " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);
                        announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);

                        int ran3;
                        do
                        {
                            ran3 = Random.Range(1, 3);
                        } while ((ran3 == 1 && cpu1.card1 == -1) || (ran3 == 2 && cpu1.card2 == -1));

                        int box = 0;
                        if (ran3 == 1)
                        {
                            box = cpu1.card1;
                            cpu1.card1 = -1;
                            cpu1cards[0].SetActive(false);
                        }
                        else if (ran3 == 2)
                        {
                            box = cpu1.card2;
                            cpu1.card2 = -1;
                            cpu1cards[1].SetActive(false);
                        }




                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);

                     endgame--;
                    }
                    else
                    {
                        announcer.text = " ﺩﺮﮐ ﻝﻮﺒﻗ " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);
                        //done;
                    }
                }
                else
                {
                    int ran4 = Random.Range(1, 5);
                    if (ran4 == 1)
                    {
                        print("BLOF");
                        announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);

                        int ran2 = Random.Range(1, 4);
                        if (ran2 == 1)
                        {
                            announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu3Name + " " + name_script.cpu1Name;
                            yield return new WaitForSeconds(2);
                            announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu1Name;
                            yield return new WaitForSeconds(2);



                            int box = 0;
                            if (cpu3.card1 != -1)
                            {
                                box = cpu3.card1;
                                cpu3.card1 = -1;
                                cpu3cards[0].SetActive(false);
                                if (box == 1)
                                {
                                    if (mali == "banker")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                    }
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                            }
                            if (cpu3.card2 != -1)
                            {
                                box = cpu3.card2;
                                cpu3.card2 = -1;
                                cpu3cards[1].SetActive(false);
                                if (box == 1)
                                {
                                    if (mali == "banker")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                    }
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                            }





                         endgame-=2;
                        }
                        else
                        {
                            announcer.text = " ﺩﺮﮐ ﻝﻮﺒﻗ " + name_script.cpu1Name;
                            yield return new WaitForSeconds(2);
                            //done;
                        }
                    }
                    else
                    {
                        int ran3;
                        do
                        {
                            ran3 = Random.Range(1, 3);
                        } while ((ran3 == 1 && cpu3.card1 == -1) || (ran3 == 2 && cpu3.card2 == -1));

                        int box = 0;
                        if (ran3 == 1)
                        {
                            box = cpu3.card1;
                            cpu3.card1 = -1;
                            cpu3cards[0].SetActive(false);
                        }
                        else if (ran3 == 2)
                        {
                            box = cpu3.card2;
                            cpu3.card2 = -1;
                            cpu3cards[1].SetActive(false);
                        }




                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);
                      endgame--;
                    }
                }
            }
            else if (ran == 0)
            {print("wow");
                announcer.text = " ﺩﺮﮐ ﻪﻠﻤﺣ ﺎﻤﺷ ﻪﺑ " + name_script.cpu1Name;
                yield return new WaitForSeconds(2.5f);
                Reaction.SetActive(true);
                yield return new WaitUntil(()=> robotWait == true);
                robotWait = false;
                 
                print("myreaction = " + myReaction);
                yield return new WaitForSeconds(1);
                if (myReaction)
                {
                    int ran3 = Random.Range(1, 4);
                    if (ran3 == 1)
                    {
                        announcer.text = " ﺖﻓﺮﯾﺬﭙﻧ ﺍﺭ ﺎﻤﺷ ﻡﺍﺪﻗﺍ " + name_script.cpu1Name;
                        yield return new WaitForSeconds(1);
                        

                        if (Me.card1 == 3 || Me.card2 == 3)
                        {announcer.color = Color.green;
                        announcer.text =  " ﯼﺪﺷ ﻩﺪﻧﺮﺑ ";
                        yield return new WaitForSeconds(1.5f);
                        announcer.text = "";
                        announcer.color = Color.black;
                            int ran2;
                            do
                            {
                                ran2 = Random.Range(1, 3);
                            } while ((ran2 == 1 && cpu1.card1 == -1) || (ran2 == 2 && cpu1.card2 == -1));

                            int box = 0;
                            if (ran2 == 1)
                            {
                                box = cpu1.card1;
                                cpu1.card1 = -1;
                                cpu1cards[0].SetActive(false);
                            }
                            else
                            {
                                box = cpu1.card2;
                                cpu1.card2 = -1;
                                cpu1cards[1].SetActive(false);
                            }

                            if (box == 1)
                            {
                                if (mali == "banker")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                                }
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                                }
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                                }
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                                }
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                                }
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);

                            endgame--;
                        }
                        else
                        {
                        announcer.color = Color.green;
                        announcer.text =  " ﯼﺩﺭﻮﺧ ﺖﺴﮑﺷ ";
                        yield return new WaitForSeconds(1.5f);
                        announcer.text = "";
                        announcer.color = Color.black;
                            int joon = 0;
                            if (Me.card1 != -1)
                                joon++;
                            if (Me.card2 != -1)
                                joon++;
                            if (joon == 2)
                            {
                                losingy();
                                yield return new WaitUntil(() => losetwice == true);
                                losetwice = false;
                                losingy();
                                yield return new WaitUntil(() => losetwice == true);
                                losetwice = false;
                            }
                            else
                            {
                                losingy();
                                yield return new WaitUntil(() => losingClick == true);
                                losingClick = false;
                            }

                        }
                    }
                    else
                    {announcer.text = " ﺖﻓﺮﯾﺬﭙ ﺍﺭ ﺎﻤﺷ ﻡﺍﺪﻗﺍ " + name_script.cpu1Name;
                        yield return new WaitForSeconds(1);
                        //done;
                    }
                }
                else
                {
                    losingy();
                    yield return new WaitUntil(() => losingClick == true);
                    losingClick = false;
                }

            }

        }
        else if (cpu2turn)
        {
            if (attack == "cherik")
            {
                cpu2.coin -= 4;
                cointxt[2].text = cpu2.coin.ToString();
            }

        


            int ran;
            do
            {
                ran = Random.Range(0, 4);
            } while ((ran == 2) || (ran == 1 && !cpu1.Alive) || (ran == 3 && !cpu3.Alive) || (ran == WhoSolh));

            if (ran == 1)
            {
                announcer.text = " ﺩﺮﮐ ﻪﻠﻤﺣ " + name_script.cpu1Name + " ﻪﺑ " + name_script.cpu2Name;
                yield return new WaitForSeconds(4);
                if (cpu1.card1 == 3 || cpu1.card2 == 3)
                {
                    announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu1Name;
                    yield return new WaitForSeconds(2);

                    int ran2 = Random.Range(1, 4);
                    if (ran2 == 1)
                    {
                        announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu1Name + " " + name_script.cpu2Name;
                        yield return new WaitForSeconds(2);
                        announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu2Name;
                        yield return new WaitForSeconds(2);

                        int ran3;
                        do
                        {
                            ran3 = Random.Range(1, 3);
                        } while ((ran3 == 1 && cpu2.card1 == -1) || (ran3 == 2 && cpu2.card2 == -1));

                        int box = 0;
                        if (ran3 == 1)
                        {
                            box = cpu2.card1;
                            cpu2.card1 = -1;
                            cpu2cards[0].SetActive(false);
                        }
                        else if (ran3 == 2)
                        {
                            box = cpu2.card2;
                            cpu2.card2 = -1;
                            cpu2cards[1].SetActive(false);
                        }




                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);

endgame--;
                    }
                    else
                    {
                        announcer.text = " ﺩﺮﮐ ﻝﻮﺒﻗ " + name_script.cpu2Name;
                        yield return new WaitForSeconds(2);
                        //done;
                    }
                }
                else
                {
                    int ran4 = Random.Range(1, 5);
                    if (ran4 == 1)
                    {
                        print("BLOF");
                        announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);

                        int ran2 = Random.Range(1, 4);
                        if (ran2 == 1)
                        {
                            announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu1Name + " " + name_script.cpu2Name;
                            yield return new WaitForSeconds(2);
                            announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu2Name;
                            yield return new WaitForSeconds(2);

                         

                            int box = 0;
                            if (cpu1.card1 != -1)
                            {
                                box = cpu1.card1;
                                cpu1.card1 = -1;
                                cpu1cards[0].SetActive(false);
                                if (box == 1)
                                {
                                    if (mali == "banker")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                    }
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                            }
                            if (cpu1.card2 != -1)
                            {
                                box = cpu1.card2;
                                cpu1.card2 = -1;
                                cpu1cards[1].SetActive(false);
                                if (box == 1)
                            {
                                if (mali == "banker")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                }
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);
                            }




                        
endgame-=2;

                        }
                        else
                        {
                            announcer.text = " ﺩﺮﮐ ﻝﻮﺒﻗ " + name_script.cpu2Name;
                            yield return new WaitForSeconds(2);
                            //done;
                        }
                    }
                    else
                    {
                        int ran3;
                        do
                        {
                            ran3 = Random.Range(1, 3);
                        } while ((ran3 == 1 && cpu1.card1 == -1) || (ran3 == 2 && cpu1.card2 == -1));

                        int box = 0;
                        if (ran3 == 1)
                        {
                            box = cpu1.card1;
                            cpu1.card1 = -1;
                            cpu1cards[0].SetActive(false);
                        }
                        else if (ran3 == 2)
                        {
                            box = cpu1.card2;
                            cpu1.card2 = -1;
                            cpu1cards[1].SetActive(false);
                        }




                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);
endgame--;
                    }
                }
            }
            else if (ran == 3)
            {

                announcer.text = " ﺩﺮﮐ ﻪﻠﻤﺣ " + name_script.cpu3Name + " ﻪﺑ " + name_script.cpu2Name;
                yield return new WaitForSeconds(4);
                if (cpu3.card1 == 3 || cpu3.card2 == 3)
                {
                    announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu3Name;
                    yield return new WaitForSeconds(2);

                    int ran2 = Random.Range(1, 4);
                    if (ran2 == 1)
                    {
                        announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu3Name + " " + name_script.cpu2Name;
                        yield return new WaitForSeconds(2);
                        announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu2Name;
                        yield return new WaitForSeconds(2);

                        int ran3;
                        do
                        {
                            ran3 = Random.Range(1, 3);
                        } while ((ran3 == 1 && cpu2.card1 == -1) || (ran3 == 2 && cpu2.card2 == -1));

                        int box = 0;
                        if (ran3 == 1)
                        {
                            box = cpu2.card1;
                            cpu2.card1 = -1;
                            cpu2cards[0].SetActive(false);
                        }
                        else if (ran3 == 2)
                        {
                            box = cpu2.card2;
                            cpu2.card2 = -1;
                            cpu2cards[1].SetActive(false);
                        }




                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);

endgame--;
                    }
                    else
                    {
                        announcer.text = " ﺩﺮﮐ ﻝﻮﺒﻗ " + name_script.cpu2Name;
                        yield return new WaitForSeconds(2);
                        //done;
                    }
                }
                else
                {
                    int ran4 = Random.Range(1, 5);
                    if (ran4 == 1)
                    {
                        print("BLOF");
                        announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);

                        int ran2 = Random.Range(1, 4);
                        if (ran2 == 1)
                        {
                            announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu3Name + " " + name_script.cpu2Name;
                            yield return new WaitForSeconds(2);
                            announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu2Name;
                            yield return new WaitForSeconds(2);

                        

                            int box = 0;
                            if (cpu3.card1 != -1)
                            {
                                box = cpu3.card1;
                                cpu3.card1 = -1;
                                cpu3cards[0].SetActive(false);
                                if (box == 1)
                                {
                                    if (mali == "banker")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                    }
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                            }
                            if (cpu3.card2 != -1)
                            {
                                box = cpu3.card2;
                                cpu3.card2 = -1;
                                cpu3cards[1].SetActive(false);
                                if (box == 1)
                            {
                                if (mali == "banker")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                }
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);
                            }




                           
endgame-=2;

                        }
                        else
                        {
                            announcer.text = " ﺩﺮﮐ ﻝﻮﺒﻗ " + name_script.cpu2Name;
                            yield return new WaitForSeconds(2);
                            //done;
                        }
                    }
                    else
                    {
                        int ran3;
                        do
                        {
                            ran3 = Random.Range(1, 3);
                        } while ((ran3 == 1 && cpu3.card1 == -1) || (ran3 == 2 && cpu3.card2 == -1));

                        int box = 0;
                        if (ran3 == 1)
                        {
                            box = cpu3.card1;
                            cpu3.card1 = -1;
                            cpu3cards[0].SetActive(false);
                        }
                        else if (ran3 == 2)
                        {
                            box = cpu3.card2;
                            cpu3.card2 = -1;
                            cpu3cards[1].SetActive(false);
                        }




                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);
endgame--;
                    }
                }
            }
            else if (ran == 0)
            {
                announcer.text = " ﺩﺮﮐ ﻪﻠﻤﺣ ﺎﻤﺷ ﻪﺑ " + name_script.cpu2Name;
                yield return new WaitForSeconds(2.5f);
                Reaction.SetActive(true);
                yield return new WaitUntil(()=> robotWait == true);
                robotWait = false;
 yield return new WaitForSeconds(1);
                if (myReaction)
                {
                    int ran3 = Random.Range(1, 4);
                    if (ran3 == 1)
                    {
                         announcer.text = " ﺖﻓﺮﯾﺬﭙﻧ ﺍﺭ ﺎﻤﺷ ﻡﺍﺪﻗﺍ " + name_script.cpu2Name;
                        yield return new WaitForSeconds(1);
                        if (Me.card1 == 3 || Me.card2 == 3)
                        {announcer.color = Color.green;
                        announcer.text =  " ﯼﺪﺷ ﻩﺪﻧﺮﺑ ";
                        yield return new WaitForSeconds(1.5f);
                        announcer.text = "";
                        announcer.color = Color.black;
                            int ran2;
                            do
                            {
                                ran2 = Random.Range(1, 3);
                            } while ((ran2 == 1 && cpu2.card1 == -1) || (ran2 == 2 && cpu2.card2 == -1));

                            int box = 0;
                            if (ran2 == 1)
                            {
                                box = cpu2.card1;
                                cpu2.card1 = -1;
                                cpu2cards[0].SetActive(false);
                            }
                            else
                            {
                                box = cpu2.card2;
                                cpu2.card2 = -1;
                                cpu2cards[1].SetActive(false);
                            }

                            if (box == 1)
                            {
                                if (mali == "banker")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                                }
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                                }
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                                }
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                                }
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                                }
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);

                            endgame--;
                        }
                        else
                        {announcer.color = Color.green;
                        announcer.text =  " ﯼﺩﺭﻮﺧ ﺖﺴﮑﺷ ";
                        yield return new WaitForSeconds(1.5f);
                        announcer.text = "";
                        announcer.color = Color.black;
                            int joon = 0;
                            if (Me.card1 != -1)
                                joon++;
                            if (Me.card2 != -1)
                                joon++;
                            if (joon == 2)
                            {
                                 losingy();
                                yield return new WaitUntil(() => losetwice == true);
                                losetwice = false;
                                losingy();
                                yield return new WaitUntil(() => losetwice == true);
                                losetwice = false;
                            }
                            else
                            {
                            losingy();
                                yield return new WaitUntil(() => losingClick == true);
                                losingClick = false;
                              
                            }

                        }
                    }
                    else
                    {announcer.text = " ﺖﻓﺮﯾﺬﭙ ﺍﺭ ﺎﻤﺷ ﻡﺍﺪﻗﺍ " + name_script.cpu2Name;
                        yield return new WaitForSeconds(1);
                        //done;
                    }
                }
                else
                {
                                 losingy();
                                yield return new WaitUntil(() => losingClick == true);
                                losingClick = false;
                              
                }

            }
        }
        else if (cpu3turn)
        {
            if (attack == "cherik")
            {
                cpu3.coin -= 4;
                cointxt[3].text = cpu3.coin.ToString();
            }

           


            int ran;
            do
            {
                ran = Random.Range(0, 4);
            } while ((ran == 3) || (ran == 1 && !cpu1.Alive) || (ran == 2 && !cpu2.Alive) || (ran == WhoSolh));

            if (ran == 1)
            {
                announcer.text = " ﺩﺮﮐ ﻪﻠﻤﺣ " + name_script.cpu1Name + " ﻪﺑ " + name_script.cpu3Name;
                yield return new WaitForSeconds(4);
                if (cpu1.card1 == 3 || cpu1.card2 == 3)
                {
                    announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu1Name;
                    yield return new WaitForSeconds(2);

                    int ran2 = Random.Range(1, 4);
                    if (ran2 == 1)
                    {
                        announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu1Name + " " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);
                        announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);

                        int ran3;
                        do
                        {
                            ran3 = Random.Range(1, 3);
                        } while ((ran3 == 1 && cpu3.card1 == -1) || (ran3 == 2 && cpu3.card2 == -1));

                        int box = 0;
                        if (ran3 == 1)
                        {
                            box = cpu3.card1;
                            cpu3.card1 = -1;
                            cpu3cards[0].SetActive(false);
                        }
                        else if (ran3 == 2)
                        {
                            box = cpu3.card2;
                            cpu3.card2 = -1;
                            cpu3cards[1].SetActive(false);
                        }




                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);

endgame--;
                    }
                    else
                    {
                        announcer.text = " ﺩﺮﮐ ﻝﻮﺒﻗ " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);
                        //done;
                    }
                }
                else
                {
                    int ran4 = Random.Range(1, 5);
                    if (ran4 == 1)
                    {
                        print("BLOF");
                        announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);

                        int ran2 = Random.Range(1, 4);
                        if (ran2 == 1)
                        {
                            announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu1Name + " " + name_script.cpu3Name;
                            yield return new WaitForSeconds(2);
                            announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu3Name;
                            yield return new WaitForSeconds(2);

                      

                            int box = 0;
                            if (cpu1.card1 != -1)
                            {
                                box = cpu1.card1;
                                cpu1.card1 = -1;
                                cpu1cards[0].SetActive(false);
                                if (box == 1)
                            {
                                if (mali == "banker")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                }
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);
                            }
                            if (cpu1.card2 != -1)
                            {
                                box = cpu1.card2;
                                cpu1.card2 = -1;
                                cpu1cards[1].SetActive(false);
                                if (box == 1)
                            {
                                if (mali == "banker")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                }
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);
                            }



endgame-=2;
                        }
                        else
                        {
                            announcer.text = " ﺩﺮﮐ ﻝﻮﺒﻗ " + name_script.cpu3Name;
                            yield return new WaitForSeconds(2);
                            //done;
                        }
                    }
                    else
                    {
                        int ran3;
                        do
                        {
                            ran3 = Random.Range(1, 3);
                        } while ((ran3 == 1 && cpu1.card1 == -1) || (ran3 == 2 && cpu1.card2 == -1));

                        int box = 0;
                        if (ran3 == 1)
                        {
                            box = cpu1.card1;
                            cpu1.card1 = -1;
                            cpu1cards[0].SetActive(false);
                        }
                        else if (ran3 == 2)
                        {
                            box = cpu1.card2;
                            cpu1.card2 = -1;
                            cpu1cards[1].SetActive(false);
                        }




                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);
endgame--;
                    }
                }
            }
            else if (ran == 2)
            {

                announcer.text = " ﺩﺮﮐ ﻪﻠﻤﺣ " + name_script.cpu2Name + " ﻪﺑ " + name_script.cpu3Name;
                yield return new WaitForSeconds(4);
                if (cpu2.card1 == 3 || cpu2.card2 == 3)
                {
                    announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu2Name;
                    yield return new WaitForSeconds(2);

                    int ran2 = Random.Range(1, 4);
                    if (ran2 == 1)
                    {
                        announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu2Name + " " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);
                        announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);

                        int ran3;
                        do
                        {
                            ran3 = Random.Range(1, 3);
                        } while ((ran3 == 1 && cpu3.card1 == -1) || (ran3 == 2 && cpu3.card2 == -1));

                        int box = 0;
                        if (ran3 == 1)
                        {
                            box = cpu3.card1;
                            cpu3.card1 = -1;
                            cpu3cards[0].SetActive(false);
                        }
                        else if (ran3 == 2)
                        {
                            box = cpu3.card2;
                            cpu3.card2 = -1;
                            cpu3cards[1].SetActive(false);
                        }




                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);

endgame--;
                    }
                    else
                    {
                        announcer.text = " ﺩﺮﮐ ﻝﻮﺒﻗ " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);
                        //done;
                    }
                }
                else
                {
                    int ran4 = Random.Range(1, 5);
                    if (ran4 == 1)
                    {
                        print("BLOF");
                        announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu2Name;
                        yield return new WaitForSeconds(2);

                        int ran2 = Random.Range(1, 4);
                        if (ran2 == 1)
                        {
                            announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu2Name + " " + name_script.cpu3Name;
                            yield return new WaitForSeconds(2);
                            announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu3Name;
                            yield return new WaitForSeconds(2);

                           

                            int box = 0;
                            if (cpu2.card1 != -1)
                            {
                                box = cpu2.card1;
                                cpu2.card1 = -1;
                                cpu2cards[0].SetActive(false);
                                if (box == 1)
                            {
                                if (mali == "banker")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                }
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);
                            }
                            if (cpu2.card2 != -1)
                            {
                                box = cpu2.card2;
                                cpu2.card2 = -1;
                                cpu2cards[1].SetActive(false);
                                if (box == 1)
                            {
                                if (mali == "banker")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                }
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);
                            }




                           

endgame-=2;
                        }
                        else
                        {
                            announcer.text = " ﺩﺮﮐ ﻝﻮﺒﻗ " + name_script.cpu3Name;
                            yield return new WaitForSeconds(2);
                            //done;
                        }
                    }
                    else
                    {
                        int ran3;
                        do
                        {
                            ran3 = Random.Range(1, 3);
                        } while ((ran3 == 1 && cpu2.card1 == -1) || (ran3 == 2 && cpu2.card2 == -1));

                        int box = 0;
                        if (ran3 == 1)
                        {
                            box = cpu2.card1;
                            cpu2.card1 = -1;
                            cpu2cards[0].SetActive(false);
                        }
                        else if (ran3 == 2)
                        {
                            box = cpu2.card2;
                            cpu2.card2 = -1;
                            cpu2cards[1].SetActive(false);
                        }




                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);
endgame--;
                    }
                }
            }
            else if (ran == 0)
            {
                 announcer.text = " ﺩﺮﮐ ﻪﻠﻤﺣ ﺎﻤﺷ ﻪﺑ " + name_script.cpu3Name;
                yield return new WaitForSeconds(2.5f);
                Reaction.SetActive(true);
                yield return new WaitUntil(()=> robotWait == true);
                robotWait = false;
 yield return new WaitForSeconds(1);

                if (myReaction)
                {
                    int ran3 = Random.Range(1, 4);
                    if (ran3 == 1)
                    {  announcer.text = " ﺖﻓﺮﯾﺬﭙﻧ ﺍﺭ ﺎﻤﺷ ﻡﺍﺪﻗﺍ " + name_script.cpu3Name;
                        yield return new WaitForSeconds(1);
                        if (Me.card1 == 3 || Me.card2 == 3)
                        {announcer.color = Color.green;
                        announcer.text =  " ﯼﺪﺷ ﻩﺪﻧﺮﺑ ";
                        yield return new WaitForSeconds(1.5f);
                        announcer.text = "";
                        announcer.color = Color.black;
                            int ran2;
                            do
                            {
                                ran2 = Random.Range(1, 3);
                            } while ((ran2 == 1 && cpu3.card1 == -1) || (ran2 == 2 && cpu3.card2 == -1));

                            int box = 0;
                            if (ran2 == 1)
                            {
                                box = cpu3.card1;
                                cpu3.card1 = -1;
                                cpu3cards[0].SetActive(false);
                            }
                            else
                            {
                                box = cpu3.card2;
                                cpu3.card2 = -1;
                                cpu3cards[1].SetActive(false);
                            }

                            if (box == 1)
                            {
                                if (mali == "banker")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                                }
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                {
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                }
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);

                            endgame--;
                        }
                        else
                        {announcer.color = Color.green;
                        announcer.text =  " ﯼﺩﺭﻮﺧ ﺖﺴﮑﺷ ";
                        yield return new WaitForSeconds(1.5f);
                        announcer.text = "";
                        announcer.color = Color.black;
                            int joon = 0;
                            if (Me.card1 != -1)
                                joon++;
                            if (Me.card2 != -1)
                                joon++;
                            if (joon == 2)
                            {
                                losingy();
                                yield return new WaitUntil(() => losetwice == true);
                                losetwice = false;
                                losingy();
                                yield return new WaitUntil(() => losetwice == true);
                                losetwice = false;
                            }
                            else
                            {
                               losingy();
                                yield return new WaitUntil(() => losingClick == true);
                                losingClick = false;
                                
                            }

                        }
                    }
                    else
                    {announcer.text = " ﺖﻓﺮﯾﺬﭙ ﺍﺭ ﺎﻤﺷ ﻡﺍﺪﻗﺍ " + name_script.cpu3Name;
                        yield return new WaitForSeconds(1);
                        //done;
                    }
                }
                else
                {
                    losingy();
                                yield return new WaitUntil(() => losingClick == true);
                                losingClick = false;
                               
                }

            }
        }
        cClicked = true;
    }

    IEnumerator Robuniqe5()
    {
        if (uniqe5 == "siasat")
        {
            if (cpu1turn)
            {
                bool can = true;
                can = canAttack(1);

                if (can)
                {

                    int ran;
                    do
                    {
                        ran = Random.Range(0, 4);
                    } while ((ran == 1) || (ran == 2 && !cpu2.Alive) || (ran == 3 && !cpu3.Alive) || (ran == WhoSolh));

                    if (ran == 2)
                    {
                        if (cpu2.card1 == 5 || cpu2.card2 == 5)
                        {
                            announcer.text = " ﻡﺭﺍﺩ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                            yield return new WaitForSeconds(2);

                            int ran2 = Random.Range(1, 4);
                            if (ran2 == 1)
                            {
                                announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu2Name + " " + name_script.cpu1Name;
                                yield return new WaitForSeconds(2);
                                announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu1Name;
                                yield return new WaitForSeconds(2);

                                int ran3;
                                do
                                {
                                    ran3 = Random.Range(1, 3);
                                } while ((ran3 == 1 && cpu1.card1 == -1) || (ran3 == 1 && cpu1.card2 == -1));

                                int box = 0;
                                if (ran3 == 1)
                                {
                                    box = cpu1.card1;
                                    cpu1.card1 = -1;
                                    cpu1cards[0].SetActive(false);
                                }
                                else
                                {
                                    box = cpu1.card2;
                                    cpu1.card2 = -1;
                                    cpu1cards[1].SetActive(false);
                                }

                                if (box == 1)
                                {
                                    if (mali == "banker")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu1Name;
                                    }
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu1Name;
                                    }
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu1Name;
                                    }
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu1Name;
                                    }
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu1Name;
                                    }
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                                endgame--;
                            }
                            else
                            {
                                //done;
                            }

                        }
                        else
                        {
                            int ran3 = Random.Range(1, 4);
                            if (ran3 == 1)
                            {
                                print("BLOF");
                                announcer.text = " ﻡﺭﺍﺩ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                                yield return new WaitForSeconds(2);

                                int ran4 = Random.Range(1, 4);
                                if (ran4 == 1)
                                {
                                    announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu2Name + " " + name_script.cpu1Name;
                                    yield return new WaitForSeconds(2);
                                    announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu1Name;
                                    yield return new WaitForSeconds(2);

                                    int ran5;
                                    do
                                    {
                                        ran5 = Random.Range(1, 3);
                                    } while ((ran5 == 1 && cpu2.card1 == -1) || (ran5 == 1 && cpu2.card2 == -1));

                                    int box = 0;
                                    if (ran5 == 1)
                                    {
                                        box = cpu2.card1;
                                        cpu2.card1 = -1;
                                        cpu2cards[0].SetActive(false);
                                    }
                                    else
                                    {
                                        box = cpu2.card2;
                                        cpu2.card2 = -1;
                                        cpu2cards[1].SetActive(false);
                                    }

                                    if (box == 1)
                                    {
                                        if (mali == "banker")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu2Name;
                                        }
                                    }
                                    else if (box == 2)
                                    {
                                        if (ertebat == "director")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu2Name;
                                        }
                                    }
                                    else if (box == 3)
                                    {
                                        if (attack == "cherik")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu2Name;
                                        }
                                    }
                                    else if (box == 4)
                                    {
                                        if (uniqe4 == "solh")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu2Name;
                                        }
                                    }
                                    else if (box == 5)
                                    {
                                        if (uniqe5 == "siasat")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu2Name;
                                        }
                                    }

                                    for (int i = 0; i < lost.Length; i++)
                                    {
                                        if (lost[i] == -1)
                                        {
                                            lost[i] = box;
                                            break;
                                        }
                                    }
                                    printLost();
                                    yield return new WaitForSeconds(2);

                                    if (cpu2.coin >= 2)
                                    {
                                        cpu2.coin -= 2;
                                        cpu1.coin += 2;
                                    }
                                    else
                                    {
                                        cpu1.coin += cpu2.coin;
                                        cpu2.coin = 0;
                                    }
                                    cointxt[1].text = cpu1.coin.ToString();
                                    cointxt[2].text = cpu2.coin.ToString();
                                    endgame--;
                                }
                                else
                                {
                                    //done;
                                }
                            }
                            else
                            {
                                if (cpu2.coin >= 2)
                                {
                                    cpu2.coin -= 2;
                                    cpu1.coin += 2;
                                }
                                else
                                {
                                    cpu1.coin += cpu2.coin;
                                    cpu2.coin = 0;
                                }
                                cointxt[1].text = cpu1.coin.ToString();
                                cointxt[2].text = cpu2.coin.ToString();
                            }
                        }
                    }
                    else if (ran == 3)
                    {
                        if (cpu3.card1 == 5 || cpu3.card2 == 5)
                        {
                            announcer.text = " ﻡﺭﺍﺩ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                            yield return new WaitForSeconds(2);

                            int ran2 = Random.Range(1, 4);
                            if (ran2 == 1)
                            {
                                announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu3Name + " " + name_script.cpu1Name;
                                yield return new WaitForSeconds(2);
                                announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu1Name;
                                yield return new WaitForSeconds(2);

                                int ran3;
                                do
                                {
                                    ran3 = Random.Range(1, 3);
                                } while ((ran3 == 1 && cpu1.card1 == -1) || (ran3 == 1 && cpu1.card2 == -1));

                                int box = 0;
                                if (ran3 == 1)
                                {
                                    box = cpu1.card1;
                                    cpu1.card1 = -1;
                                    cpu1cards[0].SetActive(false);
                                }
                                else
                                {
                                    box = cpu1.card2;
                                    cpu1.card2 = -1;
                                    cpu1cards[1].SetActive(false);
                                }

                                if (box == 1)
                                {
                                    if (mali == "banker")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu1Name;
                                    }
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu1Name;
                                    }
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu1Name;
                                    }
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu1Name;
                                    }
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu1Name;
                                    }
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                                endgame--;
                            }
                            else
                            {
                                //done;
                            }

                        }
                        else
                        {
                            int ran3 = Random.Range(1, 4);
                            if (ran3 == 1)
                            {
                                print("BLOF");
                                announcer.text = " ﻡﺭﺍﺩ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                yield return new WaitForSeconds(2);

                                int ran4 = Random.Range(1, 4);
                                if (ran4 == 1)
                                {
                                    announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu3Name + " " + name_script.cpu1Name;
                                    yield return new WaitForSeconds(2);
                                    announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu1Name;
                                    yield return new WaitForSeconds(2);

                                    int ran5;
                                    do
                                    {
                                        ran5 = Random.Range(1, 3);
                                    } while ((ran5 == 1 && cpu3.card1 == -1) || (ran5 == 1 && cpu3.card2 == -1));

                                    int box = 0;
                                    if (ran5 == 1)
                                    {
                                        box = cpu3.card1;
                                        cpu3.card1 = -1;
                                        cpu3cards[0].SetActive(false);
                                    }
                                    else
                                    {
                                        box = cpu3.card2;
                                        cpu3.card2 = -1;
                                        cpu3cards[1].SetActive(false);
                                    }

                                    if (box == 1)
                                    {
                                        if (mali == "banker")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu3Name;
                                        }
                                    }
                                    else if (box == 2)
                                    {
                                        if (ertebat == "director")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu3Name;
                                        }
                                    }
                                    else if (box == 3)
                                    {
                                        if (attack == "cherik")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu3Name;
                                        }
                                    }
                                    else if (box == 4)
                                    {
                                        if (uniqe4 == "solh")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu3Name;
                                        }
                                    }
                                    else if (box == 5)
                                    {
                                        if (uniqe5 == "siasat")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu3Name;
                                        }
                                    }

                                    for (int i = 0; i < lost.Length; i++)
                                    {
                                        if (lost[i] == -1)
                                        {
                                            lost[i] = box;
                                            break;
                                        }
                                    }
                                    printLost();
                                    yield return new WaitForSeconds(2);

                                    if (cpu3.coin >= 2)
                                    {
                                        cpu3.coin -= 2;
                                        cpu1.coin += 2;
                                    }
                                    else
                                    {
                                        cpu1.coin += cpu3.coin;
                                        cpu3.coin = 0;
                                    }
                                    cointxt[1].text = cpu1.coin.ToString();
                                    cointxt[3].text = cpu3.coin.ToString();
                                    endgame--;
                                }
                                else
                                {
                                    //done;
                                }
                            }
                            else
                            {
                                if (cpu3.coin >= 2)
                                {
                                    cpu3.coin -= 2;
                                    cpu1.coin += 2;
                                }
                                else
                                {
                                    cpu1.coin += cpu3.coin;
                                    cpu3.coin = 0;
                                }
                                cointxt[1].text = cpu1.coin.ToString();
                                cointxt[3].text = cpu3.coin.ToString();
                            }
                        }
                    }
                    else if (ran == 0)
                    {
                        announcer.text = "ﻩﺪﺑ ﻪﮑﺳ : " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);

                        Reaction.SetActive(true);
                        yield return new WaitUntil(() => robotWait == true);
                        robotWait = false;

                        if (myReaction)
                        {
                            int rand = Random.Range(1, 4);
                            if (rand == 1)
                            {
                                if (Me.card1 == 5 || Me.card2 == 5)
                                {
                                    announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu1Name;
                                    yield return new WaitForSeconds(2);

                                    int ran5;
                                    do
                                    {
                                        ran5 = Random.Range(1, 3);
                                    } while ((ran5 == 1 && cpu1.card1 == -1) || (ran5 == 1 && cpu1.card2 == -1));

                                    int box = 0;
                                    if (ran5 == 1)
                                    {
                                        box = cpu1.card1;
                                        cpu1.card1 = -1;
                                        cpu1cards[0].SetActive(false);
                                    }
                                    else
                                    {
                                        box = cpu1.card2;
                                        cpu1.card2 = -1;
                                        cpu1cards[1].SetActive(false);
                                    }

                                    if (box == 1)
                                    {
                                        if (mali == "banker")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu1Name;
                                        }
                                    }
                                    else if (box == 2)
                                    {
                                        if (ertebat == "director")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu1Name;
                                        }
                                    }
                                    else if (box == 3)
                                    {
                                        if (attack == "cherik")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu1Name;
                                        }
                                    }
                                    else if (box == 4)
                                    {
                                        if (uniqe4 == "solh")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu1Name;
                                        }
                                    }
                                    else if (box == 5)
                                    {
                                        if (uniqe5 == "siasat")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu1Name;
                                        }
                                    }

                                    for (int i = 0; i < lost.Length; i++)
                                    {
                                        if (lost[i] == -1)
                                        {
                                            lost[i] = box;
                                            break;
                                        }
                                    }
                                    printLost();
                                    yield return new WaitForSeconds(2);
                                    endgame--;
                                }
                                else
                                {
                                    announcer.text = " ﯽﺘﺧﺎﺑ ﺍﺭ ﺶﻟﺎﭼ ";
                                    yield return new WaitForSeconds(2);
                                    losingy();
                                    yield return new WaitUntil(() => losingClick == true);
                                    losingClick = false;

                                    if (Me.coin >= 2)
                                    {
                                        Me.coin -= 2;
                                        cpu1.coin += 2;
                                    }
                                    else
                                    {
                                        cpu1.coin += Me.coin;
                                        Me.coin = 0;
                                    }
                                    cointxt[0].text = Me.coin.ToString();
                                    cointxt[1].text = cpu1.coin.ToString();
                                }
                            }
                            else
                            {
                                //done
                            }
                        }
                        else
                        {
                            if (Me.coin >= 2)
                            {
                                Me.coin -= 2;
                                cpu1.coin += 2;
                            }
                            else
                            {
                                cpu1.coin += Me.coin;
                                Me.coin = 0;
                            }
                            cointxt[0].text = Me.coin.ToString();
                            cointxt[1].text = cpu1.coin.ToString();
                        }
                    }

                }

            }
            else if (cpu2turn)
            {
                bool can = true;
                can = canAttack(2);

                if (can)
                {

                    int ran;
                    do
                    {
                        ran = Random.Range(0, 4);
                    } while ((ran == 2) || (ran == 1 && !cpu1.Alive) || (ran == 3 && !cpu3.Alive) || (ran == WhoSolh));

                    if (ran == 1)
                    {
                        if (cpu1.card1 == 5 || cpu1.card2 == 5)
                        {
                            announcer.text = " ﻡﺭﺍﺩ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                            yield return new WaitForSeconds(2);

                            int ran2 = Random.Range(1, 4);
                            if (ran2 == 1)
                            {
                                announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu1Name + " " + name_script.cpu2Name;
                                yield return new WaitForSeconds(2);
                                announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu2Name;
                                yield return new WaitForSeconds(2);

                                int ran3;
                                do
                                {
                                    ran3 = Random.Range(1, 3);
                                } while ((ran3 == 1 && cpu2.card1 == -1) || (ran3 == 1 && cpu2.card2 == -1));

                                int box = 0;
                                if (ran3 == 1)
                                {
                                    box = cpu2.card1;
                                    cpu2.card1 = -1;
                                    cpu2cards[0].SetActive(false);
                                }
                                else
                                {
                                    box = cpu2.card2;
                                    cpu2.card2 = -1;
                                    cpu2cards[1].SetActive(false);
                                }

                                if (box == 1)
                                {
                                    if (mali == "banker")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu2Name;
                                    }
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                                endgame--;
                            }
                            else
                            {
                                //done;
                            }

                        }
                        else
                        {
                            int ran3 = Random.Range(1, 4);
                            if (ran3 == 1)
                            {
                                print("BLOF");
                                announcer.text = " ﻡﺭﺍﺩ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                                yield return new WaitForSeconds(2);

                                int ran4 = Random.Range(1, 4);
                                if (ran4 == 1)
                                {
                                    announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu1Name + " " + name_script.cpu2Name;
                                    yield return new WaitForSeconds(2);
                                    announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu2Name;
                                    yield return new WaitForSeconds(2);

                                    int ran5;
                                    do
                                    {
                                        ran5 = Random.Range(1, 3);
                                    } while ((ran5 == 1 && cpu1.card1 == -1) || (ran5 == 1 && cpu1.card2 == -1));

                                    int box = 0;
                                    if (ran5 == 1)
                                    {
                                        box = cpu1.card1;
                                        cpu1.card1 = -1;
                                        cpu1cards[0].SetActive(false);
                                    }
                                    else
                                    {
                                        box = cpu1.card2;
                                        cpu1.card2 = -1;
                                        cpu1cards[1].SetActive(false);
                                    }

                                    if (box == 1)
                                    {
                                        if (mali == "banker")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu1Name;
                                        }
                                    }
                                    else if (box == 2)
                                    {
                                        if (ertebat == "director")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu1Name;
                                        }
                                    }
                                    else if (box == 3)
                                    {
                                        if (attack == "cherik")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu1Name;
                                        }
                                    }
                                    else if (box == 4)
                                    {
                                        if (uniqe4 == "solh")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu1Name;
                                        }
                                    }
                                    else if (box == 5)
                                    {
                                        if (uniqe5 == "siasat")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu1Name;
                                        }
                                    }

                                    for (int i = 0; i < lost.Length; i++)
                                    {
                                        if (lost[i] == -1)
                                        {
                                            lost[i] = box;
                                            break;
                                        }
                                    }
                                    printLost();
                                    yield return new WaitForSeconds(2);

                                    if (cpu1.coin >= 2)
                                    {
                                        cpu1.coin -= 2;
                                        cpu2.coin += 2;
                                    }
                                    else
                                    {
                                        cpu2.coin += cpu1.coin;
                                        cpu1.coin = 0;
                                    }
                                    cointxt[2].text = cpu2.coin.ToString();
                                    cointxt[1].text = cpu1.coin.ToString();
                                    endgame--;
                                }
                                else
                                {
                                    //done;
                                }
                            }
                            else
                            {
                                if (cpu1.coin >= 2)
                                {
                                    cpu1.coin -= 2;
                                    cpu2.coin += 2;
                                }
                                else
                                {
                                    cpu2.coin += cpu1.coin;
                                    cpu1.coin = 0;
                                }
                                cointxt[1].text = cpu1.coin.ToString();
                                cointxt[2].text = cpu2.coin.ToString();
                            }
                        }
                    }
                    else if (ran == 3)
                    {
                        if (cpu3.card1 == 5 || cpu3.card2 == 5)
                        {
                            announcer.text = " ﻡﺭﺍﺩ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                            yield return new WaitForSeconds(2);

                            int ran2 = Random.Range(1, 4);
                            if (ran2 == 1)
                            {
                                announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu3Name + " " + name_script.cpu2Name;
                                yield return new WaitForSeconds(2);
                                announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu2Name;
                                yield return new WaitForSeconds(2);

                                int ran3;
                                do
                                {
                                    ran3 = Random.Range(1, 3);
                                } while ((ran3 == 1 && cpu2.card1 == -1) || (ran3 == 1 && cpu2.card2 == -1));

                                int box = 0;
                                if (ran3 == 1)
                                {
                                    box = cpu2.card1;
                                    cpu2.card1 = -1;
                                    cpu2cards[0].SetActive(false);
                                }
                                else
                                {
                                    box = cpu2.card2;
                                    cpu2.card2 = -1;
                                    cpu2cards[1].SetActive(false);
                                }

                                if (box == 1)
                                {
                                    if (mali == "banker")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu2Name;
                                    }
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu2Name;
                                    }
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                                endgame--;
                            }
                            else
                            {
                                //done;
                            }

                        }
                        else
                        {
                            int ran3 = Random.Range(1, 4);
                            if (ran3 == 1)
                            {
                                print("BLOF");
                                announcer.text = " ﻡﺭﺍﺩ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                yield return new WaitForSeconds(2);

                                int ran4 = Random.Range(1, 4);
                                if (ran4 == 1)
                                {
                                    announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu3Name + " " + name_script.cpu2Name;
                                    yield return new WaitForSeconds(2);
                                    announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu2Name;
                                    yield return new WaitForSeconds(2);

                                    int ran5;
                                    do
                                    {
                                        ran5 = Random.Range(1, 3);
                                    } while ((ran5 == 1 && cpu3.card1 == -1) || (ran5 == 1 && cpu3.card2 == -1));

                                    int box = 0;
                                    if (ran5 == 1)
                                    {
                                        box = cpu3.card1;
                                        cpu3.card1 = -1;
                                        cpu3cards[0].SetActive(false);
                                    }
                                    else
                                    {
                                        box = cpu3.card2;
                                        cpu3.card2 = -1;
                                        cpu3cards[1].SetActive(false);
                                    }

                                    if (box == 1)
                                    {
                                        if (mali == "banker")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu3Name;
                                        }
                                    }
                                    else if (box == 2)
                                    {
                                        if (ertebat == "director")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu3Name;
                                        }
                                    }
                                    else if (box == 3)
                                    {
                                        if (attack == "cherik")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu3Name;
                                        }
                                    }
                                    else if (box == 4)
                                    {
                                        if (uniqe4 == "solh")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu3Name;
                                        }
                                    }
                                    else if (box == 5)
                                    {
                                        if (uniqe5 == "siasat")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu3Name;
                                        }
                                    }

                                    for (int i = 0; i < lost.Length; i++)
                                    {
                                        if (lost[i] == -1)
                                        {
                                            lost[i] = box;
                                            break;
                                        }
                                    }
                                    printLost();
                                    yield return new WaitForSeconds(2);

                                    if (cpu3.coin >= 2)
                                    {
                                        cpu3.coin -= 2;
                                        cpu2.coin += 2;
                                    }
                                    else
                                    {
                                        cpu2.coin += cpu3.coin;
                                        cpu3.coin = 0;
                                    }
                                    cointxt[2].text = cpu2.coin.ToString();
                                    cointxt[3].text = cpu3.coin.ToString();
                                    endgame--;
                                }
                                else
                                {
                                    //done;
                                }
                            }
                            else
                            {
                                if (cpu3.coin >= 2)
                                {
                                    cpu3.coin -= 2;
                                    cpu2.coin += 2;
                                }
                                else
                                {
                                    cpu2.coin += cpu3.coin;
                                    cpu3.coin = 0;
                                }
                                cointxt[2].text = cpu2.coin.ToString();
                                cointxt[3].text = cpu3.coin.ToString();
                            }
                        }
                    }
                    else if (ran == 0)
                    {
                        announcer.text = "ﻩﺪﺑ ﻪﮑﺳ : " + name_script.cpu2Name;
                        yield return new WaitForSeconds(2);

                        Reaction.SetActive(true);
                        yield return new WaitUntil(() => robotWait == true);
                        robotWait = false;

                        if (myReaction)
                        {
                            int rand = Random.Range(1, 4);
                            if (rand == 1)
                            {
                                if (Me.card1 == 5 || Me.card2 == 5)
                                {
                                    announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu2Name;
                                    yield return new WaitForSeconds(2);

                                    int ran5;
                                    do
                                    {
                                        ran5 = Random.Range(1, 3);
                                    } while ((ran5 == 1 && cpu2.card1 == -1) || (ran5 == 1 && cpu2.card2 == -1));

                                    int box = 0;
                                    if (ran5 == 1)
                                    {
                                        box = cpu2.card1;
                                        cpu2.card1 = -1;
                                        cpu2cards[0].SetActive(false);
                                    }
                                    else
                                    {
                                        box = cpu2.card2;
                                        cpu2.card2 = -1;
                                        cpu2cards[1].SetActive(false);
                                    }

                                    if (box == 1)
                                    {
                                        if (mali == "banker")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu2Name;
                                        }
                                    }
                                    else if (box == 2)
                                    {
                                        if (ertebat == "director")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu2Name;
                                        }
                                    }
                                    else if (box == 3)
                                    {
                                        if (attack == "cherik")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu2Name;
                                        }
                                    }
                                    else if (box == 4)
                                    {
                                        if (uniqe4 == "solh")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu2Name;
                                        }
                                    }
                                    else if (box == 5)
                                    {
                                        if (uniqe5 == "siasat")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu2Name;
                                        }
                                    }

                                    for (int i = 0; i < lost.Length; i++)
                                    {
                                        if (lost[i] == -1)
                                        {
                                            lost[i] = box;
                                            break;
                                        }
                                    }
                                    printLost();
                                    yield return new WaitForSeconds(2);
                                    endgame--;
                                }
                                else
                                {
                                    announcer.text = " ﯽﺘﺧﺎﺑ ﺍﺭ ﺶﻟﺎﭼ ";
                                    yield return new WaitForSeconds(2);
                                    losingy();
                                    yield return new WaitUntil(() => losingClick == true);
                                    losingClick = false;

                                    if (Me.coin >= 2)
                                    {
                                        Me.coin -= 2;
                                        cpu2.coin += 2;
                                    }
                                    else
                                    {
                                        cpu2.coin += Me.coin;
                                        Me.coin = 0;
                                    }
                                    cointxt[0].text = Me.coin.ToString();
                                    cointxt[2].text = cpu2.coin.ToString();
                                }
                            }
                            else
                            {
                                //done
                            }
                        }
                        else
                        {
                            if (Me.coin >= 2)
                            {
                                Me.coin -= 2;
                                cpu2.coin += 2;
                            }
                            else
                            {
                                cpu2.coin += Me.coin;
                                Me.coin = 0;
                            }
                            cointxt[0].text = Me.coin.ToString();
                            cointxt[2].text = cpu2.coin.ToString();
                        }
                    }

                }
            }
            else if (cpu3turn)
            {
                bool can = true;
                can = canAttack(3);

                if (can)
                {

                    int ran;
                    do
                    {
                        ran = Random.Range(0, 4);
                    } while ((ran == 3) || (ran == 1 && !cpu1.Alive) || (ran == 2 && !cpu2.Alive) || (ran == WhoSolh));

                    if (ran == 1)
                    {
                        if (cpu1.card1 == 5 || cpu1.card2 == 5)
                        {
                            announcer.text = " ﻡﺭﺍﺩ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                            yield return new WaitForSeconds(2);

                            int ran2 = Random.Range(1, 4);
                            if (ran2 == 1)
                            {
                                announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu1Name + " " + name_script.cpu3Name;
                                yield return new WaitForSeconds(2);
                                announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu3Name;
                                yield return new WaitForSeconds(2);

                                int ran3;
                                do
                                {
                                    ran3 = Random.Range(1, 3);
                                } while ((ran3 == 1 && cpu3.card1 == -1) || (ran3 == 1 && cpu3.card2 == -1));

                                int box = 0;
                                if (ran3 == 1)
                                {
                                    box = cpu3.card1;
                                    cpu3.card1 = -1;
                                    cpu3cards[0].SetActive(false);
                                }
                                else
                                {
                                    box = cpu3.card2;
                                    cpu3.card2 = -1;
                                    cpu3cards[1].SetActive(false);
                                }

                                if (box == 1)
                                {
                                    if (mali == "banker")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu3Name;
                                    }
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                                endgame--;
                            }
                            else
                            {
                                //done;
                            }

                        }
                        else
                        {
                            int ran3 = Random.Range(1, 4);
                            if (ran3 == 1)
                            {
                                print("BLOF");
                                announcer.text = " ﻡﺭﺍﺩ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                                yield return new WaitForSeconds(2);

                                int ran4 = Random.Range(1, 4);
                                if (ran4 == 1)
                                {
                                    announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu1Name + " " + name_script.cpu3Name;
                                    yield return new WaitForSeconds(2);
                                    announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu3Name;
                                    yield return new WaitForSeconds(2);

                                    int ran5;
                                    do
                                    {
                                        ran5 = Random.Range(1, 3);
                                    } while ((ran5 == 1 && cpu1.card1 == -1) || (ran5 == 1 && cpu1.card2 == -1));

                                    int box = 0;
                                    if (ran5 == 1)
                                    {
                                        box = cpu1.card1;
                                        cpu1.card1 = -1;
                                        cpu1cards[0].SetActive(false);
                                    }
                                    else
                                    {
                                        box = cpu1.card2;
                                        cpu1.card2 = -1;
                                        cpu1cards[1].SetActive(false);
                                    }

                                    if (box == 1)
                                    {
                                        if (mali == "banker")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu1Name;
                                        }
                                    }
                                    else if (box == 2)
                                    {
                                        if (ertebat == "director")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu1Name;
                                        }
                                    }
                                    else if (box == 3)
                                    {
                                        if (attack == "cherik")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu1Name;
                                        }
                                    }
                                    else if (box == 4)
                                    {
                                        if (uniqe4 == "solh")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu1Name;
                                        }
                                    }
                                    else if (box == 5)
                                    {
                                        if (uniqe5 == "siasat")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu1Name;
                                        }
                                    }

                                    for (int i = 0; i < lost.Length; i++)
                                    {
                                        if (lost[i] == -1)
                                        {
                                            lost[i] = box;
                                            break;
                                        }
                                    }
                                    printLost();
                                    yield return new WaitForSeconds(2);

                                    if (cpu1.coin >= 2)
                                    {
                                        cpu1.coin -= 2;
                                        cpu3.coin += 2;
                                    }
                                    else
                                    {
                                        cpu3.coin += cpu1.coin;
                                        cpu1.coin = 0;
                                    }
                                    cointxt[3].text = cpu3.coin.ToString();
                                    cointxt[1].text = cpu1.coin.ToString();
                                    endgame--;
                                }
                                else
                                {
                                    //done;
                                }
                            }
                            else
                            {
                                if (cpu1.coin >= 2)
                                {
                                    cpu1.coin -= 2;
                                    cpu3.coin += 2;
                                }
                                else
                                {
                                    cpu3.coin += cpu1.coin;
                                    cpu1.coin = 0;
                                }
                                cointxt[1].text = cpu1.coin.ToString();
                                cointxt[3].text = cpu3.coin.ToString();
                            }
                        }
                    }
                    else if (ran == 2)
                    {
                        if (cpu2.card1 == 5 || cpu2.card2 == 5)
                        {
                            announcer.text = " ﻡﺭﺍﺩ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                            yield return new WaitForSeconds(2);

                            int ran2 = Random.Range(1, 4);
                            if (ran2 == 1)
                            {
                                announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu2Name + " " + name_script.cpu3Name;
                                yield return new WaitForSeconds(2);
                                announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu3Name;
                                yield return new WaitForSeconds(2);

                                int ran3;
                                do
                                {
                                    ran3 = Random.Range(1, 3);
                                } while ((ran3 == 1 && cpu3.card1 == -1) || (ran3 == 1 && cpu3.card2 == -1));

                                int box = 0;
                                if (ran3 == 1)
                                {
                                    box = cpu3.card1;
                                    cpu3.card1 = -1;
                                    cpu3cards[0].SetActive(false);
                                }
                                else
                                {
                                    box = cpu3.card2;
                                    cpu3.card2 = -1;
                                    cpu3cards[1].SetActive(false);
                                }

                                if (box == 1)
                                {
                                    if (mali == "banker")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu3Name;
                                    }
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                    {
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu3Name;
                                    }
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                                endgame--;
                            }
                            else
                            {
                                //done;
                            }

                        }
                        else
                        {
                            int ran3 = Random.Range(1, 4);
                            if (ran3 == 1)
                            {
                                print("BLOF");
                                announcer.text = " ﻡﺭﺍﺩ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                                yield return new WaitForSeconds(2);

                                int ran4 = Random.Range(1, 4);
                                if (ran4 == 1)
                                {
                                    announcer.text = " ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ " + name_script.cpu2Name + " " + name_script.cpu3Name;
                                    yield return new WaitForSeconds(2);
                                    announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu3Name;
                                    yield return new WaitForSeconds(2);

                                    int ran5;
                                    do
                                    {
                                        ran5 = Random.Range(1, 3);
                                    } while ((ran5 == 1 && cpu2.card1 == -1) || (ran5 == 1 && cpu2.card2 == -1));

                                    int box = 0;
                                    if (ran5 == 1)
                                    {
                                        box = cpu2.card1;
                                        cpu2.card1 = -1;
                                        cpu2cards[0].SetActive(false);
                                    }
                                    else
                                    {
                                        box = cpu2.card2;
                                        cpu2.card2 = -1;
                                        cpu2cards[1].SetActive(false);
                                    }

                                    if (box == 1)
                                    {
                                        if (mali == "banker")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu2Name;
                                        }
                                    }
                                    else if (box == 2)
                                    {
                                        if (ertebat == "director")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu2Name;
                                        }
                                    }
                                    else if (box == 3)
                                    {
                                        if (attack == "cherik")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu2Name;
                                        }
                                    }
                                    else if (box == 4)
                                    {
                                        if (uniqe4 == "solh")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu2Name;
                                        }
                                    }
                                    else if (box == 5)
                                    {
                                        if (uniqe5 == "siasat")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu2Name;
                                        }
                                    }

                                    for (int i = 0; i < lost.Length; i++)
                                    {
                                        if (lost[i] == -1)
                                        {
                                            lost[i] = box;
                                            break;
                                        }
                                    }
                                    printLost();
                                    yield return new WaitForSeconds(2);

                                    if (cpu2.coin >= 2)
                                    {
                                        cpu2.coin -= 2;
                                        cpu3.coin += 2;
                                    }
                                    else
                                    {
                                        cpu3.coin += cpu2.coin;
                                        cpu2.coin = 0;
                                    }
                                    cointxt[2].text = cpu2.coin.ToString();
                                    cointxt[3].text = cpu3.coin.ToString();
                                    endgame--;
                                }
                                else
                                {
                                    //done;
                                }
                            }
                            else
                            {
                                if (cpu2.coin >= 2)
                                {
                                    cpu2.coin -= 2;
                                    cpu3.coin += 2;
                                }
                                else
                                {
                                    cpu3.coin += cpu2.coin;
                                    cpu2.coin = 0;
                                }
                                cointxt[2].text = cpu2.coin.ToString();
                                cointxt[3].text = cpu3.coin.ToString();
                            }
                        }
                    }
                    else if (ran == 0)
                    {
                        announcer.text = "ﻩﺪﺑ ﻪﮑﺳ : " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);

                        Reaction.SetActive(true);
                        yield return new WaitUntil(() => robotWait == true);
                        robotWait = false;

                        if (myReaction)
                        {
                            int rand = Random.Range(1, 4);
                            if (rand == 1)
                            {
                                if (Me.card1 == 5 || Me.card2 == 5)
                                {
                                    announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu3Name;
                                    yield return new WaitForSeconds(2);

                                    int ran5;
                                    do
                                    {
                                        ran5 = Random.Range(1, 3);
                                    } while ((ran5 == 1 && cpu3.card1 == -1) || (ran5 == 1 && cpu3.card2 == -1));

                                    int box = 0;
                                    if (ran5 == 1)
                                    {
                                        box = cpu3.card1;
                                        cpu3.card1 = -1;
                                        cpu3cards[0].SetActive(false);
                                    }
                                    else
                                    {
                                        box = cpu3.card2;
                                        cpu3.card2 = -1;
                                        cpu3cards[1].SetActive(false);
                                    }

                                    if (box == 1)
                                    {
                                        if (mali == "banker")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu3Name;
                                        }
                                    }
                                    else if (box == 2)
                                    {
                                        if (ertebat == "director")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu3Name;
                                        }
                                    }
                                    else if (box == 3)
                                    {
                                        if (attack == "cherik")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu3Name;
                                        }
                                    }
                                    else if (box == 4)
                                    {
                                        if (uniqe4 == "solh")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu3Name;
                                        }
                                    }
                                    else if (box == 5)
                                    {
                                        if (uniqe5 == "siasat")
                                        {
                                            announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu3Name;
                                        }
                                    }

                                    for (int i = 0; i < lost.Length; i++)
                                    {
                                        if (lost[i] == -1)
                                        {
                                            lost[i] = box;
                                            break;
                                        }
                                    }
                                    printLost();
                                    yield return new WaitForSeconds(2);
                                    endgame--;
                                }
                                else
                                {
                                    announcer.text = " ﯽﺘﺧﺎﺑ ﺍﺭ ﺶﻟﺎﭼ ";
                                    yield return new WaitForSeconds(2);
                                    losingy();
                                    yield return new WaitUntil(() => losingClick == true);
                                    losingClick = false;

                                    if (Me.coin >= 2)
                                    {
                                        Me.coin -= 2;
                                        cpu3.coin += 2;
                                    }
                                    else
                                    {
                                        cpu3.coin += Me.coin;
                                        Me.coin = 0;
                                    }
                                    cointxt[0].text = Me.coin.ToString();
                                    cointxt[3].text = cpu3.coin.ToString();
                                }
                            }
                            else
                            {
                                //done
                            }
                        }
                        else
                        {
                            if (Me.coin >= 2)
                            {
                                Me.coin -= 2;
                                cpu3.coin += 2;
                            }
                            else
                            {
                                cpu3.coin += Me.coin;
                                Me.coin = 0;
                            }
                            cointxt[0].text = Me.coin.ToString();
                            cointxt[3].text = cpu3.coin.ToString();
                        }
                    }

                }
            }
        }
        yield return new WaitForSeconds(2);
        cClicked = true;
    }

    IEnumerator cpuProgress(string whichAction)
    {
        bool[] result = { false, false, false };

        if (cpu1turn)
        {
            if (whichAction == "mali")
            {
                if (mali == "banker")
                {
                    announcer.text = "ﻡﺭﺍﺪﮑﻧﺎﺑ : " + name_script.cpu1Name;
                    edea[0].SetActive(true);
                    edea[0].GetComponent<Image>().color = roleColor[0];
                }
                yield return new WaitForSeconds(1.5f);

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
                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }
            }
            else if (whichAction == "ertebat")
            {
                if (ertebat == "director")
                {
                    announcer.text = "ﻢﻧﺍﺩﺮﮔﺭﺎﮐ : " + name_script.cpu1Name;
                    edea[0].SetActive(true);
                    edea[0].GetComponent<Image>().color = roleColor[1];
                }
                yield return new WaitForSeconds(1.5f);

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
                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }
            }
            else if (whichAction == "attack")
            {
                if (attack == "cherik")
                {
                    announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu1Name;
                    edea[0].SetActive(true);
                    edea[0].GetComponent<Image>().color = roleColor[2];
                }
                yield return new WaitForSeconds(1.5f);

                if (cpu2.Alive)
                {
                    result[1] = cpu2.Chalesh(3, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu2Name;
                }
                yield return new WaitForSeconds(1);
                if (cpu3.Alive)
                {
                    result[2] = cpu3.Chalesh(3, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu3Name;
                }
                yield return new WaitForSeconds(1);
                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }
            }
            else if (whichAction == "uniqe4")
            {
                if (uniqe4 == "solh")
                {
                    announcer.text = "ﻢﺒﻠﻃ ﺢﻠﺻ : " + name_script.cpu1Name;
                    edea[0].SetActive(true);
                    edea[0].GetComponent<Image>().color = roleColor[3];
                }
                yield return new WaitForSeconds(1.5f);

                if (cpu2.Alive)
                {
                    result[1] = cpu2.Chalesh(4, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu2Name;
                }
                yield return new WaitForSeconds(1);
                if (cpu3.Alive)
                {
                    result[2] = cpu3.Chalesh(4, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu3Name;
                }
                yield return new WaitForSeconds(1);
                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }
            }
            else if (whichAction == "uniqe5")
            {
                if (uniqe5 == "siasat")
                {
                    announcer.text = "ﻡﺭﺍﺪﻤﺘﺳﺎﯿﺳ : " + name_script.cpu1Name;
                    edea[0].SetActive(true);
                    edea[0].GetComponent<Image>().color = roleColor[4];
                }
                yield return new WaitForSeconds(1.5f);

                if (cpu2.Alive)
                {
                    result[1] = cpu2.Chalesh(5, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu2Name;
                }
                yield return new WaitForSeconds(1);
                if (cpu3.Alive)
                {
                    result[2] = cpu3.Chalesh(5, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu3Name;
                }
                yield return new WaitForSeconds(1);
                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }
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
            if (mychallange)
                permision = false;

            //testing
            
            
            //

            if (permision)
            {
                if (whichAction == "mali")
                    StartCoroutine(Mali());
                else if (whichAction == "ertebat")
                    StartCoroutine(ertebatat());
                else if (whichAction == "attack")
                {
                    StartCoroutine(RobAttack());
                    yield return new WaitUntil(() => cClicked == true);
                    cClicked = false;
                }
                else if (whichAction == "uniqe4")
                    StartCoroutine(uniqe4y());
                else if (whichAction == "uniqe5")
                {
                    StartCoroutine(Robuniqe5());
                    yield return new WaitUntil(() => cClicked == true);
                    cClicked = false;
                }
                    
            }
            else
            {

                if (result[1])
                {
                    announcer.text = "ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ ﺍﺭ " + name_script.cpu1Name + " " + name_script.cpu2Name;
                    yield return new WaitForSeconds(2);
                    int target = 0;
                    if (whichAction == "mali")
                    {
                        target = 1;
                    }
                    else if (whichAction == "ertebat")
                    {
                        target = 2;
                    }
                    else if (whichAction == "attack")
                    {
                        target = 3;
                    }
                    else if (whichAction == "uniqe4")
                    {
                        target = 4;
                    }
                    else if (whichAction == "uniqe5")
                    {
                        target = 5;
                    }

                    if (cpu1.card1 == target || cpu1.card2 == target)
                    {
                        announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);

                        announcer.text = "ﺪﯾﺪﺟ ﺕﺭﺎﮐ ﺏﺎﺨﺘﻧﺍ ﻝﺎﺣ ﺭﺩ";

                        print("cpu1 : " + cpu1.card1 + " - " + cpu1.card2);

                        backToDeck(target);
                        ShuffleArray(numbers, false);

                        if (cpu1.card1 == target)
                            cpu1.card1 = getFromDeck();
                        else
                            cpu1.card2 = getFromDeck();

                        print("cpu1 : " + cpu1.card1 + " - " + cpu1.card2);

                        yield return new WaitForSeconds(1.5f);
                        announcer.text = "";




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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
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

                        if (whichAction == "mali")
                            StartCoroutine(Mali());
                        else if (whichAction == "ertebat")
                            StartCoroutine(ertebatat());
                        else if (whichAction == "attack")
                        {
                            StartCoroutine(RobAttack());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                        else if (whichAction == "uniqe4")
                            StartCoroutine(uniqe4y());
                        else if (whichAction == "uniqe5")
                        {
                            StartCoroutine(Robuniqe5());
                             yield return new WaitUntil(() => cClicked == true);
                             cClicked = false;
                        }
                           

                    }
                    else
                    {
                        announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
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
                            cpu1cards[1].SetActive(false);
                        }

                        printLost();
                        yield return new WaitForSeconds(3);

                    }
                    endgame--;
                }
                else if (result[2])
                {
                    announcer.text = "ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ ﺍﺭ " + name_script.cpu1Name + " " + name_script.cpu3Name;
                    yield return new WaitForSeconds(2);

                    int target = 0;
                    if (whichAction == "mali")
                    {
                        target = 1;
                    }
                    else if (whichAction == "ertebat")
                    {
                        target = 2;
                    }
                    else if (whichAction == "attack")
                    {
                        target = 3;
                    }
                    else if (whichAction == "uniqe4")
                    {
                        target = 4;
                    }
                    else if (whichAction == "uniqe5")
                    {
                        target = 5;
                    }

                    if (cpu1.card1 == target || cpu1.card2 == target)
                    {
                        announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);

                        announcer.text = "ﺪﯾﺪﺟ ﺕﺭﺎﮐ ﺏﺎﺨﺘﻧﺍ ﻝﺎﺣ ﺭﺩ";

                        print("cpu1 : " + cpu1.card1 + " - " + cpu1.card2);

                        backToDeck(target);
                        ShuffleArray(numbers, false);

                        if (cpu1.card1 == target)
                            cpu1.card1 = getFromDeck();
                        else
                            cpu1.card2 = getFromDeck();

                        print("cpu1 : " + cpu1.card1 + " - " + cpu1.card2);

                        yield return new WaitForSeconds(1.5f);
                        announcer.text = "";


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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
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

                        if (whichAction == "mali")
                            StartCoroutine(Mali());
                        else if (whichAction == "ertebat")
                            StartCoroutine(ertebatat());
                        else if (whichAction == "attack")
                        {
                            StartCoroutine(RobAttack());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                        else if (whichAction == "uniqe4")
                            StartCoroutine(uniqe4y());
                        else if (whichAction == "uniqe5")
                        {
                            StartCoroutine(Robuniqe5());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                            

                    }
                    else
                    {
                        announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
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
                            cpu1cards[1].SetActive(false);
                        }

                        printLost();
                        yield return new WaitForSeconds(3);

                    }
                    endgame--;
                }
                else if (mychallange)
                {
                    announcer.text = "ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ ﺍﺭ " + name_script.cpu1Name + " ﺎﻤﺷ ";
                    yield return new WaitForSeconds(2);



                    int target = 0;
                    if (whichAction == "mali")
                    {
                        target = 1;
                    }
                    else if (whichAction == "ertebat")
                    {
                        target = 2;
                    }
                    else if (whichAction == "attack")
                    {
                        target = 3;
                    }
                    else if (whichAction == "uniqe4")
                    {
                        target = 4;
                    }
                    else if (whichAction == "uniqe5")
                    {
                        target = 5;
                    }

                    if (cpu1.card1 == target || cpu1.card2 == target)
                    {
                        announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);

                        announcer.text = "ﺪﯾﺪﺟ ﺕﺭﺎﮐ ﺏﺎﺨﺘﻧﺍ ﻝﺎﺣ ﺭﺩ";

                        print("cpu1 : " + cpu1.card1 + " - " + cpu1.card2);

                        backToDeck(target);
                        ShuffleArray(numbers, false);

                        if (cpu1.card1 == target)
                            cpu1.card1 = getFromDeck();
                        else
                            cpu1.card2 = getFromDeck();

                        print("cpu1 : " + cpu1.card1 + " - " + cpu1.card2);

                        yield return new WaitForSeconds(1.5f);
                        announcer.text = "";


                        losingy();
                        yield return new WaitUntil(() => losingClick == true);
                        losingClick = false;


                        if (whichAction == "mali")
                            StartCoroutine(Mali());
                        else if (whichAction == "ertebat")
                            StartCoroutine(ertebatat());
                        else if (whichAction == "attack")
                        {
                            StartCoroutine(RobAttack());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                        else if (whichAction == "uniqe4")
                            StartCoroutine(uniqe4y());
                        else if (whichAction == "uniqe5")
                        {
                            StartCoroutine(Robuniqe5());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                           

                    }
                    else
                    {
                        announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu1Name;
                        yield return new WaitForSeconds(2);
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
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
                            cpu1cards[1].SetActive(false);
                        }

                        printLost();
                        yield return new WaitForSeconds(3);
                        endgame--;
                    }
                }
            }

        }
        else if (cpu2turn)
        {
            if (whichAction == "mali")
            {
                if (mali == "banker")
                {
                    announcer.text = "ﻡﺭﺍﺪﮑﻧﺎﺑ : " + name_script.cpu2Name;
                    edea[1].SetActive(true);
                    edea[1].GetComponent<Image>().color = roleColor[0];
                }
                yield return new WaitForSeconds(1.5f);

                if (cpu3.Alive)
                {
                    result[2] = cpu3.Chalesh(1, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu3Name;
                }
                yield return new WaitForSeconds(1);
                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }

                if (cpu1.Alive)
                {
                    result[0] = cpu1.Chalesh(1, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu1Name;
                }
                yield return new WaitForSeconds(1);

            }
            else if (whichAction == "ertebat")
            {
                if (ertebat == "director")
                {
                    announcer.text = "ﻢﻧﺍﺩﺮﮔﺭﺎﮐ : " + name_script.cpu2Name;
                    edea[1].SetActive(true);
                    edea[1].GetComponent<Image>().color = roleColor[1];
                }
                yield return new WaitForSeconds(1.5f);

                if (cpu3.Alive)
                {
                    result[2] = cpu3.Chalesh(2, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu3Name;
                }
                yield return new WaitForSeconds(1);
                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }

                if (cpu1.Alive)
                {
                    result[0] = cpu1.Chalesh(2, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu1Name;
                }
                yield return new WaitForSeconds(1);

            }
            else if (whichAction == "attack")
            {
                if (attack == "cherik")
                {
                    announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu1Name;
                    edea[1].SetActive(true);
                    edea[1].GetComponent<Image>().color = roleColor[2];
                }
                yield return new WaitForSeconds(1.5f);

                if (cpu3.Alive)
                {
                    result[2] = cpu3.Chalesh(3, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu3Name;
                }
                yield return new WaitForSeconds(1);

                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }

                if (cpu1.Alive)
                {
                    result[0] = cpu2.Chalesh(3, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu1Name;
                }
                yield return new WaitForSeconds(1);
               
            }
            else if (whichAction == "uniqe4")
            {
                if (uniqe4 == "solh")
                {
                    announcer.text = "ﻢﺒﻠﻃ ﺢﻠﺻ : " + name_script.cpu2Name;
                    edea[1].SetActive(true);
                    edea[1].GetComponent<Image>().color = roleColor[3];
                }
                yield return new WaitForSeconds(1.5f);

                if (cpu3.Alive)
                {
                    result[2] = cpu3.Chalesh(4, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu3Name;
                }
                yield return new WaitForSeconds(1);
                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }
                if (cpu1.Alive)
                {
                    result[0] = cpu1.Chalesh(4, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu1Name;
                }
                yield return new WaitForSeconds(1);

            }
            else if (whichAction == "uniqe5")
            {
                if (uniqe5 == "siasat")
                {
                    announcer.text = "ﻡﺭﺍﺪﻤﺘﺳﺎﯿﺳ : " + name_script.cpu2Name;
                    edea[1].SetActive(true);
                    edea[1].GetComponent<Image>().color = roleColor[4];
                }
                yield return new WaitForSeconds(1.5f);

                if (cpu3.Alive)
                {
                    result[2] = cpu3.Chalesh(5, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu3Name;
                }
                yield return new WaitForSeconds(1);
                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }

                if (cpu1.Alive)
                {
                    result[0] = cpu1.Chalesh(5, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu1Name;
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
            if (mychallange)
                permision = false;

            //testing
          


            if (permision)
            {
                if (whichAction == "mali")
                    StartCoroutine(Mali());
                else if (whichAction == "ertebat")
                    StartCoroutine(ertebatat());
                else if (whichAction == "attack")
                {
                    StartCoroutine(RobAttack());
                    yield return new WaitUntil(() => cClicked == true);
                    cClicked = false;
                }
                else if (whichAction == "uniqe4")
                    StartCoroutine(uniqe4y());
                else if (whichAction == "uniqe5")
                {
                    StartCoroutine(Robuniqe5());
                    yield return new WaitUntil(() => cClicked == true);
                    cClicked = false;       
                }
                            
            }
            else
            {
                if (result[2])
                {
                     announcer.text = "ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ ﺍﺭ " + name_script.cpu2Name + " " + name_script.cpu3Name;
                    yield return new WaitForSeconds(2);
                    int target = 0;
                    if (whichAction == "mali")
                    {
                        target = 1;
                    }
                    else if (whichAction == "ertebat")
                    {
                        target = 2;
                    }
                    else if (whichAction == "attack")
                    {
                        target = 3;
                    }
                    else if (whichAction == "uniqe4")
                    {
                        target = 4;
                    }
                    else if (whichAction == "uniqe5")
                    {
                        target = 5;
                    }

                    if (cpu2.card1 == target || cpu2.card2 == target)
                    { 
                     announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu2Name;
                        yield return new WaitForSeconds(2);

                        announcer.text = "ﺪﯾﺪﺟ ﺕﺭﺎﮐ ﺏﺎﺨﺘﻧﺍ ﻝﺎﺣ ﺭﺩ";

                        print("cpu2 : " +cpu2.card1 + " - " + cpu2.card2);

                backToDeck(target);
                ShuffleArray(numbers,false);

                if (cpu2.card1 == target)
                    cpu2.card1 = getFromDeck();
                else
                    cpu2.card2 = getFromDeck();

                    print("cpu2 : " + cpu2.card1 + " - " + cpu2.card2);

               yield return new WaitForSeconds(1.5f);
                announcer.text = "";
                
       

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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
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

                        if (whichAction == "mali")
                            StartCoroutine(Mali());
                        else if (whichAction == "ertebat")
                            StartCoroutine(ertebatat());
                        else if (whichAction == "attack")
                        {
                            StartCoroutine(RobAttack());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                        else if (whichAction == "uniqe4")
                            StartCoroutine(uniqe4y());
                        else if (whichAction == "uniqe5")
                        {
                            StartCoroutine(Robuniqe5());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                           

                    }
                    else
                    {
                        announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu2Name;
                        yield return new WaitForSeconds(2);
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
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
                endgame--;
                }else if (mychallange)
                {announcer.text = "ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ ﺍﺭ " + name_script.cpu2Name + " ﺎﻤﺷ ";
                    yield return new WaitForSeconds(2);
                     
                    int target = 0;
                    if (whichAction == "mali")
                    {
                        target = 1;
                    }
                    else if (whichAction == "ertebat")
                    {
                        target = 2;
                    }
                    else if (whichAction == "attack")
                    {
                        target = 3;
                    }
                    else if (whichAction == "uniqe4")
                    {
                        target = 4;
                    }
                    else if (whichAction == "uniqe5")
                    {
                        target = 5;
                    }

                    if (cpu2.card1 == target || cpu2.card2 == target)
                    {
                        announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu2Name;
                    yield return new WaitForSeconds(2);

                      announcer.text = "ﺪﯾﺪﺟ ﺕﺭﺎﮐ ﺏﺎﺨﺘﻧﺍ ﻝﺎﺣ ﺭﺩ";

                        print("cpu2 : " +cpu2.card1 + " - " + cpu2.card2);

                backToDeck(target);
                ShuffleArray(numbers,false);

                if (cpu2.card1 == target)
                    cpu2.card1 = getFromDeck();
                else
                    cpu2.card2 = getFromDeck();

                    print("cpu2 : " + cpu2.card1 + " - " + cpu2.card2);

               yield return new WaitForSeconds(1.5f);
                announcer.text = "";
                
                    

                        losingy();
                       yield return new WaitUntil(() => losingClick == true);
                       losingClick = false;

                        if (whichAction == "mali")
                            StartCoroutine(Mali());
                        else if (whichAction == "ertebat")
                            StartCoroutine(ertebatat());
                        else if (whichAction == "attack")
                        {
                            StartCoroutine(RobAttack());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                        else if (whichAction == "uniqe4")
                            StartCoroutine(uniqe4y());
                        else if (whichAction == "uniqe5")
                        {
                            StartCoroutine(Robuniqe5());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                           

                    }
                    else
                    {
                       
                        announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu2Name;
                        yield return new WaitForSeconds(2);
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
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
                    endgame--;
                    }
                }
                else if (result[0])
                {
                    announcer.text = "ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ ﺍﺭ " + name_script.cpu2Name + " " + name_script.cpu1Name;
                    yield return new WaitForSeconds(2);
              
                    int target = 0;
                    if (whichAction == "mali")
                    {
                        target = 1;
                    }
                    else if (whichAction == "ertebat")
                    {
                        target = 2;
                    }
                    else if (whichAction == "attack")
                    {
                        target = 3;
                    }
                    else if (whichAction == "uniqe4")
                    {
                        target = 4;
                    }
                    else if (whichAction == "uniqe5")
                    {
                        target = 5;
                    }

                    if (cpu2.card1 == target || cpu2.card2 == target)
                    { 
                announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu2Name;
                yield return new WaitForSeconds(2);
               
                  announcer.text = "ﺪﯾﺪﺟ ﺕﺭﺎﮐ ﺏﺎﺨﺘﻧﺍ ﻝﺎﺣ ﺭﺩ";

                        print("cpu2 : " +cpu2.card1 + " - " + cpu2.card2);

                backToDeck(target);
                ShuffleArray(numbers,false);

                if (cpu2.card1 == target)
                    cpu2.card1 = getFromDeck();
                else
                    cpu2.card2 = getFromDeck();

                    print("cpu2 : " + cpu2.card1 + " - " + cpu2.card2);

               
                
                yield return new WaitForSeconds(1.5f);
announcer.text = "";
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
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
                            cpu1cards[1].SetActive(false);
                        }

                        printLost();
                        yield return new WaitForSeconds(3);

                        if (whichAction == "mali")
                            StartCoroutine(Mali());
                        else if (whichAction == "ertebat")
                            StartCoroutine(ertebatat());
                        else if (whichAction == "attack")
                        {
                            StartCoroutine(RobAttack());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                        else if (whichAction == "uniqe4")
                            StartCoroutine(uniqe4y());
                        else if (whichAction == "uniqe5")
                        {
                            StartCoroutine(Robuniqe5());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                            

                    }
                    else
                    { 
                         announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu2Name;
                        yield return new WaitForSeconds(2);
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
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
                    endgame--;
                }
                
            }

        }
        else if (cpu3turn)
        {
            if (whichAction == "mali")
            {
                if (mali == "banker")
                {
                    announcer.text = "ﻡﺭﺍﺪﮑﻧﺎﺑ : " + name_script.cpu3Name;
                    edea[2].SetActive(true);
                    edea[2].GetComponent<Image>().color = roleColor[0];
                }
                yield return new WaitForSeconds(1.5f);

                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }
                yield return new WaitForSeconds(1);

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
                
            }
            else if (whichAction == "ertebat")
            {
                if (ertebat == "director")
                {
                    announcer.text = "ﻢﻧﺍﺩﺮﮔﺭﺎﮐ : " + name_script.cpu3Name;
                    edea[2].SetActive(true);
                    edea[2].GetComponent<Image>().color = roleColor[1];
                }
                yield return new WaitForSeconds(1.5f);
                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }yield return new WaitForSeconds(1);
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
                
                
                
            }
            else if (whichAction == "attack")
            {
                if (attack == "cherik")
                {
                    announcer.text = "ﻢﮑﯾﺮﭼ : " + name_script.cpu3Name;
                    edea[2].SetActive(true);
                    edea[2].GetComponent<Image>().color = roleColor[2];
                }
                yield return new WaitForSeconds(1.5f);
                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }
                yield return new WaitForSeconds(1);
                if (cpu1.Alive)
                {
                    result[0] = cpu2.Chalesh(3, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu1Name;
                }yield return new WaitForSeconds(1);
                if (cpu2.Alive)
                {
                    result[1] = cpu2.Chalesh(3, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu2Name;
                }
                yield return new WaitForSeconds(1);
                
                
                
               
            }
            else if (whichAction == "uniqe4")
            {
                if (uniqe4 == "solh")
                {
                    announcer.text = "ﻢﺒﻠﻃ ﺢﻠﺻ : " + name_script.cpu3Name;
                    edea[2].SetActive(true);
                    edea[2].GetComponent<Image>().color = roleColor[3];
                }
                yield return new WaitForSeconds(1.5f);
                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }yield return new WaitForSeconds(1);
                if (cpu1.Alive)
                {
                    result[0] = cpu1.Chalesh(4, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu1Name;
                }yield return new WaitForSeconds(1);
                if (cpu2.Alive)
                {
                    result[1] = cpu2.Chalesh(4, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu2Name;
                }
                yield return new WaitForSeconds(1);
                  
                
              
                
            }
            else if (whichAction == "uniqe5")
            {
                if (uniqe5 == "siasat")
                {
                    announcer.text = "ﻡﺭﺍﺪﻤﺘﺳﺎﯿﺳ : " + name_script.cpu3Name;
                    edea[2].SetActive(true);
                    edea[2].GetComponent<Image>().color = roleColor[4];
                }
                yield return new WaitForSeconds(1.5f);
                if (Me.Alive)
                {
                    announcer.text = "";
                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                }
                yield return new WaitForSeconds(1);
                if (cpu1.Alive)
                {
                    result[0] = cpu1.Chalesh(5, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu1Name;
                } yield return new WaitForSeconds(1);
                if (cpu2.Alive)
                {
                    result[1] = cpu2.Chalesh(5, lost);
                    announcer.text = ". . . ﻥﺩﺮﮐ ﺮﮑﻓ ﻝﺎﺣﺭﺩ " + name_script.cpu2Name;
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
            if (mychallange)
                permision = false;

            //testing
          

            if (permision)
            {
                if (whichAction == "mali")
                    StartCoroutine(Mali());
                else if (whichAction == "ertebat")
                    StartCoroutine(ertebatat());
                else if (whichAction == "attack")
                {
                    StartCoroutine(RobAttack());
                    yield return new WaitUntil(() => cClicked == true);
                    cClicked = false;
                }
                else if (whichAction == "uniqe4")
                    StartCoroutine(uniqe4y());
                else if (whichAction == "uniqe5")
                {
                    StartCoroutine(Robuniqe5());   
                    yield return new WaitUntil(() => cClicked == true);
                    cClicked = false;     
                }
                           
            }
            else
            {   
                if (mychallange)
                {announcer.text = "ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ ﺍﺭ " + name_script.cpu3Name + " ﺎﻤﺷ ";
                    yield return new WaitForSeconds(2);

                    

               
                announcer.text = "";
                yield return new WaitForSeconds(1.5f);
                      
                    int target = 0;
                    if (whichAction == "mali")
                    {
                        target = 1;
                    }
                    else if (whichAction == "ertebat")
                    {
                        target = 2;
                    }
                    else if (whichAction == "attack")
                    {
                        target = 3;
                    }
                    else if (whichAction == "uniqe4")
                    {
                        target = 4;
                    }
                    else if (whichAction == "uniqe5")
                    {
                        target = 5;
                    }

                    if (cpu3.card1 == target || cpu3.card2 == target)
                    {
                        announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu3Name;
                    yield return new WaitForSeconds(2);

                      announcer.text = "ﺪﯾﺪﺟ ﺕﺭﺎﮐ ﺏﺎﺨﺘﻧﺍ ﻝﺎﺣ ﺭﺩ";

                        print("cpu3 : " +cpu3.card1 + " - " + cpu3.card2);

                backToDeck(target);
                ShuffleArray(numbers,false);

                if (cpu3.card1 == target)
                    cpu3.card1 = getFromDeck();
                else
                    cpu3.card2 = getFromDeck();

                    print("cpu3 : " + cpu3.card1 + " - " + cpu3.card2);

                        yield return new WaitForSeconds(1.5f);
                        losingy();
                        yield return new WaitUntil(() => losingClick == true);
                        losingClick = false;

                        if (whichAction == "mali")
                            StartCoroutine(Mali());
                        else if (whichAction == "ertebat")
                            StartCoroutine(ertebatat());
                        else if (whichAction == "attack")
                        {
                            StartCoroutine(RobAttack());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                        else if (whichAction == "uniqe4")
                            StartCoroutine(uniqe4y());
                        else if (whichAction == "uniqe5")
                        {
                            StartCoroutine(Robuniqe5());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                            

                    }
                    else
                    {
                        announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
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
                     endgame--;
                    }
                }else if (result[0])
                { announcer.text = "ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ ﺍﺭ " + name_script.cpu3Name + " " + name_script.cpu1Name;
                    yield return new WaitForSeconds(2);
                     
                    int target = 0;
                    if (whichAction == "mali")
                    {
                        target = 1;
                    }
                    else if (whichAction == "ertebat")
                    {
                        target = 2;
                    }
                    else if (whichAction == "attack")
                    {
                        target = 3;
                    }
                    else if (whichAction == "uniqe4")
                    {
                        target = 4;
                    }
                    else if (whichAction == "uniqe5")
                    {
                        target = 5;
                    }

                    if (cpu3.card1 == target || cpu3.card2 == target)
                    { 
                     announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);

                          announcer.text = "ﺪﯾﺪﺟ ﺕﺭﺎﮐ ﺏﺎﺨﺘﻧﺍ ﻝﺎﺣ ﺭﺩ";

                        print("cpu3 : " +cpu3.card1 + " - " + cpu3.card2);

                backToDeck(target);
                ShuffleArray(numbers,false);

                if (cpu3.card1 == target)
                    cpu3.card1 = getFromDeck();
                else
                    cpu3.card2 = getFromDeck();

                    print("cpu3 : " + cpu3.card1 + " - " + cpu3.card2);
                    yield return new WaitForSeconds(1.5f);

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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
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
                            cpu1cards[1].SetActive(false);
                        }

                        printLost();
                        yield return new WaitForSeconds(3);

                        if (whichAction == "mali")
                            StartCoroutine(Mali());
                        else if (whichAction == "ertebat")
                            StartCoroutine(ertebatat());
                        else if (whichAction == "attack")
                        {
                            StartCoroutine(RobAttack());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                        else if (whichAction == "uniqe4")
                            StartCoroutine(uniqe4y());
                        else if (whichAction == "uniqe5")
                        {
                            StartCoroutine(Robuniqe5());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                            

                    }
                    else
                    {
                         announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
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
                 endgame--;
                }
                else if (result[1])
                { announcer.text = "ﺪﯿﺸﮐ ﺶﻟﺎﭼ ﻪﺑ ﺍﺭ " + name_script.cpu3Name + " " + name_script.cpu2Name;
                    yield return new WaitForSeconds(2);

                 
                    int target = 0;
                    if (whichAction == "mali")
                    {
                        target = 1;
                    }
                    else if (whichAction == "ertebat")
                    {
                        target = 2;
                    }
                    else if (whichAction == "attack")
                    {
                        target = 3;
                    }
                    else if (whichAction == "uniqe4")
                    {
                        target = 4;
                    }
                    else if (whichAction == "uniqe5")
                    {
                        target = 5;
                    }

                    if (cpu3.card1 == target || cpu3.card2 == target)
                    {
                     announcer.text = " ﺪﺷ ﻩﺪﻧﺮﺑ " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);
                 
                          announcer.text = "ﺪﯾﺪﺟ ﺕﺭﺎﮐ ﺏﺎﺨﺘﻧﺍ ﻝﺎﺣ ﺭﺩ";

                        print("cpu3 : " +cpu3.card1 + " - " + cpu3.card2);

                backToDeck(target);
                ShuffleArray(numbers,false);

                if (cpu3.card1 == target)
                    cpu3.card1 = getFromDeck();
                else
                    cpu3.card2 = getFromDeck();

                    print("cpu3 : " + cpu3.card1 + " - " + cpu3.card2);
                  yield return new WaitForSeconds(1.5f);
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
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

                        if (whichAction == "mali")
                            StartCoroutine(Mali());
                        else if (whichAction == "ertebat")
                            StartCoroutine(ertebatat());
                        else if (whichAction == "attack")
                        {
                            StartCoroutine(RobAttack());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                        else if (whichAction == "uniqe4")
                            StartCoroutine(uniqe4y());
                        else if (whichAction == "uniqe5")
                        {
                            StartCoroutine(Robuniqe5());
                            yield return new WaitUntil(() => cClicked == true);
                            cClicked = false;
                        }
                            

                    }
                    else
                    { 
                         announcer.text = " ﺩﺭﻮﺧ ﺖﺴﮑﺷ " + name_script.cpu3Name;
                        yield return new WaitForSeconds(2);
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
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
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
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
                    endgame--;
                }
                
            }
        }


        yield return new WaitForSeconds(2);
        edea[0].SetActive(false);
        edea[1].SetActive(false);
        edea[2].SetActive(false);
        AllCheckAlive();
        next();
        Done = true;
        
    }

    IEnumerator ActionRob()
    {
        bool[] result = { false, false, false };
        announcer.color = Color.black;
        if (cpu1turn)
        {

           
            int ran = Random.Range(1,8);
            // 1 = means cpu1 will select earn money

            // 2 and 3 are BLOF

            //4 and 5 and 6 and 7 are Real

            //8 is coup

            //testing
            
            //

            if (cpu1.coin >= 7)
            {
                int u = Random.Range(1, 3);
                if (u == 1)
                ran = 8;
            }

            if (cpu1.coin >= 10)
                ran = 8;


            if (ran == 1)
            {
                announcer.text = " ﻢﻨﮐ ﯽﻣ ﺪﻣﺁﺭﺩ ﺐﺴﮐ :" + name_script.cpu1Name;
                yield return new WaitForSeconds(2);
                StartCoroutine(earny());
                yield return new WaitForSeconds(1);
                AllCheckAlive();
                next();
                Done = true;
            }
            else if (ran == 2 || ran == 3)
            {
                print("FARIB");
                int ran2;
                do
                {
                    ran2 = Random.Range(1, 6);
                } while (ran2 == cpu1.card1 || ran2 == cpu1.card2);

                bool allow = RobotOffCheck(cpu1, ran2);
                if (allow)
                {
                    if (ran2 == 1)
                    {
                        StartCoroutine(cpuProgress("mali"));
                    }
                    else if (ran2 == 2)
                    {
                        StartCoroutine(cpuProgress("ertebat"));
                    }
                    else if (ran2 == 3)
                    {
                        StartCoroutine(cpuProgress("attack"));
                    }
                    else if (ran2 == 4)
                    {
                        StartCoroutine(cpuProgress("uniqe4"));
                    }
                    else if (ran2 == 5)
                    {
                        StartCoroutine(cpuProgress("uniqe5"));
                    }
                }
                else
                {
                    announcer.text = " ﻢﻨﮐ ﯽﻣ ﺪﻣﺁﺭﺩ ﺐﺴﮐ :" + name_script.cpu1Name;
                    yield return new WaitForSeconds(2);
                    StartCoroutine(earny());
                    yield return new WaitForSeconds(1);
                AllCheckAlive();
                next();
                Done = true;
                }


            }
            else if (ran > 3 && ran < 8)
            {
                int ran2;
                do
                {
                    ran2 = Random.Range(1, 3);
                } while ((ran2 == 1 && cpu1.card1 == -1) || (ran2 == 2 && cpu1.card2 == -1));

                int op = 0;
                if (ran2 == 1)
                    op = cpu1.card1;
                if (ran2 == 2)
                    op = cpu1.card2;

                
                bool allow = RobotOffCheck(cpu1, op);
                // testing
                
                //
                if (allow)
                {
                    if (op == 1)
                    {
                        StartCoroutine(cpuProgress("mali"));
                    }
                    else if (op == 2)
                    {
                        StartCoroutine(cpuProgress("ertebat"));
                    }
                    else if (op == 3)
                    {
                        StartCoroutine(cpuProgress("attack"));
                    }
                    else if (op == 4)
                    {
                        StartCoroutine(cpuProgress("uniqe4"));
                    }
                    else if (op == 5)
                    {
                        StartCoroutine(cpuProgress("uniqe5"));
                    }
                }
                else
                {
                    announcer.text = " ﻢﻨﮐ ﯽﻣ ﺪﻣﺁﺭﺩ ﺐﺴﮐ :" + name_script.cpu1Name;
                    yield return new WaitForSeconds(2);
                    StartCoroutine(earny());
                    yield return new WaitForSeconds(1);
                AllCheckAlive();
                next();
                Done = true;
                }


            }
            else if (ran == 8)
            {
                announcer.text = " ﻢﻨﮑﯿﻣ ﺎﺗﺩﻮﮐ : " + name_script.cpu1Name;
                yield return new WaitForSeconds(2);
                int coupChoose;
                do
                {
                    coupChoose = Random.Range(1, 4);
                } while ((coupChoose == 1 && !Me.Alive) || (coupChoose == 2 && !cpu2.Alive) || (coupChoose == 3 && !cpu3.Alive));



                if (coupChoose == 1)
                {

                    announcer.text = "ﺪﺷ ﺎﺗﺩﻮﮐ ﻮﺗ ﻪﯿﻠﻋ";
                    yield return new WaitForSeconds(1.5f);
                    losingy();
                    yield return new WaitUntil(() => losingClick == true);
                    losingClick = false;

                }
                else if (coupChoose == 2)
                {
                    announcer.text = " ﺪﺷ ﺎﺗﺩﻮﮐ " + name_script.cpu2Name + " ﻪﯿﻠﻋ ";
                    yield return new WaitForSeconds(1.5f);

                    int ran2;
                    do
                    {
                        ran2 = Random.Range(1, 3);
                    } while ((ran2 == 1 && cpu2.card1 == -1) || (ran2 == 2 && cpu2.card2 == -1));

                    if (ran2 == 1)
                    {


                        int box = cpu2.card1;
                        cpu2.card1 = -1;
                        cpu2cards[0].SetActive(false);

                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu2Name;
                            }

                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu2Name;
                            }

                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu2Name;
                            }

                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu2Name;
                            }

                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu2Name;
                            }

                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);

                    }
                    else
                    {
                        int box = cpu2.card2;
                        cpu2.card2 = -1;
                        cpu2cards[1].SetActive(false);


                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu2Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu2Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu2Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu2Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu2Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);
                    }
                    endgame--;
                }
                else if (coupChoose == 3)
                {
                    announcer.text = " ﺪﺷ ﺎﺗﺩﻮﮐ " + name_script.cpu3Name + " ﻪﯿﻠﻋ ";
                    yield return new WaitForSeconds(1.5f);

                    int ran2;
                    do
                    {
                        ran2 = Random.Range(1, 3);
                    } while ((ran2 == 1 && cpu3.card1 == -1) || (ran2 == 2 && cpu3.card2 == -1));

                    if (ran2 == 1)
                    {


                        int box = cpu3.card1;
                        cpu3.card1 = -1;
                        cpu3cards[0].SetActive(false);


                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu3Name;
                            }

                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu3Name;
                            }

                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu3Name;
                            }

                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu3Name;
                            }

                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu3Name;
                            }

                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);

                    }
                    else
                    {
                        int box = cpu3.card2;
                        cpu3.card2 = -1;
                        cpu3cards[1].SetActive(false);

                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu3Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu3Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu3Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu3Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu3Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);
                    }
                    endgame--;
                }
               yield return new WaitForSeconds(1);
                AllCheckAlive();
                next();
                Done = true;
            }

        }else if(cpu2turn){
          int ran = Random.Range(1,8);
            // 1 = means cpu1 will select earn money

            // 2 and 3 are BLOF

            //4 and 5 and 6 and 7 are Real

            //8 is coup

            //testing
           
            //


            if (cpu2.coin >= 7)
            {
                int u = Random.Range(1, 3);
                if (u == 1)
                ran = 8;
            }

            if (cpu2.coin >= 10)
                ran = 8;

            if (ran == 1)
            {
                announcer.text = " ﻢﻨﮐ ﯽﻣ ﺪﻣﺁﺭﺩ ﺐﺴﮐ :" + name_script.cpu2Name;
                yield return new WaitForSeconds(2);
                StartCoroutine(earny());
                yield return new WaitForSeconds(1);
                AllCheckAlive();
                next();
                Done = true;
            }
            else if (ran == 2 || ran == 3)
            {
                print("FARIB");
                int ran2;
                do
                {
                    ran2 = Random.Range(1, 6);
                } while (ran2 == cpu2.card1 || ran2 == cpu2.card2);

                bool allow = RobotOffCheck(cpu2, ran2);
                if (allow)
                {
                    if (ran2 == 1)
                    {
                        StartCoroutine(cpuProgress("mali"));
                    }
                    else if (ran2 == 2)
                    {
                        StartCoroutine(cpuProgress("ertebat"));
                    }
                    else if (ran2 == 3)
                    {
                        StartCoroutine(cpuProgress("attack"));
                    }
                    else if (ran2 == 4)
                    {
                        StartCoroutine(cpuProgress("uniqe4"));
                    }
                    else if (ran2 == 5)
                    {
                        StartCoroutine(cpuProgress("uniqe5"));
                    }
                }
                else
                {
                    announcer.text = " ﻢﻨﮐ ﯽﻣ ﺪﻣﺁﺭﺩ ﺐﺴﮐ :" + name_script.cpu2Name;
                    yield return new WaitForSeconds(2);
                    StartCoroutine(earny());
                    yield return new WaitForSeconds(1);
                AllCheckAlive();
                next();
                Done = true;
                }
            }
            else if (ran > 3 && ran < 8)
            {
                int ran2;
                do
                {
                    ran2 = Random.Range(1, 3);
                } while ((ran2 == 1 && cpu2.card1 == -1) || (ran2 == 2 && cpu2.card2 == -1));

                int op = 0;
                if (ran2 == 1)
                    op = cpu2.card1;
                if (ran2 == 2)
                    op = cpu2.card2;

                // testing

                //

                bool allow = RobotOffCheck(cpu2, op);

                if (allow)
                {
                    if (op == 1)
                    {
                        StartCoroutine(cpuProgress("mali"));
                    }
                    else if (op == 2)
                    {
                        StartCoroutine(cpuProgress("ertebat"));
                    }
                    else if (op == 3)
                    {
                        StartCoroutine(cpuProgress("attack"));
                    }
                    else if (op == 4)
                    {
                        StartCoroutine(cpuProgress("uniqe4"));
                    }
                    else if (op == 5)
                    {
                        StartCoroutine(cpuProgress("uniqe5"));
                    }
                }
                else
                {
                    announcer.text = " ﻢﻨﮐ ﯽﻣ ﺪﻣﺁﺭﺩ ﺐﺴﮐ :" + name_script.cpu2Name;
                    yield return new WaitForSeconds(2);
                    StartCoroutine(earny());
                    yield return new WaitForSeconds(1);
                AllCheckAlive();
                next();
                Done = true;
                }


            }
            else if (ran == 8)
            {
                announcer.text = " ﻢﻨﮑﯿﻣ ﺎﺗﺩﻮﮐ : " + name_script.cpu2Name;
                yield return new WaitForSeconds(2);
                int coupChoose;
                do
                {
                    coupChoose = Random.Range(1, 4);
                } while ((coupChoose == 2 && !Me.Alive) || (coupChoose == 1 && !cpu1.Alive) || (coupChoose == 3 && !cpu3.Alive));



                if (coupChoose == 2)
                {

                    announcer.text = "ﺪﺷ ﺎﺗﺩﻮﮐ ﻮﺗ ﻪﯿﻠﻋ";
                    yield return new WaitForSeconds(1.5f);
                    losingy();
                    yield return new WaitUntil(() => losingClick == true);
                    losingClick = false;

                }
                else if (coupChoose == 1)
                {
                    announcer.text = " ﺪﺷ ﺎﺗﺩﻮﮐ " + name_script.cpu1Name + " ﻪﯿﻠﻋ ";
                    yield return new WaitForSeconds(1.5f);

                    int ran2;
                    do
                    {
                        ran2 = Random.Range(1, 3);
                    } while ((ran2 == 1 && cpu1.card1 == -1) || (ran2 == 2 && cpu1.card2 == -1));

                    if (ran2 == 1)
                    {


                        int box = cpu1.card1;
                        cpu1.card1 = -1;
                        cpu1cards[0].SetActive(false);

                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu1Name;
                            }

                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu1Name;
                            }

                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu1Name;
                            }

                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu1Name;
                            }

                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu1Name;
                            }

                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);

                    }
                    else
                    {
                        int box = cpu1.card2;
                        cpu1.card2 = -1;
                        cpu1cards[1].SetActive(false);


                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu1Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu1Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu1Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu1Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu1Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);
                    }
                    endgame--;
                }
                else if (coupChoose == 3)
                {
                    announcer.text = " ﺪﺷ ﺎﺗﺩﻮﮐ " + name_script.cpu3Name + " ﻪﯿﻠﻋ ";
                    yield return new WaitForSeconds(1.5f);

                    int ran2;
                    do
                    {
                        ran2 = Random.Range(1, 3);
                    } while ((ran2 == 1 && cpu3.card1 == -1) || (ran2 == 2 && cpu3.card2 == -1));

                    if (ran2 == 1)
                    {


                        int box = cpu3.card1;
                        cpu3.card1 = -1;
                        cpu3cards[0].SetActive(false);


                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu3Name;
                            }

                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu3Name;
                            }

                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu3Name;
                            }

                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu3Name;
                            }

                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu3Name;
                            }

                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);

                    }
                    else
                    {
                        int box = cpu3.card2;
                        cpu3.card2 = -1;
                        cpu3cards[1].SetActive(false);

                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu3Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu3Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu3Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu3Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu3Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);
                    }
                    endgame--;
                }
yield return new WaitForSeconds(1);
                AllCheckAlive();
                next();
                Done = true;
            }

        }else if(cpu3turn){
              int ran = Random.Range(1,8);
            // 1 = means cpu1 will select earn money

            // 2 and 3 are BLOF

            //4 and 5 and 6 and 7 are Real

            //8 is coup

            //testing
           
            //

            if (cpu3.coin >= 7)
            {
                int u = Random.Range(1, 3);
                if (u == 1)
                ran = 8;
            }

            if (cpu3.coin >= 10)
                ran = 8;

            if (ran == 1)
            {
                announcer.text = " ﻢﻨﮐ ﯽﻣ ﺪﻣﺁﺭﺩ ﺐﺴﮐ :" + name_script.cpu3Name;
                yield return new WaitForSeconds(2);
                StartCoroutine(earny());
                yield return new WaitForSeconds(1);
                AllCheckAlive();
                next();
                Done = true;
            }
            else if (ran == 2 || ran == 3)
            {
                print("FARIB");
                int ran2;
                do
                {
                    ran2 = Random.Range(1, 6);
                } while (ran2 == cpu3.card1 || ran2 == cpu3.card2);

                bool allow = RobotOffCheck(cpu3, ran2);
                if (allow)
                {
                    if (ran2 == 1)
                    {
                        StartCoroutine(cpuProgress("mali"));
                    }
                    else if (ran2 == 2)
                    {
                        StartCoroutine(cpuProgress("ertebat"));
                    }
                    else if (ran2 == 3)
                    {
                        StartCoroutine(cpuProgress("attack"));
                    }
                    else if (ran2 == 4)
                    {
                        StartCoroutine(cpuProgress("uniqe4"));
                    }
                    else if (ran2 == 5)
                    {
                        StartCoroutine(cpuProgress("uniqe5"));
                    }
                }
                else
                {
                    announcer.text = " ﻢﻨﮐ ﯽﻣ ﺪﻣﺁﺭﺩ ﺐﺴﮐ :" + name_script.cpu3Name;
                    yield return new WaitForSeconds(2);
                    StartCoroutine(earny());
                    yield return new WaitForSeconds(1);
                AllCheckAlive();
                next();
                Done = true;
                }
            }
            else if (ran > 3 && ran < 8)
            {
                int ran2;
                do
                {
                    ran2 = Random.Range(1, 3);
                } while ((ran2 == 1 && cpu3.card1 == -1) || (ran2 == 2 && cpu3.card2 == -1));

                int op = 0;
                if (ran2 == 1)
                    op = cpu3.card1;
                if (ran2 == 2)
                    op = cpu3.card2;

                // testing

                //

                bool allow = RobotOffCheck(cpu3, op);
                if (allow)
                {
                    if (op == 1)
                    {
                        StartCoroutine(cpuProgress("mali"));
                    }
                    else if (op == 2)
                    {
                        StartCoroutine(cpuProgress("ertebat"));
                    }
                    else if (op == 3)
                    {
                        StartCoroutine(cpuProgress("attack"));
                    }
                    else if (op == 4)
                    {
                        StartCoroutine(cpuProgress("uniqe4"));
                    }
                    else if (op == 5)
                    {
                        StartCoroutine(cpuProgress("uniqe5"));
                    }
                }
                else
                {
                    announcer.text = " ﻢﻨﮐ ﯽﻣ ﺪﻣﺁﺭﺩ ﺐﺴﮐ :" + name_script.cpu3Name;
                    yield return new WaitForSeconds(2);
                    StartCoroutine(earny());
                    yield return new WaitForSeconds(1);
                AllCheckAlive();
                next();
                Done = true;
                }


            }
            else if (ran == 8)
            {
                announcer.text = " ﻢﻨﮑﯿﻣ ﺎﺗﺩﻮﮐ : " + name_script.cpu3Name;
                yield return new WaitForSeconds(2);
                int coupChoose;
                do
                {
                    coupChoose = Random.Range(1, 4);
                } while ((coupChoose == 3 && !Me.Alive) || (coupChoose == 2 && !cpu2.Alive) || (coupChoose == 1 && !cpu1.Alive));



                if (coupChoose == 3)
                {

                    announcer.text = "ﺪﺷ ﺎﺗﺩﻮﮐ ﻮﺗ ﻪﯿﻠﻋ";
                    yield return new WaitForSeconds(1.5f);
                    losingy();
                    yield return new WaitUntil(() => losingClick == true);
                    losingClick = false;

                }
                else if (coupChoose == 2)
                {
                    announcer.text = " ﺪﺷ ﺎﺗﺩﻮﮐ " + name_script.cpu2Name + " ﻪﯿﻠﻋ ";
                    yield return new WaitForSeconds(1.5f);

                    int ran2;
                    do
                    {
                        ran2 = Random.Range(1, 3);
                    } while ((ran2 == 1 && cpu2.card1 == -1) || (ran2 == 2 && cpu2.card2 == -1));

                    if (ran2 == 1)
                    {


                        int box = cpu2.card1;
                        cpu2.card1 = -1;
                        cpu2cards[0].SetActive(false);

                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu2Name;
                            }

                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu2Name;
                            }

                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu2Name;
                            }

                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu2Name;
                            }

                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu2Name;
                            }

                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);

                    }
                    else
                    {
                        int box = cpu2.card2;
                        cpu2.card2 = -1;
                        cpu2cards[1].SetActive(false);


                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu2Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu2Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu2Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu2Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu2Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);
                    }
                    endgame--;
                }
                else if (coupChoose == 1)
                {
                    announcer.text = " ﺪﺷ ﺎﺗﺩﻮﮐ " + name_script.cpu1Name + " ﻪﯿﻠﻋ ";
                    yield return new WaitForSeconds(1.5f);

                    int ran2;
                    do
                    {
                        ran2 = Random.Range(1, 3);
                    } while ((ran2 == 1 && cpu1.card1 == -1) || (ran2 == 2 && cpu1.card2 == -1));

                    if (ran2 == 1)
                    {


                        int box = cpu1.card1;
                        cpu1.card1 = -1;
                        cpu1cards[0].SetActive(false);


                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu1Name;
                            }

                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu1Name;
                            }

                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu1Name;
                            }

                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu1Name;
                            }

                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu1Name;
                            }

                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);

                    }
                    else
                    {
                        int box = cpu1.card2;
                        cpu1.card2 = -1;
                        cpu1cards[1].SetActive(false);

                        if (box == 1)
                        {
                            if (mali == "banker")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ" + name_script.cpu1Name;
                            }
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ" + name_script.cpu1Name;
                            }
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ" + name_script.cpu1Name;
                            }
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ" + name_script.cpu1Name;
                            }
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                            {
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ" + name_script.cpu1Name;
                            }
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                        yield return new WaitForSeconds(2);
                    }
                    endgame--;
                }
yield return new WaitForSeconds(1);
                AllCheckAlive();
                next();
                Done = true;
            }

        }
        yield return new WaitForSeconds(1);
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
        endgame--;

        AllCheckAlive();
        next();
        Done = true;
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
            AllCheckAlive();
            next();
            Done = true;
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
                announcer.color = Color.green;
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
            yield return new WaitForSeconds(2.5f);
        announcer.color = Color.black;
        announcer.text = "";
        yield return new WaitForSeconds(1.5f);
        }
        
    }

    IEnumerator ertebatat()
    {
        announcer.text = "";
        int Role1 = 0, Role2 = 0, index1=0, index2=0;
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



        if (myturn)
        {
            print(Me.card1 + " -- " + Me.card2);
            midIconCheck(Role1, Role2);
            

            int lives = 0;
            if (Me.card1 != -1)
                lives++;
            if (Me.card2 != -1)
                lives++;

            if (lives == 1)
                joontitle.text = "ﻦﮐ ﺏﺎﺨﺘﻧﺍ ﺕﺭﺎﮐ ﮏﯾ";
            else if(lives == 2)
                joontitle.text = "ﻦﮐ ﺏﺎﺨﺘﻧﺍ ﺕﺭﺎﮐ ﻭﺩ";

            midField.SetActive(true);
            yield return new WaitUntil(() => ertebatClick == lives);
            midField.SetActive(false);
            ertebatClick = 0;
            print("run");

            if (ertebat == "director")
            {
                if (lives == 2)
                {

                    if (Firstchoice == 0)
                    {
                        if (secondchoice == 1)
                        {
                            // nothing
                        }
                        else if (secondchoice == 2)
                        {
                            int box = Me.card2;
                            Me.card2 = Role1;
                            numbers[index1] = box;
                            print(Me.card1 + " -- " + Role1);
                        }
                        else if (secondchoice == 3)
                        {
                            int box = Me.card2;
                            Me.card2 = Role2;
                            numbers[index2] = box;
                            print(Me.card1 + " -- " + Role2);
                        }
                    }
                    else if (Firstchoice == 1)
                    {
                        if (secondchoice == 0)
                        {  //nothing
                            print(Me.card1 + " -- " + Me.card2);
                        }
                        else if (secondchoice == 2)
                        {
                            int box = Me.card1;
                            Me.card1 = Role1;
                            numbers[index1] = box;
                            print(Me.card2 + " -- " + Role1);
                        }
                        else if (secondchoice == 3)
                        {
                            int box = Me.card1;
                            Me.card1 = Role2;
                            numbers[index2] = box;
                            print(Me.card2 + " -- " + Role2);
                        }
                    }
                    else if (Firstchoice == 2)
                    {
                        if (secondchoice == 0)
                        {
                            int box = Me.card2;
                            Me.card2 = Role1;
                            numbers[index1] = box;
                            print(Role1 + " -- " + Me.card1);
                        }
                        else if (secondchoice == 1)
                        {
                            int box = Me.card1;
                            Me.card1 = Role1;
                            numbers[index1] = box;
                            print(Role1 + " -- " + Me.card2);
                        }
                        else if (secondchoice == 3)
                        {
                            int box1 = Me.card1;
                            int box2 = Me.card2;
                            Me.card1 = Role1;
                            Me.card2 = Role2;
                            numbers[index1] = box1;
                            numbers[index2] = box2;
                            print(Role1 + " -- " + Role2);
                        }
                    }
                    else if (Firstchoice == 3)
                    {
                        if (secondchoice == 0)
                        {
                            int box = Me.card2;
                            Me.card2 = Role2;
                            numbers[index2] = box;
                            print(Role2 + " -- " + Me.card1);
                        }
                        else if (secondchoice == 1)
                        {
                            int box = Me.card1;
                            Me.card1 = Role2;
                            numbers[index2] = box;
                            print(Role2 + " -- " + Me.card2);
                        }
                        else if (secondchoice == 2)
                        {
                            print(Role2 + " -- " + Role1);
                            int box1 = Me.card1;
                            int box2 = Me.card2;
                            Me.card1 = Role1;
                            Me.card2 = Role2;
                            numbers[index1] = box1;
                            numbers[index2] = box2;
                            print(Role1 + " -- " + Role2);
                        }
                    }
                }
                else if (lives == 1)
                {
                    if (Firstchoice != 2 && Firstchoice != 3)
                    {
                        //nothing
                    }
                    else
                    {
                        if (Firstchoice == 2)
                        {
                            if (Me.card1 != -1)
                            {
                                int box = Me.card1;
                                Me.card1 = Role1;
                                numbers[index1] = box;
                            }
                            else
                            {
                                int box = Me.card2;
                                Me.card2 = Role1;
                                numbers[index1] = box;
                            }
                        }
                        else if (Firstchoice == 3)
                        {
                            if (Me.card1 != -1)
                            {
                                int box = Me.card1;
                                Me.card1 = Role2;
                                numbers[index2] = box;
                            }
                            else
                            {
                                int box = Me.card2;
                                Me.card2 = Role2;
                                numbers[index2] = box;
                            }
                        }
                    }
                }
            }

            MeIconCheck();
            print("finaly : " + Me.card1 + " -- " + Me.card2);




            Firstchoice = -1;
            secondchoice = -1;
        }
        else
        {
            int joon = 0;
            if (cpu1.card1 != -1)
                joon++;
            if (cpu1.card2 != -1)
                joon++;

            if (ertebat == "director")
            {



                Player temp = new Player();
                if (cpu1turn)
                    temp = cpu1;
                else if (cpu2turn)
                    temp = cpu2;
                else if (cpu3turn)
                    temp = cpu3;

                print("mine : " + temp.card1 + " , " + temp.card2);
                print("deck : " + Role1 + " , " + Role2);

                if (joon == 2)
                {
                    int select1 = 0, select2 = 0;

                    select1 = Random.Range(1, 5);

                    do
                    {
                        select2 = Random.Range(1, 5);
                    } while (select2 == select1);
print("select : " + select1 + " , " + select2);
                    if (select1 == 1)
                    {
                        if (select2 == 2)
                        {
                            //nothing
                        }
                        else if (select2 == 3)
                        {
                            int box = temp.card2;

                            if (temp == cpu1)
                                cpu1.card2 = Role1;
                            else if (temp == cpu2)
                                cpu2.card2 = Role1;
                            else if (temp == cpu3)
                                cpu3.card2 = Role1;

                            numbers[index1] = box;
                        }
                        else if (select2 == 4)
                        {
                            int box = temp.card2;

                            if (temp == cpu1)
                                cpu1.card2 = Role2;
                            else if (temp == cpu2)
                                cpu2.card2 = Role2;
                            else if (temp == cpu3)
                                cpu3.card2 = Role2;

                            numbers[index2] = box;
                        }
                    }
                    else if (select1 == 2)
                    {
                        if (select2 == 1)
                        {
                            //nothing
                        }
                        else if (select2 == 3)
                        {
                            int box = temp.card1;

                            if (temp == cpu1)
                                cpu1.card1 = Role1;
                            else if (temp == cpu2)
                                cpu2.card1 = Role1;
                            else if (temp == cpu3)
                                cpu3.card1 = Role1;

                            numbers[index1] = box;
                        }
                        else if (select2 == 4)
                        {
                            int box = temp.card1;

                            if (temp == cpu1)
                                cpu1.card1 = Role2;
                            else if (temp == cpu2)
                                cpu2.card1 = Role2;
                            else if (temp == cpu3)
                                cpu3.card1 = Role2;

                            numbers[index2] = box;
                        }
                    }
                    else if (select1 == 3)
                    {
                        if (select2 == 1)
                        {
                            int box = temp.card2;

                            if (temp == cpu1)
                                cpu1.card2 = Role1;
                            else if (temp == cpu2)
                                cpu2.card2 = Role1;
                            else if (temp == cpu3)
                                cpu3.card2 = Role1;

                            numbers[index1] = box;
                        }
                        else if (select2 == 2)
                        {
                            int box = temp.card1;

                            if (temp == cpu1)
                                cpu1.card1 = Role1;
                            else if (temp == cpu2)
                                cpu2.card1 = Role1;
                            else if (temp == cpu3)
                                cpu3.card1 = Role1;

                            numbers[index1] = box;

                        }
                        else if (select2 == 4)
                        {
                            int box1 = temp.card1;
                            int box2 = temp.card2;

                            if (temp == cpu1)
                                cpu1.card1 = Role1;
                            else if (temp == cpu2)
                                cpu2.card1 = Role1;
                            else if (temp == cpu3)
                                cpu3.card1 = Role1;

                            if (temp == cpu1)
                                cpu1.card2 = Role2;
                            else if (temp == cpu2)
                                cpu2.card2 = Role2;
                            else if (temp == cpu3)
                                cpu3.card2 = Role2;

                            numbers[index1] = box1;
                            numbers[index2] = box2;
                        }
                    }
                    else if (select1 == 4)
                    {
                        if (select2 == 1)
                        {
                            int box = temp.card2;

                            if (temp == cpu1)
                                cpu1.card2 = Role2;
                            else if (temp == cpu2)
                                cpu2.card2 = Role2;
                            else if (temp == cpu3)
                                cpu3.card2 = Role2;

                            numbers[index2] = box;
                        }
                        else if (select2 == 2)
                        {
                            int box = temp.card1;

                            if (temp == cpu1)
                                cpu1.card1 = Role2;
                            else if (temp == cpu2)
                                cpu2.card1 = Role2;
                            else if (temp == cpu3)
                                cpu3.card1 = Role2;

                            numbers[index2] = box;
                        }
                        else if (select2 == 3)
                        {
                            int box1 = temp.card1;
                            int box2 = temp.card2;

                            if (temp == cpu1)
                                cpu1.card1 = Role1;
                            else if (temp == cpu2)
                                cpu2.card1 = Role1;
                            else if (temp == cpu3)
                                cpu3.card1 = Role1;

                            if (temp == cpu1)
                                cpu1.card2 = Role2;
                            else if (temp == cpu2)
                                cpu2.card2 = Role2;
                            else if (temp == cpu3)
                                cpu3.card2 = Role2;

                            numbers[index1] = box1;
                            numbers[index2] = box2;
                        }
                    }

                }
                else if (joon == 1)
                {
                    int select = Random.Range(1, 4);
print("select : " + select);
                    if (select == 1)
                    {
                        //nothinh
                    }
                    else if (select == 2)
                    {
                        int box;
                        if (temp.card1 != -1)
                            box = temp.card1;
                        else
                            box = temp.card2;

                        if (temp.card1 != -1)
                        {
                            if (temp == cpu1)
                                cpu1.card1 = Role1;
                            else if (temp == cpu2)
                                cpu2.card1 = Role1;
                            else if (temp == cpu3)
                                cpu3.card1 = Role1;
                            numbers[index1] = box;
                        }
                        else
                        {
                            if (temp == cpu1)
                                cpu1.card2 = Role2;
                            else if (temp == cpu2)
                                cpu2.card2 = Role2;
                            else if (temp == cpu3)
                                cpu3.card2 = Role2;
                            numbers[index1] = box;
                        }
                    }
                    else if (select == 3)
                    {
                        int box;
                        if (temp.card1 != -1)
                            box = temp.card1;
                        else
                            box = temp.card2;

                        if (temp.card1 != -1)
                        {
                            if (temp == cpu1)
                                cpu1.card1 = Role2;
                            else if (temp == cpu2)
                                cpu2.card1 = Role2;
                            else if (temp == cpu3)
                                cpu3.card1 = Role2;
                            numbers[index2] = box;
                        }
                        else
                        {
                            if (temp == cpu1)
                                cpu1.card2 = Role2;
                            else if (temp == cpu2)
                                cpu2.card2 = Role2;
                            else if (temp == cpu3)
                                cpu3.card2 = Role2;
                            numbers[index2] = box;
                        }
                    }
                }


                if (temp == cpu1)
                    print("now : " + cpu1.card1 + " , " + cpu1.card2);
                else if (temp == cpu2)
                    print("now : " + cpu2.card1 + " , " + cpu2.card2);
                else if(temp == cpu3)
                    print("now : " + cpu3.card1 + " , " + cpu3.card2);
            }
            
        }

        
        cClicked = true;
    }

    IEnumerator Attack()
    {

        announcer.text = "";


        if (WhoSolh == 1)
        {
            attackCircle[0].SetActive(false);
        }
        else
        {
            attackCircle[0].SetActive(true);
        }

        if (WhoSolh == 2)
        {
            attackCircle[1].SetActive(false);
        }
        else
        {
            attackCircle[1].SetActive(true);
        }
        if (WhoSolh == 3)
        {
            attackCircle[2].SetActive(false);
        }
        else
        {
            attackCircle[2].SetActive(true);
        }

        if (!cpu1.Alive)
        {
            attackCircle[0].SetActive(false);
        }
        if (!cpu2.Alive)
        {
            attackCircle[1].SetActive(false);
        }
        if (!cpu3.Alive)
        {
            attackCircle[2].SetActive(false);
        }

       

        attackCanvas.SetActive(true);

        yield return new WaitUntil(() => meWait == true);
        meWait = false;
        attackCanvas.SetActive(false);
        
        if (attack == "cherik")
        {
            Me.coin -= 4;
            cointxt[0].text = Me.coin.ToString();
        }
        
        if (whoAttacked == 1)
        {


            if (cpu1.card1 == 3 || cpu1.card2 == 3)
            {
                if (attack == "cherik")
                {
                    announcer.text = " ﻡﺭﺍﺩ ﮏﯾﺮﭼ :" + name_script.cpu1Name;
                }

                chalesh.SetActive(true);
                yield return new WaitUntil(() => meWait == true);
                meWait = false;

                if (mychallange)
                {
                    announcer.color = Color.red;
                    announcer.text = "ﯼﺩﺭﻮﺧ ﺖﺴﮑﺷ";
                    yield return new WaitForSeconds(1.5f);
                    announcer.color = Color.black;
                    announcer.text = "";

                    losingy();
                    yield return new WaitUntil(() => losingClick == true);
                    losingClick = false;

                }
                else
                {
                    // Done
                }

            }
            else
            {
                int ran = Random.Range(1, 5);

                if (ran == 1)
                {
                    print("BLOF");
                    if (attack == "cherik")
                    {
                        announcer.text = " ﻡﺭﺍﺩ ﮏﯾﺮﭼ :" + name_script.cpu1Name;
                    }

                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                    if (mychallange)
                    {
                        announcer.color = Color.green;
                        announcer.text = "ﺪﯾﺪﺷ ﺶﻟﺎﭼ ﻩﺪﻧﺮﺑ";
                        yield return new WaitForSeconds(1.5f);
                        announcer.text = "";
                        announcer.color = Color.black;

                        if (cpu1.card1 != -1)
                        {
                            int box = cpu1.card1;
                            cpu1.card1 = -1;
                            cpu1cards[0].SetActive(false);

                            if (box == 1)
                            {
                                if (mali == "banker")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);
                            endgame--;
                        }
                        if (cpu1.card2 != -1)
                        {
                            int box = cpu1.card2;
                            cpu1.card1 = -1;
                            cpu1cards[1].SetActive(false);

                            if (box == 1)
                            {
                                if (mali == "banker")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);
                            endgame--;
                        }
                        
                        cpu1.Alive = false;
                    }
                    else
                    {
                        // Done
                    }

                }
                else
                {
                    int ran2;
                    do
                    {
                        ran2 = Random.Range(1, 3);
                    } while ((ran2 == 1 && cpu1.card1 == -1) || (ran2 == 2 && cpu1.card2 == -1));

                    if (ran2 == 1)
                    {
                        int box = cpu1.card1;
                        cpu1.card1 = -1;
                        cpu1cards[0].SetActive(false);

                        if (box == 1)
                        {
                            if (mali == "banker")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();

                    }
                    else
                    {
                        int box = cpu1.card2;
                        cpu1.card2 = -1;
                        cpu1cards[1].SetActive(false);

                        if (box == 1)
                        {
                            if (mali == "banker")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                    }
                    yield return new WaitForSeconds(2);
                    endgame--;
                }
            }
        }
        else if (whoAttacked == 2)
        {
            if (cpu2.card1 == 3 || cpu2.card2 == 3)
            {
                if (attack == "cherik")
                {
                    announcer.text = " ﻡﺭﺍﺩ ﮏﯾﺮﭼ :" + name_script.cpu2Name;
                }

                chalesh.SetActive(true);
                yield return new WaitUntil(() => meWait == true);
                meWait = false;

                if (mychallange)
                {
                    announcer.color = Color.red;
                    announcer.text = "ﯼﺩﺭﻮﺧ ﺖﺴﮑﺷ";
                    yield return new WaitForSeconds(1.5f);
                    announcer.color = Color.black;
                    announcer.text = "";

                    losingy();
                    yield return new WaitUntil(() => losingClick == true);
                    losingClick = false;

                }
                else
                {
                    // Done
                }
            }
            else
            {
                int ran = 2;//Random.Range(1, 5);

                if (ran == 1)
                {
                    print("BLOF");
                    if (attack == "cherik")
                    {
                        announcer.text = " ﻡﺭﺍﺩ ﮏﯾﺮﭼ :" + name_script.cpu2Name;
                    }

                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                    if (mychallange)
                    {
                        announcer.color = Color.green;
                        announcer.text = "ﺪﯾﺪﺷ ﺶﻟﺎﭼ ﻩﺪﻧﺮﺑ";
                        yield return new WaitForSeconds(1.5f);
                        announcer.color = Color.black;
                        announcer.text = "";

                        if (cpu2.card1 != -1)
                        {
                            int box = cpu2.card1;
                            cpu2.card1 = -1;
                            cpu2cards[0].SetActive(false);

                            if (box == 1)
                            {
                                if (mali == "banker")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);
                            endgame--;
                        }
                        if (cpu2.card2 != -1)
                        {
                            int box = cpu2.card2;
                            cpu2.card1 = -1;
                            cpu2cards[1].SetActive(false);

                            if (box == 1)
                            {
                                if (mali == "banker")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);
                            endgame--;
                        }
                       
                        cpu2.Alive = false;
                    }
                    else
                    {
                        // Done
                    }

                }
                else
                {
                    int ran2;
                    do
                    {
                        ran2 = Random.Range(1, 3);
                    } while ((ran2 == 1 && cpu2.card1 == -1) || (ran2 == 2 && cpu2.card2 == -1));

                    if (ran2 == 1)
                    {
                        int box = cpu2.card1;
                        cpu2.card1 = -1;
                        cpu2cards[0].SetActive(false);

                        if (box == 1)
                        {
                            if (mali == "banker")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();

                    }
                    else
                    {
                        int box = cpu2.card2;
                        cpu2.card2 = -1;
                        cpu2cards[1].SetActive(false);

                        if (box == 1)
                        {
                            if (mali == "banker")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                    }
                    yield return new WaitForSeconds(2);
                    endgame--;
                }
            }
        }
        else if (whoAttacked == 3)
        {
            if (cpu3.card1 == 3 || cpu3.card2 == 3)
            {
                if (attack == "cherik")
                {
                    announcer.text = " ﻡﺭﺍﺩ ﮏﯾﺮﭼ :" + name_script.cpu3Name;
                }

                chalesh.SetActive(true);
                yield return new WaitUntil(() => meWait == true);
                meWait = false;

                if (mychallange)
                {
                    announcer.color = Color.red;
                    announcer.text = "ﯼﺩﺭﻮﺧ ﺖﺴﮑﺷ";
                    yield return new WaitForSeconds(1.5f);
                    announcer.color = Color.black;
                    announcer.text = "";

                    losingy();
                    yield return new WaitUntil(() => losingClick == true);
                    losingClick = false;

                }
                else
                {
                    // Done
                }
            }
            else
            {
                int ran = 2;//Random.Range(1, 5);

                if (ran == 1)
                {
                    print("BLOF");
                   
                    if (attack == "cherik")
                    {
                        announcer.text = " ﻡﺭﺍﺩ ﮏﯾﺮﭼ :" + name_script.cpu3Name;
                    }

                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                    if (mychallange)
                    {
                        announcer.color = Color.green;
                        announcer.text = "ﺪﯾﺪﺷ ﺶﻟﺎﭼ ﻩﺪﻧﺮﺑ";
                        yield return new WaitForSeconds(1.5f);
                        announcer.color = Color.black;
                        announcer.text = "";

                        if (cpu3.card1 != -1)
                        {
                            int box = cpu3.card1;
                            cpu3.card1 = -1;
                            cpu3cards[0].SetActive(false);

                            if (box == 1)
                            {
                                if (mali == "banker")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);
                            endgame--;
                        }
                        if (cpu3.card2 != -1)
                        {
                            int box = cpu3.card2;
                            cpu3.card1 = -1;
                            cpu3cards[1].SetActive(false);

                            if (box == 1)
                            {
                                if (mali == "banker")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                            }
                            else if (box == 2)
                            {
                                if (ertebat == "director")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                            }
                            else if (box == 3)
                            {
                                if (attack == "cherik")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                            }
                            else if (box == 4)
                            {
                                if (uniqe4 == "solh")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                            }
                            else if (box == 5)
                            {
                                if (uniqe5 == "siasat")
                                    announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                            }

                            for (int i = 0; i < lost.Length; i++)
                            {
                                if (lost[i] == -1)
                                {
                                    lost[i] = box;
                                    break;
                                }
                            }
                            printLost();
                            yield return new WaitForSeconds(2);
                            endgame--;
                        }
                        
                        cpu3.Alive = false;
                    }
                    else
                    {
                        // Done
                    }

                }
                else
                {
                    int ran2;
                    do
                    {
                        ran2 = Random.Range(1, 3);
                    } while ((ran2 == 1 && cpu3.card1 == -1) || (ran2 == 2 && cpu3.card2 == -1));

                    if (ran2 == 1)
                    {
                        int box = cpu3.card1;
                        cpu3.card1 = -1;
                        cpu3cards[0].SetActive(false);

                        if (box == 1)
                        {
                            if (mali == "banker")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();

                    }
                    else
                    {
                        int box = cpu3.card2;
                        cpu3.card2 = -1;
                        cpu3cards[1].SetActive(false);

                        if (box == 1)
                        {
                            if (mali == "banker")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                        }
                        else if (box == 2)
                        {
                            if (ertebat == "director")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                        }
                        else if (box == 3)
                        {
                            if (attack == "cherik")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                        }
                        else if (box == 4)
                        {
                            if (uniqe4 == "solh")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                        }
                        else if (box == 5)
                        {
                            if (uniqe5 == "siasat")
                                announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                        }

                        for (int i = 0; i < lost.Length; i++)
                        {
                            if (lost[i] == -1)
                            {
                                lost[i] = box;
                                break;
                            }
                        }
                        printLost();
                    }
                    yield return new WaitForSeconds(2);
                    endgame--;
                }
            }
        }

    cClicked = true;
    }


    IEnumerator uniqe4y()
    {
        if (uniqe4 == "solh")
        {
            if (myturn)
            {
                Me.coin += 1;
                cointxt[0].text = Me.coin.ToString();
                announcer.text = " ﻪﮑﺳ " + "+1";
                yield return new WaitForSeconds(1.5f);
                WhoSolh = 0;
                solhIcon[0].SetActive(true);
                solhIcon[1].SetActive(false);
                solhIcon[2].SetActive(false);
                solhIcon[3].SetActive(false);
            }
            else if (cpu1turn)
            {
                cpu1.coin += 1;
                cointxt[1].text = cpu1.coin.ToString();
                announcer.text = " ﻪﮑﺳ " + "+1";
                yield return new WaitForSeconds(1.5f);
                WhoSolh = 1;
                solhIcon[1].SetActive(true);
                solhIcon[0].SetActive(false);
                solhIcon[2].SetActive(false);
                solhIcon[3].SetActive(false);
            }
            else if (cpu2turn)
            {
                cpu2.coin += 1;
                cointxt[2].text = cpu2.coin.ToString();
                announcer.text = " ﻪﮑﺳ " + "+1";
                yield return new WaitForSeconds(1.5f);
                WhoSolh = 2;
                solhIcon[2].SetActive(true);
                solhIcon[0].SetActive(false);
                solhIcon[1].SetActive(false);
                solhIcon[3].SetActive(false);
            }
            else if (cpu3turn)
            {
                cpu3.coin += 1;
                cointxt[3].text = cpu3.coin.ToString();
                announcer.text = " ﻪﮑﺳ " + "+1";
                yield return new WaitForSeconds(1.5f);
                WhoSolh = 3;
                solhIcon[3].SetActive(true);
                solhIcon[0].SetActive(false);
                solhIcon[1].SetActive(false);
                solhIcon[2].SetActive(false);
            }
            
        }
    }

    IEnumerator uniqe5y()
    {
        if (uniqe5 == "siasat")
        {
            announcer.text = "";

            if (WhoSolh == 1)
            {
                politicCircle[0].SetActive(false);
            }
            else
            {
                politicCircle[0].SetActive(true);
            }

            if (WhoSolh == 2)
            {
                politicCircle[1].SetActive(false);
            }
            else
            {
                politicCircle[1].SetActive(true);
            }
            if (WhoSolh == 3)
            {
                politicCircle[2].SetActive(false);
            }
            else
            {
                politicCircle[2].SetActive(true);
            }


            if (!cpu1.Alive)
            {
                politicCircle[0].SetActive(false);
            }
            if (!cpu2.Alive)
            {
                politicCircle[1].SetActive(false);
            }
            if (!cpu3.Alive)
            {
                politicCircle[2].SetActive(false);
            }



            politicCanvas.SetActive(true);

            yield return new WaitUntil(() => cClicked == true);
            cClicked = false;
            politicCanvas.SetActive(false);

            if (whoPolitic == 1)
            {


                if (cpu1.card1 == 5 || cpu1.card2 == 5)
                {
                    
                    announcer.text = " ﻡﺭﺍﺪﻤﺘﺳﺎﯿﺳ : " + name_script.cpu1Name;


                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                    if (mychallange)
                    {
                        announcer.color = Color.red;
                        announcer.text = "ﯼﺩﺭﻮﺧ ﺖﺴﮑﺷ";
                        yield return new WaitForSeconds(1.5f);
                        announcer.text = "";
                        announcer.color = Color.black;

                        losingy();
                        yield return new WaitUntil(() => losingClick == true);
                        losingClick = false;

                    }
                    else
                    {
                        // Done
                    }

                }
                else
                {
                    int ran = Random.Range(1, 5);

                    if (ran == 1)
                    {
                        print("BLOF");


                        announcer.text = " ﻡﺭﺍﺪﻤﺘﺳﺎﯿﺳ : " + name_script.cpu1Name;


                        chalesh.SetActive(true);
                        yield return new WaitUntil(() => meWait == true);
                        meWait = false;

                        if (mychallange)
                        {
                            announcer.color = Color.green;
                            announcer.text = "ﺪﯾﺪﺷ ﺶﻟﺎﭼ ﻩﺪﻧﺮﺑ";
                            yield return new WaitForSeconds(1.5f);
                            announcer.text = "";
                            announcer.color = Color.black;

                            int ran2;
                            do
                            {
                                ran2 = Random.Range(1, 3);
                            } while ((ran2 == 1 && cpu1.card1 == -1) || (ran2 == 2 && cpu1.card2 == -1));

                            if (ran2 == 1)
                            {
                                int box = cpu1.card1;
                                cpu1.card1 = -1;
                                cpu1cards[0].SetActive(false);

                                if (box == 1)
                                {
                                    if (mali == "banker")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                            }
                            else if (ran2 == 2)
                            {
                                int box = cpu1.card2;
                                cpu1.card1 = -1;
                                cpu1cards[1].SetActive(false);

                                if (box == 1)
                                {
                                    if (mali == "banker")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu1Name;
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu1Name;
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu1Name;
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu1Name;
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu1Name;
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                            }

                            if (cpu1.coin >= 2)
                            {
                                Me.coin += 2;
                                cpu1.coin -= 2;
                                cointxt[0].text = Me.coin.ToString();
                                cointxt[1].text = cpu1.coin.ToString();
                            }
                            else
                            {
                                Me.coin += cpu1.coin;
                                cpu1.coin = 0;
                                cointxt[0].text = Me.coin.ToString();
                                cointxt[1].text = cpu1.coin.ToString();
                            }
                            endgame--;
                        }
                        else
                        {
                            // Done
                        }

                    }
                    else
                    {
                        if (cpu1.coin >= 2)
                        {
                            Me.coin += 2;
                            cpu1.coin -= 2;
                            cointxt[0].text = Me.coin.ToString();
                            cointxt[1].text = cpu1.coin.ToString();
                        }
                        else
                        {
                            Me.coin += cpu1.coin;
                            cpu1.coin = 0;
                            cointxt[0].text = Me.coin.ToString();
                            cointxt[1].text = cpu1.coin.ToString();
                        }
                    }

                }
            }
            else if (whoPolitic == 2)
            {
                if (cpu2.card1 == 5 || cpu2.card2 == 5)
                {

                    announcer.text = " ﻡﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;


                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                    if (mychallange)
                    {
                        announcer.color = Color.red;
                        announcer.text = "ﯼﺩﺭﻮﺧ ﺖﺴﮑﺷ";
                        yield return new WaitForSeconds(1.5f);
                        announcer.text = "";
                        announcer.color = Color.black;

                        losingy();
                        yield return new WaitUntil(() => losingClick == true);
                        losingClick = false;

                    }
                    else
                    {
                        // Done
                    }
                }
                else
                {
                    int ran = Random.Range(1, 5);

                    if (ran == 1)
                    {
                        print("BLOF");

                        announcer.text = " ﻡﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;


                        chalesh.SetActive(true);
                        yield return new WaitUntil(() => meWait == true);
                        meWait = false;

                        if (mychallange)
                        {
                            announcer.color = Color.green;
                            announcer.text = "ﺪﯾﺪﺷ ﺶﻟﺎﭼ ﻩﺪﻧﺮﺑ";
                            yield return new WaitForSeconds(1.5f);
                            announcer.text = "";
                            announcer.color = Color.black;

                            int ran2;
                            do
                            {
                                ran2 = Random.Range(1, 3);
                            } while ((ran2 == 1 && cpu2.card1 == -1) || (ran2 == 2 && cpu2.card2 == -1));

                            if (ran2 == 1)
                            {
                                int box = cpu2.card1;
                                cpu2.card1 = -1;
                                cpu2cards[0].SetActive(false);

                                if (box == 1)
                                {
                                    if (mali == "banker")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                            }
                            else if (ran2 == 2)
                            {
                                int box = cpu2.card2;
                                cpu2.card1 = -1;
                                cpu2cards[1].SetActive(false);

                                if (box == 1)
                                {
                                    if (mali == "banker")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu2Name;
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu2Name;
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu2Name;
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu2Name;
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu2Name;
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                            }

                            if (cpu2.coin >= 2)
                            {
                                Me.coin += 2;
                                cpu2.coin -= 2;
                                cointxt[0].text = Me.coin.ToString();
                                cointxt[2].text = cpu2.coin.ToString();
                            }
                            else
                            {
                                Me.coin += cpu2.coin;
                                cpu2.coin = 0;
                                cointxt[0].text = Me.coin.ToString();
                                cointxt[2].text = cpu2.coin.ToString();
                            }
                            endgame--;
                        }
                        else
                        {
                            // Done
                        }

                    }
                    else
                    {

                        if (cpu2.coin >= 2)
                        {
                            Me.coin += 2;
                            cpu2.coin -= 2;
                            cointxt[0].text = Me.coin.ToString();
                            cointxt[2].text = cpu2.coin.ToString();
                        }
                        else
                        {
                            Me.coin += cpu2.coin;
                            cpu2.coin = 0;
                            cointxt[0].text = Me.coin.ToString();
                            cointxt[2].text = cpu2.coin.ToString();
                        }
                    }
                }
            }
            else if (whoPolitic == 3)
            {
                if (cpu3.card1 == 5 || cpu3.card2 == 5)
                {

                    announcer.text = " ﻡﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;


                    chalesh.SetActive(true);
                    yield return new WaitUntil(() => meWait == true);
                    meWait = false;

                    if (mychallange)
                    {
                        announcer.color = Color.red;
                        announcer.text = "ﯼﺩﺭﻮﺧ ﺖﺴﮑﺷ";
                        yield return new WaitForSeconds(1.5f);
                        announcer.text = "";
                        announcer.color = Color.black;

                        losingy();
                        yield return new WaitUntil(() => losingClick == true);
                        losingClick = false;

                    }
                    else
                    {
                        // Done
                    }
                }
                else
                {
                    int ran = Random.Range(1, 5);

                    if (ran == 1)
                    {
                        print("BLOF");

                        announcer.text = " ﻡﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;


                        chalesh.SetActive(true);
                        yield return new WaitUntil(() => meWait == true);
                        meWait = false;

                        if (mychallange)
                        {
                            announcer.color = Color.green;
                            announcer.text = "ﺪﯾﺪﺷ ﺶﻟﺎﭼ ﻩﺪﻧﺮﺑ";
                            yield return new WaitForSeconds(1.5f);
                            announcer.text = "";
                            announcer.color = Color.black;

                            int ran2;
                            do
                            {
                                ran2 = Random.Range(1, 3);
                            } while ((ran2 == 1 && cpu3.card1 == -1) || (ran2 == 2 && cpu3.card2 == -1));

                            if (ran2 == 1)
                            {
                                int box = cpu3.card1;
                                cpu3.card1 = -1;
                                cpu3cards[0].SetActive(false);

                                if (box == 1)
                                {
                                    if (mali == "banker")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                            }
                            else if (ran2 == 2)
                            {
                                int box = cpu3.card2;
                                cpu3.card2 = -1;
                                cpu3cards[1].SetActive(false);
                                if (box == 1)
                                {
                                    if (mali == "banker")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﮑﻧﺎﺑ " + name_script.cpu3Name;
                                }
                                else if (box == 2)
                                {
                                    if (ertebat == "director")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﻥﺍﺩﺮﮔﺭﺎﮐ " + name_script.cpu3Name;
                                }
                                else if (box == 3)
                                {
                                    if (attack == "cherik")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﮏﯾﺮﭼ " + name_script.cpu3Name;
                                }
                                else if (box == 4)
                                {
                                    if (uniqe4 == "solh")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺐﻠﻃ ﺢﻠﺻ " + name_script.cpu3Name;
                                }
                                else if (box == 5)
                                {
                                    if (uniqe5 == "siasat")
                                        announcer.text = " ﺪﻧﺍﺯﻮﺳ ﺍﺭ ﺭﺍﺪﻤﺘﺳﺎﯿﺳ " + name_script.cpu3Name;
                                }

                                for (int i = 0; i < lost.Length; i++)
                                {
                                    if (lost[i] == -1)
                                    {
                                        lost[i] = box;
                                        break;
                                    }
                                }
                                printLost();
                                yield return new WaitForSeconds(2);
                            }



                            if (cpu3.coin >= 2)
                            {
                                Me.coin += 2;
                                cpu3.coin -= 2;
                                cointxt[0].text = Me.coin.ToString();
                                cointxt[3].text = cpu3.coin.ToString();
                            }
                            else
                            {
                                Me.coin += cpu3.coin;
                                cpu3.coin = 0;
                                cointxt[0].text = Me.coin.ToString();
                                cointxt[3].text = cpu3.coin.ToString();
                            }
                            endgame--;
                        }
                        else
                        {
                            // Done
                        }

                    }
                    else
                    {


                        if (cpu3.coin >= 2)
                        {
                            Me.coin += 2;
                            cpu3.coin -= 2;
                            cointxt[0].text = Me.coin.ToString();
                            cointxt[3].text = cpu3.coin.ToString();
                        }
                        else
                        {
                            Me.coin += cpu3.coin;
                            cpu3.coin = 0;
                            cointxt[0].text = Me.coin.ToString();
                            cointxt[3].text = cpu3.coin.ToString();
                        }

                    }
                }
            }

        }
        cClicked = true;
    }

    public void selectPolitic(int num)
    {
        whoPolitic = num;
        cClicked = true;
    }

    public void selectAttack(int num)
    {
        whoAttacked = num;
        meWait = true;
    }

    void midIconCheck(int role1, int role2)
    {
        for (int i = 0; i < 4; i++)
        {
            midFiledCards[i].SetActive(true);
        }

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
        }
        else if (Me.card2 == -1)
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

    public void selectErtebat(int whichCard)
    {
        ertebatClick++;
        if (Firstchoice == -1)
        {
            Firstchoice = whichCard;
        }
        else if (secondchoice == -1)
        {
            secondchoice = whichCard;
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
            formula *= 300;
            formula = 100 - formula;
            formula /= 3;
            Debug.Log(formula);
            int go = Random.Range(1, 101);
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
