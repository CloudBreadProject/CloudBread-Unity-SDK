using UnityEngine;
using System.Collections;


namespace CloudBread
{
    /// <summary>
    /// CloudBread Test Call.
    /// </summary>
    /// <typeparam name="T">receiveDataType</typeparam>
    public class TestCall<T> : MonoBehaviour where T : struct
    {
        [SerializeField]
        public T _receiveData;

        public void Callback(T item_)
        {
            _receiveData = item_;
            OnReceive(item_);
        }

        virtual public void OnReceive(T item_) { }
    }

    /// <summary>
    /// CloudBread Test Call.
    /// </summary>
    /// <typeparam name="T">receiveDataType</typeparam>
    public class TestCallArray<T> : MonoBehaviour where T : struct
    {
        [SerializeField]
        public T[] _receiveData;

        public void Callback(T[] item_)
        {
            _receiveData = item_;
            OnReceive(item_);
        }

        virtual public void OnReceive(T[] item_) { }
    }

    /// <summary>
    /// CloudBread Test Call.
    /// </summary>
    /// <typeparam name="T1">postDataType</typeparam>
    /// <typeparam name="T2">receiveDataType</typeparam>
    public class TestCall<T1, T2> : TestCall<T2> where T1 : struct where T2 : struct
    {
        [SerializeField]
        public T1 _postData;

        virtual public void ErrorCallback_(string error_)
        {
            Debug.Log(error_);
        }
    }

    /// <summary>
    /// CloudBread Test Call.
    /// </summary>
    /// <typeparam name="T1">postDataType</typeparam>
    /// <typeparam name="T2">receiveDataType</typeparam>
    public class TestCallArray<T1, T2> : TestCallArray<T2> where T1 : struct where T2 : struct
    {
        [SerializeField]
        public T1 _postData;

        virtual public void ErrorCallback_(string error_)
        {
            Debug.Log(error_);
        }
    }
}