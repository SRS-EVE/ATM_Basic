using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 게임매니저 싱글턴화

    private UserData userData; // 유저데이터 선언

    [SerializeField] private TextMeshProUGUI nameText; // 고객이름 inspector할당
    [SerializeField] private TextMeshProUGUI cashText; // 현금 inspector할당
    [SerializeField] private TextMeshProUGUI balanceText; // 통장잔액 inspector할당

    [SerializeField] private GameObject popupPanel; // 팝업 오브젝트 inspector 할당
    [SerializeField] private TextMeshProUGUI popupMassage; // 팝업메시지 변경을위해 팝업text inspector할당

    [SerializeField] private GameObject userInfoPanel; // 유저정보 오브젝트 inspector할당
    [SerializeField] private GameObject atmPanel; // atm 오브젝트 inspector 할당
    [SerializeField] private GameObject depositPanel; // 입금ui 오브젝트 inspector 할당
    [SerializeField] private GameObject withdrawPanel; // 입금ui 오브젝트 inspector 할당


    [SerializeField] private GameObject login; // 로그인창 오브젝트 inspector할당
    [SerializeField] private GameObject signUp; // 회원가입창 오브젝트 inspector할당

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
        // userData = new UserData("김석호", "ksh", "2585", 100000, 50000);// 생성자로 유저 정보 선언 * LoadPlayerDataToJson() ?? = 왼쪽값이 null이면 오른쪽값 사용
        //SavePlayerDataToJson(userData);
        //Debug.Log("유저할당됨");
        LoadIndexJson(); // 시작할때 딕셔너리를 초기화
    }
    private void Start()
    {
        // userData = new UserData("김석호", "ksh", "2585", 100000, 50000);// 생성자로 유저 정보 선언 * LoadPlayerDataToJson() ?? = 왼쪽값이 null이면 오른쪽값 사용
        RefreshUI();
    }
    // 입금 트라이함수
    public void TryDeposit(int amount) // 게임매니저가 싱글턴화 되어있으므로 
    {
        if (transaction.OnClickDeposit(amount)) // BankTransaction값이 true면
        {
            RefreshUI(); // ui갱신
        }
    }
    // 위 함수와 같은 출금 트라이함수
    public void TryWithDraw(int amount)
    {
        if (transaction.OnClickWithdraw(amount)) 
        {
            RefreshUI();
        }
        else
        {
            // 텍스트바꾸고
            Popup("잔액이 부족합니다");// 잔액부족시 팝업활성화
        }
    }
    public void TryRemittanc(int amount, string remUser)
    {
        UserData remUserData = LoadPlayerDataToJson(FindIdByName(remUser)); // 딕셔너리 value값을 참고해 id.json을 userdata객체형태로 받아옴
        if (transaction.OnClickRemittanc(amount, remUserData))
        {
            RefreshUI();
        }
        else
        {
            Popup("입력하신 고객님의 정보가 없습니다.");
        }
    }
    // ui갱신함수
    private void RefreshUI()
    {
        nameText.text = userData.customerName;
        cashText.text = string.Format("{0:N0}", userData.cash); // 1000단위로 끊는 로직
        balanceText.text = string.Format("{0:N0}", userData.balance);
    }
    public void Popup(string text)
    {
        popupMassage.text = text;
        popupPanel.SetActive(true);
    }
    public void PopupExit() // Popup 버튼 함수
    {
        popupPanel.SetActive(false);
    }

    [ContextMenu("To Json Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명령어가 생성됨
    public void SavePlayerDataToJson(UserData userData, bool isNewUser = false) // 유저 데이터를 JSON 형식으로 저장하는 함수 (신규 가입 여부는 isNewUser로 구분)
    {
        string jsonData = JsonUtility.ToJson(userData); // UserData 객체를 JSON 문자열로 변환
        string jsonIndexData = JsonUtility.ToJson(ConvertDictionaryToWrapper(nameIndex)); // 딕셔너리를 JSON으로 직렬화하기 위해 리스트 래퍼로 변환 후 JSON 문자열 생성

        string path = Application.persistentDataPath + $"/{userData.customerId}.json"; // 유저 개인 데이터 저장 경로 설정
        string indexPath = Application.persistentDataPath + "/namaIndex.json"; // 이름 인덱스 저장 경로 설정

        // 신규 유저일 경우, 동일한 ID가 이미 존재하면 저장하지 않음
        if (isNewUser && File.Exists(path))
        {
            GameManager.instance.Popup("이미 존재하는 아이디입니다."); // 팝업 메시지 출력
            return; // 저장 중단
        }

        File.WriteAllText(path, jsonData); // 유저 데이터 JSON 파일로 저장 (덮어쓰기 포함)

        // key: 아이디, value: 이름 구조로 nameIndex 딕셔너리에 반영
        nameIndex[userData.customerId] = userData.customerName;

        File.WriteAllText(indexPath, jsonIndexData); // 인덱스 JSON 저장 (이름 인덱스 갱신)
    }

    public UserData LoadPlayerDataToJson(string id)// 저장된 JSON 파일을 읽어와서 UserData 객체로 불러오는 함수
    {
        string path = Application.persistentDataPath + $"/{id}.json"; // 저장된 JSON 파일 경로 설정

        // 파일이 실제로 존재하는지 확인
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path); // 파일 내용을 읽어서 문자열로 가져오기
            return JsonUtility.FromJson<UserData>(json); // JSON 문자열을 UserData 객체로 변환하여 반환
        }
        return null; // 파일이 없으면 null 반환
    }
    // 딕셔너리를 리스트 형태로 바꾸는 함수
    NameIndexWrapper ConvertDictionaryToWrapper(Dictionary<string, string> dict)
    // 유니티는 Dictionary를 직접 JSON으로 직렬화할 수 없기 때문에, 리스트 형태로 변환하는 함수
    {
        NameIndexWrapper wrapper = new NameIndexWrapper(); // 직렬화 가능한 리스트 형태의 래퍼 클래스 생성

        foreach (var kvp in dict) // 딕셔너리의 모든 key-value 쌍을 순회
        {
            wrapper.entries.Add(new NameIndex // key-value 쌍을 리스트 항목으로 변환하여 추가
            {
                key = kvp.Key,
                value = kvp.Value
            });
        }
        return wrapper; // JSON으로 직렬화 가능한 형태로 변환된 래퍼 객체 반환
    }
    public string FindIdByName(string name) // 이름값을 받아 딕셔너리에서 id값을 찾는 함수
    {
        foreach (var pair in nameIndex)
        {
            if (pair.Value == name)
            {
                return pair.Key; // 이름이 일치하면 ID 반환
            }
        }
        return null; // 못 찾으면 null 반환
    }
    //유저 인덱스 json load
    private void LoadIndexJson() // 시작할 때 JSON 파일을 읽어 딕셔너리를 초기화하는 함수
    {
        string indexPath = Application.persistentDataPath + "/namaIndex.json"; // 인덱스 JSON 파일 경로 설정

        if (File.Exists(indexPath)) // 파일이 존재하는지 확인
        {
            string json = File.ReadAllText(indexPath); // 파일 내용을 문자열로 읽어옴
            NameIndexWrapper wrapper = JsonUtility.FromJson<NameIndexWrapper>(json); // JSON 문자열을 리스트 래퍼 객체로 변환
            nameIndex = wrapper.entries.ToDictionary(e => e.key, e => e.value); // 리스트를 Dictionary<string, string> 형태로 변환
        }
        else
        {
            nameIndex = new Dictionary<string, string>(); // 파일이 없을 경우 새로운 빈 딕셔너리 생성
        }
    }

    public void SignUpBTN() // 회원가입창 진입
    {
        login.SetActive(false);
        signUp.SetActive(true);
    }
    public void LoginSuccess(string id) // 로그인성공 함수
    {
        userData = LoadPlayerDataToJson(id); // 로그인한 유저의 json데이터 가져오기
        transaction = new BankTransaction(userData); // BankTransaction에서 참조할 수 있도록 유저데이터를 넘김
        login.SetActive(false);
        atmPanel.SetActive(true);
        userInfoPanel.SetActive(true);
        LoginRefreshUI(userData); // 로그인성공시 ui갱신
    }
    private void LoginRefreshUI(UserData userData) // 로그인성공시 ui첫갱신
    {
        nameText.text = userData.customerName;
        cashText.text = string.Format("{0:N0}", userData.cash); // 1000단위로 끊는 로직
        balanceText.text = string.Format("{0:N0}", userData.balance);
    }
    public void SignUpSuccess()
    {
        signUp.SetActive(false);
        login.SetActive(true);
    }
}
