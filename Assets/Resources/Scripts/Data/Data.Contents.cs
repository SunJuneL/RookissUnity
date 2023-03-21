using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    /*
        // Serializable과 DeSerializable의 뜻?
        아래 클래스 부분과 같이 메모리에서 들고있는 것들을
        json등과 같이 파일로 변환할 수 있다는 의미가 된다.
    */

    [Serializable]
    public class Stat
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }
    // 참고로 위 클래스 부분에서 public으로 안하면 읽어들이지 못한다. 그래서 안된다.
    // 만일 public을 안붙여서 private로 하고 싶다면 [SerializeField]를 붙여주면 된다.
    // 또한 반드시 연결시켜줄 변수의 이름이 완전히 같아야 한다.

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

// namespace의 의미? -> 별칭이 하나 더 붙는다? (가문의 이름)
// 외부에서 이 스크립트의 stat클래스에 접근하려면 Data.Stat으로 접근한다.