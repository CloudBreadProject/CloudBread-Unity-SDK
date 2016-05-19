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

        const string _serchCancelStyle = "SearchCancelButton";
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
        string _selectRequestName = null;
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
                    GUILayout.BeginHorizontal();
                    {
                        _serchText = GUILayout.TextArea(_serchText, _serchStyle, GUILayout.Width(position.width * _leftBodySize - 10));

                        if (GUILayout.Button("", _serchCancelStyle))
                        {
                            _serchText = string.Empty;
                        }
                    }
                    GUILayout.EndHorizontal();
                    _leftBodyScrollPos = GUILayout.BeginScrollView(_leftBodyScrollPos);
                    {
                        foreach (var element in _postman.requests)
                        {
                            if (element.name.ToLower().Contains(_serchText.ToLower()))
                            {
                                string title = string.Format("[{0}] {1}", element.method, element.name);
                                if (null != _selectedRequest && element.Equals(_selectedRequest))
                                {
                                    GUILayout.Box(title, _selectStyle);
                                }
                                else if (GUILayout.Button(title))
                                {
                                    _selectedRequest = element;
                                    _selectRequestName = _selectedRequest.name.Replace(" ", "");
                                    _selectRequestName = _selectRequestName.Replace("-", "_");
                                    int index = element.url.IndexOf("api/");
                                    if (!CBSetting.serverAddress.EndsWith("/"))
                                    {
                                        --index;
                                    }
                                    _requestURL = element.url.Substring(index);

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

        string _requestURL = null;
        string _requestFullURL { get { return _useMyServer ? CBSetting.serverAddress + _requestURL : _selectedRequest.url; } }
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
        bool _useMyServer = false;
        private void DrawBodyRight()
        {
            GUILayout.BeginVertical();
            {
                if (null != _postman && null != _selectedRequest)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Box(_selectedRequest.method, _boxStyle);
                        EditorGUILayout.TextArea(_requestFullURL, _boxStyle, GUILayout.Width(400));

                        _useMyServer = GUILayout.Toggle(_useMyServer, "Use CB.Setting Server");

                        if (GUILayout.Button("Send"))
                        {
                            ResetBodyRight();
                            RequestPostmanTest(_requestFullURL, _requestPostData, _requestHeaders, delegate (string text_)
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(text_))
                                    {
                                        _receiveJson = text_;
                                        _receiveStruct = GenerateStruct(text_, "Receive");
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

                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Box(_requestMenu[_selectRequestMenuIndex], _boxStyle);

                            if (0 == _selectRequestMenuIndex && GUILayout.Button("struct Print", GUILayout.Width(100)))
                            {
                                string genStructStr = GenerateStruct(_requestPostData, "Post");
                                Debug.Log(genStructStr);
                            }

                            if (0 == _selectRequestMenuIndex && GUILayout.Button("Decrypt Print", GUILayout.Width(100)))
                            {
                                string decryptStr = CBAuthentication.AES_decrypt(CBTool.GetElementValueFromJson(CloudBread._aseEncryptDefine, _requestPostData));
                                Debug.Log(decryptStr);
                            }

                            if (0 == _selectRequestMenuIndex && GUILayout.Button("Generate Protocol", GUILayout.Width(150)))
                            {
                                string url = _selectedRequest.url.Substring(_selectedRequest.url.IndexOf("api/"));
                                string postStruct = GenerateStruct(_requestPostData, "Post");
                                string receiveStruct = GenerateStruct(_requestPostData, "Receive");

                                // postData가 있는 경우/없는경우.
                                RequestPostmanTest(_requestFullURL, _requestPostData, _requestHeaders, delegate (string text_)
                                {
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(text_))
                                        {
                                            string body = CBToolEditor.GetClassTextFile("Template.CBClass");
                                            receiveStruct = GenerateStruct(text_, "Receive");

                                            string fileText = string.Format(body, _selectRequestName, url, postStruct, receiveStruct, string.IsNullOrEmpty(postStruct) ? postStruct : "Post postData_", null == receiveStruct ? null : text_.StartsWith("[") ? "<Receive[]>" : "<Receive>", string.IsNullOrEmpty(postStruct) ? "null" : "JsonUtility.ToJson(postData_)");
                                            string fileName = string.Format("/CloudBread/Protocols/CloudBread.{0}.{1}.cs", _selectedRequest.method, _selectRequestName);

                                            CBToolEditor.SaveTextFileInProject(fileName, fileText);
                                            AssetDatabase.Refresh();
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

                        _classBodyScroll_rquestDataPos = GUILayout.BeginScrollView(_classBodyScroll_rquestDataPos, GUILayout.Height(100));
                        {
                            if (0 == _selectRequestMenuIndex)
                            {
                                _requestPostData = EditorGUILayout.TextArea(_requestPostData);
                            }
                            else
                            {
                                _requestHeaders = EditorGUILayout.TextArea(_requestHeaders);
                            }
                        }
                        GUILayout.EndScrollView();
                    }
                    GUILayout.EndVertical();
                }

                if(!string.IsNullOrEmpty(_receiveJson))
                {
                    _classBodyScroll_receiveJsonPos = GUILayout.BeginScrollView(_classBodyScroll_receiveJsonPos, GUILayout.Height(position.height * 0.2f));
                    {
                        EditorGUILayout.TextArea(_receiveJson);
                    }
                    GUILayout.EndScrollView();
                }

                if (!string.IsNullOrEmpty(_receiveStruct))
                {
                    _classBodyScroll_receiveStructPos = GUILayout.BeginScrollView(_classBodyScroll_receiveStructPos);
                    {
                        EditorGUILayout.TextArea(_receiveStruct);
                    }
                    GUILayout.EndScrollView();
                }
            }
            GUILayout.EndVertical();
        }

        // struct or class.
        const string _structType = "struct";
        private string GenerateStruct(string postData_, string header_)
        {
            if (string.IsNullOrEmpty(postData_))
                return string.Empty;

            if (postData_.Contains(CloudBread._aseEncryptDefine))
            {
                string token = CBAuthentication.AES_decrypt(CBTool.GetElementValueFromJson(CloudBread._aseEncryptDefine, postData_));
                return MakeStructFromJson(header_, _structType, token);
            }
            else
            {
                return MakeStructFromJson(header_, _structType, postData_);
            }
        }

        /// <summary>
        /// json에 있는 element를 string변수로 갖는 클래스나 구조체형태의 텍스트 생성.
        /// </summary>
        /// <param name="structName_">클래스/구조체 이름.</param>
        /// <param name="structType_">class or struct.</param>
        /// <param name="jsonText_">json Text.</param>
        /// <returns></returns>
        static string MakeStructFromJson(string structName_, string structType_, string jsonText_)
        {
            structName_ = structName_.Replace(" ", string.Empty);
            if (structName_.Contains("-"))
            {
                structName_ = structName_.Replace("-", "_");
            }

            string[] element = ExtractElementFromJson(jsonText_);
            if(null == element)
            {
                return null;
            }

            string variable = "";

            for (int i = 0; i < element.Length; ++i)
            {
                variable += string.Format("            [SerializeField]\n            public string {0};\n", element[i]);
            }
            string body = CBToolEditor.GetClassTextFile("Template.Class");
            return string.Format(body, structType_, structName_, variable);
        }

        /// <summary>
        /// json으로부터 element 추출.
        /// </summary>
        /// <param name="jsonText_">json text.</param>
        /// <returns></returns>
        static string[] ExtractElementFromJson(string jsonText_)
        {
            string text = jsonText_;

            // 배열로 들어온거면 배열 처리 뺌. 동일한 형식으로[{a}, {a}] 들어온다고 가정하고, 처음{}형식의 요소들만 추출.
            if (jsonText_.StartsWith("["))
            {
                text = text.Remove(jsonText_.Length - 1).Remove(0, 1);
            }

            // 처음 들어온 {} 안의 내용만 추출. 없으면 항목 없음.
            if (!text.Contains("{"))
            {
                return null;
            }
            int startIndex = text.IndexOf('{') + 1;
            int endIndex = text.IndexOf('}', startIndex);
            text = text.Substring(startIndex, endIndex - startIndex);

            // postman에 있을 주석 제거.
            if (text.Contains("//"))
            {
                using (var reader = new System.IO.StringReader(text))
                {
                    //text = "";
                    string newText = "";
                    while (true)
                    {
                        string strLine = reader.ReadLine();
                        if (null == strLine)
                        {
                            break;
                        }
                        if (strLine.Contains("//"))
                        {
                            startIndex = strLine.IndexOf("//");
                            strLine = strLine.Remove(startIndex);
                        }
                        newText += strLine;
                    }
                    text = newText;
                }
            }

            // element단위로 이름만 추출.
            string[] list = text.Split(',');
            List<string> result = new List<string>();
            try
            {
                for (int i = 0; i < list.Length; ++i)
                {
                    list[i] = list[i].Trim();
                    if (!string.IsNullOrEmpty(list[i]))
                    {
                        startIndex = list[i].IndexOf('"') + 1;
                        endIndex = list[i].IndexOf('"', startIndex);
                        result.Add(list[i].Substring(startIndex, endIndex - startIndex));
                    }
                }
            }
            catch
            {
                Debug.Log(string.Format("Error.{0}/{1}", startIndex, endIndex));
                foreach (var element in list)
                {
                    Debug.Log(element);
                }
            }

            return result.ToArray();
        }

        void RequestPostmanTest(string url_, string postdata_, string headers_, System.Action<string> callback_)
        {
            try
            {
                byte[] post = null;
                if (!string.IsNullOrEmpty(postdata_))
                {
                    post = System.Text.Encoding.UTF8.GetBytes(postdata_);
                }
                DirectWWW(new WWW(url_, post, GenerateHeader(headers_)), callback_);
            }
            catch (System.Exception e_)
            {
                Debug.LogError(string.Format("[{0}], {1}", url_, e_));
            }
        }

        static Dictionary<string, string> GenerateHeader(string header_)
        {
            Dictionary<string, string> head = new Dictionary<string, string>();
            string[] elements = header_.Split('\n');

            foreach (var element in elements)
            {
                // 주석 제외.
                if (element.StartsWith("//"))
                    continue;

                // Encoding 일단 생략. zip압축
                if (!element.StartsWith("Accept-Encoding") && element.Contains(": "))
                {
                    string[] var = System.Text.RegularExpressions.Regex.Split(element, ": ");
                    head.Add(var[0], var[1]);
                }
            }

            return head;
        }

        /// <summary>
        /// IEnumerator가 아닌 에디터를 위한 WWW 동기코드.
        /// </summary>
        /// <param name="www">WWW.</param>
        /// <param name="request_">요청된 포스트맨의 정보.</param>
        /// <param name="callback_"></param>
        static void DirectWWW(WWW www, System.Action<string> callback_)
        {
            while (!www.isDone) { }

            if (null != www.error)
            {
                Debug.LogWarning(string.Format("[{0}] - {1}", www.url, www.error));
            }
            else if (null != callback_)
            {
                string text = System.Text.Encoding.UTF8.GetString(www.bytes);

                // ISSUE - 무조건 token이 encrypy되어 날라오는건지. 확인해야함.
                if (text.Contains(CloudBread._aseEncryptDefine))
                {
                    string token = CBTool.GetElementValueFromJson(CloudBread._aseEncryptDefine, text);
                    try
                    {
                        text = CBAuthentication.AES_decrypt(token);
                    }
                    catch
                    {
                        Debug.Log(token);
                        Debug.LogWarning(string.Format("{0} - {1}", www.url, text));
                        throw;
                    }
                }
                callback_(text);
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
                        string json = CBToolEditor.ReadFile(fileFullpath);
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