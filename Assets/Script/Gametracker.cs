using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gametracker : MonoBehaviour //TOD: Keep track of Winning and losing the game. Maybe move the timer into here. Lots of placeholder stuff until we decide how to move forward with the design
{
    private Transform canvas;
    private GameObject winLoseContainer,uiMissionWin, uiMissionFail;

    public float globalSuspicion;
    // Start is called before the first frame update
    void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;
        canvas.gameObject.SetActive(true);
        winLoseContainer = canvas.Find("UI_VictoryContainer").gameObject;
        uiMissionWin = winLoseContainer.transform.Find("UI_MissionWin").gameObject;
        uiMissionFail = GameObject.Find("UI_MissionFail");
        uiMissionFail.SetActive(false);
        uiMissionWin.SetActive(false);

    }

    // Update is called once per frame
    public void AcquirePhone() //Test method
    {
        Debug.Log("Activating Game Win");
        WinGame();
    }
    private void WinGame()
    {
        uiMissionWin.SetActive(true);
        //Invoke("ReturnToMenu", 4f);
    }
    private void LoseGame() //TODO: Would be cool to slow time to a halt and blur the game view
    {
        uiMissionFail.SetActive(true);
        //Invoke("ReturnToMenu", 4f);
    }
    private void ReturnToMenu()
    {
        canvas.gameObject.SetActive(false);
        SceneManager.LoadScene(0);

    }
}
