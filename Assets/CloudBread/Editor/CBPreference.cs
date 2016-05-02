using UnityEngine;
using UnityEditor;
using System.Collections;


namespace CloudBread
{
    [InitializeOnLoad]
    public class CBPreference
    {
        //static CBPreference()
        //{
        //    Debug.Log("Welcome. CloudBread.");
        //}

        [PreferenceItem("CloudBread")]
        static void OnGUI()
        {
            GUILayout.Box("CloudBread");
            if(GUILayout.Button("Test"))
            {
                Debug.Log("Hello CloudBread.");
            }
        }
    }
}