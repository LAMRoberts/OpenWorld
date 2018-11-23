using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    private struct Block
    {
        GameObject blockType;
        Material defaultMaterial;
        Material highlightedMaterial;
    }

    [SerializeField]
    private Object[] defaultMaterials;
    [SerializeField]
    private Object[] highlightedMaterials;

    [SerializeField]
    private List<Block> blocks;

    public Material defaultMaterial;
    public Material highlightMaterial;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        defaultMaterials = Resources.LoadAll("Blocks/Materials/Default");

        foreach (Material m in defaultMaterials)
        {

        }
    }

    public void Update()
    {
        if (player.GetComponent<PlayerWorldView>().getLastHighlightedObject() != null)
        {
            if (player.GetComponent<PlayerWorldView>().getLastHighlightedObject().GetComponent<Renderer>().material != defaultMaterial)
            {
                player.GetComponent<PlayerWorldView>().getLastHighlightedObject().GetComponent<Renderer>().material = defaultMaterial;
            }
        }

        if (player.GetComponent<PlayerWorldView>().getHighlightedObject() != null)
        {
            if (player.GetComponent<PlayerWorldView>().getHighlightedObject().GetComponent<Renderer>().material != highlightMaterial)
            {
                player.GetComponent<PlayerWorldView>().getHighlightedObject().GetComponent<Renderer>().material = highlightMaterial;
            }
        }
    }
}
