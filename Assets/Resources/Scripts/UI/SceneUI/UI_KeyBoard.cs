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

    enum Texts
    {
        LeftButtonText,
        RightButtonText,
        UpButtonText,
        DownButtonText,
        AnnounceText,
    }

    enum Buttons
    {
        LeftButton,
        RightButton,
        UpButton,
        DownButton,
    }

    void Start()
    {
        Init();
        player = GameObject.FindWithTag("Player");
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        GetButton((int)Buttons.LeftButton).gameObject.AddUIEvent(LeftButtonClicked);
        GetButton((int)Buttons.RightButton).gameObject.AddUIEvent(RightButtonClicked);
        GetButton((int)Buttons.UpButton).gameObject.AddUIEvent(UpButtonClicked);
        GetButton((int)Buttons.DownButton).gameObject.AddUIEvent(DownButtonClicked);
    }

    void LeftButtonClicked(PointerEventData data)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
        transform.position += Vector3.left * Time.deltaTime * _speed;
    }

    void RightButtonClicked(PointerEventData data)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
        transform.position += Vector3.right * Time.deltaTime * _speed;
    }

    void UpButtonClicked(PointerEventData data)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
        transform.position += Vector3.forward * Time.deltaTime * _speed;
    }

    void DownButtonClicked(PointerEventData data)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
        transform.position += Vector3.back * Time.deltaTime * _speed;
    }
}
