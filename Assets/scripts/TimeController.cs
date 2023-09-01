using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

public class TimeController : MonoBehaviour
{

    public void GetMoscowTime()
    {
        StartCoroutine(GetData_Coroutine());
    }

    IEnumerator GetData_Coroutine()
    {
        string url = "http://www.unn.ru/time/";
        //string url = "https://time100.ru/";
        //string url = "https://www.example.com";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            //string[] pages = url.Split('/');
            //int page = pages.Length - 1;
            
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    //Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    //Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Regex regex = new Regex("\\b\\d{2}:\\d{2}:\\d{2}\\b");
                    MatchCollection matches = regex.Matches(webRequest.downloadHandler.text);
                    string jsCode = $"alert('{matches[0].Value}');";
                    Application.ExternalEval(jsCode);
                    break;
            }
        }
    }
}