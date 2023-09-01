using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

public class TimeController : MonoBehaviour
{
    private string jsCode;

    public void GetMoscowTime()
    {
        StartCoroutine(GetData_Coroutine());

        if(jsCode!= null)
        {
            Application.ExternalEval(jsCode);
        }
    }

    private IEnumerator GetData_Coroutine()
    {
        string url = "http://www.unn.ru/time/";
        
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log(webRequest.error);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log(webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log(webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:

                    Regex regex = new Regex("\\b\\d{2}:\\d{2}:\\d{2}\\b");
                    MatchCollection matches = regex.Matches(webRequest.downloadHandler.text);

                    jsCode = $"alert('{matches[0].Value}');";

                    Debug.Log("Complite");
                    break;
            }
        }
    }
}