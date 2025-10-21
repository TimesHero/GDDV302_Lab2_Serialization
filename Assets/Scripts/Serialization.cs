using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

using UnityEngine;

public class Serialization : MonoBehaviour
{
    public GameObject playerUIPrefab;
    public string filename = "Lab2.xml";
    public List<PlayerData> playerDataList = new();

    private string[] prefixArray = { "Ar", "Bel", "Cor", "Dan", "El", "Fen", "Gal", "Hor" };
    private string[] middleArray = { "a", "en", "or", "is", "us", "on", "ir", "an" };
    private string[] suffixArray = { "dor", "mir", "nus", "ral", "thos", "vin", "zor" };

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            playerDataList.Add(new PlayerData()
            {
                playerName = prefixArray[Random.Range(0, prefixArray.Length)] +
                    middleArray[Random.Range(0, middleArray.Length)] +
                    suffixArray[Random.Range(0, suffixArray.Length)],
                healthRemaining = Mathf.Round(Random.value * 100) / 100,
                position = Random.insideUnitCircle * 4
            });
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreatePlayers();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveToXML();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadFromXML();
        }
    }

    // 5 MARKS
    private void SaveToXML()
    {
        // Build a file path string that points to the persistent data path using the filename provided.
        string path = Application.persistentDataPath + "/" + filename;
        Debug.Log($"The Save file, <color=green>{filename}</color> has been assigned the file path of: <color=lightblue>{path}</color>.");

        // Set up the tools needed to handle XML serialization.
        XmlSerializer playerNameSerializer = new XmlSerializer(typeof(List<PlayerData>));
        Debug.Log($"<color=orange>Player Name Serializer prepared.</color>");

        // Prepare a writer to hold the serialized data in memory.
        StringWriter xmlMemoryBuffer = new StringWriter();
        Debug.Log($"XML Memory Buffer String prepared.");

        // Serialize the data in playerDataList to that writer.
        playerNameSerializer.Serialize(xmlMemoryBuffer, playerDataList);
        Debug.Log($"Player Data List Serialized: <color=yellow>{xmlMemoryBuffer.ToString()}</color>");

        // Write the final serialized output to the file path.
        File.WriteAllText(path, xmlMemoryBuffer.ToString());
        Debug.Log($"<color=green>{filename}</color> saved to: <color=lightblue>{path}</color>.");
    }

    // 5 MARKS
    private void LoadFromXML()
    {
        // Build a file path string that points to the persistent data path using the filename provided.
        string path = Application.persistentDataPath + "/" + filename;
        Debug.Log($"The Save file, <color=green>{filename}</color> should be found at: <color=lightblue>{path}</color>.");

        // Set up the tools to handle XML deserialization.
        XmlSerializer playerNameSerializer = new XmlSerializer(typeof(List<PlayerData>));
        Debug.Log($"<color=orange>Player Name Serializer prepared.</color>");

        // Read all the contents from the file path.
        string xmlText;
        if (File.Exists(path))
        {
            xmlText = System.IO.File.ReadAllText(path);
            Debug.Log($"The save file, <color=green>{filename}</color>, has been found.");
        }
        else
        {
            Debug.LogWarning($"Save File does not exist!");
            return;
        }

        // Prepare a reader for the string 
        StringReader reader = new StringReader(xmlText);
        Debug.Log($"StringReader prepared.");

        // Deserialize the contents into playerDataList.
        var loadedList = (List<PlayerData>)playerNameSerializer.Deserialize(reader);
        playerDataList = loadedList;
        Debug.Log($"List Loaded!");

    }

    // 5 MARKS
    private void CreatePlayers()
    {
        // Using whichever method you like (query or method syntax), use Linq to create an array of PlayerData called eligiblePlayers
        // that contains only players whose healthRemaining property is greater than 0.3.

        // For each PlayerData retrieved and stored in eligiblePlayers, instantiate a new playerUIPrefab and set its position, 
        // playerName text, and healthFill fill amount accordingly. Refer to PlayerUIController for property details.
    }
}
