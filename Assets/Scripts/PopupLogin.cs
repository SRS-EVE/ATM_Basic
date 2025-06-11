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
            Debug.Log("������: " + file);
        }*/
    }
    public void login()
    {
        string userId = loginField[0].text;
        string userPassword = loginField[1].text;
        userData = GameManager.instance.LoadPlayerDataToJson(userId);

        if(userData == null)
        {
            // �˾�����
            GameManager.instance.Popup("ȸ�������� �����ϴ�.");
            return;
        }
        else
        {
            if (userPassword == userData.customerPassword)
            {
                GameManager.instance.LoginSuccess(userId);
                Debug.Log("�ҷ��� ����: " + userData.customerName);
            }
            else
            {
                // �˾�����
                GameManager.instance.Popup("��й�ȣ�� Ʋ�Ƚ��ϴ�.");
            }
        }        
    }
    public void SignUpWindow()
    {
        GameManager.instance.SignUpBTN();
    }
    public void SignUp() // ȸ������ �Լ�
    {
        // userdata�����ڰ��� field���� �ޱ�
        userData = new UserData($"{signUpField[0].text}", $"{signUpField[1].text}", $"{signUpField[2].text}", int.Parse(signUpField[4].text), int.Parse(signUpField[5].text));

        // �����Ѱ��̶󵵺����ٸ�
        foreach (var field in signUpField)
        {
            if(string.IsNullOrEmpty(field.text))
            {
                GameManager.instance.Popup("��� ������ �Է����ּ���");
                return;
            }
        }

        // �����н����� Ȯ���� Ʋ�ȴٸ�
        if (signUpField[2].text != signUpField[3].text)
        {
            GameManager.instance.Popup("��й�ȣ Ȯ���� ��ġ���� �ʽ��ϴ�.");
            return;
        }

        GameManager.instance.SavePlayerDataToJson(userData, true);

        string jsonData = JsonUtility.ToJson(userData); // UserData ��ü�� JSON ���ڿ��� ��ȯ
        string path = Application.persistentDataPath + $"/{userData.customerId}.json"; // ������ ���� ��� ���� (�ü���� ���� �ڵ� �����Ǵ� ���)
        File.WriteAllText(path, jsonData); // ���Ͽ� JSON �����͸� ������ ���� (�����)
        GameManager.instance.SignUpSuccess();
    }
}
