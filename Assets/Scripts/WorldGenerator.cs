using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject player;

    private PlayerWorldView pwv;

    GameObject chunkNodePrefab;
    GameObject chunkPrefab;

    private void Start ()
    {
        chunkNodePrefab = (GameObject)Resources.Load("ChunkNode", typeof(GameObject));
        chunkPrefab = (GameObject)Resources.Load("Chunk", typeof(GameObject));

        for (int y = 0; y < 512; y += 16)
        {
            for (int x = 0; x < 512; x += 16)
            {
                GameObject c = Instantiate(chunkNodePrefab, transform);
                c.transform.position = new Vector3(x + 8, 0.5f, y + 8);
            }
        }

        pwv = player.GetComponent<PlayerWorldView>();
    }

    private void Update()
    {
        foreach (GameObject playerChunkNode in pwv.playerChunkNodes)
        {
            if (playerChunkNode.transform.childCount == 0)
            {
                Instantiate(chunkPrefab, playerChunkNode.transform);
            }
        }
    }

    public void DestroyChunk(Transform worldChunkNode)
    {
        Transform worldChunk = worldChunkNode.transform.GetChild(0);

        Destroy(worldChunk.gameObject);
    }
}
