using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Common;

public class RegisterPanel : MonoBehaviour {
    public InputField usernameIF;
    public InputField passwordIF;
    RegisterRequest registerrequest;

    public Text Tip;
    // Use this for initialization
    void Start () {
        registerrequest = GetComponent<RegisterRequest>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void DefaultRequest()
    {
        Tip.text = "";
        registerrequest.username = usernameIF.text;
        registerrequest.password = passwordIF.text;
        registerrequest.DefaultRequest();
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {       
        ResultCode result = (ResultCode)operationResponse.ReturnCode;
        if (result == ResultCode.Success)
        {
            Tip.text = "注册成功";
        }
        else
        {
            Tip.text = "用户名重复";
        }
    }
}
