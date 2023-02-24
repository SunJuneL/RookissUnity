using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    enum Buttons
    {
        PointButton,

    }

    enum Texts
    {
        PointText,
        ScoreText,

    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        WeaponIcon,
        ItemIcon,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        // Get<Text>((int)Texts.ScoreText).text = "Bind Test";
        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);

        GameObject go = GetImange((int)Images.ItemIcon).gameObject;
        AddUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }


    int _score = 0;

    public void OnButtonClicked(PointerEventData data)
    {
        _score ++;
        Debug.Log($"점수 : {_score}");
        // _text.text = $"점수 : {_score}";
    }
}
