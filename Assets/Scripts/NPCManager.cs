using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class NPCManager : MonoBehaviour
{
    const string fileName = "Assets/NPCData/NPCData.txt";

    [SerializeField]
    private List<Vector3> allNPCs;
    [SerializeField]
    private List<Vector3> loadedNPCs;

    readonly GameObject npcPrefab = (GameObject)Resources.Load("NPC", typeof(GameObject));

    void Update ()
    {
        StreamReader reader = new StreamReader(fileName);

        allNPCs = new List<Vector3>();
                     
        // make list of positions from file
        using (reader)
        {
            while (true)
            {
                string line = reader.ReadLine();

                if (line != null)
                {
                    string[] positionString = line.Split(","[0]);
                                       
                    Vector3 npcPos = new Vector3(float.Parse(positionString[0]), float.Parse(positionString[1]), float.Parse(positionString[2]));
                    
                    allNPCs.Add(npcPos);
                }
                else
                {
                    break;
                }
            }
        }

        reader.Close();
    }
       
    void LoadNPC(Vector3 pos)
    { 
        if (!loadedNPCs.Contains(pos))
        {
            GameObject npc = Instantiate(npcPrefab, transform);
        }
    }
}
