using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WalkScript : MonoBehaviour
{
    public float speed;
    public bool fly;
    public bool invert = false;
    public float minAngle = 45;
    public float maxAngle = 315;
    public float upwardForce = 300;
    public float flapTime = 0.9f;
    [Tooltip("Speed at which the bird flies forward")]
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

    private int inverted = 0;
    public float axisH, axisVWalk, axisVFly, lTrigger, rTrigger, flySpeed;
    // Start is called before the first frame update
    void Start()
    {
        if (speed == 0) speed = 10;
        cc = GetComponent<CharacterController>();
        anim = transform.GetComponentInChildren<Animator>();
        anim.SetBool("IsWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!invert) inverted = 1;
        else inverted = -1;
        flySpeed = Input.GetAxis("flySpeed");
        lTrigger = Input.GetAxis("slowFly");
        rTrigger = Input.GetAxis("fastFly");
        axisH = Input.GetAxis("Horizontal");
        axisVWalk = Input.GetAxis("Vertical");
        axisVFly = Input.GetAxis("VerticalFly");
        if (Input.GetButtonDown("ToggleFlight"))
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
        if (flySpeed != 0)
        {
            if (flySpeed > 0)
            cc.Move(transform.forward * (zVo * 2) * Time.deltaTime);
            else cc.Move(transform.forward * (zVo / 2) * Time.deltaTime);
        }
        else cc.Move(transform.forward * (zVo) * Time.deltaTime);
        /*
        if (rTrigger > 0)
        {
            cc.Move(transform.forward * (zVo * (2f* rTrigger)) * Time.deltaTime);
        }
        else if (lTrigger > 0)
        {
            cc.Move(transform.forward * (zVo / (2f * lTrigger)) * Time.deltaTime);
        }
        else
        {
            cc.Move(transform.forward * (zVo * flySpeed) * Time.deltaTime);
            //rb.velocity = transform.forward * zVo; 
        }
        */

        Quaternion targetRotation = transform.rotation;
        Vector3 targetAngles = targetRotation.eulerAngles;

            //Change Pitch
                targetAngles.x += axisVFly * Time.deltaTime * inverted *50f;
            //Change Direction
                targetAngles.y += axisH * Time.deltaTime *50f;

            
        

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
        float movement = axisVWalk * Time.deltaTime * speed;
        cc.Move(transform.forward * movement); //forwards
        
        float rotation = axisH * Time.deltaTime * 50f;
        transform.Rotate(0, rotation, 0);
        

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
