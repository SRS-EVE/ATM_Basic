# 🕹️ ATM

## 🛠️ 개발 환경

- 언어: **C#**

## 📁 프로젝트 구조

```
📁 Scripts/
├── 📁 UserData/ #유저데이터
│   ├── NameIndex.cs
│   ├── NameIndexWrapper.cs
│   └── UserData.cs
├── BankTransaction.cs #입금, 출금, 송금 연산을 담당하는 스크립트
├── GameManager.cs #UI제어, JSON저장 등 전반적인 기능을 담당하는 스크립트
├── PopupBank.cs #입금, 출금, 송금 기능을 담당하는 스크립트
├── PopupLogin.cs #로그인, 회원가입을 담당하는 스크립트
└── StepOne_Test.cs #테스트용 스크립트

```

## 🎯 주요 기능

1. 로그인 및 회원가입
 - 사용자 이름 기반 로그인 기능 (PopupLogin)
 - 새 사용자 생성하는 회원가입  
2. 입출금 기능
 - ***입금(Deposit)***

  입력한 금액을 사용자 계좌에 입금
  GameManager → BankTransaction → UserData로 데이터 전달 및 저장

 - ***출금(Withdraw)***

   잔액보다 큰 금액은 출금 불가

   출금 성공 시 잔액 차감  



3. UI 연동
  ATM UI 팝업창 (PopupBank)

  - 입금/출금 버튼

  - 금액 입력 필드

  - 보유현금/통장잔액 표시  



4. 데이터 저장/불러오기
 - JsonUtility를 이용한 사용자 데이터 JSON 저장

 - 경로: Application.persistentDataPath/userData.customerId.json

 - 시작 시 자동 로드  



5. 데이터 구조
 - UserData: 사용자 이름, 사용자ID,사용자 패스워드, 보유현금, 계좌잔액 

 - NameIndexWrapper: 여러 유저 관리용 인덱스 클래스(이름값으로 사용자를 찾기위함)

### 📌 기본 시스템


