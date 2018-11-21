using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject player;

    private PlayerWorldView pwv;

    GameObject chunkNode;
    GameObject chunk;

    private void Start ()
    {
        chunkNode = (GameObject)Resources.Load("ChunkNode", typeof(GameObject));
        chunk = (GameObject)Resources.Load("Chunk", typeof(GameObject));

        for (int y = -256; y < 256; y += 16)
        {
            for (int x = -256; x < 256; x += 16)
            {
                GameObject c = Instantiate(chunkNode, transform);
                c.transform.position = new Vector3(x + 8, 0.5f, y + 8);
            }
        }

        pwv = player.GetComponent<PlayerWorldView>();
    }

    private void Update()
    {
        foreach (GameObject node in pwv.chunkNodes)
        {
            if (node.transform.childCount == 0)
            {
                Instantiate(chunk, node.transform);
            }
        }
    }

    public void DestroyChunk(GameObject parent)
    {

    }
}
