using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text trophyTxt , score , nickName , winTxt , loseTxt , NumberPlayTxt , MostTrophyTxt , NumberCardTxt , leageTxt , coinPotion;
    public Text lvlPotion, operatorPotion, infinityPotion;
    public Text[] Name = new Text[2];
    public TextMeshProUGUI cointxt, diamondtxt;
    public Slider MusicMenuSlide;
    public GameObject MusicMenu ,MusicClick , MusicClick2;
    int coin;
    int trophy;
    int diamond;
      public Image[] profilePic =new Image[2];
      public Sprite[] profileList = new Sprite[7];
      public GameObject CoinPotionOff, LVLPotionOff, infinityPotionOff;
      // Start is called before the first frame update
      void Start()
      {
            Name[0].text = PlayerPrefs.GetString("NAME");
            Name[1].text = PlayerPrefs.GetString("NAME");
            coinPotion.text = PlayerPrefs.GetInt("CoinPotion").ToString();
            lvlPotion.text = PlayerPrefs.GetInt("LVLPotion").ToString();
            operatorPotion.text = PlayerPrefs.GetInt("OperatorPotion").ToString();
            infinityPotion.text = PlayerPrefs.GetInt("InfinityPotion").ToString();
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
                        profilePic[0].sprite = profileList[i];
                        profilePic[1].sprite = profileList[i];
                        break;
                  }
            }

            int check_click_muisc = PlayerPrefs.GetInt("ClickMusic");
            if (check_click_muisc == 0)
            {
                  MusicClick.SetActive(false);
                  MusicClick2.SetActive(false); 
            }

            // coin Potion Check
            int howMany_coinPotion = PlayerPrefs.GetInt("CoinPotion");
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
            else
            {
                  if (howMany_coinPotion > 0)
                        CoinPotionOff.SetActive(false);
                  else
                        CoinPotionOff.SetActive(true);
            }

            // lvl potion check
            int howMany_LVLPotion = PlayerPrefs.GetInt("LVLPotion");
            int LVLPotion_have = PlayerPrefs.GetInt("LVLPotionInUse");
            if (LVLPotion_have == 1)
            {
                  int LVLPotion_time = PlayerPrefs.GetInt("LVLPotionTime");
                  int now = System.DateTime.Now.Day;

                  if ((now == 1 || now == 2) && (LVLPotion_time != 1 || LVLPotion_time != 2))
                        now += 30;

                  if (now >= LVLPotion_time + 2)
                  {
                        PlayerPrefs.SetInt("LVLPotionTime", 0);
                        PlayerPrefs.SetInt("LVLPotionInUse", 0);
                        LVLPotionOff.SetActive(false);
                  }
                  else
                  {
                        LVLPotionOff.SetActive(true);
                  }
            }
            else
            {
                  if (howMany_LVLPotion > 0)
                        LVLPotionOff.SetActive(false);
                  else
                        LVLPotionOff.SetActive(true);
            }
            // infinity potion check
            int howMany_InfinityPotion = PlayerPrefs.GetInt("InfinityPotion");
            int infinity_have = PlayerPrefs.GetInt("InfinityPotionInUse");
            if (infinity_have == 1)
            {
                  int infinity_time = PlayerPrefs.GetInt("InfinityPotionTime");
                  int now = System.DateTime.Now.Day;

                  if ((now == 1 || now == 2) && (infinity_have != 1 || infinity_have != 2))
                        now += 30;

                  if (now >= infinity_have + 1)
                  {
                        PlayerPrefs.SetInt("InfinityPotionTime", 0);
                        PlayerPrefs.SetInt("InfinityPotionInUse", 0);
                        infinityPotionOff.SetActive(false);
                  }
                  else
                  {
                        infinityPotionOff.SetActive(true);
                  }
            }
            else
            {
                  if (howMany_InfinityPotion > 0)
                        infinityPotionOff.SetActive(false);
                  else
                        infinityPotionOff.SetActive(true);
            }
    }

    // Update is called once per frame
    void Update()
    {
        MusicMenu.GetComponent<AudioSource>().volume = MusicMenuSlide.value;
    }

    public void OffMusic(){
      PlayerPrefs.SetInt("ClickMusic",0);
    }
    public void OnMusic(){
      PlayerPrefs.SetInt("ClickMusic",1);
    }
}