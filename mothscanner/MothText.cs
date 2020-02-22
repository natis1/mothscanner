using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace mothscanner
{
    public class mothtext : MonoBehaviour
    {
        

        private GUIStyle style;
        // What text should be drawn
        private string drawString = "WARNING!\nGIANT CUTE MOTH GOD DETECTED\n";
        public string drawAppend = "";
        public bool draw = false;

        private void Start()
        {
            SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
            SceneManager.activeSceneChanged += SceneManagerOnactiveSceneChanged;
            MothScan.Instance.LogPublic("Setup Mothtext");
        }

        private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            MothScan.Instance.LogPublic("Detected Unity scene change " + arg0.name);
            switch (MothScan.game)
            {
                case MothScan.GameDetected.HOLLOW_KNIGHT:
                    draw = (arg0.name != "Menu_Title");
                    break;
                case MothScan.GameDetected.UNDERHERO:
                    draw = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void SceneManagerOnactiveSceneChanged(Scene arg0, Scene arg1)
        {
            MothScan.Instance.LogPublic("Detected Unity scene change " + arg0.name + " -> " + arg1.name);
            switch (MothScan.game)
            {
                case MothScan.GameDetected.HOLLOW_KNIGHT:
                    draw = (arg1.name != "Menu");
                    break;
                case MothScan.GameDetected.UNDERHERO:
                    draw = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnGUI()
        {
            Color c1 = Color.black;
            Color c2 = Color.yellow;
            if (!draw)
            {
                c1 = Color.clear;
                c2 = Color.clear;
            }
            // First, make sure our style is loaded. this contains important information like the font used.
            if (style == null)
            {
                style = new GUIStyle(GUI.skin.label);
            }

            // A Matrix4x4 is a unity object that allows us to place images and text overlaying the game.
            Matrix4x4 matrix = GUI.matrix;
            
            // Set our text to the current color we have selected
            GUI.backgroundColor = c1;
            GUI.contentColor = c2;
            GUI.color = c2;
            
            
            // This essentially creates a 1920x1080 pixel screen on which to draw the text
            // This will overlay the game screen.
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
                new Vector3(Screen.width / 1920f, Screen.height / 1080f, -1f));
            
            // We want our text to appear in the middle of the screen, some number of pixels down from the top.
            // This alignment centers our text for us
            style.alignment = TextAnchor.UpperCenter;
            style.fontSize = 32;
            
            // The first two numbers represent our x and y positions for the text since it is anchored to the upper middle
            // For the upper-middle of the screen we can set x to 0 and y to around 300 pixels. This is the number of pixels
            // from the upper middle to move right and then down respectively.
            GUI.Label(new Rect((float) 0f, 300f, 1920f, 1080f), drawString + drawAppend, style);
            GUI.matrix = matrix;
        }
    }
}