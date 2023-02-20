using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // 1) 나 혹은 상대한테 RigidBody가 있어야 한다. (IsKinematic : Off)
    // 2) 나한테 Collider가 있어야 한다. (IsTrigger : Off)
    // 3) 상대한테 Collider가 있어야 한다. (IsTrigger : Off)

    // OnCollisionEnter와 같이 물리적인 충돌이 일어날 경우, 물리적인 연산을 하기 위해서 굉장히 많은 부하가 일어나게 된다.
    // 또한, Collider의 Material에 마찰력/탄성력 등과 같은 여러 물리적인 성질을 넣을 수 있다.
    // 하지만 이런 물리적인 연산은 위에서 말했듯 연산이 많아져 게임 퍼포먼스에도 나쁘다
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision @ {collision.gameObject.name} !");
    }

    // 물리연산이 아닌 트리거 이벤트만 발생한다. -> 부하가 적다.

    // 1) 둘 다 Collider가 있어야 한다.
    // 2) 둘 중 하나는 IsTrigger : On
    // 3) 둘 중 하나는 RigidBody가 있어야 한다.

    // 트리거는 스킬과 같은 것에도 사용한다.

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger @ {other.gameObject.name} !");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 look = transform.TransformDirection(Vector3.forward);   // 캐릭터가 바라보는 곳을 월드 좌표로 변환

        Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);   // 광성(Ray) 화면에 출력

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, look, out hit, 10))
        {
            Debug.Log($"Raycast @ {hit.collider.gameObject.name} !");
        }
        
        // Local <-> World <-> Viewport <-> Screen (화면)
        
        // Debug.Log(Input.mousePosition); // 콘솔 창에 현재 마우스의 위치를 픽셀좌표(스크린 좌표)로 출력한다.

        // Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));  // 뷰포트(ViewPort) 좌표 출력

        // 3D화면에서 2D화면으로 넘어갈 때 좌표가 하나 없어진 다는 것이 굉장히 중요하다!
        // 3D화면이 점차 카메라에 가면서 사진과 같이 되는 것을 투영이라고 한다.
        // 2D좌표에서 3D좌표를 얻기 위해서는 어떻게든 깊이를 넣어주어야 한다!


        /*
        // 마우스를 눌렀을 때만 체크
        if (Input.GetMouseButtonDown(1))
        {
            // 클릭한 지점의 월드 좌표를 얻는 방법 - 다 풀어쓴 방법
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 dir = mousePos - Camera.main.transform.position;
            dir = dir.normalized;
            
            // 클릭한 지점의 월드 좌표를 얻는 방법
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);

            if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            }
        }
        */

        // 마우스를 눌렀을 때만 체크
        if (Input.GetMouseButtonDown(1))
        {
            // 클릭한 지점의 월드 좌표를 얻는 방법
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            // 각 레이어를 판정하고 싶다면 판정하고 싶은 레이어 번호의 비트를 1로 켜두면 된다.
            // int mask = (1 << 8);

            // 또는 이런식으로 할 수 있다.
            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");


            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            }
        }

        // RayCasting은 무겁고 신중히 써야하는 작업이다.
        // 충돌 연산이 무거우니까, 부하를 줄이기 위해 충돌 전용 collider를 만드는 방법이 많이 사용된다.

        // Layer를 이용하면 내가 Raycasting으로 연상하고 싶은 애들만 연산할 수 있게 된다.
        // LayerMask를 붙이는 건 비트플래그를 이용한다.
        // Layer는 0~31, 총 32비트를 사용하므로 4바이트인 int를 사용하는 것을 알 수 있다.
        
        // GameObject.FindGameObjectWirhTag()

        





    }
}
