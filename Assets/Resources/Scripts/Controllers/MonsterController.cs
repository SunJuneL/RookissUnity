using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;

    [SerializeField]
    float _scanRange = 10;

    [SerializeField]
    float _attackRange = 2;

    public override void Init()
    {
         _stat = gameObject.GetComponent<PlayerStat>();

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected override void UpdateIdle()
    {
        Debug.Log("Monster UpdateIdle");

        // Player를 찾아와야하는데, 플레이어와 몬스터를 총괄하는 매니저는 사용하지 않고 있다. -> 나중에 매니저가 생기면 하자!
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            return;
        
        float distance = (player.transform.position - transform.position).magnitude;
        if (distance <= _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;
            return;
        }
    }

    protected override void UpdateMoving()
    {
        // 몬스터가 내 사정거리 내로 들어오면 공격
        if (_lockTarget != null )
        {
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= _attackRange)
            {
                NavMeshAgent nea = gameObject.GetOrAddComponent<NavMeshAgent>();
                nea.SetDestination(transform.position);
                State = Define.State.Skill;
                return;
            }
        }

        // 이동
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nea = gameObject.GetOrAddComponent<NavMeshAgent>();
            nea.SetDestination(_destPos);
            nea.speed = _stat.MoveSpeed;

            // transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
    }

    protected override void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }


    }

    void OnHitEvent()
    {
        if (_lockTarget == null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            Stat myStat = gameObject.GetComponent<Stat>();
            int damage = Mathf.Max(0, myStat.Attack - targetStat.Defence);
            targetStat.Hp -= damage;

            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _attackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Idle;
            }
        }
        else
        {
            State = Define.State.Idle;
        }
    }
}

