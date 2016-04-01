using UnityEngine;
using System.Collections;


namespace CloudBread
{
    [System.Serializable]
    public class Postman
    {
        [SerializeField]
        string id;

        [SerializeField]
        string name;

        [SerializeField]
        string description;

        [SerializeField]
        string[] order;

        [SerializeField]
        string timestamp;

        [SerializeField]
        string owner;

        [SerializeField]
        bool hasRequests;

        [System.Serializable]
        public class Request
        {
            [SerializeField]
            public string id;
            [SerializeField]
            public string headers;
            [SerializeField]
            public string url;
            [SerializeField]
            public string preRequestScript;
            //[SerializeField]
            //string pathVariables;
            [SerializeField]
            public string method;
            //[SerializeField]
            //string[] data;
            [SerializeField]
            public string dataMode;
            [SerializeField]
            public string version;
            [SerializeField]
            public string tests;
            [SerializeField]
            public string currentHelper;
            //[SerializeField]
            //string helperAttributes;
            [SerializeField]
            public string time;
            [SerializeField]
            public string name;
            [SerializeField]
            public string description;
            [SerializeField]
            public string collectionId;
            //[SerializeField]
            //string responses;
            [SerializeField]
            public string rawModeData;
        }

        [SerializeField]
        public Request[] requests;
    }
}