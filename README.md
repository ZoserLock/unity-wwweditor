# Unity Editor WWW
An utility class to be able to call WWW request inside the Unity Editor

#### Functions
```cs
    // Async requests
    EditorWWW.RequestAsync(string url, System.Action<bool, string> callback = null)
    EditorWWW.RequestAsync(string url, byte[] postData, System.Action<bool, string> callback = null)
    EditorWWW.RequestAsync(string url, byte[] postData, Dictionary<string, string> headers, System.Action<bool, string> callback = null)
    // Sync requests
    EditorWWW.RequestSync(string url, System.Action<bool, string> callback)
    EditorWWW.RequestSync(string url, byte[] postData, System.Action<bool, string> callback = null)
    EditorWWW.RequestSync(string url, byte[] postData, Dictionary<string, string> headers, System.Action<bool, string> callback = null)
```
#### Example code

```cs
    EditorWWW.RequestSync("http://example.org",(success, response)=>
    {
        if(success)
        {
            Debug.Log("Response: " + response);
        }
        else
        {
            Debug.LogError("Error: " + response);
        }
    });
```
