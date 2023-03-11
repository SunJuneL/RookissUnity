using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public interface ILoader<Key, Value>
{
    // �������̽��� ������ �ִ� �ݵ�� ��ųʸ��� Ű�� ���� ����ִ� MakeDict()��� �ָ� �ݵ�� �����ؾ� �Ѵ�.
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
    }

    // Ű�� ���� ���ϰ� �ִ� ILoader�� �ݵ�� ��� �־�� �Ѵٴ� �ǹ�
    Loader LoadJson<Loader, Key, Value>(string path) where Loader :ILoader<Key, Value>
    {
        // ���⼭ TextAsset�� �ؽ�Ʈ ������ ���ϴ� ����
        TextAsset textAsset = Managers.Resources.Load<TextAsset>($"Data/{path}");

        // Unity���� JSON�� �Ľ��ϴ� ����� �������� -> �Ʒ��� ���� ������� �˾Ƽ� Ŭ������ �Ľ�����
        return JsonUtility.FromJson<Loader>(textAsset.text);
        // ���� Ŭ������ �������� ������ ���� �� ���̰�,
        // JsonUtility.FromJson���� "���Ͽ� �ִ� ���� �޸𸮿� �ܾ���� �۾��� ���־�(=deSerialize ���־�)" �۾��� �� ���̴�.
    }
    // Loader�� 
}
