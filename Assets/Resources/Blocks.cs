using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class Block
{
    [XmlAttribute("Name")]
    public string blockName;

    [XmlElement("xPos")]
    public float xPos;

    [XmlElement("yPos")]
    public float yPos;

    [XmlElement("zPos")]
    public float zPos;
}
