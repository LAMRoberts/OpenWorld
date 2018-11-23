using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.0f;

    public float minAngle;
    public float maxAngle;

    public float speedH;
    public float speedV;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update ()
    {
        Vector3 forward = new Vector3(transform.forward.x, 0.0f, transform.forward.z);

		if (Input.GetKey("w"))
        {
            transform.position += (forward * (Time.deltaTime * moveSpeed));
        }

        if (Input.GetKey("s"))
        {
            transform.position += -(forward * (Time.deltaTime * moveSpeed));
        }

        if (Input.GetKey("a"))
        {
            transform.position += -(transform.right * (Time.deltaTime * moveSpeed));
        }

        if (Input.GetKey("d"))
        {
            transform.position += (transform.right * (Time.deltaTime * moveSpeed));
        }

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch = Mathf.Clamp(pitch - (speedV * Input.GetAxis("Mouse Y")), minAngle, maxAngle);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
