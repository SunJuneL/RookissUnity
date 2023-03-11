using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    // 이 스크립트는
    // 이 스크립트를 컴포넌트로 들고 있는 오브젝트(객체)는 풀링할 애들
    // 스크립트를 컴포넌트로 안들고 있는 애들은 풀링하지 않을 애들
    // 딱 구별 용도

    public bool IsUsing;
}
