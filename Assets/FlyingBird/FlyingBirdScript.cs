using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class FlyingBirdScript : MonoBehaviour
{
    public float minAngle = 45;
    public float maxAngle = 315;
    public float upwardForce = 300;
    public float flapTime = 0.9f;
    public float zVo = 100;
    public float xVo = 50;
    public float yVo = 50;
    public int maxReps = 11;
    public bool readyToFly = true;
    public bool rested = true;
    private CharacterController cc;
    private int reps = 0;
    //private float tiltAngle = 40f;
    //private GameObject Bird;
    private float xAngle, yAngle, zAngle;

    //public GameObject Bird1 => Bird;

    // Start is called before the first frame update
    private void Start() 
    {
        Debug.Log("Fly script added to: " + gameObject.name);
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            cc.Move(transform.forward * (zVo * 2) * Time.deltaTime);
        }

        else
        {
            cc.Move(transform.forward * (zVo) * Time.deltaTime);
            //rb.velocity = transform.forward * zVo;
        }

        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            //transform.Rotate(1f, 0, 0);
            transform.Rotate(Vector3.right, 1f);
        }

        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            //transform.Rotate(-1f, 0, 0);
            transform.Rotate(Vector3.right, -1f);
        }

        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            //transform.Rotate(0, -1f, 0);
            transform.Rotate(Vector3.up, -1f);
        }

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            //transform.Rotate(0, 1f, 0);
            transform.Rotate(Vector3.up, 1f);
        }

        //transform.Rotate(Input.GetAxis("Vertical"),0.0f, - Input.GetAxis("Horizontal"));


        if (reps > maxReps)
        {
            rested = false;
            StartCoroutine("Resting");
            reps = 0;
        }

        if (readyToFly && rested)
        {
            readyToFly = false;
            //StartCoroutine("FlapWing");
        }

        float spinangle = BirdClamp(transform.rotation.eulerAngles.y, minAngle, maxAngle);
        Debug.Log(spinangle + " " + transform.rotation.eulerAngles.y);
        
    }

        float BirdClamp(float cur, float min, float max)
    {
        if (cur > 45.0f && cur < 180)
            return 45;
        if (cur < 315 && cur > 180)
            return 315;
        return cur;
    }
}

