using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SatelliteChunkManager : MonoBehaviour
{
    string fileName;

    [SerializeField]
    private List<string> blocks;

    StreamReader reader;

    public List<Transform> blocksToDelete;

    void Start()
    {
        // set file to read and write from
        fileName = "Assets/ChunkData/Satellite/" + transform.position.x.ToString() + " " + transform.position.z.ToString() + ".txt";

        if (File.Exists(fileName))
        {
            reader = new StreamReader(fileName);
        }
        else
        {
            reader = new StreamReader("Assets/ChunkData/Satellite/default.txt");
        }

        GameObject building = (GameObject)Resources.Load("Satellite/building", typeof(GameObject));
        //GameObject road = (GameObject)Resources.Load("Satellite/road", typeof(GameObject));
        //GameObject grassB = (GameObject)Resources.Load("Satellite/grassB", typeof(GameObject));
        //GameObject river = (GameObject)Resources.Load("Satellite/river", typeof(GameObject));
        //GameObject water = (GameObject)Resources.Load("Satellite/water", typeof(GameObject));
        GameObject forest = (GameObject)Resources.Load("Satellite/forest", typeof(GameObject));
        //GameObject house = (GameObject)Resources.Load("Satellite/house", typeof(GameObject));
        //GameObject sandB = (GameObject)Resources.Load("Satellite/sandB", typeof(GameObject));
        GameObject air = (GameObject)Resources.Load("air", typeof(GameObject));

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
                case "BUILDING":
                    {
                        Instantiate(building, block);
                        break;
                    }
                //case "road":
                //    {
                //        Instantiate(road, block);
                //        break;
                //    }
                //case "grassB":
                //    {
                //        Instantiate(grassB, block);
                //        break;
                //    }
                //case "river":
                //    {
                //        Instantiate(river, block);
                //        break;
                //    }
                //case "water":
                //    {
                //        Instantiate(water, block);
                //        break;
                //    }
                case "FOREST":
                    {
                        Instantiate(forest, block);
                        break;
                    }
                //case "house":
                //    {
                //        Instantiate(house, block);
                //        break;
                //    }
                //case "sandB":
                //    {
                //        Instantiate(sandB, block);
                //        break;
                //    }
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

        fileName = "Assets/ChunkData/Satellite/" + transform.position.x.ToString() + " " + transform.position.z.ToString() + ".txt";

        if (!File.Exists(fileName))
        {
            FileUtil.CopyFileOrDirectory("Assets/ChunkData/Satellite/default.txt", fileName);
        }

        string[] lines = File.ReadAllLines(fileName);
        lines[blockID - 1] = "air";
        File.WriteAllLines(fileName, lines);

        Debug.Log("Murdered a " + worldBlock.name + " block at " + fileName);
    }
}