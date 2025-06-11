[System.Serializable] // Unity�� JsonUtility �Ǵ� Inspector���� �� Ŭ������ ����ȭ(Serialize)�� �� �ֵ��� �����ϴ� Ư��(Attribute)

public class UserData
{
    /*private string customerName; // ���̸�
    private int cash; // �Ա��� �ݾ�
    private int balance; // ���� ���� �ݾ�
    */ // <- ������Ƽ�� ����Ҷ����

    public string customerName; // ���̸�
    public string customerId; // �����̵�
    public string customerPassword; // ���н�����
    public int cash; // �Ա��� �ݾ�
    public int balance; // ���� ���� �ݾ�

    public UserData(string customerName, int cash, int balance)
    {
        this.customerName = customerName;
        this.cash = cash;
        this.balance = balance;
    }
    
    public UserData(string customerName, string customerId, string customerPassword, int cash, int balance)
    {

        this.customerName = customerName;
        this.customerId = customerId;
        this.customerPassword = customerPassword;
        this.cash = cash;
        this.balance = balance;
    }

    // ������ ������Ƽȭ
    // *������Ƽ�� : ���������� ������Ÿ�� ������Ƽ�� ���� ������
    // ������ ������ ���� �ܺο��� ������ �� �ֵ��� �ϸ鼭 ���ÿ� ĸ��ȭ�� �����ϴ� c# ����� ���
    // public string CustomerName
    // {
    //     get { return customerName; }
    //     set { customerName = value; }
    // }
    // public int Money
    // {
    //     get { return cash; }
    //     set { cash = value; }
    // }
    // public int Balance
    // {
    //     get { return balance; }
    //     set { balance = value; }
    // }
    // ����
    // UserData user = new UserData();
    // user.CustomerName = "�輮ȣ";       // set�� ���� ������ private �ʵ忡 �� ����
    // Debug.Log(user.CustomerName);       // get�� ���� �� �б�
}
