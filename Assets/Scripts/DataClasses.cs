using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersonData
{
    public string name { get; private set; }
    public string description { get; private set; }

    public PersonData(string name, string description)
    {
        this.name = name;
        this.description = description;
    }
}