using UnityEngine;
using System.Collections;


namespace CloudBread
{
    public class CBInstance<T> : MonoBehaviour where T : Component
    {
        static T _instance = null;
        static public T instance
        {
            get
            {
                if (null == _instance)
                {
                    CreateInstance();
                }
                return _instance;
            }
        }

        static void CreateInstance()
        {
            if (null == _instance)
            {
                _instance = FindObjectOfType<T>();
                if (null == _instance)
                {
                    GameObject go = GameObject.Find("CloudBread");
                    if (null == go)
                    {
                        go = new GameObject("CloudBread");
                    }
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<T>();
                    (_instance as CBInstance<T>).OnInitialize();
                }
            }
        }

        virtual public void OnInitialize() { }
    }
}