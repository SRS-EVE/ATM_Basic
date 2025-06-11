

using System;

public class BankTransaction
{
    // ���� ��ũ��Ʈ

    private UserData userData; // ���ӸŴ��� ���������͸� �����ޱ����� ����

    public BankTransaction(UserData data) // ���ӸŴ����� �ִ� ���������͸� �����ޱ����� ������ ����
    {
        userData = data;
    }

    // �Աݿ���
    public bool OnClickDeposit(int amount)
    {
        if (amount <= userData.cash) // ���� �Ա��� �ݾ��� ���ݺ����׺��� ���ų� ���ٸ�
        {
            // �Ա�����
            userData.cash -= amount; // �������ִ� ���ݿ��� �Ա��� �ݾ׸�ŭ ���ֱ�
            userData.balance += amount; // �����ܾ��� �Ա��� ��ŭ �־��ֱ�
            GameManager.instance.SavePlayerDataToJson(userData, false); // 
            return true;
        }
        return false;
    }

    // ��ݿ���
    public bool OnClickWithdraw(int amount)
    {
        if(amount <= userData.balance)// ���� ��ݱݾ��� �����ܾ׺��� ���ų� ���ٸ�
        {
            // �������
            userData.balance -= amount; // �����ܾ׿��� ���ֱ�
            userData.cash += amount; // ���� ���ݺ����� +
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
        if(amount <= userData.balance) // ���� �۱ݱݾ��� �����ܾ׺��� ���ų� ���ٸ�
        {
            // �۱ݹ޴� ���������� amount �߰� ������ ��� �����ܾ��� amount��ŭ����
            RemUserData.balance += amount;
            userData.balance -= amount;
            GameManager.instance.SavePlayerDataToJson(userData, false);
            GameManager.instance.SavePlayerDataToJson(RemUserData, false);
            return true;
        }
        return false;
    }
}
