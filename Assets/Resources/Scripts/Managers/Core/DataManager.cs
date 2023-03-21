using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public interface ILoader<Key, Value>
{
    // 인터페이스를 적용한 애는 반드시 딕셔너리의 키와 값을 뱉어주는 MakeDict()라는 애를 반드시 구현해야 한다.
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
    }

    // 키와 값을 지니고 있는 ILoader를 반드시 들고 있어야 한다는 의미
    Loader LoadJson<Loader, Key, Value>(string path) where Loader :ILoader<Key, Value>
    {
        // 여기서 TextAsset은 텍스트 파일을 말하는 것임
        TextAsset textAsset = Managers.Resources.Load<TextAsset>($"Data/{path}");

        // Unity에서 JSON을 파싱하는 방법을 제공해줌 -> 아래와 같은 방법으로 알아서 클래스에 파싱해줌
        return JsonUtility.FromJson<Loader>(textAsset.text);
        // 위의 클래스로 데이터의 형식을 맞춰 준 것이고,
        // JsonUtility.FromJson에서 "파일에 있는 것을 메모리에 긁어오는 작업을 해주어(=deSerialize 해주어)" 작업을 한 것이다.
    }
    // Loader는 
}
