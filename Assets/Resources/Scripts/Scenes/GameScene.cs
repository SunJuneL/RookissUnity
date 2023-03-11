using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    Coroutine co;

    #region Coroutine Test

    // class Test
    // {
    //     public int id = 0;
    // }

    // class CoroutineTest : IEnumerable
    // {
    //     public IEnumerator GetEnumerator()
    //     {
    //         yield return new Test() { id = 1 };
    //         yield return new Test() { id = 2 };
    //         yield return new Test() { id = 3 };
    //         yield return new Test() { id = 4 };
    //         // 일반 return처럼 바로 종료가 아니라 다음 yield return도 return된다.
    //         // 그렇다면 바로 return하고 싶다면?
    //             // yield break;
    //     }
    // }
    #endregion

    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        #region UITest

        // Managers.UI.ShowSceneUI<UI_KeyBoard>();
        // Managers.Resources.Instantiate("UI/Scene/UI_KeyBoard");
        #endregion

        #region CoroutineAction

        // CoroutineTest test = new CoroutineTest();
        // 
        // foreach (System.Object t in test)
        // {
        //     // int value = (int)t;
        //     // Debug.Log(t);
        // 
        //     Test value = (Test)t;   // yield return으로 우리가 원하는 어떤 값이든 전달 및 return할 수 있다.
        //     Debug.Log(value.id);
        // }
        #endregion

        co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
        StartCoroutine("CoStopExplode", 2.0f);

        // DataTest
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();
    }

    // 코루틴 실습
    IEnumerator ExplodeAfterSeconds(float seconds)
    {
        Debug.Log("Explode Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Explode End");
    }

    IEnumerator CoStopExplode(float seconds)
    {
        Debug.Log("Stop Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Stop Execute");
        if (co != null)
        {
            StopCoroutine(co);
            co = null;
        }
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

}
