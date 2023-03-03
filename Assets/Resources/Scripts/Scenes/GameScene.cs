using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_KeyBoard>();
        // Managers.Resources.Instantiate("UI/Scene/UI_KeyBoard");
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

}
