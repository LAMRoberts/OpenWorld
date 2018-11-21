using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldView : MonoBehaviour
{
    public List<GameObject> chunkNodes;

	// Use this for initialization
	void Start ()
    {
        chunkNodes = new List<GameObject>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ChunkNode")
        {
            chunkNodes.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ChunkNode")
        {
            chunkNodes.Remove(other.gameObject);

            Destroy(other.transform.GetChild(0).gameObject);
        }
    }
}
