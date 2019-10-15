using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is for storing object data only. Do not include any code to handle interaction unless it is necessary

public class ObjectInfo : MonoBehaviour
{
    public string objectName;
    public string objectType;
    public bool canGrab;
    public float objectHealth;

    public string[] tags;


    public Transform initialParent;
    private void Awake()
    {
        initialParent = gameObject.GetComponent<Transform>().parent;
    }
}
