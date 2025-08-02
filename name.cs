using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class name : MonoBehaviour
{

    string[] nameList = { "ﺎﻤﺳﺍ", "ﻡﺍﺭﺁ", "ﺭﺎﻬﺑ", "ﺎﻤﻠﺣ", "ﺎﯾﺭﺩ", "ﺍﺭﺎﺳ", "ﺮﯿﻣﺍ", "ﻦﯿﻣﺍ", "ﺎﺗﺭﺁ", "ﺎﯾﻮﭘ", "ﺪﻤﺤﻣ", "ﻡﺎﺴﺣ", "ﻩﺎﻨﭘ", "ﺎﺴﻟﺩ", "ﻪﻬﻟﺍ", "ﻢﺗﺎﺣ", "ﻦﯿﻌﻣ", "ﻦﯿﺘﻣ", "ﺎﻫﺭ", "ﻞﺴﻋ", "ﻝﺰﻏ", "ﯽﻠﻋ", "ﺎﺿﺭ", "ﯼﺪﻬﻣ", "ﺎﺴﻬﻣ", "ﺭﺎﮕﻧ", "ﺪﯿﻤﺣ","ﻢﯾﺮﻣ","ﻑﺭﺎﻋ","ﺪﯾﻮﻧ"};
    public Text[]  nametext = new Text[3];
    public string cpu1Name, cpu2Name, cpu3Name;
    // Start is called before the first frame update
    void Start()
    {
        int num1 = Random.Range(0, 30);

        int num2;
        do
        {
            num2 = Random.Range(0, 30);
        } while (num2 == num1);


        int num3;
        do
        {
            num3 = Random.Range(0, 30);
        } while ((num3 == num1) || (num3 == num2));


        nametext[0].text = nameList[num1];
        nametext[1].text = nameList[num2];
        nametext[2].text = nameList[num3];

        cpu1Name = nameList[num1];
        cpu2Name = nameList[num2];
        cpu3Name = nameList[num3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
