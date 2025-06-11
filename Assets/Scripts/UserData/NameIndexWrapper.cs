using System.Collections.Generic;

[System.Serializable] // 유니티는 [System.Serializable]가 없으면 직렬화를 지원하지 않음
public class NameIndexWrapper
{
    public List<NameIndex> entries = new List<NameIndex>();
}
