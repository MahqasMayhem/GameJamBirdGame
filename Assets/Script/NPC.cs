using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    #region Variable Declaration
    public GameObject player;
    public int viewRange;
    public float suspicionModifier;

    private float npcSuspicion;
    private GameObject visibleIndicator, suspicionIndicator; //Test elements


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        if (!player)
        {
            player = GameObject.Find("Player");
        }
        if (viewRange == 0)
        {
            viewRange = 120;
        }
        npcSuspicion = 0f;
        #region Test variable Declarations
        visibleIndicator = GameObject.Find("Spotted_TESTUI");
        suspicionIndicator = GameObject.Find("Suspicion_TESTUI");
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
    private void UpdateSuspicion()
    {
        while (IsVisible())
        {
            //Backlog-ish
        }
    }
    
    private bool IsVisible()
    {

        return;
    }
    */

}
