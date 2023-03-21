using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    /*
        // Serializable�� DeSerializable�� ��?
        �Ʒ� Ŭ���� �κа� ���� �޸𸮿��� ����ִ� �͵���
        json��� ���� ���Ϸ� ��ȯ�� �� �ִٴ� �ǹ̰� �ȴ�.
    */

    [Serializable]
    public class Stat
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }
    // ����� �� Ŭ���� �κп��� public���� ���ϸ� �о������ ���Ѵ�. �׷��� �ȵȴ�.
    // ���� public�� �Ⱥٿ��� private�� �ϰ� �ʹٸ� [SerializeField]�� �ٿ��ָ� �ȴ�.
    // ���� �ݵ�� ��������� ������ �̸��� ������ ���ƾ� �Ѵ�.

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();

            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion
}

// namespace�� �ǹ�? -> ��Ī�� �ϳ� �� �ٴ´�? (������ �̸�)
// �ܺο��� �� ��ũ��Ʈ�� statŬ������ �����Ϸ��� Data.Stat���� �����Ѵ�.