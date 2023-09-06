using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스 왼쪽버튼을 누르면 총을 쏘고싶다.
// 필요한 요소 : 손, 총알 자국(VFX)
public class Fire : MonoBehaviour
{
    public Transform hand;
    public GameObject bulletImpactFactory;
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 왼쪽버튼을 누르면 총을 쏘고싶다.
        // 손 위치에서 손의 앞방향으로 Ray를 만들고
        // 바라보고 부딪힌 곳에 있다면
        // 그곳에 총알자국을 남기고싶다.
        Ray ray = new Ray(hand.position, hand.forward);
        lr.SetPosition(0, ray.origin);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo))
        {
            lr.SetPosition(1, hitInfo.point);
            if (Input.GetButtonDown("Fire1"))
            {
                var bulletImpact = Instantiate(bulletImpactFactory);
                bulletImpact.transform.position = hitInfo.point;
                Destroy(bulletImpact, 2);
            }
            else
            {
                lr.SetPosition(1, ray.origin + ray.direction * 1000);
            }
            
        }
    }
}
