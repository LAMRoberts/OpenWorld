using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("BlockCollection")]
public class BlockContainer
{
    [XmlArray("Blocks")]
    [XmlArrayItem("Block")]
    public List<Blocks> blocks = new List<Blocks>();

    public static BlockContainer Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(BlockContainer));

        StringReader reader = new StringReader(_xml.text);

        BlockContainer blocks = serializer.Deserialize(reader) as BlockContainer;

        reader.Close();

        return blocks;
    }
}
