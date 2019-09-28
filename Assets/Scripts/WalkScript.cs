using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WalkScript : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public bool fly;
// Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown("space"))
            fly = !fly;
        if (fly)
            Debug.Log("Flying");
        else
            Walk();
    }

    private void Walk()
    {
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            rb.velocity = transform.forward * speed;
            Debug.Log("up");
        }

        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            rb.velocity = -transform.forward * speed;
            Debug.Log("down");
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            //rb.velocity = -transform.right * speed;
            transform.Rotate(0, -3, 0);
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            //rb.velocity = transform.right * speed;
            transform.Rotate(0, 3, 0);
        }
    }
}
