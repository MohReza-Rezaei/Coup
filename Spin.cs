using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spin : MonoBehaviour
{
    public GameObject matn;
    public TextMeshProUGUI winText;
    public Image winIcon;
    public Sprite coinIcon , diamondIcon;
    public GameObject winScreen;
    public GameObject off;
    public int lastDate;
    public float rotatePower;
    public float stopPower;

    public int permision;

    private Rigidbody2D rbody;
    int inRotate;
    // Start is called before the first frame update
    void Start()
    {
        lastDate = PlayerPrefs.GetInt("DAY");
        rbody = GetComponent<Rigidbody2D>();
        rotatePower = Random.Range(2500, 5001);
        stopPower = Random.Range(200, 501);
        print(rotatePower);
        print(stopPower);
        if (lastDate != System.DateTime.Now.Day)
        {
            permision = 1;
            off.SetActive(false);
            matn.SetActive(false);
        }
    }

    float t;
    // Update is called once per frame
    void Update()
    {

        if (rbody.angularVelocity > 0)
        {
            rbody.angularVelocity -= stopPower * Time.deltaTime;
            rbody.angularVelocity = Mathf.Clamp(rbody.angularVelocity, 0, 1440);
        }

        if (rbody.angularVelocity == 0 && inRotate == 1)
        {
            t += 1 * Time.deltaTime;

            if (t >= 0.5f)
            {
                GetReward();

                inRotate = 0;
                t = 0;
            }

        }

       
        

    }


    IEnumerator deleteWin()
    {
        yield return new WaitForSeconds(5f);

        winScreen.SetActive(false);
    }

    public void Rotate()
    {
        if (inRotate == 0 && permision == 1)
        {
            rbody.AddTorque(rotatePower);
            inRotate = 1;

            lastDate = System.DateTime.Now.Day;
            PlayerPrefs.SetInt("DAY", lastDate);
            permision = 0;
            off.SetActive(true);
            matn.SetActive(true);
        }

    }

    public void GetReward(){
        float rot = transform.eulerAngles.z;

        if(rot > 0 && rot <= 45){
     GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,45);       
     win(25,1);
        }else if(rot > 45 && rot <= 90){
    GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,90);  
     win(500,1);
        }else if(rot > 90 && rot <= 135){
     GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,135);         
     win(25,1);
        }else if(rot > 135 && rot <= 180){
     GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,180);         
     win(2,2);
        }else if(rot >180 && rot <= 225){
     GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,225);         
     win(50,1);
        }else if(rot > 225 && rot <= 270){
     GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,270);         
     win(10,2);
        }else if(rot > 270 && rot <= 315){
     GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,315);         
     win(100,1);
        }else if(rot > 315 && rot <= 360){
     GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,0);         
     win(1,2);
        }
    }

    public void win(int score, int which)
    {
      // print(score);
        winScreen.SetActive(true);
        winText.text = "+" + score.ToString();
        StartCoroutine(deleteWin());
        if (which == 1)
        {
            winIcon.sprite = coinIcon;
        }
        else
        {
            winIcon.sprite = diamondIcon;
        }
    }

}
