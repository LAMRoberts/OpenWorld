using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    public struct Block
    {
        public GameObject blockType;
        public Material defaultMaterial;
        public Material highlightedMaterial;

        public Block(GameObject bt, Material dm, Material hm)
        {
            blockType = bt;
            defaultMaterial = dm;
            highlightedMaterial = hm;
        }
    }
    private List<Block> blocks;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Material[] defaultMaterials = Resources.LoadAll<Material>("Materials/Default");
        Material[] highlightedMaterials = Resources.LoadAll<Material>("Materials/Highlighted");

        GameObject[] blockTypes = Resources.LoadAll<GameObject>("Blocks");

        blocks = new List<Block>();

        for (int i = 0; i < blockTypes.Length; i++)
        {
            Block temp = new Block(blockTypes[i], defaultMaterials[i], highlightedMaterials[i]);

            blocks.Add(temp);
        }
    }

    public void Update()
    {
        if (player.GetComponent<PlayerWorldView>().getLastHighlightedObject() != null)
        {
            player.GetComponent<PlayerWorldView>().getLastHighlightedObject().GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        if (player.GetComponent<PlayerWorldView>().getHighlightedObject() != null)
        {
            player.GetComponent<PlayerWorldView>().getHighlightedObject().GetComponent<Renderer>().material.color = new Color(0.5f, 0.75f, 1.0f, 1.0f);          
        }
    }
}
