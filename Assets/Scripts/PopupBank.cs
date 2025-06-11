using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    [SerializeField] private GameObject atmPanel; // atm ������Ʈ inspector �Ҵ�
    [SerializeField] private GameObject depositPanel; // �Ա�ui ������Ʈ inspector �Ҵ�
    [SerializeField] private GameObject withdrawPanel; // �Ա�ui ������Ʈ inspector �Ҵ�
    [SerializeField] private GameObject remittancPanel; // �۱�ui ������Ʈ inspector �Ҵ�
 
    // ������� �ݾ�
    private int money = 10000;

    [SerializeField] private List<Button> depositBtnList;  // �Աݹ�ư inspector �Ҵ�
    
    // �Ա��� �ݾ� ��ư inspector�Ҵ�
    [SerializeField] private Button depositMoney1;
    [SerializeField] private Button depositMoney2;
    [SerializeField] private Button depositMoney3;
        
    [SerializeField] private List<Button> withdrawBtnList; // ��ݹ�ư inspector �Ҵ�
    
    // ����� �ݾ� ��ư inspector�Ҵ�
    [SerializeField] private Button withdrawMoney1;
    [SerializeField] private Button withdrawMoney2;
    [SerializeField] private Button withdrawMoney3;

    [SerializeField] private Button depositButton; // �Աݹ�ư
    [SerializeField] private Button withdrawButton; // ��ݹ�ư
    [SerializeField] private Button remittancButton; // �۱ݹ�ư
    [SerializeField] private Button depositResultButton; // �Ա�ó����ư
    [SerializeField] private Button withdrawResultButton; // ���ó����ư
    [SerializeField] private Button remittancResultButton; // �۱�ó����ư
    [SerializeField] private Button depositBackButton; // (�Ա�)�ڷΰ����ư
    [SerializeField] private Button withdrawBackButton; // (���)�ڷΰ����ư
    [SerializeField] private Button remittancBackButton; // (�۱�)�ڷΰ����ư

    // inputField inspector�Ҵ�
    [SerializeField] private List<TMP_InputField> inputField;

    [SerializeField] private TMP_InputField remFieldName; // �۱ݹ��� ������ �̸��� �޾ƿ� text������Ʈ inspector�Ҵ�
    [SerializeField] private TMP_InputField remFieldBalance; // �󸶳� �۱��� ������ ������ �޾ƿ� text������Ʈ inspector�Ҵ�

    string remUser;

    // Start is called before the first frame update
    private void Awake()
    {
        // ��ư�� �̺�Ʈ ����
        depositButton.onClick.AddListener(Onclick_Deposit);
        withdrawButton.onClick.AddListener(Onclick_Withdraw);
        remittancButton.onClick.AddListener(OnClick_Remittanc);

        depositBackButton.onClick.AddListener(Onclick_Back);
        withdrawBackButton.onClick.AddListener(Onclick_Back);
        remittancBackButton.onClick.AddListener(Onclick_Back);
    }
    private void Onclick_Deposit() // �Աݹ�ư�� ������ ��
    {
        // �ٸ��г� ��Ȱ��ȭ
        atmPanel.SetActive(false);
        depositPanel.SetActive(true);
        // ��ư �ؿ� �ִ� TextMeshProUGUI �ڽ� ������Ʈ�� ����
        TextMeshProUGUI text1 = depositMoney1.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI text2 = depositMoney2.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI text3 = depositMoney3.GetComponentInChildren<TextMeshProUGUI>();

        // TextMeshProUGUI �� ����
        text1.text = string.Format("{0:N0}", money);
        text2.text = string.Format("{0:N0}", (money*2));
        text3.text = string.Format("{0:N0}", (money*3));
    }
    private void Onclick_Withdraw() // ��ݹ�ư�� ������ ��
    {
        // �ٸ��г� ��Ȱ��ȭ
        atmPanel.SetActive(false);
        withdrawPanel.SetActive(true);
        // ��ư �ؿ� �ִ� TextMeshProUGUI �ڽ� ������Ʈ�� ����
        TextMeshProUGUI text1 = withdrawMoney1.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI text2 = withdrawMoney2.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI text3 = withdrawMoney3.GetComponentInChildren<TextMeshProUGUI>();

        // TextMeshProUGUI �� ����
        text1.text = string.Format("{0:N0}", money);
        text2.text = string.Format("{0:N0}", (money * 3));
        text3.text = string.Format("{0:N0}", (money * 5));
    }
    private void OnClick_Remittanc() // �۱ݹ�ư�� ������ ��
    {
        atmPanel.SetActive(false);
        remittancPanel.SetActive(true);
    }
    public void Deposit(int amount) // �Աݷ��� popupbank > gamemanager > banktransaction
                                    // public�̾�� inspector���� ���ᰡ�� private�� �ϰ������ ��ũ��Ʈ���� ����
    {
        GameManager.instance.TryDeposit(amount); // ���ӸŴ����� �Ա�ó���Լ� ȣ��
    }
    // ��ݷ���
    public void Withdraw(int amount)
    {
        GameManager.instance.TryWithDraw(amount); // ���ӸŴ����� ���ó���Լ� ȣ��
    }
    
    public void ConfirmDeposit() // �Է¹��� �Աݱݾ� ó���ϴ� �Լ�
    {
        string amount = inputField[0].text; // *inputfield�� ��ũ��Ʈ ���ο��� �Ҵ���� �ʾƵ�
                                            // ����Ƽ������ ������ �ڵ����� �Ҵ�����
        GameManager.instance.TryDeposit(int.Parse(amount));
    }
    public void ConfirmWithdraw() // �Է¹��� ��ݱݾ� ó���ϴ� �Լ�
    {
        string amount = inputField[1].text;
        GameManager.instance.TryWithDraw(int.Parse(amount));
    }
    public void ConfirmRemittanc() // �Է¹��� �۱ݱݾ� ó���ϴ� �Լ�
    {
        int amount = int.Parse(remFieldBalance.text);
        remUser = remFieldName.text;
        GameManager.instance.TryRemittanc(amount, remUser); // ���ӸŴ����� ���ó���Լ� ȣ��
    }
    private void Onclick_Back() // �ڷΰ��� ��ư���
    {
        atmPanel.SetActive(true);
        depositPanel.SetActive(false);
        withdrawPanel.SetActive(false);
        remittancPanel.SetActive(false);
    }
}
