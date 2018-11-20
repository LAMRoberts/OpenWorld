using UnityEngine;
using UnityEditor;
using System.IO;

public class ChunkManager : MonoBehaviour
{
    public GameObject stone;

    void Start ()
    {
        //GameObject stone = (GameObject)AssetDatabase.LoadAssetAtPath(("Assets/Prefabs/Stone"), typeof(GameObject));

        int i = 0;

		foreach (Transform block in transform)
        {
            string path = "Assets/StreamingAssets/0";
            
            Instantiate(stone, block.position, Quaternion.identity);

            i++;
        }
	}
}
