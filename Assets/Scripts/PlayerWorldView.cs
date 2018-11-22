using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldView : MonoBehaviour
{
    public Transform world;
    public float loadDistance;
    public List<GameObject> playerChunkNodes;
    public float reach;

    private Camera playerCam;
    private Camera cullCam;

	private void Start ()
    {
        playerChunkNodes = new List<GameObject>();

        playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cullCam = GameObject.FindGameObjectWithTag("Cull Camera").GetComponent<Camera>();
        loadDistance += Mathf.Abs(cullCam.transform.position.z);
	}

    private void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cullCam);

        foreach (Transform worldChunkNode in world)
        {
            if (worldChunkNode.tag == "ChunkNode")
            {
                if (CloseTo(transform, worldChunkNode))
                {
                    Collider c = worldChunkNode.GetComponent<Collider>();

                    if (GeometryUtility.TestPlanesAABB(planes, c.bounds))
                    {
                        if (!playerChunkNodes.Contains(worldChunkNode.gameObject))
                        {
                            playerChunkNodes.Add(worldChunkNode.gameObject);
                        }
                    }
                    else
                    {
                        if (playerChunkNodes.Contains(worldChunkNode.gameObject))
                        {
                            playerChunkNodes.Remove(worldChunkNode.gameObject);

                            world.GetComponent<WorldGenerator>().DestroyChunk(worldChunkNode);
                        }
                    }
                }
            }
        }

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(playerCam.transform.position, transform.forward, out hit))
        {
            if (hit.distance < reach)
            {
                Debug.DrawRay(playerCam.transform.position, transform.forward * reach, Color.green);

                Debug.Log(hit.transform);
            }

            Debug.DrawRay(playerCam.transform.position, transform.forward * reach, Color.red);
        }
        else
        {
            Debug.DrawRay(playerCam.transform.position, transform.forward * reach, Color.red);
        }
    }

    bool CloseTo(Transform from, Transform to)
    {
        float d = Vector3.Distance(from.position, to.position);

        if (d < loadDistance)
        {
            return true;
        }

        return false;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "ChunkNode")
    //    {
    //        chunkNodes.Add(other.gameObject);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "ChunkNode")
    //    {
    //        chunkNodes.Remove(other.gameObject);

    //        Destroy(other.transform.GetChild(0).gameObject);
    //    }
    //}
}
