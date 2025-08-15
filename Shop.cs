using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    int coin;
    int diamond;

    public Text Cointxt, Diamondtxt;
    // pic
    public int[] pic = new int[7];
    public Button[] picbut;
    public Text[] pictxt;
    public Image[] profilePic = new Image[2];
    public Sprite[] profileList = new Sprite[7];

    ///coin potion

    public Button coin_potion_btn;
    public Text coin_potion_text;
    public Color coin_potion_color;
    public Text coin_potion_status_text;
    public GameObject coin_potion_off;

    //lvl potion

    public Button lvl_potion_btn;
    public Text lvl_potion_text;
    public Text lvl_potion_status_text;
    public GameObject lvl_potion_off;

    //operator potion

    public Button Operator_potion_btn;
    public Text Operator_potion_text;
    public Text Operator_potion_status_text;
    public GameObject Operator_potion_off;

    //infinity potion

    public Button Infinity_potion_btn;
    public Text Infinity_potion_text;
    public Text Infinity_potion_status_text;
    public GameObject Infinity_potion_off;

    // Start is called before the first frame update
    void Start()
    {
        diamond = PlayerPrefs.GetInt("Diamond");

        pic[0] = PlayerPrefs.GetInt("pic1");
        pic[1] = PlayerPrefs.GetInt("pic2");
        pic[2] = PlayerPrefs.GetInt("pic3");
        pic[3] = PlayerPrefs.GetInt("pic4");
        pic[4] = PlayerPrefs.GetInt("pic5");
        pic[5] = PlayerPrefs.GetInt("pic6");
        pic[6] = PlayerPrefs.GetInt("pic7");
        for (int i = 0; i < pic.Length; i++)
        {
            if (pic[i] == 0)
            {
                picbut[i].GetComponent<Image>().color = Color.blue;
                pictxt[i].text = "ﺪﯾﺮﺧ";
            }
            else if (pic[i] == 1)
            {
                picbut[i].GetComponent<Image>().color = Color.yellow;
                pictxt[i].text = "ﺏﺎﺨﺘﻧﺍ";
            }
            else if (pic[i] == 2)
            {
                picbut[i].GetComponent<Image>().color = Color.green;
                pictxt[i].text = "ﺪﺷ ﺏﺎﺨﺘﻧﺍ";
            }
        }

        coin = PlayerPrefs.GetInt("Coin");
        diamond = PlayerPrefs.GetInt("Diamond");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PicSelect(int x)
    {
        if (pic[x] == 1)
        {
            for (int i = 0; i < pic.Length; i++)
            {
                if (pic[i] == 2)
                {
                    pic[i] = 1;
                    picbut[i].GetComponent<Image>().color = Color.yellow;
                    pictxt[i].text = "ﺏﺎﺨﺘﻧﺍ";
                    PlayerPrefs.SetInt("pic" + (i + 1), pic[i]);
                }

            }
            pic[x] = 2;
            picbut[x].GetComponent<Image>().color = Color.green;
            pictxt[x].text = "ﺪﺷ ﺏﺎﺨﺘﻧﺍ";
            PlayerPrefs.SetInt("pic" + (x + 1), pic[x]);
            profilePic[0].sprite = profileList[x];
            profilePic[1].sprite = profileList[x];
        }


    }

    public void CoinPotion()
    {
        if (diamond >= 7)
        {
            StartCoroutine(CoinPotiony());
            diamond -= 7;
            PlayerPrefs.SetInt("Diamond", diamond);
            Diamondtxt.text = diamond.ToString();    
        }

    }

    IEnumerator CoinPotiony()
    {
        int coinpotion = PlayerPrefs.GetInt("CoinPotion");
        coinpotion++;
        coin_potion_status_text.text = coinpotion.ToString();
        PlayerPrefs.SetInt("CoinPotion", coinpotion);
        coin_potion_btn.GetComponent<Image>().color = Color.green;
        coin_potion_text.text = " +1 ";
        yield return new WaitForSeconds(2);
        coin_potion_btn.GetComponent<Image>().color = coin_potion_color;
        coin_potion_text.text = " ﺪﯾﺮﺧ ";
    }

    public void UseCoinPotion()
    {
        coin_potion_off.SetActive(true);
        int coinPotion = PlayerPrefs.GetInt("CoinPotion");
        coinPotion--;
        PlayerPrefs.SetInt("CoinPotion", coinPotion);
        coin_potion_status_text.text = coinPotion.ToString();
        PlayerPrefs.SetInt("CoinPotionTime", System.DateTime.Now.Day);
        PlayerPrefs.SetInt("CoinPotionInUse", 1);
    }


    ///////////////////////////////////////////////////////
    public void LVLPotion()
    {
          if (diamond >= 3)
         {
        StartCoroutine(LVLPotiony());
           diamond -= 3;
        PlayerPrefs.SetInt("Diamond", diamond);
        Diamondtxt.text = diamond.ToString();     
        }

    }

    IEnumerator LVLPotiony()
    {
        int lvlPotion = PlayerPrefs.GetInt("LVLPotion");
        lvlPotion++;
        lvl_potion_status_text.text = lvlPotion.ToString();
        PlayerPrefs.SetInt("LVLPotion", lvlPotion);
        lvl_potion_btn.GetComponent<Image>().color = Color.green;
        lvl_potion_text.text = " +1 ";
        yield return new WaitForSeconds(2);
        lvl_potion_btn.GetComponent<Image>().color = coin_potion_color;
        lvl_potion_text.text = " ﺪﯾﺮﺧ ";
    }

    public void UseLVLPotion()
    {
        lvl_potion_off.SetActive(true);
        int lvlpotion = PlayerPrefs.GetInt("LVLPotion");
        lvlpotion--;
        PlayerPrefs.SetInt("LVLPotion", lvlpotion);
        lvl_potion_status_text.text = lvlpotion.ToString();
        PlayerPrefs.SetInt("LVLPotionTime", System.DateTime.Now.Day);
        PlayerPrefs.SetInt("LVLPotionInUse", 1);
    }
    
    ///////////////////////////////////////////////////////
    public void OperatorPotion()
    {
        if (diamond >= 5)
        {
            StartCoroutine(OperatorPotiony());
            diamond -= 5;
             PlayerPrefs.SetInt("Diamond", diamond);
           Diamondtxt.text = diamond.ToString();   
        }

    }

    IEnumerator OperatorPotiony()
    {
        int operatorPotion = PlayerPrefs.GetInt("OperatorPotion");
        operatorPotion++;
        Operator_potion_status_text.text = operatorPotion.ToString();
        PlayerPrefs.SetInt("OperatorPotion", operatorPotion);
        Operator_potion_btn.GetComponent<Image>().color = Color.green;
        Operator_potion_text.text = " +1 ";
        yield return new WaitForSeconds(2);
        Operator_potion_btn.GetComponent<Image>().color = coin_potion_color;
        Operator_potion_text.text = " ﺪﯾﺮﺧ ";
    }

    public void UseOperatorPotion()
    {
        Operator_potion_off.SetActive(true);
        int operatorPotion = PlayerPrefs.GetInt("OperatorPotion");
        operatorPotion--;
        PlayerPrefs.SetInt("OperatorPotion", operatorPotion);
        Operator_potion_status_text.text = operatorPotion.ToString();
        PlayerPrefs.SetInt("OperatorPotionTime", System.DateTime.Now.Day);
        PlayerPrefs.SetInt("OperatorPotionInUse", 1);
    }
    ///////////////////////////////////////////////////////
    public void InfinityPotion()
    {
        if (diamond >= 15)
        {
            StartCoroutine(InfinityPotiony());
            diamond -= 15;
            PlayerPrefs.SetInt("Diamond", diamond);
            Diamondtxt.text = diamond.ToString(); 
        }

    }

    IEnumerator InfinityPotiony()
    {
        int InfinityPotion = PlayerPrefs.GetInt("InfinityPotion");
        InfinityPotion++;
        Infinity_potion_status_text.text = InfinityPotion.ToString();
        PlayerPrefs.SetInt("InfinityPotion", InfinityPotion);
        Infinity_potion_btn.GetComponent<Image>().color = Color.green;
        Operator_potion_text.text = " +1 ";
        yield return new WaitForSeconds(2);
        Infinity_potion_btn.GetComponent<Image>().color = coin_potion_color;
        Infinity_potion_text.text = " ﺪﯾﺮﺧ ";
    }

    public void UseInfinityPotion()
    {
        Infinity_potion_off.SetActive(true);
        int InfinityPotion = PlayerPrefs.GetInt("InfinityPotion");
        InfinityPotion--;
        PlayerPrefs.SetInt("InfinityPotion", InfinityPotion);
        Infinity_potion_status_text.text = InfinityPotion.ToString();
        PlayerPrefs.SetInt("InfinityPotionTime", System.DateTime.Now.Day);
        PlayerPrefs.SetInt("InfinityPotionInUse", 1);
    }
}
