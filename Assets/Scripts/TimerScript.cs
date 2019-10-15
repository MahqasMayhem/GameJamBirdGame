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
    // Start is called before the first frame update
    private void Start()
    {
        timer = gameObject.GetComponent<Text>();
        missionFailed = GameObject.Find("UI_MissionFail");
        missionFailed.SetActive(false);
        timer.text = ("00:"+(int)minutes+":"+(int)seconds);
    }

    // Update is called once per frame
    void Update()
    {
        seconds -= Time.deltaTime;
        if (seconds>=1 && !missionFailed.activeSelf)
        {
            timer.text = ("00:" + (int)minutes + ":" + (int)seconds);
        }
        else if (seconds <= 0 && minutes >= 1 && !missionFailed.activeSelf)
        {
            minutes -= 1;
            seconds = 59d;
            timer.text = ("00:" + (int)minutes + ":" + (int)seconds);
        }
        else if (seconds <= 0 && !missionFailed.activeSelf)
        {
            missionFailed.SetActive(true);
            timer.text = ("00:00:00");
        }
        else if (Input.anyKeyDown)
        {
            
        }
        if (!missionFailed.activeSelf) timer.text = ("00:" + (int)minutes + ":" + (int)seconds);
    }

}
