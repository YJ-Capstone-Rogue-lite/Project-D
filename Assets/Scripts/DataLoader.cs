using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataLoader : MonoBehaviour
{
    public enum TEST { SAVE, LOAD };
    public TEST test;
    public string id;
    public string pw;

    void Start()
    {
        StartCoroutine(UnityWebRequestGETTest());
    }

    IEnumerator UnityWebRequestGETTest()
    {
        WWWForm form = new WWWForm();
        string url = "http://localhost:8181/ProjectD/";
        form.AddField("id", id);
        form.AddField("pw", pw);
        form.AddField("playerdata", JsonUtility.ToJson(DataManager.Instance.data));
        
        switch(test)
        {
            case TEST.SAVE:
                url += "DataSave.jsp";
            break;
            case TEST.LOAD:
                url += "DataLoad.jsp";
            break;
        }

        UnityWebRequest www = UnityWebRequest.Post(url, form);

        yield return www.SendWebRequest();  // 응답이 올때까지 대기한다.

        if (www.error == null)  // 에러가 나지 않으면 동작.
        {
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.LogError("WebRequestException: " + www.error);
        }
    }
}
