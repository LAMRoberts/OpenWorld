using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotate : MonoBehaviour
{
    public float speed = 5.0f;

	void Update ()
    {
        transform.Rotate(0.0f, 0.0f, speed);
	}
}
