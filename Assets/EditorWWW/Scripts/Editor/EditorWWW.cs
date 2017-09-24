using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorWWW
{
    private static int sTimeoutTimeMs = 30 * 1000;
    private static string sTimeoutMessage = "TIMEOUT";

    private WWW _request;
    private bool _finished;
    private bool _success;
    private System.Action<bool,string> _callback;

    #region Get/Set

    public bool Finished
    {
        get { return _finished; }
    }

    public bool Success
    {
        get { return _success; }
    }

    public WWW Request
    {
        get { return _request; }
    }

    #endregion

    public static EditorWWW RequestAsync(string url, System.Action<bool, string> callback = null)
    {
        return new EditorWWW(url, false, callback);
    }

    public static EditorWWW RequestAsync(string url, byte[] postData, System.Action<bool, string> callback = null)
    {
        return new EditorWWW(url, postData, false, callback);
    }

    public static EditorWWW RequestAsync(string url, byte[] postData, Dictionary<string, string> headers, System.Action<bool, string> callback = null)
    {
        return new EditorWWW(url,postData, headers, false, callback);
    }

    public static EditorWWW RequestSync(string url, System.Action<bool, string> callback)
    {
        return new EditorWWW(url, true, callback);
    }

    public static EditorWWW RequestSync(string url, byte[] postData, System.Action<bool, string> callback = null)
    {
        return new EditorWWW(url, postData, true, callback);
    }

    public static EditorWWW RequestSync(string url, byte[] postData, Dictionary<string, string> headers, System.Action<bool, string> callback = null)
    {
        return new EditorWWW(url, postData, headers, true, callback);
    }

    private EditorWWW(string url, bool sync, System.Action<bool,string> callback = null)
    {
        _finished = false;
        _success = false;
        _callback = callback;
        _request = new WWW(url);
        Execute(sync);
    }

    private EditorWWW(string url,byte[] postData, bool sync, System.Action<bool, string> callback)
    {
        _finished = false;
        _success  = false;
        _callback = callback;
        _request  = new WWW(url, postData);
        Execute(sync);
    }

    private EditorWWW(string url, byte[] postData, Dictionary<string,string> headers, bool sync, System.Action<bool, string> callback)
    {
        _finished = false;
        _success  = false;
        _callback = callback;
        _request  = new WWW(url, postData, headers);
        Execute(sync);
    }

    private void Execute(bool sync)
    {
        if (sync)
        {
            DateTime start = DateTime.Now;
            while (true)
            {
                TimeSpan duration = DateTime.Now - start;
                if (duration.TotalMilliseconds > sTimeoutTimeMs)
                {
                    FinishRequest(true);
                    break;
                }

                if (!Request.MoveNext())
                {
                    FinishRequest(false);
                    break;
                }
            }
        }
        else
        {
            EditorApplication.update += Update;
        }
    }

    private void Update()
    {
        if(!Request.MoveNext())
        {
            FinishRequest(false);
            EditorApplication.update -= Update;
        }
    }

    private void FinishRequest(bool hasTimeout)
    {
        if (_request.isDone)
        {
            _finished = true;
            string response = "";
            if (hasTimeout)
            {
                _success = false;
                response = sTimeoutMessage;
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.error))
                {
                    _success = false;
                    response = _request.error;
                }
                else
                {
                    _success = true;
                    response = _request.text;
                }
            }

            if(_callback != null)
            {
                _callback(_success, response);
            }
        }
    }
}
