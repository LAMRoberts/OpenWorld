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
    StreamWriter writer;

    void Start()
    {
        // set file to read and write from
        fileName = "Assets/StreamingAssets/" + transform.position.x.ToString() + " " + transform.position.z.ToString() + ".txt";
        
        if (!File.Exists(fileName))
        {
            FileUtil.CopyFileOrDirectory("Assets/StreamingAssets/default.txt", fileName);
        }

        reader = new StreamReader(fileName);

        GameObject stone = (GameObject)Resources.Load("Blocks/Stone", typeof(GameObject));
        GameObject dirt = (GameObject)Resources.Load("Blocks/Dirt", typeof(GameObject));
        GameObject grass = (GameObject)Resources.Load("Blocks/Grass", typeof(GameObject));
        GameObject air = (GameObject)Resources.Load("Air", typeof(GameObject));

        // list of blocks in this chunk
        blocks = new List<string>();

        // read file
        using (reader)
        {
            // while not fucked up
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
        
        int i = 0;
        foreach (Transform block in transform)
        {
            switch (blocks[i])
            {
                case "stone":
                    {
                        Instantiate(stone, block);
                        break;
                    }
                case "dirt":
                    {
                        Instantiate(dirt, block);
                        break;
                    }
                case "grass":
                    {
                        Instantiate(grass, block);
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

    public void DestroyBlock(Transform worldBlockNode)
    {
        Transform worldBlock = worldBlockNode.transform.GetChild(0);
        int blockID = worldBlockNode.transform.GetSiblingIndex();

        Destroy(worldBlock.gameObject);

        string[] lines = File.ReadAllLines(fileName);
        lines[blockID - 1] = "air";
        File.WriteAllLines(fileName, lines);

        Debug.Log(fileName.ToString());
    }
}
