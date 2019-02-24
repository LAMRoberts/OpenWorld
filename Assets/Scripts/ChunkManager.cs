using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ChunkManager : MonoBehaviour
{
    string fileName;

    [SerializeField]
    private List<string> blocks;

    StreamReader reader;

    public List<Transform> blocksToDelete;

    void Start()
    {
        // set file to read and write from
        fileName = "Assets/ChunkData/" + transform.position.x.ToString() + " " + transform.position.z.ToString() + ".txt";

        if (File.Exists(fileName))
        {
            reader = new StreamReader(fileName);
        }
        else
        {
            reader = new StreamReader("Assets/ChunkData/default.txt");
        }

        GameObject brick = (GameObject)Resources.Load("Blocks/Brick", typeof(GameObject));
        GameObject cobble = (GameObject)Resources.Load("Blocks/Cobble", typeof(GameObject));
        GameObject dirt = (GameObject)Resources.Load("Blocks/Dirt", typeof(GameObject));
        GameObject glass = (GameObject)Resources.Load("Blocks/Glass", typeof(GameObject));
        GameObject grass = (GameObject)Resources.Load("Blocks/Grass", typeof(GameObject));
        GameObject log = (GameObject)Resources.Load("Blocks/Log", typeof(GameObject));
        GameObject sand = (GameObject)Resources.Load("Blocks/Sand", typeof(GameObject));        
        GameObject stone = (GameObject)Resources.Load("Blocks/Stone", typeof(GameObject));
        GameObject wood = (GameObject)Resources.Load("Blocks/Wood", typeof(GameObject));
        GameObject air = (GameObject)Resources.Load("Air", typeof(GameObject));

        // list of blocks in this chunk
        blocks = new List<string>();

        // read file
        using (reader)
        {
            // while not f'd up
            while (true)
            {
                // read line
                string line = reader.ReadLine();

                // add to list or break
                if (line != null)
                {
                    blocks.Add(line);
                }
                else
                { 
                    break;
                }
            }
        }
        
        // instanciate blocks
        int i = 0;
        foreach (Transform block in transform)
        {
            switch (blocks[i])
            {
                case "brick":
                    {
                        Instantiate(brick, block);
                        break;
                    }
                case "cobble":
                    {
                        Instantiate(cobble, block);
                        break;
                    }
                case "dirt":
                    {
                        Instantiate(dirt, block);
                        break;
                    }
                case "glass":
                    {
                        Instantiate(glass, block);
                        break;
                    }
                case "grass":
                    {
                        Instantiate(grass, block);
                        break;
                    }
                case "log":
                    {
                        Instantiate(log, block);
                        break;
                    }
                case "sand":
                    {
                        Instantiate(sand, block);
                        break;
                    }
                case "stone":
                    {
                        Instantiate(stone, block);
                        break;
                    }
                case "wood":
                    {
                        Instantiate(wood, block);
                        break;
                    }
                default:
                    {
                        Instantiate(air, block);
                        break;
                    }
            }

            i++;
        }
    }

    private void Update()
    {
        if (blocksToDelete.Count > 0)
        {
            foreach (Transform blockNode in blocksToDelete)
            {
                DestroyBlock(blockNode);
            }

            blocksToDelete.Clear();
        }
    }

    private void DestroyBlock(Transform worldBlockNode)
    {
        Transform worldBlock = worldBlockNode.transform.GetChild(0);
        int blockID = worldBlockNode.transform.GetSiblingIndex();

        Destroy(worldBlock.gameObject);

        fileName = "Assets/ChunkData/" + transform.position.x.ToString() + " " + transform.position.z.ToString() + ".txt";

        if (!File.Exists(fileName))
        {
            FileUtil.CopyFileOrDirectory("Assets/ChunkData/default.txt", fileName);
        }

        string[] lines = File.ReadAllLines(fileName);
        lines[blockID - 1] = "air";
        File.WriteAllLines(fileName, lines);

        Debug.Log("Murdered a " + worldBlock.name + " block at " + fileName);
    }
}