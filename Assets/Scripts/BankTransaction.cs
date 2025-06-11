

using System;

public class BankTransaction
{
    // 연산 스크립트

    private UserData userData; // 게임매니저 유저데이터를 참조받기위해 선언

    public BankTransaction(UserData data) // 게임매니저에 있는 유저데이터를 참조받기위한 생성자 선언
    {
        userData = data;
    }

    // 입금연산
    public bool OnClickDeposit(int amount)
    {
        if (amount <= userData.cash) // 만약 입금할 금액이 현금보유액보다 적거나 같다면
        {
            // 입금진행
            userData.cash -= amount; // 가지고있는 현금에서 입금할 금액만큼 빼주기
            userData.balance += amount; // 통장잔액을 입금한 만큼 넣어주기
            GameManager.instance.SavePlayerDataToJson(userData, false); // 
            return true;
        }
        return false;
    }

    // 출금연산
    public bool OnClickWithdraw(int amount)
    {
        if(amount <= userData.balance)// 만약 출금금액이 통장잔액보다 적거나 같다면
        {
            // 출금진행
            userData.balance -= amount; // 통장잔액에서 빼주기
            userData.cash += amount; // 고객의 현금보유량 +
            GameManager.instance.SavePlayerDataToJson(userData, false);
            return true;
        }
        return false;
    }
    public bool OnClickRemittanc(int amount, UserData RemUserData)
    {
        if(RemUserData == null)
        {
            return false;
        }
        if(amount <= userData.balance) // 만약 송금금액이 통장잔액보다 적거나 같다면
        {
            // 송금받는 유저정보에 amount 추가 보내는 사람 통장잔액은 amount만큼감소
            RemUserData.balance += amount;
            userData.balance -= amount;
            GameManager.instance.SavePlayerDataToJson(userData, false);
            GameManager.instance.SavePlayerDataToJson(RemUserData, false);
            return true;
        }
        return false;
    }
}
