using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ð����� �����忡�� ������ ��ġ�� ���ϰ�
// �װ��� ���� ���� ������ ��ġ�� ��ġ�ϰ�ʹ�.
// �ʿ� ��� : ����ð�, �����ð�, ���� �� ��(����)

public class CloudSpawnManager : MonoBehaviour
{
    float currentTime;
    public float makeTime = 2f;
    public GameObject enemyFactory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �ð��� �帣�ٰ�
        currentTime += Time.deltaTime;

        // ���� �ð��� �Ǹ�
        if(currentTime >= makeTime)
        {
            // �������� ��ġ���� �޾Ƽ�
            Vector3 pos = GetRandomPosition();

            // ���� �����Ѵ�.
            MakeEnemy(pos);

            // �ð��� �ʱ�ȭ
            currentTime = 0f;
        }

    }

    private Vector3 GetRandomPosition()
    {
        // �簢���� ���ʿ� ������ �� �ϳ��� ��
        float minX, maxX, minZ, maxZ;
        Vector3 p = transform.position; // �� ��ġ�� ��������
        float width = transform.localScale.x; // ���ο� ������ ����
        float height = transform.localScale.y;
        minX = p.x - width * 0.5f;
        maxX = p.x + width * 0.5f;
        minZ = p.z - height * 0.5f;
        maxZ = p.z + height * 0.5f;

        float x = UnityEngine.Random.Range(minX, maxX);
        float y = p.y;
        float z = UnityEngine.Random.Range(minZ, maxZ);

        Vector3 origin = new Vector3(x, y, x); // ray�� ���� �����

        // �� ��ġ���� �Ʒ��� ���ϴ� Ray�� �����
        Ray ray = new Ray(origin, Vector3.down);
        RaycastHit hit;
        // �������� �±װ� ����� �ƴϰ�
        // Ground��� �� ��ġ�� ��ȯ�ϰ�ʹ�.
        
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.CompareTag("Ground"))
            {
                return hit.point;
            }
        }

        return GetRandomPosition();
        return Vector3.zero;
    }

    // ���� �����ϴ� �޼���
    void MakeEnemy(Vector3 pos)
    {
        // �� ���忡�� ���� ����
        var enemy = Instantiate(enemyFactory);
        enemy.transform.position = pos;
        // pos��ġ�� ��ġ
    }
}
