using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace CloudBread
{
    public partial class CloudBread : CBInstance<CloudBread>
    {
        List<IEnumerator> _requestList;

        static public string Address { get { return CBSetting.serverAddress; } }
        static public Dictionary<string, string> cbHeader { get { return CBSetting.cbHeader; } }
        static public WWWForm cbForm { get { return CBSetting.cbForm; } }

        bool _retry = false;
        int _retryCount = 0;
        const int _maxRetry = 3;

        public override void OnInitialize()
        {
            _requestList = new List<IEnumerator>();
        }

        void Update()
        {
            if (0 < _requestList.Count && !_requestList[0].MoveNext())
            {
                if (_retry)
                {
                    _retry = false;
                    _requestList[0].Reset();
                }
                else
                {
                    _retryCount = 0;
                    _requestList.RemoveAt(0);
                    if (0 == _requestList.Count)
                    {
                        enabled = false;
                    }
                }
            }
        }

        bool RetryRequest()
        {
            Debug.Log("!!!" + _retryCount);
            if (_retryCount < _maxRetry)
            {
                ++_retryCount;
                _retry = true;
                return true;
            }
            else
            {
                _retry = false;
                return false;
            }
        }

        public static void Request(string path_, string postData_, System.Action callback_, System.Action<string> errorCallback_ = null)
        {
            instance.AddRequest(instance.WWWRequest(path_, postData_, callback_, errorCallback_));
        }
        public static void Request<T>(string path_, string postData_, System.Action<T> callback_, System.Action<string> errorCallback_ = null)
        {
            instance.AddRequest(instance.WWWRequest<T>(path_, postData_, callback_, errorCallback_));
        }
        public static void Request<T>(string path_, string postData_, System.Action<T[]> callback_, System.Action<string> errorCallback_ = null)
        {
            instance.AddRequest(instance.WWWRequest<T>(path_, postData_, callback_, errorCallback_));
        }

        public void AddRequest(IEnumerator itor_)
        {
            _requestList.Add(itor_);
            enabled = true;
        }

        IEnumerator WWWRequest(string path_, string postData_, System.Action callback_, System.Action<string> errorCallback_ = null)
        {
            IEnumerator itor = WWWProcess(path_, postData_,
                delegate (WWW www)
                {
                    if (null != callback_)
                    {
                        callback_();
                    }
                    www.Dispose();
                },
                delegate (string text_)
                {
                    if (null != errorCallback_)
                    {
                        errorCallback_(text_);
                    }
                }
            );

            while (itor.MoveNext())
            {
                yield return null;
            }
        }

        static public string _aseEncryptDefineHeader { get { return @"{""token"":"; } }
        static public string _aseEncryptDefine { get { return "token"; } }

        string ReceiveDataPostProcess(string text_)
        {
            if (CBSetting.useEncrypt || text_.Contains(_aseEncryptDefineHeader))
            {
                return CBAuthentication.AES_decrypt(CBTool.GetElementValueFromJson(_aseEncryptDefine, text_).Trim());
            }
            return text_.Trim();
        }

        // json 배열을 감싸는 리스트를 사용할것인가?
        //const string _listCover = @"{{ ""list"" : {0} }}";
        IEnumerator WWWRequest<T>(string path_, string postData_, System.Action<T[]> callback_, System.Action<string> errorCallback_ = null)
        {
            IEnumerator itor = WWWProcess(path_, postData_,
                delegate (WWW www)
                {
                    if (null != callback_)
                    {
                        string receiveText = ReceiveDataPostProcess(www.text);

                        try
                        {
                            // #ISSUE
                            // 배열[]이 바로 들어옴.
                            // 클래스에 입히기 위해 배열인 경우 맴버변수에 대한 선언이 필요함.
                            // ex) {"list" : 기존배열 }
                            // 위와 같이 감싸야함.
                            //string text = string.Format(_listCover, www.text);
                            //callback_(JsonUtility.FromJson<T>(text));

                            // 아니면 어쩌나..
                            // 파싱 후 리스트로..근데 항상 배열로 넘어오나? -> 그렇다함.
                            // 아니었음. 커뮤니케이션 미스. ㅠㅠ.

                            // 단일 객체는 따로 처리. 제너레이터 수정함. 추후에 변경되더라도 단일 객체가 배열로 리턴되도록 남겨 둠.
                            if (receiveText.StartsWith("{"))
                            {
                                callback_(new T[] { JsonUtility.FromJson<T>(receiveText) });
                            }
                            else // 배열.
                            {
                                int start = receiveText.IndexOf("[")+1;
                                int end = receiveText.LastIndexOf("]");
                                string receiveData = receiveText.Substring(start, end - start);

                                if (!string.IsNullOrEmpty(receiveData))
                                {
                                    // }, 로 split시에 맨뒤에 있는 항목은 }가 없음. 맨뒤에 } 제거한상태로 각 객체가 뒤에 }가 없게 처리.
                                    receiveData = receiveData.Substring(0, receiveData.LastIndexOf("}"));
                                    string[] list = System.Text.RegularExpressions.Regex.Split(receiveData, "},");
                                    T[] tList = new T[list.Length];
                                    for (int i = 0; i < list.Length; ++i)
                                    {
                                        //tList[i] = JsonUtility.FromJson<T>(list[i].EndsWith("}") ? list[i] : list[i]+"}");
                                        tList[i] = JsonUtility.FromJson<T>(list[i] + "}");
                                    }
                                    callback_(tList);
                                }
                                else
                                {
                                    callback_(null);
                                }
                            }
                        }
                        catch (System.Exception e)
                        {
                            string errorMessage = string.Format("{0}\nurl\n{1}\nwww.text\n{2}\ndata\n{3}\n{4}", "ERROR : Json Parse.", www.url, www.text, receiveText, e);
                            if (null != errorCallback_)
                            {
                                errorCallback_(errorMessage);
                            }
                        }
                    }
                    www.Dispose();
                },
                delegate (string text_)
                {
                    if (null != errorCallback_)
                    {
                        errorCallback_(text_);
                    }
                }
            );

            while (itor.MoveNext())
            {
                yield return null;
            }
        }

        // 단일 객체.
        IEnumerator WWWRequest<T>(string path_, string postData_, System.Action<T> callback_, System.Action<string> errorCallback_ = null)
        {
            IEnumerator itor = WWWProcess(path_, postData_,
                delegate (WWW www)
                {
                    if (null != callback_)
                    {
                        string receiveText = ReceiveDataPostProcess(www.text);

                        try
                        {
                            // 단일 객체.
                            callback_(JsonUtility.FromJson<T>(receiveText));
                        }
                        catch (System.Exception e)
                        {
                            //Debug.Log(errorMessage);
                            if (null != errorCallback_)
                            {
                                string errorMessage = string.Format("{0}\nurl\n{1}\nwww.text\n{2}\ndata\n{3}\n{4}", "ERROR : Json Parse.", www.url, www.text, receiveText, e);
                                errorCallback_(errorMessage);
                            }
                        }
                    }
                    www.Dispose();
                },
                delegate (string text_)
                {
                    if (null != errorCallback_)
                    {
                        errorCallback_(text_);
                    }
                }
            );

            while (itor.MoveNext())
            {
                yield return null;
            }
        }

        IEnumerator WWWProcess(string path_, string postData_, System.Action<WWW> completeCallback_, System.Action<string> errorCallback_)
        {
            WWW www = null;
            if (null != postData_)
            {
                if (CBSetting.useEncrypt)
                {
                    postData_ = string.Format(@"{{""{0}"": ""{1}""}}", _aseEncryptDefine, CBAuthentication.AES_encrypt(postData_));
                }
                www = new WWW(path_, System.Text.Encoding.UTF8.GetBytes(postData_), cbHeader);
            }
            else
            {
                www = new WWW(path_, cbForm);
            }

            while (!www.isDone)
            {
                yield return null;
            }

            if (null != www.error)
            {
                if(null != errorCallback_)
                {
                    //if(!RetryRequest())
                    {
                        Debug.LogError(www.error);
                        errorCallback_(www.error);
                    }
                }
            }
            else
            {
                if(null != completeCallback_)
                {
                    completeCallback_(www);
                }
            }

            www.Dispose();
        }

        public static string MakeFullUrl(string apiPath_)
        {
            if (CBSetting.serverAddress.EndsWith("/"))
            {
                return CBSetting.serverAddress + apiPath_;
            }
            else
            {
                return string.Format("{0}/{1}", CBSetting.serverAddress, apiPath_);
            }
        }
    }
}