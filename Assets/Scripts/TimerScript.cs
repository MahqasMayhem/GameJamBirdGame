using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
   
{
    
    public GameObject missionFailed;
    private Text timer;
    public double seconds = 59d, minutes = 14d;
    private bool failed = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.GetComponent<Text>();
        //missionFailed = GameObject.Find("UI_MissionFail");
        missionFailed.GetComponent<CanvasRenderer>().SetAlpha(0);
        timer.text = ("00:"+(int)minutes+":"+(int)seconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (!failed) UpdateTimer();
        else
        {
            timer.text = ("00:00:00");
            missionFailed.SetActive(true);
        }
    }
    private void UpdateTimer()
    {
        seconds -= Time.deltaTime;
        if (seconds >= 1)
        {
            timer.text = ("00:" + (int)minutes + ":" + (int)seconds);
        }
        else if (seconds <= 0 && minutes >= 1)
        {
            minutes -= 1;
            seconds = 59d;
            timer.text = ("00:" + (int)minutes + ":" + (int)seconds);
        }
        else if (seconds <= 0)
        {
            missionFailed.GetComponent<CanvasRenderer>().SetAlpha(100);
            failed = true;
            timer.text = ("00:00:00");
        }
        else if (Input.anyKeyDown)
        {

        }
        else timer.text = ("00:" + (int)minutes + ":" + (int)seconds);
    }
}
