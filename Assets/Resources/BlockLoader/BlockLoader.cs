using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLoader : MonoBehaviour
{
    public string path = "BlockLoader/defaultChunk";

	private void Start ()
    {
        BlockContainer bc = BlockContainer.Load(path);

        foreach (Blocks block in bc.blocks)
        {
            print(block.blockName + " " + block.xPos + " " + block.yPos + " " + block.zPos);
        }
	}

    private void Update()
    {
        BlockContainer bc = BlockContainer.Load(path);

        if (bc.blocks.Count < 10)
        {
            Blocks newBlock = new Blocks
            {
                blockName = "Dirt",
                xPos = 0,
                yPos = 0,
                zPos = 0
            };

            bc.blocks.Add(newBlock);
        }
    }
}
