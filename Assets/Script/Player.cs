using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variable declaration
    public GameObject player, missionComplete, missionFail, targetPhone;
    public Transform beakBindPoint, currentEavesdrop;
    public float listenModifier, eavesdropLevel, suspicion;
    public bool eavesdropping;

    private Transform playerT;
    private ObjectHandler obj;
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        #region Variable Definition
        obj = new ObjectHandler();
        if (!player)
        {
            player = this.gameObject;
        }

        suspicion = 0f;
        while (!targetPhone)
        {
            targetPhone = TargetDevice();
        }

        missionComplete = GameObject.Find("UI_MissionWin");
        missionComplete.SetActive(false);
        missionFail = GameObject.Find("UI_MissionFail");
        missionFail.SetActive(false);
        beakBindPoint = transform.Find("BeakBindPoint").GetComponent<Transform>();
        playerT = player.GetComponent<Transform>();
        eavesdropLevel = 0f;
        eavesdropping = false;
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        // UpdateEavesdropLevel();
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.CompareTag("Phone"))
        {
            Grab(other.gameObject.GetComponent<Transform>());
        }
        */
        if (other.CompareTag("Interactive"))
        {
            obj.GrabObject(other.gameObject);
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
        if (other.gameObject.name == "BirdNest")
        {
            obj.DropObject(other.gameObject);
            /*
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
            */
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "ListenField")
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
        Debug.Log(Spy.ToString() + " is a spy!", Spy);
        GameObject targetDevice;
        if (Spy.transform.parent.tag == "Group")
        {
            Spy = Spy.transform.parent.gameObject;
            foreach (Transform deviceT in Spy.GetComponentsInChildren<Transform>())
            {
                if (deviceT.gameObject.tag == "Phone")
                {
                    targetDevice = deviceT.gameObject;
                    return targetDevice;
                }


            }
            Debug.LogError("No phone found. Game broke");
            return Spy;

        }
        else
        {
            Spy.tag = "Spy";
            targetDevice = Spy.transform.Find("Phone Variant").gameObject;
            Debug.Log("Not a group");
            Spy.transform.Find("ListenField").tag = "Spy";
            return targetDevice;
        }

    }
    /*
    private void Grab(Transform device)
    {
        if (beakBindPoint.childCount < 1)
        {
            device.SetParent(beakBindPoint);
            device.position = beakBindPoint.position;
            device.rotation = Quaternion.Euler(0f, 0f, 0f);
            device.localRotation = Quaternion.Euler(0f, 0f, 0f);
            device.GetComponent<BoxCollider>().enabled = false;
        }
    }
    */
    private void UpdateEavesdropLevel()
    {
        if (eavesdropping == false)
        {
            eavesdropLevel = 5;
        }
        else
        {
            float distance = Vector3.Distance(currentEavesdrop.position, playerT.position);
            eavesdropLevel = listenModifier * distance;
        }

    }


}
