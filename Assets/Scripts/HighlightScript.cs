using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        if (player.GetComponent<PlayerWorldView>().GetLastHighlightedObject() != null)
        {
            player.GetComponent<PlayerWorldView>().GetLastHighlightedObject().GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        if (player.GetComponent<PlayerWorldView>().GetHighlightedObject() != null)
        {
            player.GetComponent<PlayerWorldView>().GetHighlightedObject().GetComponent<Renderer>().material.color = new Color(0.5f, 0.75f, 1.0f, 1.0f);          
        }
    }
}