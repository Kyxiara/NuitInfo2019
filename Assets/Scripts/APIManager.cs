using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

[Serializable]
public class APIResponse
{
    public string sender;
    public string message;
}

public class APIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         Debug.Log(this.GetAPIInfo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private APIResponse GetAPIInfo()
	{
	    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://localhost:5005/webhooks/rest/webhook"));
	    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
	    StreamReader reader = new StreamReader(response.GetResponseStream());
	    string jsonResponse = reader.ReadToEnd();
	    APIResponse apiResponse = JsonUtility.FromJson<APIResponse>(jsonResponse);
	    return apiResponse;
	}
}
