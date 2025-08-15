using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daily : MonoBehaviour
{
// Start is called before the first frame update
    
   public int lastDate;

   public int Day_1;
   public GameObject OFF_1;
   public GameObject Active_1;
   public GameObject Check_1;

   public int Day_2;
   public GameObject OFF_2;
   public GameObject Active_2;
   public GameObject Check_2;

   public int Day_3;
   public GameObject OFF_3;
   public GameObject Active_3;
   public GameObject Check_3;

   public int Day_4;
   public GameObject OFF_4;
   public GameObject Active_4;
   public GameObject Check_4;

   public int Day_5;
   public GameObject OFF_5;
   public GameObject Active_5;
   public GameObject Check_5;

   public int Day_6;
   public GameObject OFF_6;
   public GameObject Active_6;
   public GameObject Check_6;

   public int Day_7;
   public GameObject OFF_7;
   public GameObject Active_7;
   public GameObject Check_7;

void Start()
    {
    
        Day_1 =  PlayerPrefs.GetInt("Day1");
        Day_2 =  PlayerPrefs.GetInt("Day2");
        Day_3 =  PlayerPrefs.GetInt("Day3");
        Day_4 =  PlayerPrefs.GetInt("Day4");
        Day_5 =  PlayerPrefs.GetInt("Day5");
        Day_6 =  PlayerPrefs.GetInt("Day6");
        Day_7 =  PlayerPrefs.GetInt("Day7");
        
        lastDate = PlayerPrefs.GetInt("LastDate");

        Reward();

    if(lastDate != System.DateTime.Now.Day){
        if(Day_1 == 0){
            Day_1 = 1;
        }
        else if(Day_2 == 0){
           Day_2 = 1;
        }else if(Day_3 == 0){
           Day_3 = 1;
        }else if(Day_4 == 0){
           Day_4 = 1;
        }else if(Day_5 == 0){
           Day_5 = 1;
        }else if(Day_6 == 0){
           Day_6 = 1;
        }else if(Day_7 == 0){
           Day_7 = 1;
        }

        Reward();
    }

    }


   public void Reward(){
   
   if(Day_1 == 0){
    OFF_1.SetActive(true);
    Active_1.SetActive(false);
    Check_1.SetActive(false);
   }
   if(Day_1 == 1){
    Active_1.SetActive(true);
    OFF_1.SetActive(false);
    Check_1.SetActive(false);
   }
   if(Day_1 == 2){
    Check_1.SetActive(true);
    Active_1.SetActive(false);
    OFF_1.SetActive(false);
   }

   if(Day_2 == 0){
    OFF_2.SetActive(true);
    Active_2.SetActive(false);
    Check_2.SetActive(false);
   }
   if(Day_2 == 1){
    Active_2.SetActive(true);
    OFF_2.SetActive(false);
    Check_2.SetActive(false);
   }
   if(Day_2 == 2){
    Check_2.SetActive(true);
    OFF_2.SetActive(false);
    Active_2.SetActive(false);
   }

   if(Day_3 == 0){
    OFF_3.SetActive(true);
    Active_3.SetActive(false);
    Check_3.SetActive(false);
   }
   if(Day_3 == 1){
    Active_3.SetActive(true);
    OFF_3.SetActive(false);
    Check_3.SetActive(false);
   }
   if(Day_3 == 2){
    Check_3.SetActive(true);
    OFF_3.SetActive(false);
    Active_3.SetActive(false);
   }

   if(Day_4 == 0){
    OFF_4.SetActive(true);
    Active_4.SetActive(false);
    Check_4.SetActive(false);
   }
   if(Day_4 == 1){
    Active_4.SetActive(true);
    OFF_4.SetActive(false);
    Check_4.SetActive(false);
   }
   if(Day_4 == 2){
    Check_4.SetActive(true);
    OFF_4.SetActive(false);
    Active_4.SetActive(false);
   }



   if(Day_5 == 0){
    OFF_5.SetActive(true);
    Active_5.SetActive(false);
    Check_5.SetActive(false);
   }
   if(Day_5 == 1){
    Active_5.SetActive(true);
    OFF_5.SetActive(false);
    Check_5.SetActive(false);
   }
   if(Day_5 == 2){
    Check_5.SetActive(true);
    OFF_5.SetActive(false);
    Active_5.SetActive(false);
   }

   if(Day_6 == 0){
    OFF_6.SetActive(true);
    Active_6.SetActive(false);
    Check_6.SetActive(false);
   }
   if(Day_6 == 1){
    Active_6.SetActive(true);
    OFF_6.SetActive(false);
    Check_6.SetActive(false);
   }
   if(Day_6 == 2){
    Check_6.SetActive(true);
    OFF_6.SetActive(false);
    Active_6.SetActive(false);
   }

   if(Day_7 == 0){
    OFF_7.SetActive(true);
    Active_7.SetActive(false);
    Check_7.SetActive(false);
   }
   if(Day_7 == 1){
    Active_7.SetActive(true);
    OFF_7.SetActive(false);
    Check_7.SetActive(false);
   }
   if(Day_7 == 2){
    Check_7.SetActive(true);
    OFF_7.SetActive(false);
    Active_7.SetActive(false);
   }

   }

   public void GetReward1(){
    lastDate = System.DateTime.Now.Day;
    PlayerPrefs.SetInt("LastDate" , lastDate);

   int coin = PlayerPrefs.GetInt("Coin");
   coin += 400;
   PlayerPrefs.SetInt("Coin",coin);

    Day_1 = 2;
    PlayerPrefs.SetInt("Day1",2);

    Reward();
   }

    public void GetReward2(){
    lastDate = System.DateTime.Now.Day;
    PlayerPrefs.SetInt("LastDate" , lastDate);

   int Diamond = PlayerPrefs.GetInt("Diamond");
   Diamond += 3;
   PlayerPrefs.SetInt("Diamond",Diamond);

    Day_2 = 2;
    PlayerPrefs.SetInt("Day2",2);

    Reward();
   }

    public void GetReward3(){
    lastDate = System.DateTime.Now.Day;
    PlayerPrefs.SetInt("LastDate" , lastDate);

    int lvlpotion = PlayerPrefs.GetInt("LVLPotion");
    lvlpotion++;
    PlayerPrefs.SetInt("LVLPotion", lvlpotion);

    Day_3 = 2;
    PlayerPrefs.SetInt("Day3",2);

    Reward();
   }
   
    public void GetReward4(){
    lastDate = System.DateTime.Now.Day;
    PlayerPrefs.SetInt("LastDate" , lastDate);

   int coin = PlayerPrefs.GetInt("Coin");
   coin += 1000;
   PlayerPrefs.SetInt("Coin",coin);

    Day_4 = 2;
    PlayerPrefs.SetInt("Day4",2);

    Reward();
   }

    public void GetReward5(){
    lastDate = System.DateTime.Now.Day;
    PlayerPrefs.SetInt("LastDate" , lastDate);

   int Diamond = PlayerPrefs.GetInt("Diamond");
   Diamond += 5;
   PlayerPrefs.SetInt("Diamond",Diamond);

    Day_5 = 2;
    PlayerPrefs.SetInt("Day5",2);

    Reward();
   }

    public void GetReward6(){
    lastDate = System.DateTime.Now.Day;
    PlayerPrefs.SetInt("LastDate" , lastDate);

   int coin = PlayerPrefs.GetInt("Coin");
   coin += 500;
   PlayerPrefs.SetInt("Coin",coin);

    Day_6 = 2;
    PlayerPrefs.SetInt("Day6",2);

    Reward();
   }

    public void GetReward7(){
    lastDate = System.DateTime.Now.Day;
    PlayerPrefs.SetInt("LastDate" , lastDate);

   int Diamond = PlayerPrefs.GetInt("Diamond");
   Diamond += 10;
   PlayerPrefs.SetInt("Diamond",Diamond);

    Day_7 = 2;
    PlayerPrefs.SetInt("Day7",2);

    Reward();
   }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
