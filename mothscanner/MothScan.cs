using System.Reflection;
using UnityEngine;

namespace mothscanner
{
    public class MothScan : Modding.Mod
    {
        public enum GameDetected
        {
            UNDERHERO,
            HOLLOW_KNIGHT
        }

        public static GameDetected game = GameDetected.UNDERHERO;

        public static MothScan Instance = null;
        
        
        public override void Initialize()
        {
            Instance = this;
            GameObject go = new GameObject("Scouter 9000");
            mothtext mt = go.AddComponent<mothtext>();
            mt.drawAppend = "Underhero";
            if (typeof(Modding.ModHooks).GetField("_modVersion", BindingFlags.NonPublic | BindingFlags.Static) != null)
            {
                game = GameDetected.HOLLOW_KNIGHT;
                mt.drawAppend = "Hollow Knight";
            }
            Object.DontDestroyOnLoad(go);
        }

        public void LogPublic(string msg)
        {
            //Log(msg);
        }
    }
}