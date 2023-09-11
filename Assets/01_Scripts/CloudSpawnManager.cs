using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일정시간마다 적공장에서 랜덤한 위치를 정하고
// 그곳에 적을 만들어서 랜덤한 위치에 배치하고싶다.
// 필요 요소 : 현재시간, 생성시간, 생성 할 적(공장)

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
        // 시간이 흐르다가
        currentTime += Time.deltaTime;

        // 생성 시간이 되면
        if(currentTime >= makeTime)
        {
            // 랜덤으로 위치값을 받아서
            Vector3 pos = GetRandomPosition();

            // 적을 생성한다.
            MakeEnemy(pos);

            // 시간을 초기화
            currentTime = 0f;
        }

    }

    private Vector3 GetRandomPosition()
    {
        // 사각형의 안쪽에 임의의 점 하나를 찍어서
        float minX, maxX, minZ, maxZ;
        Vector3 p = transform.position; // 내 위치를 기준으로
        float width = transform.localScale.x; // 가로와 세로의 폭을
        float height = transform.localScale.y;
        minX = p.x - width * 0.5f;
        maxX = p.x + width * 0.5f;
        minZ = p.z - height * 0.5f;
        maxZ = p.z + height * 0.5f;

        float x = UnityEngine.Random.Range(minX, maxX);
        float y = p.y;
        float z = UnityEngine.Random.Range(minZ, maxZ);

        Vector3 origin = new Vector3(x, y, x); // ray를 만들 출발점

        // 그 위치에서 아래를 향하는 Ray를 만들고
        Ray ray = new Ray(origin, Vector3.down);
        RaycastHit hit;
        // 닿은곳의 태그가 허공이 아니고
        // Ground라면 그 위치를 반환하고싶다.
        
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

    // 적을 생성하는 메서드
    void MakeEnemy(Vector3 pos)
    {
        // 적 공장에서 적을 만들어서
        var enemy = Instantiate(enemyFactory);
        enemy.transform.position = pos;
        // pos위치에 배치
    }
}
