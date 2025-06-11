using TMPro;
using UnityEngine;

public class StepOne_Test : MonoBehaviour
{
    [Header("UserInfomation")]
    [SerializeField] private TextMeshProUGUI cash; // 현금금액 text
    [SerializeField] private TextMeshProUGUI banlance; // 통장금액 text
    [SerializeField] private int userMoney = 100000; // 입금할 현금금액
    [SerializeField] private int balance = 50000; // 잔액

    UserData user = new UserData("김석호", 100000, 30000); // 유저데이터 생성

    // Start is called before the first frame update
    void Start()
    {
        if(cash == null)
        {
            Debug.Log("TextMeshProUGUI 'cash'가 인스펙터에 할당되지 않았습니다."); // 만약 cash가 비어있으면
            Transform cashTransform = transform.Find("UserInfo/CashField/Money"); // PopupBank 오브젝트 기준으로 하위 경로에 있는 Money 오브젝트를 찾음
            cash = cashTransform.GetComponent<TextMeshProUGUI>(); // Money 오브젝트와 연결
        }
        if(banlance == null)
        {
            Debug.Log("TextMeshProUGUI 'banlance'가 인스펙터에 할당되지 않았습니다."); // 만약banlance가 비어있으면
            Transform banlanceTransform = transform.Find("UserInfo/BanlanceField/BanlanceCash"); // PopupBank 오브젝트 기준으로 하위 경로에 있는 Banlance 오브젝트를 찾음
            banlance = banlanceTransform.GetComponent<TextMeshProUGUI>(); // banlance 오브젝트와 연결
        }
        UIUpdate();
    }
    private void UIUpdate() // {0:NO} 금액을 1000단위로 나누기
    {
        cash.text = string.Format("{0:N0}", userMoney) + "원"; 
        banlance.text = "balance : " + string.Format("{0:N0}", balance);
    }
}
