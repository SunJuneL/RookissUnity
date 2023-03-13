using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_KeyBoard : UI_Scene
{
    [SerializeField]
    float _speed = 10.0f;
    bool _isMoving = false;
    int _direction = -1;

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
        player = GameObject.FindWithTag("Player");
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        GetButton((int)Buttons.LeftButton).gameObject.AddUIEvent(OnLeft, Define.UIEvent.Down);
        GetButton((int)Buttons.LeftButton).gameObject.AddUIEvent(OffLeft, Define.UIEvent.Up);
        
        GetButton((int)Buttons.RightButton).gameObject.AddUIEvent(OnRight, Define.UIEvent.Down);
        GetButton((int)Buttons.RightButton).gameObject.AddUIEvent(OffRight, Define.UIEvent.Up);

        GetButton((int)Buttons.UpButton).gameObject.AddUIEvent(OnForward, Define.UIEvent.Down);
        GetButton((int)Buttons.UpButton).gameObject.AddUIEvent(OffForward, Define.UIEvent.Up);

        GetButton((int)Buttons.DownButton).gameObject.AddUIEvent(OnBack, Define.UIEvent.Down);
        GetButton((int)Buttons.DownButton).gameObject.AddUIEvent(OffBack, Define.UIEvent.Up);
    }

    void OnLeft(PointerEventData data)
    {
        _isMoving = true;
        _direction = (int)Buttons.LeftButton;
    }
    void OffLeft(PointerEventData data)
    {
        _isMoving = false;
        _direction = -1;
    }

    void OnRight(PointerEventData data)
    {
        _isMoving = true;
        _direction = (int)Buttons.RightButton;
    }
    void OffRight(PointerEventData data)
    {
        _isMoving = false;
        _direction = -1;
    }

    void OnForward(PointerEventData data)
    {
        _isMoving = true;
        _direction = (int)Buttons.UpButton;
    }
    void OffForward(PointerEventData data)
    {
        _isMoving = false;
        _direction = -1;
    }

    void OnBack(PointerEventData data)
    {
        _isMoving = true;
        _direction = (int)Buttons.DownButton;
    }
    void OffBack(PointerEventData data)
    {
        _isMoving = false;
        _direction = -1;
    }

    void Update()
    {
        if (_isMoving)
        {
            switch (_direction)
            {
                case (int)Buttons.LeftButton:
                    player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
                    player.transform.position += Vector3.left * Time.deltaTime * _speed;
                    GetText((int)Texts.AnnounceText).text = "Move To Left!";
                break;
                case (int)Buttons.RightButton:
                    player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
                    player.transform.position += Vector3.right * Time.deltaTime * _speed;
                    GetText((int)Texts.AnnounceText).text = "Move To Right!";
                break;
                case (int)Buttons.UpButton:
                    player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
                    player.transform.position += Vector3.forward * Time.deltaTime * _speed;
                    GetText((int)Texts.AnnounceText).text = "Move To Forward!";
                break;
                case (int)Buttons.DownButton:
                    player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
                    player.transform.position += Vector3.back * Time.deltaTime * _speed;
                    GetText((int)Texts.AnnounceText).text = "Move To Back!";
                break;
            }
        }
        else
            GetText((int)Texts.AnnounceText).text = "Not Moving!!";
    }
}
