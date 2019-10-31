using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to place all methods for manipulating and interacting with world objects.
public class ObjectHandler 
{
    //private readonly Transform beakBindPoint = GameObject.FindGameObjectWithTag("Player").transform.Find("BeakBindPoint");

    //-----------------------------------------------------------------------------------------
    //-----------------------------------Invokeable Methods------------------------------------
    //-----------------------------------------------------------------------------------------
    public void GrabObject(GameObject worldObject, Transform beakBindPoint) //attach object to beak
    {
        if (worldObject.GetComponent<ObjectInfo>().canGrab && beakBindPoint.childCount < 2)
        {
            worldObject.GetComponent<Rigidbody>().useGravity = false;
            Transform pickup = worldObject.transform;
            pickup.SetParent(beakBindPoint);
            pickup.position = beakBindPoint.position;
            pickup.rotation = Quaternion.Euler(0f, 0f, 0f);
            pickup.localRotation = Quaternion.Euler(0f, 0f, 0f);
            pickup.GetComponent<Collider>().enabled = false;
            if (CompareTag(worldObject, "activateOnPickup"))
            {
                ActivateObject(worldObject);
            }
        }
    }
    public void DropObject(Transform beakBindPoint)
    {
        GameObject worldObject = beakBindPoint.GetChild(0).gameObject;
        //Vector3 worldPos = worldObject.transform.position;
        beakBindPoint.DetachChildren();
        worldObject.GetComponent<Rigidbody>().useGravity = true;
        worldObject.GetComponent<Collider>().enabled = true;



        if (CompareTag(worldObject.gameObject, "activateOnDrop"))
        {
            ActivateObject(worldObject.gameObject);
        }

    }
    public void DropObject(Transform beakBindPoint, GameObject dropPoint)
    {
        Transform dropPosition = dropPoint.GetComponent<Transform>();
        Transform worldObject = beakBindPoint.GetChild(0);
        worldObject.SetParent(dropPosition);
        worldObject.position = dropPosition.position;
        worldObject.localPosition = Vector3.zero;
        //worldObject.GetComponent<Rigidbody>().useGravity = true;
        //worldObject.GetComponent<Collider>().enabled = true;

        if (GetType(worldObject.gameObject) == "Intel")
        {
            AcquireIntel(worldObject.gameObject);
        }
        else
        {
            Debug.Log("That's not an intel item!");
            if (CompareTag(worldObject.gameObject, "activateOnDrop"))
            {
                ActivateObject(worldObject.gameObject);
            }
        }

    }
   
    //-----------------------------------------------------------------------------------------
    //-----------------------------------------Invokers----------------------------------------
    //-----------------------------------------------------------------------------------------
    public void ActivateObject(GameObject worldObject)
    {
        Debug.Log("This object has activated!", worldObject);
    }
    private void AcquireIntel(GameObject intel)
    {
        Debug.Log("Intel!");
        GameObject.Find("GameTracker").GetComponent<Gametracker>().AcquirePhone();
    }
    //-----------------------------------------------------------------------------------------
    //----------------------Utility methods should go below here-------------------------------
    //-----------------------------------------------------------------------------------------
    public bool CompareTag(GameObject target, string wantedTag)
    {
        ObjectInfo objectInfo = target.GetComponent<ObjectInfo>();
        bool isEqual = false;
        foreach (string tag in objectInfo.tags)
        {
            if (tag == wantedTag)
            {
                isEqual = true;
                break;
            }
            else continue;
        }
        return isEqual;
    }
    public bool CompareTag(ObjectInfo objectInfo, string wantedTag)
    {
        bool isEqual = false;
        foreach (string tag in objectInfo.tags)
        {
            if (tag == wantedTag)
            {
                isEqual = true;
                break;
            }
            else continue;
        }
        return isEqual;
    }
    public bool CompareTag(GameObject target, List<string> wantedTag) //Overload. Check each individual tag with CompareTag
    {
        ObjectInfo objectInfo = target.GetComponent<ObjectInfo>();
        List<string> foundTags = new List<string>();
        foreach (string tag in wantedTag)
        {
            for (int i = 0; i <= objectInfo.tags.Length; i++)
            {
                if (objectInfo.tags[i] == wantedTag[i])
                {
                    foundTags.Add(objectInfo.tags[i]);
                }
            }
        }
        if (foundTags == wantedTag) return true;
        else return false;
    }
    public string GetType(GameObject target)
    {
        ObjectInfo objectInfo = target.GetComponent<ObjectInfo>();
        return objectInfo.objectType;
    }


}
