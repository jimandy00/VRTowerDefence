using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 태어날 때 요원에게 목적지를 알려주고싶다.

// FSM : 검색, 이동, 공격, 피격, 죽음(총), 공중부양 후 죽음(폭탄)
public class Enemy : MonoBehaviour
{
    public enum State
    {
        Search,
        Move,
        Attack,
        Damage,
        Die,
        FlyingDie
    }

    // public 해주면 유니티에서 해당 상태를 알 수 있다.
    public State state;

    // 나랑 가까운 위치의 타워를 찾기 위해서 assign이 아니라
    // 스크립트로 작성!
    Transform target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(target.position);
        agent.Warp(transform.position);

        state = State.Search;

        drondOriginSize = drone.localScale;

        droneOriginLocalPosition = drone.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Search:
                UpdateSearch();
                break;
            case State.Move:
                UpdateMove();
                break;
            case State.Attack:
                UpdateAttack();
                break;
            case State.Damage:
                UpdateDamage();
                break;
            case State.Die:
                UpdateDie();
                break;
            case State.FlyingDie:
                UpdateFlyingDie();
                break;
        }

    }

    public Transform drone;
    Vector3 drondOriginSize;
    Vector3 droneOriginLocalPosition;
    private void UpdateDie()
    {
        // 1초 동안
        currentTime += Time.deltaTime;

        if(currentTime <= 1)
        {
            // 2배 점점 커지게 하고싶다.
            // 그냥 키우면 enemy가 커지니까 drone을 키워야한다.
            Vector3 targetScale = drondOriginSize * 2;
            drone.localScale = Vector3.Lerp(drondOriginSize, targetScale, currentTime); // 3초동안 갈거야? currentTiem/3 == 1이니까

            // 랜덤으로 진동하고싶다.
            //drone.localPosition = droneOriginLocalPosition + UnityEngine.Random.insideUnitSphere * 0.1f;
            drone.localPosition = droneOriginLocalPosition + new Vector3(1, 0, 1) * UnityEngine.Random.value * 0.1f * currentTime;
        }
        else
        {
            // 1초 후에 펑 터지고싶다.
            Destroy(gameObject);
        }
    }


    private void UpdateFlyingDie()
    {
        throw new NotImplementedException();
    }

    

    // 데미지가 발생한 시간을 저장하고
    // 그 시간으로부터 damageTime만큼 시간이 흘렀다면
    float damageMoments;
    public float damageTime = 1;
    private void UpdateDamage()
    {
        // 시간이 흐르다가
        //currentTime += Time.deltaTime;
        if(Time.time - damageMoments >= damageTime)
        {
            // 다시 Move상태로 전이하고싶다.
            agent.isStopped = false;
            state = State.Move;
        }

    }


    float currentTime;
    
    [SerializeField]
    float fireTime = 2f;
    public GameObject bulletFactory;
    public GameObject firePosition;
    private void UpdateAttack()
    {
        // 일정 시간마다 총알을 목적지를 향해 발사하고싶다.

        // 시간이 흐르다가
        fireTime += Time.deltaTime;

        // 현재 시간이 fireTime을 초과하면
        if(fireTime < currentTime)
        {
            // 현재 시간을 0으로 초기화하고
            currentTime = 0;

            // 총알 공장에서 총알을 만들고
            var b = Instantiate(bulletFactory);

            // 총알의 앞방향을 목적지를 향하는 방향으로 회전
            b.transform.position = transform.position;
            Vector3 dir = target.position - transform.position;
            b.transform.forward = dir + Vector3.up * 10;
        }

    }

    private void UpdateMove()
    {
        // 목적지를 향해서 이동하고싶다.
        agent.SetDestination(target.position);

        // 만약 도착했다면 -> 목적지와의 거리가 stoppingDistance 이하라면
        float dist = Vector3.Distance(target.position, transform.position);

        // 공격상태로 전이하고싶다.
        if (dist <= agent.stoppingDistance)
        {
            state = State.Attack;
        }

    }

    private void UpdateSearch()
    {
        // Scene에 배치된 Tower들을 모두 찾아서
        // 나와 가장 가까운 Tower를 기억하고싶다.
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        // 최단거리(배열 번호의 타워와 나와의 최단거리), 배열번호 기억하기
        float distance = float.MaxValue;
        int chooseIndex = -1; // 배열 초기화 값은 -1을 많이 사용

        for (int i = 0; i < towers.Length; i++)
        {
            // 타워와 나와의 거리를 재고
            float temp = Vector3.Distance(towers[i].transform.position, transform.position);

            // 그 거리가 최단 거리보다 작다면
            // 최단거리를 갱신하고
            // 선택 배열번호를 갱신하고싶다.
            if(temp < distance)
            {
                distance = temp;
                chooseIndex = i;
            }
        }

        // 나와 가장 가까운 타워를 기억하고
        // 나의 상태를 Move 상태로 전이하겠다.
        if (chooseIndex != -1)
        {
            target = towers[chooseIndex].transform;
            state = State.Move;
        }

    }

    HPBase hpBase;
    public void DiePlz(int dmg)
    {
        // 일단 정지하고
        agent.isStopped = true;

        if (hpBase == null)
        {
            hpBase = GetComponent<HPBase>();
            // hpBase = new HPBase(); 이렇게 안하고 GetComponent를 해주는 이유?
        }

        // 체력을 dmg만큼 감소시키고 싶다.
        hpBase.HP -= dmg;

        if(hpBase.HP <= 0)
        {
            // 죽음 상태로 전이하고
            state = State.Die;

            // 만약 데미지가 1이면 그냥 죽음
            // 그렇지 않으면 FlyingDie
            if(dmg ==1)
            {
                state = State.Die;
            }
            else
            {
                state = State.FlyingDie;
            }
            

            // 1초 후에 파괴
            //Destroy(gameObject, 1);


        }
        else
        {
            // 다친 상태로 전이하고
            state = State.Damage;
            // currentTime = 0;
            damageMoments = Time.time; // 현재 발생한 시간을 기억하기
        }


    }
}
