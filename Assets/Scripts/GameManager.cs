using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // ���ӸŴ��� �̱���ȭ

    private UserData userData; // ���������� ����

    [SerializeField] private TextMeshProUGUI nameText; // ���̸� inspector�Ҵ�
    [SerializeField] private TextMeshProUGUI cashText; // ���� inspector�Ҵ�
    [SerializeField] private TextMeshProUGUI balanceText; // �����ܾ� inspector�Ҵ�

    [SerializeField] private GameObject popupPanel; // �˾� ������Ʈ inspector �Ҵ�
    [SerializeField] private TextMeshProUGUI popupMassage; // �˾��޽��� ���������� �˾�text inspector�Ҵ�

    [SerializeField] private GameObject userInfoPanel; // �������� ������Ʈ inspector�Ҵ�
    [SerializeField] private GameObject atmPanel; // atm ������Ʈ inspector �Ҵ�
    [SerializeField] private GameObject depositPanel; // �Ա�ui ������Ʈ inspector �Ҵ�
    [SerializeField] private GameObject withdrawPanel; // �Ա�ui ������Ʈ inspector �Ҵ�


    [SerializeField] private GameObject login; // �α���â ������Ʈ inspector�Ҵ�
    [SerializeField] private GameObject signUp; // ȸ������â ������Ʈ inspector�Ҵ�

    private BankTransaction transaction;

    private Dictionary<string, string> nameIndex = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // userData = new UserData("�輮ȣ", "ksh", "2585", 100000, 50000);// �����ڷ� ���� ���� ���� * LoadPlayerDataToJson() ?? = ���ʰ��� null�̸� �����ʰ� ���
        //SavePlayerDataToJson(userData);
        //Debug.Log("�����Ҵ��");
        LoadIndexJson(); // �����Ҷ� ��ųʸ��� �ʱ�ȭ
    }
    private void Start()
    {
        // userData = new UserData("�輮ȣ", "ksh", "2585", 100000, 50000);// �����ڷ� ���� ���� ���� * LoadPlayerDataToJson() ?? = ���ʰ��� null�̸� �����ʰ� ���
        RefreshUI();
    }
    // �Ա� Ʈ�����Լ�
    public void TryDeposit(int amount) // ���ӸŴ����� �̱���ȭ �Ǿ������Ƿ� 
    {
        if (transaction.OnClickDeposit(amount)) // BankTransaction���� true��
        {
            RefreshUI(); // ui����
        }
    }
    // �� �Լ��� ���� ��� Ʈ�����Լ�
    public void TryWithDraw(int amount)
    {
        if (transaction.OnClickWithdraw(amount)) 
        {
            RefreshUI();
        }
        else
        {
            // �ؽ�Ʈ�ٲٰ�
            Popup("�ܾ��� �����մϴ�");// �ܾ׺����� �˾�Ȱ��ȭ
        }
    }
    public void TryRemittanc(int amount, string remUser)
    {
        UserData remUserData = LoadPlayerDataToJson(FindIdByName(remUser)); // ��ųʸ� value���� ������ id.json�� userdata��ü���·� �޾ƿ�
        if (transaction.OnClickRemittanc(amount, remUserData))
        {
            RefreshUI();
        }
        else
        {
            Popup("�Է��Ͻ� ������ ������ �����ϴ�.");
        }
    }
    // ui�����Լ�
    private void RefreshUI()
    {
        nameText.text = userData.customerName;
        cashText.text = string.Format("{0:N0}", userData.cash); // 1000������ ���� ����
        balanceText.text = string.Format("{0:N0}", userData.balance);
    }
    public void Popup(string text)
    {
        popupMassage.text = text;
        popupPanel.SetActive(true);
    }
    public void PopupExit() // Popup ��ư �Լ�
    {
        popupPanel.SetActive(false);
    }

    [ContextMenu("To Json Data")] // ������Ʈ �޴��� �Ʒ� �Լ��� ȣ���ϴ� To Json Data ��� ��ɾ ������
    public void SavePlayerDataToJson(UserData userData, bool isNewUser = false) // ���� �����͸� JSON �������� �����ϴ� �Լ� (�ű� ���� ���δ� isNewUser�� ����)
    {
        string jsonData = JsonUtility.ToJson(userData); // UserData ��ü�� JSON ���ڿ��� ��ȯ
        string jsonIndexData = JsonUtility.ToJson(ConvertDictionaryToWrapper(nameIndex)); // ��ųʸ��� JSON���� ����ȭ�ϱ� ���� ����Ʈ ���۷� ��ȯ �� JSON ���ڿ� ����

        string path = Application.persistentDataPath + $"/{userData.customerId}.json"; // ���� ���� ������ ���� ��� ����
        string indexPath = Application.persistentDataPath + "/namaIndex.json"; // �̸� �ε��� ���� ��� ����

        // �ű� ������ ���, ������ ID�� �̹� �����ϸ� �������� ����
        if (isNewUser && File.Exists(path))
        {
            GameManager.instance.Popup("�̹� �����ϴ� ���̵��Դϴ�."); // �˾� �޽��� ���
            return; // ���� �ߴ�
        }

        File.WriteAllText(path, jsonData); // ���� ������ JSON ���Ϸ� ���� (����� ����)

        // key: ���̵�, value: �̸� ������ nameIndex ��ųʸ��� �ݿ�
        nameIndex[userData.customerId] = userData.customerName;

        File.WriteAllText(indexPath, jsonIndexData); // �ε��� JSON ���� (�̸� �ε��� ����)
    }

    public UserData LoadPlayerDataToJson(string id)// ����� JSON ������ �о�ͼ� UserData ��ü�� �ҷ����� �Լ�
    {
        string path = Application.persistentDataPath + $"/{id}.json"; // ����� JSON ���� ��� ����

        // ������ ������ �����ϴ��� Ȯ��
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path); // ���� ������ �о ���ڿ��� ��������
            return JsonUtility.FromJson<UserData>(json); // JSON ���ڿ��� UserData ��ü�� ��ȯ�Ͽ� ��ȯ
        }
        return null; // ������ ������ null ��ȯ
    }
    // ��ųʸ��� ����Ʈ ���·� �ٲٴ� �Լ�
    NameIndexWrapper ConvertDictionaryToWrapper(Dictionary<string, string> dict)
    // ����Ƽ�� Dictionary�� ���� JSON���� ����ȭ�� �� ���� ������, ����Ʈ ���·� ��ȯ�ϴ� �Լ�
    {
        NameIndexWrapper wrapper = new NameIndexWrapper(); // ����ȭ ������ ����Ʈ ������ ���� Ŭ���� ����

        foreach (var kvp in dict) // ��ųʸ��� ��� key-value ���� ��ȸ
        {
            wrapper.entries.Add(new NameIndex // key-value ���� ����Ʈ �׸����� ��ȯ�Ͽ� �߰�
            {
                key = kvp.Key,
                value = kvp.Value
            });
        }
        return wrapper; // JSON���� ����ȭ ������ ���·� ��ȯ�� ���� ��ü ��ȯ
    }
    public string FindIdByName(string name) // �̸����� �޾� ��ųʸ����� id���� ã�� �Լ�
    {
        foreach (var pair in nameIndex)
        {
            if (pair.Value == name)
            {
                return pair.Key; // �̸��� ��ġ�ϸ� ID ��ȯ
            }
        }
        return null; // �� ã���� null ��ȯ
    }
    //���� �ε��� json load
    private void LoadIndexJson() // ������ �� JSON ������ �о� ��ųʸ��� �ʱ�ȭ�ϴ� �Լ�
    {
        string indexPath = Application.persistentDataPath + "/namaIndex.json"; // �ε��� JSON ���� ��� ����

        if (File.Exists(indexPath)) // ������ �����ϴ��� Ȯ��
        {
            string json = File.ReadAllText(indexPath); // ���� ������ ���ڿ��� �о��
            NameIndexWrapper wrapper = JsonUtility.FromJson<NameIndexWrapper>(json); // JSON ���ڿ��� ����Ʈ ���� ��ü�� ��ȯ
            nameIndex = wrapper.entries.ToDictionary(e => e.key, e => e.value); // ����Ʈ�� Dictionary<string, string> ���·� ��ȯ
        }
        else
        {
            nameIndex = new Dictionary<string, string>(); // ������ ���� ��� ���ο� �� ��ųʸ� ����
        }
    }

    public void SignUpBTN() // ȸ������â ����
    {
        login.SetActive(false);
        signUp.SetActive(true);
    }
    public void LoginSuccess(string id) // �α��μ��� �Լ�
    {
        userData = LoadPlayerDataToJson(id); // �α����� ������ json������ ��������
        transaction = new BankTransaction(userData); // BankTransaction���� ������ �� �ֵ��� ���������͸� �ѱ�
        login.SetActive(false);
        atmPanel.SetActive(true);
        userInfoPanel.SetActive(true);
        LoginRefreshUI(userData); // �α��μ����� ui����
    }
    private void LoginRefreshUI(UserData userData) // �α��μ����� uiù����
    {
        nameText.text = userData.customerName;
        cashText.text = string.Format("{0:N0}", userData.cash); // 1000������ ���� ����
        balanceText.text = string.Format("{0:N0}", userData.balance);
    }
    public void SignUpSuccess()
    {
        signUp.SetActive(false);
        login.SetActive(true);
    }
}
