using System.IO;
using UnityEngine;

namespace Game
{
    public class GameData
    {
        // Saved data
        public uint level;

        // Singleton pattern :)
        private static GameData _instance;

        public static bool LoadData()
        {
            var fullPath = Path.Combine(Application.persistentDataPath, "game_data.json");

            try
            {
                string result = File.ReadAllText(fullPath);
                _instance = JsonUtility.FromJson<GameData>(result);
                return true;
            }
            catch
            {
                Debug.Log("No save file found. Creating a new one on next save.");
                _instance = new GameData();
                return false;
            }
        }

        public static void SaveData()
        {
            var fullPath = Path.Combine(Application.persistentDataPath, "game_data.json");
            var json = JsonUtility.ToJson(_instance);

            try
            {
                File.WriteAllText(fullPath, json);
            }
            catch
            {
                // ugly, ugly, ugly
                Debug.Log("I guess we couldn't save your data... sucks to be you :)");
            }
        }

        public static uint GetLevel()
        {
            if (_instance == null)
                LoadData();
            
            return _instance.level;
        }

        public static void SetLevel(uint level)
        {
            _instance.level = level;
            SaveData(); // Always save when completing a level
        }
    }
}