using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected float _moveSpeed;

    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defence { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed { get { return _moveSpeed;} set { _moveSpeed = value; } }

    private void Start()
    {
        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _defense = 5;
        _moveSpeed = 5.0f;
    }

    public virtual void OnAttacked(Stat attacker)
        // 공격력을 int로 받지 않고 Stat으로 받는 이유는 몬스터인지 아닌지 등의 다른 정보를 추출할 수도 있기 때문!
    {
        int damage = Mathf.Max(0, attacker.Attack - Defence);
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }

    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat;
        if (playerStat != null)
        {
            playerStat.Exp += 15;
            // 이렇게 외부에서 접근해 무언가를 바꾸게 하는 사람들도 있다.
                // 이때 문제점은 나중에 경험치를 추가해주는 부분이 많을 것이다.
                
        }
        Managers.Game.DeSpawn(gameObject);
    }
}
