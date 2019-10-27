using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WalkScript : MonoBehaviour
{
    public float speed;
    public bool fly;
    public bool invert = false;
    public GameObject MissionComplete;
    public float minAngle = 45;
    public float maxAngle = 315;
    public float upwardForce = 300;
    public float flapTime = 0.9f;
    public float zVo = 20;
    public float xVo = 10;
    public float yVo = 10;
    public int maxReps = 11;
    private CharacterController cc;
    public bool readyToFly = true;
    public bool rested = true;
    private int reps = 0;
    private float xAngle, yAngle, zAngle;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        MissionComplete = GameObject.Find("UI_MissionWin");
        MissionComplete.SetActive(false);
        anim = transform.Find("Pigeon").gameObject.GetComponent<Animator>();
        anim.SetBool("IsWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            fly = !fly;
            if (!fly)
                anim.SetBool("IsWalking", true);
            else
                anim.SetBool("IsWalking", false);
        }

        if (fly)
        {
            Fly();

            if(Input.GetKeyDown("space"))
            {
               
            }

        }

        else
        {
        Walk();
          
        }
        if (!fly && transform.rotation.x != 0)
        {

            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
    }

    private bool CanRotate(bool IsRoll, bool positive)
    {
        Transform comparisonT = GameObject.Find("BirdCompare").GetComponent<Transform>();
       // float valueX = comparisonT.rotation.eulerAngles.x - transform.rotation.eulerAngles.x;
        float valueZ = Vector3.Angle(transform.right, Vector3.right);
        //float valueZ = comparisonT.rotation.eulerAngles.z - transform.rotation.eulerAngles.z;
        float valueX = Vector3.Angle(transform.forward, Vector3.forward);
        if (IsRoll)
        {
            if (valueZ > -65 && positive) return true;
            else if (valueZ < 65 && !positive) return true;
            else return false;
        }
        else
        {
            if (valueX > -65 && positive) return true;
            else if (valueX < 65 && !positive) return true;
            else return false;
        }
    }
    void Fly()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            cc.Move(transform.forward * (zVo * 2) * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Tab))
        {
            cc.Move(transform.forward * (zVo / 2) * Time.deltaTime);
        }

        else
        {
            cc.Move(transform.forward * (zVo) * Time.deltaTime);
            //rb.velocity = transform.forward * zVo; 
        }
        /*
        if ((Input.GetKey("s") || Input.GetKey("down")) && CanRotate(false))
        {

            //transform.Rotate(1f, 0, 0); 
            transform.Rotate(Vector3.right, 1f);
        }

        if ((Input.GetKey("up") || Input.GetKey("w")) && CanRotate(false))
        {
            Debug.Log("Rotate Down");
            //transform.Rotate(-1f, 0, 0); 
            transform.Rotate(Vector3.right, -1f);
        }

        if ((Input.GetKey("a") || Input.GetKey("left")) && CanRotate(true))
        {
            //transform.Rotate(0, -1f, 0); 
            transform.Rotate(Vector3.up, -1f);
        }

        if ((Input.GetKey("d") || Input.GetKey("right")) && CanRotate(true))
        {
            //transform.Rotate(0, 1f, 0); 
            transform.Rotate(Vector3.up, 1f);
        }
        */

        if (reps > maxReps)
        {
            rested = false;
            StartCoroutine("Resting");
            reps = 0;
        }

        if (readyToFly && rested)
        {
            readyToFly = false; 
        }
        Quaternion targetRotation = transform.rotation;
        Vector3 targetAngles = targetRotation.eulerAngles;
        if (Input.GetKey("s") || Input.GetKey("down"))
        {

            

            //if (isInverted) targetAngles.x -= 1f;   //TODO
            /*else*/ //targetAngles.x += 1f;
            if (invert)
            {
                targetAngles.x -= 2f;
            }
            {
                targetAngles.x += 1f;
            }

        }

        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            Debug.Log("Rotate Down");
            //transform.Rotate(-1f, 0, 0); 
            //targetAngles.x -= 1f;
            //transform.Rotate(Vector3.right, -1f);
            if(invert)
            {
                targetAngles.x += 2f;
            }
            {
                targetAngles.x -= 1f;
            }
        }

        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            //transform.Rotate(0, -1f, 0); 
            targetAngles.y -= 1.8f;
            //transform.Rotate(Vector3.up, -1f);
        }

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            //transform.Rotate(0, 1f, 0); 
            targetAngles.y += 1.8f;
            //transform.Rotate(Vector3.up, 1f);
        }
        targetAngles.x = ClampAngle(targetAngles.x, -65, 65);
        targetAngles.z = ClampAngle(targetAngles.z, -65, 65);
        targetRotation = Quaternion.Euler(targetAngles);
        transform.rotation = targetRotation;
        /*
        float spinangle = BirdClamp(transform.rotation.eulerAngles.y, minAngle, maxAngle);
        Debug.Log(spinangle + " " + transform.rotation.eulerAngles.y);

        float BirdClamp(float cur, float min, float max)
        {
            if (cur > 45.0f && cur < 180)
                return 45;
            if (cur < 315 && cur > 180)
                return 315;
            return cur;
        }
        */
    

}
    public static float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle, 360);
        min = Mathf.Repeat(min, 360);
        max = Mathf.Repeat(max, 360);
        bool inverse = false;
        var tmin = min;
        var tangle = angle;
        if (min > 180)
        {
            inverse = !inverse;
            tmin -= 180;
        }
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        var result = !inverse ? tangle > tmin : tangle < tmin;
        if (!result)
            angle = min;

        inverse = false;
        tangle = angle;
        var tmax = max;
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        if (max > 180)
        {
            inverse = !inverse;
            tmax -= 180;
        }

        result = !inverse ? tangle < tmax : tangle > tmax;
        if (!result)
            angle = max;
        return angle;
    }


    private void Walk()
    {
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            cc.Move(transform.forward * (speed) * Time.deltaTime);
        }

        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            cc.Move(transform.forward * (-speed) * Time.deltaTime);
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            //rb.velocity = -transform.right * speed;
            transform.Rotate(0, -1.5f, 0);
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            //rb.velocity = transform.right * speed;
            transform.Rotate(0, 1.5f, 0);
        }
        if (!cc.isGrounded)
        {
            var gravity = Physics.gravity * Time.deltaTime;
            cc.Move(transform.up * gravity.y);
        }
    }
    
    public void invertControls()
    {
        invert = !invert;
    }
}
