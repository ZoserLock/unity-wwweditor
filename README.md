# Unity Editor WWW
An utility class to be able to call WWW request inside the Unity Editor

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
