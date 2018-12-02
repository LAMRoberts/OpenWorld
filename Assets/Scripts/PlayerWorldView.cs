using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldView : MonoBehaviour
{
    public Transform world;
    public float loadDistance;
    public List<GameObject> playerChunkNodes;
    public float reach;
    public Camera playerCam;
    public Camera cullCam;

    private Plane[] planes;
    private RaycastHit hit;

    [SerializeField]
    private GameObject highlightedObject = null;
    [SerializeField]
    private GameObject lastHighlightedObject = null;

    private void Start ()
    {
        playerChunkNodes = new List<GameObject>();

        loadDistance += Mathf.Abs(cullCam.transform.position.z);

        hit = new RaycastHit();
    }

    private void Update()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(cullCam);

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
        
        // raycast to block
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit))
        {
            // if block is in range
            if (hit.distance < reach)
            {
                // set last highlighted block to the block that was highlighted last
                if (highlightedObject != null)
                {
                    lastHighlightedObject = highlightedObject;
                }

                // set highlighted block
                highlightedObject = hit.transform.parent.Find("LOD0").gameObject;
            }
            else
            {
                // no block is currently highlighted
                highlightedObject = null;
            }
        }
    }

    // return true if "from" is within range of "to"
    bool CloseTo(Transform from, Transform to)
    {
        float d = Vector3.Distance(from.position, to.position);

        if (d < loadDistance)
        {
            return true;
        }

        return false;
    }

    public GameObject getLastHighlightedObject()
    {
        return lastHighlightedObject;
    }
    public GameObject getHighlightedObject()
    {
        return highlightedObject;
    }
}
