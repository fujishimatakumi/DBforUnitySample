using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking; //ネットワーク関連に使用
public class APIManager : MonoBehaviour
{
    User _user;
    [SerializeField] Text _idText;
    [SerializeField] Dropdown _idDropdown;
    [SerializeField] InputField _nameField;
    [SerializeField] InputField _castumField;

    [SerializeField] Button _updateButton;
    [SerializeField] Button _getButton;
    // Start is called before the first frame update
    void Start()
    {
        InitDropDown();
        _getButton.onClick.AddListener(() => UserDataRequest());
        _updateButton.onClick.AddListener(() => UserUpdatRequest());
    }

    public void InitDropDown()
    {
        //今回はURL直打ちだけど工夫すると便利
        SendGetRequest("http://localhost/PHPSample/GetIdLastNum.php", num => SetDropDown(int.Parse(num)));
    }
    public void SetDropDown(int num)
    {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        for (int i = 1; i < num + 1 ; i++)
        {
            Dropdown.OptionData data = new Dropdown.OptionData();
            data.text = $"id:{i}";
            options.Add(data);
        }
        _idDropdown.options = options;
    }

    public void UserDataRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", _idDropdown.value + 1);

        SendPOSTRequest("http://localhost/PHPSample/GetUserInfo.php", form,
            json => {_user =  JsonUtility.FromJson<User>(json);
            _idText.text = (_idDropdown.value + 1).ToString();
            _nameField.text = _user._name;
            _castumField.text = _user._castumData;
        });

        
    }

    public void UserUpdatRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", _idDropdown.value + 1);
        form.AddField("name", _nameField.text);
        form.AddField("castumdata", _castumField.text);

        SendPOSTRequest("http://localhost/PHPSample/UpdateUserInfo.php", form, null);

    }

    public void SendPOSTRequest(string api, WWWForm form, Action<string> onConpleat)
    {
        StartCoroutine(PostRequest(api, form, onConpleat));
    }

    public void SendGetRequest(string api, Action<string> onConpleat)
    {
        StartCoroutine(GetRequest(api,onConpleat));
    }
    //POSTリクエスト用コルーチン
    IEnumerator PostRequest(string api,WWWForm form,Action<string> onConpleat)
    {   
        UnityWebRequest request = UnityWebRequest.Post(api, form);

        yield return request.SendWebRequest();

        if (request.isHttpError)
        {
            Debug.LogError("httpError=" + request.error);
            yield break;
        }

        Debug.Log(request.downloadHandler.text);

        onConpleat?.Invoke(request.downloadHandler.text);
    }

    //GETリクエスト用コルーチン
    IEnumerator GetRequest(string api,Action<string> onConpleat)
    {
        UnityWebRequest request = UnityWebRequest.Get(api);

        yield return request.SendWebRequest();

        if (request.isHttpError)
        {
            Debug.LogError("httpError=" + request.error);
            yield break;
        }

        Debug.Log(request.downloadHandler.text);

        onConpleat?.Invoke(request.downloadHandler.text);
    }
}
