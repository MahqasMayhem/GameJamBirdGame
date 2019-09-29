using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variable declaration
    public GameObject targetPhone;
    public GameObject player, missionComplete, missionFail;
    public float suspicion;

    #endregion
    private Rigidbody rb; //Test Variable, remove later
    public Transform beakBindPoint, currentEavesdrop;
    public float listenModifier, eavesdropLevel;

    public bool eavesdropping;
    private Transform playerT;

    #region Test Variable Declarations
    private Text eavesdropIndicator;
    
    
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        #region Variable Definition
        if (!player)
        {
            player = this.gameObject;
        }
        rb = player.GetComponent<Rigidbody>(); // Test Code

        suspicion = 0f;
        targetPhone = TargetDevice();
        missionComplete = GameObject.Find("UI_MissionWin");
        missionComplete.SetActive(false);
        missionFail = GameObject.Find("UI_MissionFail");
        missionFail.SetActive(false);
        beakBindPoint = transform.Find("BeakBindPoint").GetComponent<Transform>();
        playerT = player.GetComponent<Transform>();
        eavesdropLevel = 0f;
        eavesdropping = false;
        #endregion

        #region Test Variable Definitions
        eavesdropIndicator = GameObject.Find("Eavesdrop_TESTUI").GetComponent<Text>();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        //Test movement, remove later
        //rb.position = new Vector3(rb.position.x - 0.2f, rb.position.y, rb.position.z);
        //End Temporary Test code
        UpdateEavesdropLevel(eavesdropping);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Phone"))
        {
            Grab(other.gameObject.GetComponent<Transform>());
        }
        if (other.CompareTag("NPC"))
        {
            //TODO: spike Suspicion to super-high, not 100
        }
        if ((other.gameObject.name == "ListenField") && !eavesdropping)
        {
            eavesdropping = true;
            if (other.gameObject.GetComponent<Transform>() != currentEavesdrop)
            {
                currentEavesdrop = other.gameObject.GetComponent<Transform>().parent.GetComponent<Transform>();
            }
            BroadcastMessage("EnableEavesdrop");
        }
        if (other.gameObject.name == "BirdNest" && beakBindPoint.childCount > 0)
        {
            var device = beakBindPoint.GetChild(0).gameObject;
            if (device == targetPhone) //Win
            {
                missionComplete.SetActive(true);
            }
            else if (device == null)
            {
                throw new System.NullReferenceException("Phone pickup not bound to bird or beakBindPoint child is missing");
            }
            else //Lose
            {
                missionFail.SetActive(true);
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name=="ListenField")
        {
            eavesdropping = false;
            BroadcastMessage("DisableEavesdrop");
        }
    }
    private GameObject TargetDevice()
    {
        var NPCs = new List<GameObject>();
        foreach (GameObject NPC in GameObject.FindGameObjectsWithTag("NPC"))
        {
            NPCs.Add(NPC);
        }
        GameObject Spy = NPCs[Random.Range(0, NPCs.Count)];
        Debug.Log(Spy.ToString() + " is a spy!");
        GameObject targetDevice = Spy.transform.Find("phone").gameObject;
        while (!targetDevice)
        {
            Spy = NPCs[Random.Range(0, NPCs.Count)];
            targetDevice = Spy.transform.Find("phone").gameObject;
        }
        if (Spy.tag == "Group")
        {
            Spy.GetComponent<Transform>().parent.Find("ListenField").tag = "Spy"; 
        }
        Spy.tag = "Spy";
        
        return targetDevice;
    }

    private void Grab(Transform device)
    {
        device.SetParent(beakBindPoint);
        device.position = beakBindPoint.position;
        device.rotation = Quaternion.Euler(0f,0f,0f);
        device.localRotation = Quaternion.Euler(0f, 0f, 0f);
        device.GetComponent<BoxCollider>().enabled = false;
    }
    private void UpdateEavesdropLevel(bool eavesdrop)
    {
        if (!eavesdrop)
        {
            eavesdropLevel = 0;
        }
        else
        {
            float distance = Vector3.Distance(currentEavesdrop.position, playerT.position);
            eavesdropLevel = listenModifier * distance;
        }
        eavesdropIndicator.text = ("Eavesdrop Level:" +(int)eavesdropLevel);
    }


}
