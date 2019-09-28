using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variable declaration
    public GameObject targetPhone;
    public GameObject player;
    public float suspicion;

    #endregion
    private Rigidbody rb; //Test Variable, remove later
    public Transform beakBindPoint;

    private float eavesdropLevel, listenModifier;
    private bool eavesdropping;
    private Transform currentEavesdrop, playerT;

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

        beakBindPoint = GameObject.Find("BeakBindPoint").GetComponent<Transform>();
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
        rb.position = new Vector3(rb.position.x - 0.2f, rb.position.y, rb.position.z);
        //End Temporary Test code
        UpdateEavesdropLevel(eavesdropping);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Phone"));
        {
            Grab(other.gameObject.GetComponent<Transform>());
        }
        if (other.CompareTag("NPC"));
        {
            //TODO: spike Suspicion to super-high, not 100
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!eavesdropping && other.CompareTag("ListenField"))
        {
            eavesdropping = true;
            if (!other.gameObject == currentEavesdrop)
            {
                currentEavesdrop = other.gameObject.GetComponent<Transform>();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        eavesdropping = false;
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
        return targetDevice;
    }

    private void Grab(Transform device)
    {
        device.SetParent(beakBindPoint);
        device.position = beakBindPoint.position;
        device.rotation = Quaternion.Euler(0f,0f,0f);
        device.localRotation = Quaternion.Euler(0f, 0f, 0f); //I don't know what the actual values need to be just yet??? Need the asset from Zach
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
