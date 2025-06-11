using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    [SerializeField] private GameObject atmPanel; // atm 오브젝트 inspector 할당
    [SerializeField] private GameObject depositPanel; // 입금ui 오브젝트 inspector 할당
    [SerializeField] private GameObject withdrawPanel; // 입금ui 오브젝트 inspector 할당
    [SerializeField] private GameObject remittancPanel; // 송금ui 오브젝트 inspector 할당
 
    // 입출금할 금액
    private int money = 10000;

    [SerializeField] private List<Button> depositBtnList;  // 입금버튼 inspector 할당
    
    // 입금할 금액 버튼 inspector할당
    [SerializeField] private Button depositMoney1;
    [SerializeField] private Button depositMoney2;
    [SerializeField] private Button depositMoney3;
        
    [SerializeField] private List<Button> withdrawBtnList; // 출금버튼 inspector 할당
    
    // 출금할 금액 버튼 inspector할당
    [SerializeField] private Button withdrawMoney1;
    [SerializeField] private Button withdrawMoney2;
    [SerializeField] private Button withdrawMoney3;

    [SerializeField] private Button depositButton; // 입금버튼
    [SerializeField] private Button withdrawButton; // 출금버튼
    [SerializeField] private Button remittancButton; // 송금버튼
    [SerializeField] private Button depositResultButton; // 입금처리버튼
    [SerializeField] private Button withdrawResultButton; // 출금처리버튼
    [SerializeField] private Button remittancResultButton; // 송금처리버튼
    [SerializeField] private Button depositBackButton; // (입금)뒤로가기버튼
    [SerializeField] private Button withdrawBackButton; // (출금)뒤로가기버튼
    [SerializeField] private Button remittancBackButton; // (송금)뒤로가기버튼

    // inputField inspector할당
    [SerializeField] private List<TMP_InputField> inputField;

    [SerializeField] private TMP_InputField remFieldName; // 송금받을 유저의 이름을 받아올 text오브젝트 inspector할당
    [SerializeField] private TMP_InputField remFieldBalance; // 얼마나 송금할 것인지 정보를 받아올 text오브젝트 inspector할당

    string remUser;

    // Start is called before the first frame update
    private void Awake()
    {
        // 버튼에 이벤트 연결
        depositButton.onClick.AddListener(Onclick_Deposit);
        withdrawButton.onClick.AddListener(Onclick_Withdraw);
        remittancButton.onClick.AddListener(OnClick_Remittanc);

        depositBackButton.onClick.AddListener(Onclick_Back);
        withdrawBackButton.onClick.AddListener(Onclick_Back);
        remittancBackButton.onClick.AddListener(Onclick_Back);
    }
    private void Onclick_Deposit() // 입금버튼을 눌렀을 때
    {
        // 다른패널 비활성화
        atmPanel.SetActive(false);
        depositPanel.SetActive(true);
        // 버튼 밑에 있는 TextMeshProUGUI 자식 오브젝트에 접근
        TextMeshProUGUI text1 = depositMoney1.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI text2 = depositMoney2.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI text3 = depositMoney3.GetComponentInChildren<TextMeshProUGUI>();

        // TextMeshProUGUI 값 변경
        text1.text = string.Format("{0:N0}", money);
        text2.text = string.Format("{0:N0}", (money*2));
        text3.text = string.Format("{0:N0}", (money*3));
    }
    private void Onclick_Withdraw() // 출금버튼을 눌렀을 때
    {
        // 다른패널 비활성화
        atmPanel.SetActive(false);
        withdrawPanel.SetActive(true);
        // 버튼 밑에 있는 TextMeshProUGUI 자식 오브젝트에 접근
        TextMeshProUGUI text1 = withdrawMoney1.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI text2 = withdrawMoney2.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI text3 = withdrawMoney3.GetComponentInChildren<TextMeshProUGUI>();

        // TextMeshProUGUI 값 변경
        text1.text = string.Format("{0:N0}", money);
        text2.text = string.Format("{0:N0}", (money * 3));
        text3.text = string.Format("{0:N0}", (money * 5));
    }
    private void OnClick_Remittanc() // 송금버튼을 눌렀을 때
    {
        atmPanel.SetActive(false);
        remittancPanel.SetActive(true);
    }
    public void Deposit(int amount) // 입금로직 popupbank > gamemanager > banktransaction
                                    // public이어야 inspector에서 연결가능 private로 하고싶으면 스크립트에서 연결
    {
        GameManager.instance.TryDeposit(amount); // 게임매니저의 입금처리함수 호출
    }
    // 출금로직
    public void Withdraw(int amount)
    {
        GameManager.instance.TryWithDraw(amount); // 게임매니저의 출금처리함수 호출
    }
    
    public void ConfirmDeposit() // 입력받은 입금금액 처리하는 함수
    {
        string amount = inputField[0].text; // *inputfield가 스크립트 내부에서 할당되지 않아도
                                            // 유니티에서는 값들을 자동으로 할당해줌
        GameManager.instance.TryDeposit(int.Parse(amount));
    }
    public void ConfirmWithdraw() // 입력받은 출금금액 처리하는 함수
    {
        string amount = inputField[1].text;
        GameManager.instance.TryWithDraw(int.Parse(amount));
    }
    public void ConfirmRemittanc() // 입력받은 송금금액 처리하는 함수
    {
        int amount = int.Parse(remFieldBalance.text);
        remUser = remFieldName.text;
        GameManager.instance.TryRemittanc(amount, remUser); // 게임매니저의 출금처리함수 호출
    }
    private void Onclick_Back() // 뒤로가기 버튼기능
    {
        atmPanel.SetActive(true);
        depositPanel.SetActive(false);
        withdrawPanel.SetActive(false);
        remittancPanel.SetActive(false);
    }
}
