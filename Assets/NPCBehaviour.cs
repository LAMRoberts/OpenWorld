using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 10.0f;
    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private float maxWaitTime = 2.0f;
    
    private float timeSinceLastMove;
    private float timeUntilNextMove;
    private bool moving;
    private Vector3 targetPosition;

	void Update ()
    {
        if (moving)
        {
            if (Move())
            {
                moving = false;

                timeSinceLastMove = 0.0f;
            }
        }
        else
        {
            FindNextWaitTime();
                       
		    if (timeSinceLastMove >= timeUntilNextMove)
            {
                FindNextPosition();

                moving = true;
            }
            else
            {
                timeSinceLastMove += Time.deltaTime;
            }
        }
	}

    bool Move()
    {
        if (transform.position.Equals(targetPosition))
        {
            return true;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);

            return false;
        }
    }

    void FindNextPosition()
    {
        targetPosition = new Vector3(transform.position.x + Random.Range(-maxDistance, maxDistance), transform.position.y, transform.position.z + Random.Range(-maxDistance, maxDistance));
    }

    void FindNextWaitTime()
    {
        timeUntilNextMove = Random.Range(1, maxWaitTime);
    }
}
