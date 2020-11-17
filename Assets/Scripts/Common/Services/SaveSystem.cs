using UnityEngine;
using System.IO;
using Assets.Scripts.Common.PlayerCommon;

namespace Assets.Scripts.Common.Services
{
    public static class SaveSystem
    {
        public static void SavePlayer(PlayerData data)
        {
            string path = Application.dataPath + "/player.sav";
            var json = JsonUtility.ToJson(data, true);

            if (File.Exists(path) == false)
            {
                File.Create(path);
            }

            File.WriteAllText(path, json);
        }
        public static PlayerData LoadPlayer()
        {
            string path = Application.dataPath + "/player.sav";
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                return JsonUtility.FromJson<PlayerData>(json);
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }
    }
}