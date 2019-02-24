using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform world;
    public Transform forwards;
    public Transform leftwards;
    public Transform rightwards;
    public Transform backwards;

    public float moveSpeed = 0.0f;

    public float minAngle;
    public float maxAngle;

    public float speedH;
    public float speedV;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private Camera playerCam;

    void Start ()
    {
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update ()
    {
		if (Input.GetKey("w"))
        {
            transform.position = Vector3.MoveTowards(transform.position, forwards.position, Time.deltaTime * moveSpeed);
        }

        if (Input.GetKey("s"))
        {
            transform.position = Vector3.MoveTowards(transform.position, backwards.position, Time.deltaTime * moveSpeed);
        }

        if (Input.GetKey("a"))
        {
            transform.position = Vector3.MoveTowards(transform.position, leftwards.position, Time.deltaTime * moveSpeed);
        }

        if (Input.GetKey("d"))
        {
            transform.position = Vector3.MoveTowards(transform.position, rightwards.position, Time.deltaTime * moveSpeed);
        }

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch = Mathf.Clamp(pitch - (speedV * Input.GetAxis("Mouse Y")), minAngle, maxAngle);

        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        playerCam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
