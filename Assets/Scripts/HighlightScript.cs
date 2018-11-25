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

    [SerializeField]
    Material[] defaultMaterials;
    [SerializeField]
    Material[] highlightedMaterials;
    [SerializeField]
    GameObject[] blockTypes;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        defaultMaterials = Resources.LoadAll<Material>("Materials/Default");
        highlightedMaterials = Resources.LoadAll<Material>("Materials/Highlighted");

        blockTypes = Resources.LoadAll<GameObject>("Blocks");

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