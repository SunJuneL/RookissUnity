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
    //         // �Ϲ� returnó�� �ٷ� ���ᰡ �ƴ϶� ���� yield return�� return�ȴ�.
    //         // �׷��ٸ� �ٷ� return�ϰ� �ʹٸ�?
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
        //     Test value = (Test)t;   // yield return���� �츮�� ���ϴ� � ���̵� ���� �� return�� �� �ִ�.
        //     Debug.Log(value.id);
        // }
        #endregion

        co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
        StartCoroutine("CoStopExplode", 2.0f);

        // DataTest
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();
    }

    // �ڷ�ƾ �ǽ�
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
