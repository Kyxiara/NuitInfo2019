using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;
using UnityEngine.Networking;

public class Model
{
    public string text { get; set; }
    public string custom { get; set; }
    public string quest { get; set; }
    public string type { get; set; }
}

public class APIManager : MonoBehaviour
{
    private string idForServer;

    // Start is called before the first frame update
    void Start()
    {
        idForServer = this.GetAPIId();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string POST(string bot_id, string message = "")
    {
        string res = "";
        string jsonContent = "{" +
            "\"user_id\" : \"" + idForServer + "\"," +
            "\"message\" : \"" + message + "\"," +
            "\"bot_id\" : \"" + bot_id + "\"" +
            "}";

        string url = "http://mlg.swan-blanc.fr/api/get_message";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";

        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        Byte[] byteArray = encoding.GetBytes(jsonContent);

        request.ContentLength = byteArray.Length;
        request.ContentType = @"application/json";

        using (Stream dataStream = request.GetRequestStream())
        {
            dataStream.Write(byteArray, 0, byteArray.Length);
        }

        try
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.  
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.  
                    res = reader.ReadToEnd();
                    // Display the content.  
                    //Debug.Log(res);
                }
            }
        }
        catch (WebException ex)
        {
            // Log exception and throw as for GET example above
        }
        return res;
    }


    private string GetAPIId()
	{
	    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://mlg.swan-blanc.fr/api/get_id"));
	    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
	    StreamReader reader = new StreamReader(response.GetResponseStream());
	    string id = reader.ReadToEnd();
	    return id;
	}
}
