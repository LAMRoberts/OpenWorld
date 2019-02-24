using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class NPCManager : MonoBehaviour
{
    string fileName;

    [SerializeField]
    private List<Vector3> npcs;

    StreamReader reader;

    void Start ()
    {
        fileName = "Assets/NPCData/" + transform.position.x.ToString() + " " + transform.position.z.ToString() + ".txt";

        if (fileName != "Assets/NPCData/default.txt")
        {
            if (!File.Exists(fileName))
            {
                reader = new StreamReader("Assets/NPCData/default.txt");
            }
            else
            {
                reader = new StreamReader(fileName);
            }
        }
        else
        {
            Debug.Break();
        }

        GameObject npcPrefab = (GameObject)Resources.Load("NPC", typeof(GameObject));

        npcs = new List<Vector3>();
                     
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
                    string[] positionString = line.Split(","[0]);
                                       
                    Vector3 npcPos = new Vector3(float.Parse(positionString[0]), float.Parse(positionString[1]), float.Parse(positionString[2]));
                    
                    npcs.Add(npcPos);
                }
                else
                {
                    break;
                }
            }
        }

        int i = 0;
        foreach (Vector3 position in npcs)
        {
            GameObject npc = Instantiate(npcPrefab, transform);

            npc.transform.position = new Vector3(transform.position.x + npcs[i].x, transform.position.y + npcs[i].y, transform.position.z + npcs[i].z);

            i++;
        }
    }
}
