using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    int coin;
    int diamond;
    public int[] pic = new int[7];
    public Button[] picbut;
    public Text[] pictxt;
    public Image[] profilePic = new Image[2];
    public Sprite[] profileList = new Sprite[7];
    // Start is called before the first frame update
    void Start()
    {
        
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
            }else if (pic[i] == 2)
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
}
