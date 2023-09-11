using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �̵��ϰ�ʹ�. �����������
public class EnemyBullet : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward;

    }

    // Update is called once per frame
    void Update()
    {
        if(isHit) {
            transform.rotation = hitRotation;
            transform.position = hitPosition;
        }
        else
        {

        }
        // �ڿ������� �ó��
        rb.transform.forward = rb.velocity.normalized;
    }

    // Ÿ���� �ε�����
    // �� �ܿ� �ε�����
    bool isHit;
    Quaternion hitRotation;
    Vector3 hitPosition;
    private void OnCollisionEnter(Collision collision)
    {
        // Destroy(collision.gameObject);
        // �ð��� ������ �Ѿ��� ��������
        // collision�� ���� ����̶� ���ڸ��� ��������.
        // ������ ���ؼ� ���ڸ��� ���߱�(?)�� �ϰڴ�.
        // ���� ���� 3�� ������ְ�
        if(isHit == false)
        {
            isHit = true;
            hitRotation = transform.rotation; // value type�̶� ���纻 ����. �ٲ� ���� X
            hitPosition = transform.position;
            rb.isKinematic = true; // �߷� X
            rb.useGravity = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    // fade in, out�� �� �� ��� => ���뼺 ������ ���ؼ�
    // float start, end�� �ް� �� �� �ִ�.
    IEnumerator CoFade(/*float start, float end*/)
    {
        float alpha = 1;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for (float time = 0; time <= 1; time += Time.deltaTime)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                Color c = renderers[i].material.color; // ������Ƽ�� �ٷ� = 1 �̷��� �� �־��ִϱ�
                c.a = alpha; // temp�� ��� ������ ����
                renderers[i].material.color = c;
            }
            alpha -= Time.deltaTime;
            yield return 0; // 1�� ���� �ݺ�
        }

        Destroy(gameObject);

    }
}
