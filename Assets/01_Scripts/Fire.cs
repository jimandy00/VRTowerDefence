using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���콺 ���ʹ�ư�� ������ ���� ���ʹ�.
// �ʿ��� ��� : ��, �Ѿ� �ڱ�(VFX)
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
        //// Input.touchCount // �հ��� ������ � ������?
        //Touch touch = Input.GetTouch(0);
        
        //switch(touch.phase)
        //{
        //    case TouchPhase.Began // ��ġ�� � ���·� ����ǰ� �ִ°�. ��ġ ����, ����� �巡��. 

        //}


        // ���콺 ���ʹ�ư�� ������ ���� ���ʹ�.
        // �� ��ġ���� ���� �չ������� Ray�� �����
        // �ٶ󺸰� �ε��� ���� �ִٸ�
        // �װ��� �Ѿ��ڱ��� �����ʹ�.
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

                // ���� ���� ���� Enemy���
                if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy")) // layer���
                {
                    // Enemy���� "�ı��Ǿ���"��� �ϰ�ʹ�.
                    hitInfo.transform.GetComponent<Enemy>().DiePlz(1);
                }

            }
            else
            {
                lr.SetPosition(1, ray.origin + ray.direction * 1000);
            }
            
        }
    }
}
