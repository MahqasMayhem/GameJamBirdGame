using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gametracker : MonoBehaviour //TOD: Keep track of Winning and losing the game. Maybe move the timer into here. Lots of placeholder stuff until we decide how to move forward with the design
{
    private Transform canvas;
    private GameObject winLoseContainer,uiMissionWin, uiMissionFail;

    public float globalSuspicion;
    [Header("DEBUG Values")]
    [Tooltip("[DEBUG] If enabled, forces all dialogue to print without corruption.")]
    public bool DisableDialogueCorruption;
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
        //Time.timeScale = 1;
       // Time.fixedDeltaTime = 1;
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
        StartCoroutine(LerpTime(0, 0.5f));
        //ReturnToMenu();
    }
    private void ReturnToMenu()
    {

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale;
        if (!Application.isEditor) SceneManager.LoadScene(0, LoadSceneMode.Single);

    }
        IEnumerator LerpTime(float _lerpTimeTo, float _timeToTake)
        {
            float endTime = Time.time + _timeToTake;
            float startTimeScale = Time.timeScale;
            float i = 0f;
            while (Time.time < endTime)
            {
                i += (1 / _timeToTake) * Time.deltaTime;
                Time.timeScale = Mathf.Lerp(startTimeScale, _lerpTimeTo, i);
            Time.fixedDeltaTime = Time.timeScale;
                //print(Time.timeScale);
                yield return null;
            }
            Time.timeScale = _lerpTimeTo;
        }

    }
