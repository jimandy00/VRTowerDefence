using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڷ���Ʈ
// �޼�
// ���콺 ������ ��ư�� ������
// �޼� ��ġ���� �չ������� Ray�� �߻�
// �ε��� ���� ������ �÷��̾ �װ����� �̵�

public class Teleport : MonoBehaviour
{
    public Transform hand; // ���� ��ġ
    public Transform player; // �÷��̾� ��ġ(��������� �÷��̾ �̵��ϴϱ�)
    LineRenderer lr;
    public Transform marker;
    public float kAdjust = 1f;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false; // ������Ʈ�� set active�� �ƴ� enable
        marker.localScale = Vector3.one * kAdjust;
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺 ������ ��ư�� ������ Ready ���°� �ǰ�
        // ���콺 ������ ��ư�� ���� �̵�
        if(Input.GetButtonUp("Fire2")) { // vr��� ������ ���ؼ� get button down
            Ray ray = new Ray(hand.position, hand.forward); // ���� ��ġ, ��� ��������?
            lr.SetPosition(0, ray.origin);
            RaycastHit hitInfo;
            bool isHit = Physics.Raycast(ray, out hitInfo); // �ε����� ������ isHit�� ����
            if(isHit)
            {
                // ��� �ε�����.
                lr.SetPosition(1, hitInfo.point); // point = raycast�� �ε��� ����
                marker.position = hitInfo.point;
                marker.up = hitInfo.normal;
                marker.localScale = Vector3.one * kAdjust * hitInfo.distance; // k�� ����
            }
            else
            {
                // ����̴�.
                // hand�� 100���� ��
                // ray.origin(hand ����ġ) + ray.direction(ray �չ���) * 100
                lr.SetPosition(1, ray.origin + ray.direction * 100);
                marker.position = ray.origin + ray.direction * 100;
                marker.up = -ray.direction;
                marker.localScale = Vector3.one * kAdjust * 100; // k�� ����

            }

            if (Input.GetButtonDown("Fire2"))
            {
                // ���콺 ������ ��ư�� ������ ���� ����ʹ�.
                lr.enabled = true;
            }
            
            else if(Input.GetButtonUp("Fire2"))
            {
                // ���� �������� �ִٸ�?
                // ��� �ε���
                // ���� �Ⱥ���ʹ�.
                lr.enabled = false;

                if (isHit)
                {
                    player.position = hitInfo.point;
                }

            }
        }
    }
}
