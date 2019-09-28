using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Eavesdropper : MonoBehaviour
{
    public TextHandler textManager = GameObject.Find("TextManager").GetComponent<TextHandler>();

    // Start is called before the first frame update
    void Start()
    {
        TextAsset asset = textManager.GetRandomDialogue("innocent");
        Debug.Log(asset.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
