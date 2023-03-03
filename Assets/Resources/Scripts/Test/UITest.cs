using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    [SerializeField]
    Text _text;

    private void Start()
    {
        _text.text = "change";
    }

}
