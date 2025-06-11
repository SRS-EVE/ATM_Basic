using TMPro;
using UnityEngine;

public class StepOne_Test : MonoBehaviour
{
    [Header("UserInfomation")]
    [SerializeField] private TextMeshProUGUI cash; // ���ݱݾ� text
    [SerializeField] private TextMeshProUGUI banlance; // ����ݾ� text
    [SerializeField] private int userMoney = 100000; // �Ա��� ���ݱݾ�
    [SerializeField] private int balance = 50000; // �ܾ�

    UserData user = new UserData("�輮ȣ", 100000, 30000); // ���������� ����

    // Start is called before the first frame update
    void Start()
    {
        if(cash == null)
        {
            Debug.Log("TextMeshProUGUI 'cash'�� �ν����Ϳ� �Ҵ���� �ʾҽ��ϴ�."); // ���� cash�� ���������
            Transform cashTransform = transform.Find("UserInfo/CashField/Money"); // PopupBank ������Ʈ �������� ���� ��ο� �ִ� Money ������Ʈ�� ã��
            cash = cashTransform.GetComponent<TextMeshProUGUI>(); // Money ������Ʈ�� ����
        }
        if(banlance == null)
        {
            Debug.Log("TextMeshProUGUI 'banlance'�� �ν����Ϳ� �Ҵ���� �ʾҽ��ϴ�."); // ����banlance�� ���������
            Transform banlanceTransform = transform.Find("UserInfo/BanlanceField/BanlanceCash"); // PopupBank ������Ʈ �������� ���� ��ο� �ִ� Banlance ������Ʈ�� ã��
            banlance = banlanceTransform.GetComponent<TextMeshProUGUI>(); // banlance ������Ʈ�� ����
        }
        UIUpdate();
    }
    private void UIUpdate() // {0:NO} �ݾ��� 1000������ ������
    {
        cash.text = string.Format("{0:N0}", userMoney) + "��"; 
        banlance.text = "balance : " + string.Format("{0:N0}", balance);
    }
}
