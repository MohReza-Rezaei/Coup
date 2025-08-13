using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text trophyTxt , score , nickName , winTxt , loseTxt , NumberPlayTxt , MostTrophyTxt , NumberCardTxt , leageTxt , coinPotion;
    public Text[] Name = new Text[2];
    public TextMeshProUGUI cointxt, diamondtxt;
    public Slider MusicMenuSlide;
    public GameObject MusicMenu;
    int coin;
    int trophy;
    int diamond;
      public Image profilePic;
      public Sprite[] profileList = new Sprite[7];
      public GameObject CoinPotionOff;
      // Start is called before the first frame update
      void Start()
      {
            Name[0].text = PlayerPrefs.GetString("NAME");
            Name[1].text = PlayerPrefs.GetString("NAME");
            coinPotion.text = PlayerPrefs.GetInt("CoinPotion").ToString();
            coin = PlayerPrefs.GetInt("Coin");
            cointxt.text = coin.ToString();
            diamond = PlayerPrefs.GetInt("Diamond");
            diamondtxt.text = diamond.ToString();
            trophy = PlayerPrefs.GetInt("Trophy");
            trophyTxt.text = trophy.ToString();
            score.text = trophy.ToString();
            if (trophy <= 400)
            {
                  nickName.text = "ﻦﯿﭼﺮﺒﺧ";
            }
            else if (trophy > 400 && trophy <= 800)
            {
                  nickName.text = "ﻭﺩﺎﭘ";
            }
            else if (trophy > 800 && trophy <= 1200)
            {
                  nickName.text = "ﯽﺷﺭﻮﺷ";
            }
            else if (trophy > 1200 && trophy <= 1600)
            {
                  nickName.text = "ﻦﺋﺎﺧ ﺪﻤﺘﻌﻣ";
            }
            else if (trophy > 1600 && trophy <= 2000)
            {
                  nickName.text = "ﺮﮑﻔﺘﻣ ﺰﻐﻣ";
            }
            else if (trophy > 2000 && trophy <= 2300)
            {
                  nickName.text = "ﺮﮕﺑﻮﺷﺁ";
            }
            else if (trophy > 2300 && trophy <= 2600)
            {
                  nickName.text = "ﺎﺗﺩﻮﮐ ﺭﺎﻤﻌﻣ";
            }
            else if (trophy > 2600 && trophy <= 2900)
            {
                  nickName.text = "ﻩﺎﯿﺳ ﻢﻈﻋﺍﺭﺪﺻ";
            }
            else if (trophy > 2900)
            {
                  nickName.text = "ﻩﺎﺷﺩﺎﭘ";
            }

            winTxt.text = PlayerPrefs.GetInt("Win").ToString();
            loseTxt.text = PlayerPrefs.GetInt("Lose").ToString();
            MostTrophyTxt.text = PlayerPrefs.GetInt("TrophyMax").ToString();
            NumberPlayTxt.text = PlayerPrefs.GetInt("Play").ToString();
            NumberCardTxt.text = PlayerPrefs.GetInt("Card").ToString() + " / 25";

            if (trophy <= 1200)
            {
                  leageTxt.text = "ﯼﺪﺘﺒﻣ";
            }
            else if (trophy > 1200 && trophy <= 2300)
            {
                  leageTxt.text = "ﻂﺳﻮﺘﻣ";
            }
            else if (trophy > 2300)
            {
                  leageTxt.text = "ﻪﺘﻓﺮﺸﯿﭘ";
            }

            MusicMenuSlide.value = MusicMenu.GetComponent<AudioSource>().volume;

            for (int i = 0; i < 7; i++)
            {
                  int num = PlayerPrefs.GetInt("pic" + (i + 1));
                  if (num == 2)
                  {
                        profilePic.sprite = profileList[i];
                        break;
                  }
            }

            // coin Potion Check
            int coinpotion_have = PlayerPrefs.GetInt("CoinPotionInUse");
            if (coinpotion_have == 1)
            {
                  int coinpotion_time = PlayerPrefs.GetInt("CoinPotionTime");
                  int now = System.DateTime.Now.Day;

                  if ((now == 1 || now == 2) && (coinpotion_time != 1 || coinpotion_time != 2))
                        now += 30;

                  if (now >= coinpotion_time + 2)
                  {
                        PlayerPrefs.SetInt("CoinPotionTime", 0);
                        PlayerPrefs.SetInt("CoinPotionInUse", 0);
                        CoinPotionOff.SetActive(false);
                  }
                  else
                  {
                        CoinPotionOff.SetActive(true);
                  }
            }

    }

    // Update is called once per frame
    void Update()
    {
        MusicMenu.GetComponent<AudioSource>().volume = MusicMenuSlide.value;
    }

    void Enter()
    {
        if (coin >= 100)
        {
            coin -= 100;

            // load game scene
        }
    }
}