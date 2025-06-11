[System.Serializable] // Unity의 JsonUtility 또는 Inspector에서 이 클래스를 직렬화(Serialize)할 수 있도록 지정하는 특성(Attribute)

public class UserData
{
    /*private string customerName; // 고객이름
    private int cash; // 입금할 금액
    private int balance; // 고객의 통장 금액
    */ // <- 프로퍼티로 사용할때사용

    public string customerName; // 고객이름
    public string customerId; // 고객아이디
    public string customerPassword; // 고객패스워드
    public int cash; // 입금할 금액
    public int balance; // 고객의 통장 금액

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

    // 고객정보 프로퍼티화
    // *프로퍼티란 : 접근한정자 데이터타입 프로퍼티명 으로 적으며
    // 선언한 변수의 값을 외부에서 접근할 수 있도록 하면서 동시에 캡슐화를 지원하는 c# 언어의 요소
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
    // 사용법
    // UserData user = new UserData();
    // user.CustomerName = "김석호";       // set을 통해 내부의 private 필드에 값 저장
    // Debug.Log(user.CustomerName);       // get을 통해 값 읽기
}
