﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGame()
    {
        //SceneManager.UnloadSceneAsync(0);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}