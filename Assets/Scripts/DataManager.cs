using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    // ---싱글톤으로 선언--- //
    static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "DataManager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;
                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }

    // --- 게임 데이터 파일이름 설정 ("원하는 이름(영문).json") --- //
    public string GameDataFileName = "SaveData.json";

    // --- 저장용 클래스 변수 --- //
    public PlayerData data = new PlayerData();



    public enum TEST { CHECK, CREATE, SAVE, LOAD };
    public string id;
    public string pw;

    void OnApplicationQuit() => SaveGameData();
    
    public void CreateUser(string id, string pw, Action<bool> action)
    {
        this.id = id;
        this.pw = pw;
        StartWebRequest(TEST.CREATE, (x) => {
            switch(x)
            {
                case "false":
                    action(false);
                break;
                case "true":
                    action(true);
                break;
            }
        });
    }

    public void LoginUser(string id, string pw, Action<bool> action)
    {
        this.id = id;
        this.pw = pw;
        StartWebRequest(TEST.CHECK, (x) => {
            switch(x)
            {
                case "false":
                    action(false);
                break;
                case "true":
                    action(true);
                break;
            }
        });
    }

    // 불러오기
    public void LoadGameData()
    {
        StartWebRequest(TEST.LOAD, (x) => {
            instance.data = JsonUtility.FromJson<PlayerData>(x);
            if(x == "false")
            {
                string filePath = Application.persistentDataPath + "/" + GameDataFileName;
                print(filePath);
                // 저장된 게임이 있다면
                if (File.Exists(filePath))
                {
                    // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
                    string FromJsonData = File.ReadAllText(filePath);
                    data = JsonUtility.FromJson<PlayerData>(FromJsonData);
                    print("불러오기 완료");
                    // 불러온 데이터 출력
                }
            }
            SaveGameData();
        });
    }

    // 저장하기
    public bool SaveGameData()
    {
        // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
        File.WriteAllText(filePath, ToJsonData);
        // 올바르게 저장됐는지 확인 (자유롭게 변형)
        print("저장 완료");

        bool temp = false;
        StartWebRequest(TEST.SAVE, (x) => {
            switch(x)
            {
                case "false":
                    temp = false;
                break;
                case "true":
                    temp = true;
                break;
            }
        });
        return temp;
    }
    
    void StartWebRequest(TEST test, Action<string> action = null)
    {
        var coroutineWR = StartCoroutine(UnityWebRequestGETTest(test, action));
        StartCoroutine(Waitting(coroutineWR, action));
    }

    IEnumerator Waitting(Coroutine WR, Action<string> action)
    {
        yield return new WaitForSecondsRealtime(10f);
        if(WR == null) yield break;
        StopCoroutine(WR);
        action("false");
    }
    IEnumerator UnityWebRequestGETTest(TEST test, Action<string> action = null)
    {
        WWWForm form = new WWWForm();
        string url = "http://localhost:8181/ProjectD/";
        form.AddField("id", id);
        form.AddField("pw", pw);
        form.AddField("playerdata", JsonUtility.ToJson(DataManager.Instance.data));
        
        switch(test)
        {
            case TEST.CHECK:
                url += "LoginCheck.jsp";
            break;
            case TEST.CREATE:
                url += "CreateUser.jsp";
            break;
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
            action(www.downloadHandler.text);
        }
        else
        {
            Debug.LogError("WebRequestException: " + www.error);
        }
    }
}