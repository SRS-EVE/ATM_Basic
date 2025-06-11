using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PopupLogin : MonoBehaviour
{
    UserData userData;

    [SerializeField] private List<TMP_InputField> loginField;
    [SerializeField] private List<TMP_InputField> signUpField;

    // Start is called before the first frame update
    void Start()
    {
        /*string path = Application.persistentDataPath;
        string[] files = Directory.GetFiles(path, "*.json");

        foreach (string file in files)
        {
            File.Delete(file);
            Debug.Log("삭제됨: " + file);
        }*/
    }
    public void login()
    {
        string userId = loginField[0].text;
        string userPassword = loginField[1].text;
        userData = GameManager.instance.LoadPlayerDataToJson(userId);

        if(userData == null)
        {
            // 팝업띄우기
            GameManager.instance.Popup("회원정보가 없습니다.");
            return;
        }
        else
        {
            if (userPassword == userData.customerPassword)
            {
                GameManager.instance.LoginSuccess(userId);
                Debug.Log("불러온 유저: " + userData.customerName);
            }
            else
            {
                // 팝업띄우기
                GameManager.instance.Popup("비밀번호가 틀렸습니다.");
            }
        }        
    }
    public void SignUpWindow()
    {
        GameManager.instance.SignUpBTN();
    }
    public void SignUp() // 회원가입 함수
    {
        // userdata생성자값을 field에서 받기
        userData = new UserData($"{signUpField[0].text}", $"{signUpField[1].text}", $"{signUpField[2].text}", int.Parse(signUpField[4].text), int.Parse(signUpField[5].text));

        // 만약한값이라도빠졌다면
        foreach (var field in signUpField)
        {
            if(string.IsNullOrEmpty(field.text))
            {
                GameManager.instance.Popup("모든 정보를 입력해주세요");
                return;
            }
        }

        // 만약패스워드 확인이 틀렸다면
        if (signUpField[2].text != signUpField[3].text)
        {
            GameManager.instance.Popup("비밀번호 확인이 일치하지 않습니다.");
            return;
        }

        GameManager.instance.SavePlayerDataToJson(userData, true);

        string jsonData = JsonUtility.ToJson(userData); // UserData 객체를 JSON 문자열로 변환
        string path = Application.persistentDataPath + $"/{userData.customerId}.json"; // 저장할 파일 경로 설정 (운영체제에 따라 자동 지정되는 경로)
        File.WriteAllText(path, jsonData); // 파일에 JSON 데이터를 실제로 쓰기 (덮어쓰기)
        GameManager.instance.SignUpSuccess();
    }
}
