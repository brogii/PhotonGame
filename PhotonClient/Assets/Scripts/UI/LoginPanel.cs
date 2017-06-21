using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Common;
using UnityEngine.SceneManagement;

public class LoginPanel : MonoBehaviour {
    public Transform LoginPaneltrans;
    public Transform RegPaneltrans;
    // Use this for initialization
    public InputField usernameIF;
    public InputField passwordIF;
    LoginRequest loginrequest;

    public Text Tip;

    void Start () {
        loginrequest = GetComponent<LoginRequest>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void GoToReg() {
        LoginPaneltrans.DORotate(new Vector3(0, 90, 0), 1, RotateMode.FastBeyond360).SetEase(Ease.InOutExpo).OnComplete(delegate {
            RegPaneltrans.DORotate(new Vector3(0, 0, 0), 1, RotateMode.FastBeyond360).SetEase(Ease.InOutExpo);
        });
       
    }
    public void GoToLogin()
    {
        RegPaneltrans.DORotate(new Vector3(0, 90, 0), 1, RotateMode.FastBeyond360).SetEase(Ease.InOutExpo).OnComplete(delegate {
           LoginPaneltrans.DORotate(new Vector3(0, 0, 0), 1, RotateMode.FastBeyond360).SetEase(Ease.InOutExpo);
        });
    }
    public void DefaultRequest() {
        Tip.text = "";
       loginrequest.username = usernameIF.text;
       loginrequest.password = passwordIF.text;
       loginrequest.DefaultRequest();
    }

    public void OnOperationResponse(OperationResponse operationResponse) {
        ResultCode result = (ResultCode)operationResponse.ReturnCode;
        if (result == ResultCode.Success)
        {
            Tip.text = "登陆成功";

           
          
                SceneManager.LoadScene("02");
        
        }
        else {
            Tip.text = "用户名或密码错误";
        }
    }



}
