using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is for storing object data only. Do not include any code to handle interaction unless it is necessary

public class ObjectInfo : MonoBehaviour
{
    [Tooltip("Friendly name for the object")]
    public string objectName;
    [Tooltip("Health value between 0 and 100.")]
    public float objectHealth;
    [Tooltip("Descriptor tags to be assigned to the object. NPCS: First tag must always be either 'Innocent' or 'Group'.")]
    public string[] tags;

    [Header("Object Settings")]
    public bool IsInteractive = false;

    [ConditionalHide("IsInteractive", false)]
    [Tooltip("Whether or not the object can be picked up by the Player.")]
    public bool canGrab = false;
    [ConditionalHide("IsInteractive", false)]
    [Tooltip("Whether or not the object can be picked up by the Player.")]
    public bool IsIntel = false;
    [ConditionalHide("IsInteractive", false)]
    [Tooltip("The object's type. I.E. Phone, NPCContainer")]
    public string objectType;


    [Header("NPC Settings")]
    public bool IsNPC = false;

    [ConditionalHide("IsNPC", true)]
    public bool isGroup = false;

    [ConditionalHide("IsNPC", true)]
    [Tooltip("Set which skin to use on the NPC")]
    public int skin = 0;

    [System.NonSerialized] //Do not show in Inspector
    public Transform initialParent;

    private void Awake()
    {
        initialParent = gameObject.GetComponent<Transform>().parent;
    }
    private void SetSkin()
    {
        //TODO: Need to fix the weirdness with the NPC models
    }
}
