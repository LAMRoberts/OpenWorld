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

    void Start()
    {
        Debug.Log(transform.position);

        // set file to read and write from
        fileName = transform.position.x.ToString() + " " + transform.position.z.ToString();
        
        if (!File.Exists("Assets/StreamingAssets/" + fileName + ".txt"))
        {
            FileUtil.CopyFileOrDirectory("Assets/StreamingAssets/default.txt", "Assets/StreamingAssets/" + fileName + ".txt");

            Debug.Log(fileName);
        }

        reader = new StreamReader("Assets/StreamingAssets/" + fileName + ".txt");
               
        GameObject stone = (GameObject)Resources.Load("Blocks/Stone", typeof(GameObject));
        GameObject air = (GameObject)Resources.Load("Blocks/Air", typeof(GameObject));

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
            if (blocks[i] == "1")
            {
                Instantiate(stone, block);
            }
            else
            {
                Instantiate(air, block);
            }

            i++;
        }
    }
}
