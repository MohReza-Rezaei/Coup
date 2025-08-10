using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    public Game game_script;
    public Text title ,  trophyText , coinText;
    public GameObject[] Ribbon = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        Point();  
    }

    void Point()
    {
        Time.timeScale = 0;
        print("yesssssss!");
        if (game_script.Me.Alive)
        {  // win
            Ribbon[0].SetActive(true);
            Ribbon[1].SetActive(true);
            title.text = "ﻥﺎﻣﺮﻬﻗ";
            int trophy = PlayerPrefs.GetInt("Trophy");
            int add = Random.Range(15, 25);
            trophy += add;
            trophyText.text = "+ " + add;
            PlayerPrefs.SetInt("Trophy", trophy);

            int win = PlayerPrefs.GetInt("Win");
            win++;
            PlayerPrefs.SetInt("Win", win);

            coinText.text = "+ 100";
            // win coin code

            int TheHighestTrophy = PlayerPrefs.GetInt("HighestTrophy");
            if (trophy > TheHighestTrophy)
                PlayerPrefs.SetInt("HighestTrophy", trophy);

        }
        else
        {  // lose
            Ribbon[0].SetActive(false);
            Ribbon[1].SetActive(false);
            title.text = "ﺖﺴﮑﺷ";
            int trophy = PlayerPrefs.GetInt("Trophy");
            int minus = Random.Range(15, 25);
            trophy -= minus;
            trophyText.text = "- " + minus;
            if (trophy <= 0)
                PlayerPrefs.SetInt("Trophy", 0);
            else
                PlayerPrefs.SetInt("Trophy", trophy);

            int lose = PlayerPrefs.GetInt("Lose");
            lose++;
            PlayerPrefs.SetInt("lose", lose);

            coinText.text = "0";
        }

        int playTimes = PlayerPrefs.GetInt("PlayTimes");
        playTimes++;
        PlayerPrefs.SetInt("PlayTimes",playTimes);
        
    }
}
