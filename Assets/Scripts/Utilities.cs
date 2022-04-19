using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class Utilities : MonoBehaviour
{
    static string CSVPath = "RawData";
    static string parsePath = @"D:\ITHub\2\Unity Projects\SaveLoadLab\Assets\Resources\Save.txt";
    static string audioVolumePath = @"D:\ITHub\2\Unity Projects\SaveLoadLab\Assets\Resources\AudioSave.txt";

    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        // first task

        //ParseCSV(CSVPath, parsePath);
        //PersonData[] peopleData = Deserialize(parsePath);

        //foreach (var person in peopleData)
        //{
        //    Debug.Log($"{person.name} {person.description}");
        //}   

        // second task

        ReadAudioVolume(audioVolumePath);
    }

    private void Update()
    {
        WriteAudioVolume(audioVolumePath);
    }

    public static void ParseCSV(string CSVPath, string parsePath)
    {
        TextAsset rawDataCSV = Resources.Load(CSVPath) as TextAsset;

        string[] CSVStringFormat = rawDataCSV.text.Split('\n');
        List<PersonData> peopleData = new List<PersonData>();
        foreach (var person in CSVStringFormat)
        {
            string[] splitedData = person.Split(',');
            peopleData.Add(new PersonData(splitedData[0], splitedData[1]));
        }

        JsonSerializer serializer = new JsonSerializer();
        serializer.Formatting = Formatting.Indented;

        using (StreamWriter sw = new StreamWriter(parsePath))
        using (JsonWriter jsonWriter = new JsonTextWriter(sw))
        {
            serializer.Serialize(jsonWriter, peopleData);
        }
    }

    public static PersonData[] Deserialize(string parsedPath)
    {
        PersonData[] peopleData;
        using (StreamReader file = File.OpenText(parsedPath))
        {
            JsonSerializer serializer = new JsonSerializer();
            peopleData = serializer.Deserialize(file, typeof(PersonData[])) as PersonData[];

            return peopleData;
        }
    }

    public void ReadAudioVolume(string audioVolumePath)
    {
        if (!File.Exists(audioVolumePath))
            return;

        using (StreamReader file = File.OpenText(audioVolumePath))
        {
            JsonSerializer serializer = new JsonSerializer();
            audioSource.volume = float.Parse(serializer.Deserialize(file, typeof(string)) as string);
        }
    }

    public void WriteAudioVolume(string audioVolumePath)
    {
        JsonSerializer serializer = new JsonSerializer();
        serializer.Formatting = Formatting.Indented;

        using (StreamWriter sw = new StreamWriter(audioVolumePath))
        using (JsonWriter jsonWriter = new JsonTextWriter(sw))
        {
            serializer.Serialize(jsonWriter, audioSource.volume.ToString());
        }
    }
}
