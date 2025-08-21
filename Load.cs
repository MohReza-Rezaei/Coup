using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class Load : MonoBehaviour
{
    public LoadLevel script_loadLevel;
    public Animator animator;
    public Text text;
    public Slider slider;
    float num;
    int check;
    bool permision;
    public Canvas user;
    public TMP_InputField input;
    // Start is called before the first frame update
    void Start()
    {
       
        check = PlayerPrefs.GetInt("check");
        if (check == 0)
        {
            permision = true;
        }
        else
        {
            permision = false;
        }
    }
    int count = 1;
    // Update is called once per frame
    void Update()
    {

        if (slider.value != 1)
        {
            slider.value += 0.25f * Time.deltaTime;
            count++;
            num = slider.value * 100;
            if (count == 35)
            {
                text.text = "20 / 100";
            }
            else if (count == 65)
            {
                text.text = "40 / 100";
            }
            else if (count == 115)
            {
                text.text = "60 / 100";
            }
            else if (count == 165)
            {
                text.text = "80 / 100";
            }
            else if (count == 165)
            {
                text.text = "100 / 100";
            }

        }
        else
        {text.text = "100 / 100";
            if (permision)
            {
                signIn();
            }
            else
            {
            script_loadLevel.GetlvlName("Menu");
            animator.SetTrigger("Doload");
            }

        }


    }


    public void signIn()
    {
        PlayerPrefs.SetInt("pic1", 2);
        PlayerPrefs.SetInt("pic5", 1);
        PlayerPrefs.SetInt("Card", 5);
        PlayerPrefs.SetInt("Coin", 400);
        PlayerPrefs.SetInt("Diamond", 8);
        user.gameObject.SetActive(true);
    }

    public void saveName()
    {
        print(input.text);
        if (IsEnglishOnly(input.text) && input.text.Length <= 8)
        {
            PlayerPrefs.SetString("NAME", input.text);
            PlayerPrefs.SetInt("check", 1);
            user.gameObject.SetActive(false);
            script_loadLevel.GetlvlName("Menu");
            animator.SetTrigger("Doload");
        }
        else
        {
            input.text = "";
        }
        
    }

   

public bool IsEnglishOnly(string input)
{
    return Regex.IsMatch(input, @"^[a-zA-Z\s]+$");
}


}
