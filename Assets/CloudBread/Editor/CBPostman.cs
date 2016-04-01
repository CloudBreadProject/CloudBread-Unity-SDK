using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


namespace CloudBread
{
    public class CBPostman : EditorWindow
    {
        static CBPostman _instance = null;
        [MenuItem("CloudBread/CB-PostMan")]
        public static void InitWindow()
        {
            if (null == _instance)
            {
                _instance = GetWindow<CBPostman>();
            }
            else
            {
                _instance.Close();
            }
        }
        const string _serchStyle = "SearchTextField";
        const string _boxStyle = "GroupBox";//"MeTransitionBlock";
        const string _selectStyle = "WarningOverlay";

        void OnGUI()
        {
            GUILayout.BeginVertical();
            {
                DrawTitle();
                DrawBody();
            }
            GUILayout.EndVertical();
            DrawFoot();
        }

        void DrawTitle()
        {
            GUILayout.Box("CloudBread - Simple Postman.", "LODLevelNotifyText");
        }

        void DrawFoot()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("OpenSource Project - CloudBread.", GUI.skin.label))
                {
                    Application.OpenURL("https://github.com/CloudBreadProject/");
                }
            }
            GUILayout.EndHorizontal();
        }

        const float _leftBodySize = 0.35f;
        Postman.Request _selectedRequest = null;
        Vector2 _leftBodyScrollPos = Vector2.zero;
        string _serchText = "";
        void DrawBody()
        {
            DrawBodyMenu();

            GUILayout.BeginHorizontal();
            {
                DrawBodyLeft();
                DrawBodyRight();
            }
            GUILayout.EndHorizontal();
        }

        private void DrawBodyLeft()
        {
            GUILayout.BeginVertical(GUILayout.Width(position.width * _leftBodySize));
            {
                if (null != _postman)
                {
                    _serchText = GUILayout.TextArea(_serchText, _serchStyle);
                    _leftBodyScrollPos = GUILayout.BeginScrollView(_leftBodyScrollPos);
                    {
                        foreach (var element in _postman.requests)
                        {
                            if (element.name.ToLower().Contains(_serchText.ToLower()))
                            {
                                if (null != _selectedRequest && element.name.Equals(_selectedRequest.name))
                                {
                                    GUILayout.Box(element.name, _selectStyle);
                                }
                                else if (GUILayout.Button(element.name))
                                {
                                    _selectedRequest = element;
                                    _requestPostData = element.rawModeData;
                                    _requestHeaders = element.headers;
                                    ResetBodyRight();
                                }
                            }
                        }
                    }
                    GUILayout.EndScrollView();
                }
            }
            GUILayout.EndVertical();
        }

        string _requestPostData = null;
        string _requestHeaders = null;
        string _receiveJson = null;
        string _receiveStruct = null;
        Vector2 _classBodyScroll_rquestDataPos = Vector2.zero;
        Vector2 _classBodyScroll_receiveJsonPos = Vector2.zero;
        Vector2 _classBodyScroll_receiveStructPos = Vector2.zero;

        void ResetBodyRight()
        {
            _receiveJson = null;
            _receiveStruct = null;
            _classBodyScroll_rquestDataPos = Vector2.zero;
            _classBodyScroll_receiveJsonPos = Vector2.zero;
            _classBodyScroll_receiveStructPos = Vector2.zero;
        }

        string[] _requestMenu = new string[] { "Body", "Headers" };
        int _selectRequestMenuIndex = 0;
        private void DrawBodyRight()
        {
            GUILayout.BeginVertical();
            {
                if (null != _postman && null != _selectedRequest)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Box(_selectedRequest.method, _boxStyle);
                        GUILayout.Box(_selectedRequest.url, _boxStyle, GUILayout.Width(position.width * 0.4f));
                        if (GUILayout.Button("Send"))
                        {
                            ResetBodyRight();
                            RequestPostmanTest(_requestPostData, _requestHeaders, _selectedRequest, delegate (Postman.Request request_, string text_)
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(text_))
                                    {
                                        _receiveJson = text_;
                                        _receiveStruct = MakeStructFromJson("Receive" + request_.name, "struct", text_);
                                    }
                                }
                                catch (System.Exception e_)
                                {
                                    Debug.Log(e_);
                                }
                            });
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginVertical();
                    {
                        _selectRequestMenuIndex = GUILayout.SelectionGrid(_selectRequestMenuIndex, _requestMenu, 2);
                        GUILayout.Box(_requestMenu[_selectRequestMenuIndex], _boxStyle);
                        _classBodyScroll_rquestDataPos = GUILayout.BeginScrollView(_classBodyScroll_rquestDataPos, GUILayout.Height(100));
                        {
                            if (0 == _selectRequestMenuIndex)
                            {
                                _requestPostData = GUILayout.TextArea(_requestPostData);
                            }
                            else
                            {
                                _requestHeaders = GUILayout.TextArea(_requestHeaders);
                            }
                        }
                        GUILayout.EndScrollView();
                    }
                    GUILayout.EndVertical();
                }

                if(!string.IsNullOrEmpty(_receiveJson))
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Box("Receive Json", _boxStyle);
                        if (GUILayout.Button("Copy to Clipboard"))
                        {
                            TextEditor te = new TextEditor();
                            te.text = _receiveJson;
                            te.OnFocus();
                            te.Copy();
                        }
                    }
                    GUILayout.EndHorizontal();

                    _classBodyScroll_receiveJsonPos = GUILayout.BeginScrollView(_classBodyScroll_receiveJsonPos, GUILayout.Height(position.height * 0.2f));
                    {
                        //EditorGUILayout.SelectableLabel(_receiveJson);
                        GUILayout.TextArea(_receiveJson);
                    }
                    GUILayout.EndScrollView();
                }

                if (!string.IsNullOrEmpty(_receiveStruct))
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Box("Receive Struct form Json", _boxStyle);
                        if (GUILayout.Button("Copy to Clipboard"))
                        {
                            TextEditor te = new TextEditor();
                            te.text = _receiveStruct;
                            te.OnFocus();
                            te.Copy();
                        }
                    }
                    GUILayout.EndHorizontal();

                    _classBodyScroll_receiveStructPos = GUILayout.BeginScrollView(_classBodyScroll_receiveStructPos);
                    {
                        GUILayout.TextField(_receiveStruct);
                    }
                    GUILayout.EndScrollView();
                }
            }
            GUILayout.EndVertical();
        }

        private string MakeStructFromJson(string structName_, string structType_, string jsonText_)
        {
            string[] element = ExtractElementFromJson(jsonText_);
            string variable = "";

            for (int i = 0; i < element.Length; ++i)
            {
                variable += string.Format("        [SerializeField]\n        public string {0};\n", element[i]);
            }

            string body = CBTool.GetClassTextFile();

            return string.Format(body, structType_, structName_, variable);
        }

        string[] ExtractElementFromJson(string json_)
        {
            string text = json_;

            // 배열로 들어온거면 배열 처리 뺌. 동일한 형식으로[{}, {}] 들어온다고 가정하고, 처음{}형식의 요소들만 추출.
            if (json_.StartsWith("["))
            {
                text = text.Remove(json_.Length - 1).Remove(0, 1);
            }

            // 처음 들어온 {} 안의 내용만 추출.
            int startIndex = text.IndexOf('{') + 1;
            int endIndex = text.IndexOf('}', startIndex);
            text = text.Substring(startIndex, endIndex - startIndex);
            string[] list = text.Split(',');

            for(int i = 0; i < list.Length; ++i)
            {
                startIndex = list[i].IndexOf('"') + 1;
                endIndex = list[i].IndexOf('"', startIndex);
                list[i] = list[i].Substring(startIndex, endIndex - startIndex);
            }

            return list;
        }

        void RequestPostmanTest(string postdata_, string headers_, Postman.Request request_, System.Action<Postman.Request, string> callback_)
        {
            try
            {
                DirectWWW(new WWW(request_.url, System.Text.Encoding.UTF8.GetBytes(postdata_), GenerateHeader(headers_)), request_, callback_);
            }
            catch (System.Exception e_)
            {
                Debug.LogError(string.Format("[{0}], {1}", request_.name, e_));
            }
        }

        static Dictionary<string, string> GenerateHeader(string header_)
        {
            Dictionary<string, string> head = new Dictionary<string, string>();
            string[] elements = header_.Split('\n');

            foreach (var element in elements)
            {
                // Accpt로 시작하는건 일단 생략. zip압축
                if (!element.StartsWith("Accept") && element.Contains(": "))
                {
                    string[] var = System.Text.RegularExpressions.Regex.Split(element, ": ");
                    head.Add(var[0], var[1]);
                }
            }

            return head;
        }

        void DirectWWW(WWW www, Postman.Request request_, System.Action<Postman.Request, string> callback_)
        {
            while (!www.isDone) { }

            if (null != www.error)
            {
                Debug.LogWarning(string.Format("[{0}] - {1}", www.url, www.error));
            }
            else if (null != callback_)
            {
                string text = System.Text.Encoding.UTF8.GetString(www.bytes);
                if (request_.name.StartsWith("Encrypt"))
                {
                    int endIndex = text.LastIndexOf('"');
                    int startIndex = text.LastIndexOf('"', endIndex - 1);
                    string token = text.Substring(startIndex + 1, endIndex - startIndex - 1);

                    try
                    {
                        string result = CBAuthentication.AES_decrypt(token);
                        callback_(request_, result);
                    }
                    catch
                    {
                        Debug.Log(token);
                        Debug.LogWarning(string.Format("[{0}] - {1}", request_.name, text));
                        throw;
                    }
                }
                else
                {
                    callback_(request_, text);
                }
            }
            www.Dispose();
        }

        string _loadedPostmanFileFullPath = "";
        string _loadedPostmanFileName = "";
        Postman _postman;

        void DrawBodyMenu()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(_loadedPostmanFileName);
                if (GUILayout.Button("Import"))
                {
                    string fileFullpath = EditorUtility.OpenFilePanel("CloudBread", "", "");
                    if (!string.IsNullOrEmpty(fileFullpath))
                    {
                        string json = CBTool.ReadFile(fileFullpath);
                        try
                        {
                            Postman postman = JsonUtility.FromJson<Postman>(json);
                            _postman = postman;
                            _loadedPostmanFileFullPath = fileFullpath;
                            _loadedPostmanFileName = System.IO.Path.GetFileName(_loadedPostmanFileFullPath);
                        }
                        catch (System.Exception e_)
                        {
                            Debug.LogError(e_);
                        }
                    }
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}