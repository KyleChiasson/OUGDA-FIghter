using System.IO;
using UnityEngine;

public static class SaveLoad
{
    public static void Save(Character c) => File.WriteAllText(Application.persistentDataPath + c.Name + ".json", JsonUtility.ToJson(c));
    public static Character Load(string characterName) => File.Exists(Application.persistentDataPath + characterName + ".json") ? JsonUtility.FromJson<Character>(File.ReadAllText(Application.persistentDataPath + characterName + ".json")) : null;
}