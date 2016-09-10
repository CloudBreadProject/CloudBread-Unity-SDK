using UnityEngine;
using System.Collections.Generic;


namespace CloudBread
{
    public class CBSetting : ScriptableObject
    {
        public string _serverAddress;
        static public string serverAddress { get { return instance._serverAddress; } }

        public string _apiVersion = "2.0.0";
        static public string apiVersion { get { return instance._apiVersion; } }

		// change authToken, read-only -> read-write
        public string _authToken = null;
		static public string authToken { get { return instance._authToken; } set{ instance._authToken = value; _cbHeader = null;  } }

        public bool _useEncrypt = false;
        static public bool useEncrypt { get { return instance._useEncrypt; } }
        public string _aesKey = null;
        static public string aesKey { get { return instance._aesKey; } }
        public string _aesIV = null;
        static public string aesIV { get { return instance._aesIV; } }

        static CBSetting _instance = null;
        static CBSetting instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = Resources.Load<CBSetting>("CB.Setting");
                }
                return _instance;
            }
        }

        static Dictionary<string, string> _cbHeader = null;
        static public Dictionary<string, string> cbHeader
        {
            get
            {
                if (null == _cbHeader)
                {
                    _cbHeader = new Dictionary<string, string>();

                    _cbHeader.Add("ZUMO-API-VERSION", apiVersion);
                    _cbHeader.Add("Accept", "application/json");
                    _cbHeader.Add("X-ZUMO-VERSION", "ZUMO/2.0 (lang=Managed; os=Windows Store; os_version=--; arch=X86; version=2.0.31217.0)");
                    _cbHeader.Add("X-ZUMO-FEATURES", "AJ");
                    _cbHeader.Add("X-ZUMO-INSTALLATION-ID", "fe52b710-0312-4cad-8d53-dfd28d4c6f9b");
                    _cbHeader.Add("Content-Type", "application/json");

                    if (!string.IsNullOrEmpty(authToken))
                    {
                        _cbHeader.Add("X-ZUMO-AUTH", authToken);
                    }
                }
                return _cbHeader;
            }
        }

        static WWWForm _cbForm = null;
        static public WWWForm cbForm
        {
            get
            {
                if (null == _cbForm)
                {
                    _cbForm = new WWWForm();
                    foreach (var element in cbHeader)
                    {
                        _cbForm.AddField(element.Key, element.Value);
                    }
                }
                return _cbForm;
            }
        }
    }
}