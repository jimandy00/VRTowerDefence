using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 텔레포트
// 왼손
// 마우스 오른쪽 버튼을 누르면
// 왼손 위치에서 앞방향으로 Ray를 발사
// 부딪힌 곳이 있으면 플레이어를 그곳으로 이동

public class Teleport : MonoBehaviour
{
    public Transform hand; // 손의 위치
    public Transform player; // 플레이어 위치(결과적으로 플레이어가 이동하니까)
    LineRenderer lr;
    public Transform marker;
    public float kAdjust = 1f;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false; // 컴포넌트는 set active가 아닌 enable
        marker.localScale = Vector3.one * kAdjust;
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 오른쪽 버튼을 누르면 Ready 상태가 되고
        // 마우스 오른쪽 버튼을 떼면 이동
        if(Input.GetButtonUp("Fire2")) { // vr기기 연동을 위해서 get button down
            Ray ray = new Ray(hand.position, hand.forward); // 현재 위치, 어디 방향으로?
            lr.SetPosition(0, ray.origin);
            RaycastHit hitInfo;
            bool isHit = Physics.Raycast(ray, out hitInfo); // 부딪힌게 있으면 isHit에 저장
            if(isHit)
            {
                // 어딘가 부딪혔다.
                lr.SetPosition(1, hitInfo.point); // point = raycast와 부딪힌 지점
                marker.position = hitInfo.point;
                marker.up = hitInfo.normal;
                marker.localScale = Vector3.one * kAdjust * hitInfo.distance; // k는 관용어구
            }
            else
            {
                // 허공이다.
                // hand의 100미터 앞
                // ray.origin(hand 현위치) + ray.direction(ray 앞방향) * 100
                lr.SetPosition(1, ray.origin + ray.direction * 100);
                marker.position = ray.origin + ray.direction * 100;
                marker.up = -ray.direction;
                marker.localScale = Vector3.one * kAdjust * 100; // k는 관용어구

            }

            if (Input.GetButtonDown("Fire2"))
            {
                // 마우스 오른쪽 버튼을 누르면 선을 보고싶다.
                lr.enabled = true;
            }
            
            else if(Input.GetButtonUp("Fire2"))
            {
                // 만약 닿은곳이 있다면?
                // 어딘가 부딪힘
                // 선을 안보고싶다.
                lr.enabled = false;

                if (isHit)
                {
                    player.position = hitInfo.point;
                }

            }
        }
    }
}
