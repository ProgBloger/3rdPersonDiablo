using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService
{
    private const string localApi = "http://localhost/uia/api.php";
    private const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mode=xml&appid=2526aed69b8e737bc1f8567811d6b698";
    private const string webImage = "http://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";
    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage);
        yield return request.SendWebRequest();
        callback(DownloadHandlerTexture.GetContent(request));
    }
    
    private IEnumerator CallAPI(string url, WWWForm form, Action<string> callback)
    {
        using (UnityWebRequest request = (form == null) ? UnityWebRequest.Get(url) : UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"network problem: {request.error}");
            }
            else if(request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"response error: {request.responseCode}");
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetWeatherXML(Action<string> callback){
        return CallAPI(xmlApi, null, callback);
    }

    public IEnumerator LogWeather(string name, float cloudValue, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("message", name);
        form.AddField("cloud_value", cloudValue.ToString());
        form.AddField("timestamp", DateTime.UtcNow.Ticks.ToString());

        return CallAPI(localApi, form, callback);
    }
}
