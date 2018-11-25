using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    [SerializeField]
    private GameObject highlightedBlock = null;
    [SerializeField]
    private GameObject highlightedBlockNode = null;
    [SerializeField]
    private GameObject highlightedChunk = null;

    private void Start()
    {

    }

    private void Update ()
    {
        if (transform.GetComponent<PlayerWorldView>().getHighlightedObject() != null)
        {
            highlightedBlock = transform.GetComponent<PlayerWorldView>().getHighlightedObject().transform.parent.gameObject;
            highlightedBlockNode = highlightedBlock.transform.parent.gameObject;
            highlightedChunk = highlightedBlockNode.transform.parent.gameObject;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (transform.GetComponent<PlayerWorldView>().getHighlightedObject() != null)
            {
                highlightedChunk.GetComponent<ChunkManager>().DestroyBlock(highlightedBlockNode.transform);
            }
        }
    }
}
