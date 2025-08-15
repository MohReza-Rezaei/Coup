using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Telegram : MonoBehaviour
{
    bool firstEnter = false;
    public GameObject coinIcon;
    public TextMeshProUGUI coinText;
    int check;
    void Start()
    {
         check = PlayerPrefs.GetInt("TelegramEnter");
        if (check == 1)
        {
            coinIcon.SetActive(false);
            coinText.text = "";
        }
    }

    public void tele()
    {
        if (check == 0) {
            PlayerPrefs.SetInt("TelegramEnter", 1);
            int coin = PlayerPrefs.GetInt("Coin");
            coin += 300;
            PlayerPrefs.SetInt("Coin", coin);    
        }  

        Application.OpenURL("https://t.me/Hogwarts_games");
    }
}
