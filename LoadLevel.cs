using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string lvlname = "Menu";

    public void GetlvlName(string name)
    {
        lvlname = name;
    }

    public void Doload()
    {
        if (lvlname == "Game")
        {
            int coin = PlayerPrefs.GetInt("Coin");
            if (coin >= 100 || PlayerPrefs.GetInt("InfinityPotionInUse") == 1)
            {
                if (PlayerPrefs.GetInt("InfinityPotionInUse") == 1)
                {
                    SceneManager.LoadScene("Game");
                }
                else
                {
                    coin -= 100;
                    PlayerPrefs.SetInt("Coin", coin);
                    SceneManager.LoadScene("Game");
                }
             
                
            }
            
        }
        else if (lvlname == "Menu")
        {
             SceneManager.LoadScene("Menu");
        }
    }



}
