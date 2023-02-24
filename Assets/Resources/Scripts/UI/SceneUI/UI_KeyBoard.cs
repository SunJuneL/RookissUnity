using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_KeyBoard : UI_Scene
{
    [SerializeField]
    float _speed = 10.0f;

    GameObject player;

    enum Buttons
    {
        leftButton,
        rightButton,
        upButton,
        downButton,
    }

    void Start()
    {
        Init();
        player = GameObject.FindWithTag("Player");
    }

    public override void Init()
    {
        base.Init();
        //Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.leftButton).gameObject.AddUIEvent(OnButtonClicked);
    }

    public void OnButtonClicked(PointerEventData data)
    {
        player.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
        player.transform.position += Vector3.left * Time.deltaTime * _speed;
    }
}
