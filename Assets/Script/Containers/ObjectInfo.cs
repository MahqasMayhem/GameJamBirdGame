using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is for storing object data only. Do not include any code to handle interaction unless it is necessary

public class ObjectInfo : MonoBehaviour
{
    [Tooltip("Friendly name for the object")]
    public string objectName;
    [Tooltip("The object's type. I.E. Phone, NPCContainer")]
    public string objectType;
    [Tooltip("Whether or not the object can be picked up by the Player.")]
    public bool canGrab;
    [Tooltip("Health value between 0 and 100.")]
    public float objectHealth;
    [Tooltip("Descriptor tags to be assigned to the object. NPCS: First tag must always be either 'Innocent' or 'Group'.")]
    public string[] tags;

    [System.NonSerialized] //Do not show in Inspector
    public Transform initialParent;
    private void Awake()
    {
        initialParent = gameObject.GetComponent<Transform>().parent;
    }
}
