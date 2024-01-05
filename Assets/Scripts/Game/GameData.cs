using System.IO;
using UnityEngine;

public class GameData
{
    // Saved data
    private uint _level;

    // Singleton pattern :)
    private static GameData _instance;

    public static bool LoadData()
    {
        var fullPath = Path.Combine(Application.persistentDataPath, "game_data.json");

        try {
            string result = File.ReadAllText(fullPath);
            _instance = JsonUtility.FromJson<GameData>(result);
            return true;
        } catch {
            Debug.Log("No save file found. Creating a new one on next save.");
            _instance = new GameData();
            return false;
        }
    }

    public static void SaveData()
    {
        var fullPath = Path.Combine(Application.persistentDataPath, "game_data.json");
        var json = JsonUtility.ToJson(_instance);

        try {
            File.WriteAllText(fullPath, json);
        } catch {
            // not great, but we'll see if this ever happens
            Debug.Log("An error occured while saving the game data.");
        }
    }

    public static uint GetLevel()
    {
        if (_instance == null)
            LoadData();

        return _instance._level;
    }

    public static void SetLevel(uint level)
    {
        _instance._level = level;
        SaveData(); // Always save when completing a level
    }
}
