using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    // 몬스터와 플레이어 관리
    // 서버와 연동한다는 가정 하에 int와 짝지은 gameObject를 들고 있다.

    // Dictionary<int, GameObject> _monsters = new Dictionary<int, GameObject>();   // ID를 통해 관리하면 딕셔너리 사용
    // Dictionary<int, GameObject> _players = new Dictionary<int, GameObject>(); // 플레이어가 다수인 게임에서만 사용
    // Dictionary<int, GameObject> _env = new Dictionary<int, GameObject>();   // 우리가 조작할 수 있는 환경 및 아이템 (ex : 퀘스트 용의 꽃)

    // 몬스터와 플레이어 등의 오브젝트 들을 하나의 dictionary에 넣을 것인가? 분리할 것인가?
    // 대부분은 분리해서 관리하더라

    GameObject _player;
    HashSet<GameObject>_monsters = new HashSet<GameObject>();

    public Action<int> OnSpawnEvent;

    public GameObject GetPlayer() { return _player; }

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resources.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
        }
        return go;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc != null)
            return Define.WorldObject.Unknown;
        return bc.WorldObjectType;
    }

    public void DeSpawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Monster:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.WorldObject.Player:
                if (_player == go)
                    _player = null;
                break;
        }

        Managers.Resources.Destroy(go);
    }
}
