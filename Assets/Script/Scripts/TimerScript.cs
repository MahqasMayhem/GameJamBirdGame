using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
   
{

    private GameManager gametracker;
    private Text timer;
    public double seconds = 59d, minutes = 14d;
    private bool failed;
    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.GetComponent<Text>();
        gametracker = GameObject.Find("Gametracker").GetComponent<GameManager>();
        timer.text = ("00:"+(int)minutes+":"+(int)seconds);
        failed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!failed) UpdateTimer();
        else
        {
            timer.text = ("00:00:00");
            gametracker.Invoke("LoseGame",0);
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
            
            failed = true;
            timer.text = ("00:00:00");
        }
        else if (Input.anyKeyDown)
        {

        }
        else timer.text = ("00:" + (int)minutes + ":" + (int)seconds);
    }
}
