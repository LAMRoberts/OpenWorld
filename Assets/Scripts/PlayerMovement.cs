using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.0f;
    public float rotateSpeed = 0.0f;

    void Start ()
    {
		
	}
	
	void Update ()
    {
		if (Input.GetKey("w"))
        {
            transform.position += (transform.forward * (moveSpeed * 0.01f)) ;
        }

        if (Input.GetKey("s"))
        {
            transform.position += -(transform.forward * (moveSpeed * 0.01f));
        }

        if (Input.GetKey("a"))
        {
            transform.position += -(transform.right * (moveSpeed * 0.01f));
        }

        if (Input.GetKey("d"))
        {
            transform.position += (transform.right * (moveSpeed * 0.01f));
        }

        if (Input.GetKey("q"))
        {
            transform.Rotate(-(transform.up * (Time.deltaTime * rotateSpeed)));
        }

        if (Input.GetKey("e"))
        {
            transform.Rotate(transform.up * (Time.deltaTime * rotateSpeed));
        }
    }
}
