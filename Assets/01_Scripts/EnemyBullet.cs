using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 앞으로 이동하고싶다. 물리기반으로
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
        // 자연스러운 곡선처리
        rb.transform.forward = rb.velocity.normalized;
    }

    // 타워에 부딪히면
    // 그 외에 부딪히면
    bool isHit;
    Quaternion hitRotation;
    Vector3 hitPosition;
    private void OnCollisionEnter(Collision collision)
    {
        // Destroy(collision.gameObject);
        // 시간이 지나면 총알이 없어지기
        // collision은 물리 기반이라서 닿자마자 없어진다.
        // 예방을 위해서 닿자마자 멈추기(?)를 하겠다.
        // 위에 변수 3개 만들어주고
        if(isHit == false)
        {
            isHit = true;
            hitRotation = transform.rotation; // value type이라서 복사본 저장. 바뀌어도 변경 X
            hitPosition = transform.position;
            rb.isKinematic = true; // 중력 X
            rb.useGravity = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    // fade in, out을 둘 다 사용 => 범용성 증가를 위해서
    // float start, end를 받게 할 수 있다.
    IEnumerator CoFade(/*float start, float end*/)
    {
        float alpha = 1;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for (float time = 0; time <= 1; time += Time.deltaTime)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                Color c = renderers[i].material.color; // 프로퍼티는 바로 = 1 이렇게 못 넣어주니까
                c.a = alpha; // temp에 담는 식으로 변경
                renderers[i].material.color = c;
            }
            alpha -= Time.deltaTime;
            yield return 0; // 1초 동안 반복
        }

        Destroy(gameObject);

    }
}
