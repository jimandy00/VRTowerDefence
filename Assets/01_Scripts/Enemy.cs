using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// �¾ �� ������� �������� �˷��ְ�ʹ�.

// FSM : �˻�, �̵�, ����, �ǰ�, ����(��), ���ߺξ� �� ����(��ź)
public class Enemy : MonoBehaviour
{
    public enum State
    {
        Search,
        Move,
        Attack,
        Damage,
        Die
    }

    // public ���ָ� ����Ƽ���� �ش� ���¸� �� �� �ִ�.
    public State state;

    // ���� ����� ��ġ�� Ÿ���� ã�� ���ؼ� assign�� �ƴ϶�
    // ��ũ��Ʈ�� �ۼ�!
    Transform target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(target.position);
        agent.Warp(transform.position);

        state = State.Search;

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
        }

    }

    private void UpdateDie()
    {
        throw new NotImplementedException();
    }

    // �������� �߻��� �ð��� �����ϰ�
    // �� �ð����κ��� damageTime��ŭ �ð��� �귶�ٸ�
    float damageMoments;
    public float damageTime = 1;
    private void UpdateDamage()
    {
        // �ð��� �帣�ٰ�
        //currentTime += Time.deltaTime;
        if(Time.time - damageMoments >= damageTime)
        {
            // �ٽ� Move���·� �����ϰ�ʹ�.
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
        // ���� �ð����� �Ѿ��� �������� ���� �߻��ϰ�ʹ�.

        // �ð��� �帣�ٰ�
        fireTime += Time.deltaTime;

        // ���� �ð��� fireTime�� �ʰ��ϸ�
        if(fireTime < currentTime)
        {
            // ���� �ð��� 0���� �ʱ�ȭ�ϰ�
            currentTime = 0;

            // �Ѿ� ���忡�� �Ѿ��� �����
            var b = Instantiate(bulletFactory);

            // �Ѿ��� �չ����� �������� ���ϴ� �������� ȸ��
            b.transform.position = transform.position;
            Vector3 dir = target.position - transform.position;
            b.transform.forward = dir + Vector3.up * 10;
        }

    }

    private void UpdateMove()
    {
        // �������� ���ؼ� �̵��ϰ�ʹ�.
        agent.SetDestination(target.position);

        // ���� �����ߴٸ� -> ���������� �Ÿ��� stoppingDistance ���϶��
        float dist = Vector3.Distance(target.position, transform.position);

        // ���ݻ��·� �����ϰ�ʹ�.
        if (dist <= agent.stoppingDistance)
        {
            state = State.Attack;
        }

    }

    private void UpdateSearch()
    {
        // Scene�� ��ġ�� Tower���� ��� ã�Ƽ�
        // ���� ���� ����� Tower�� ����ϰ�ʹ�.
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        // �ִܰŸ�(�迭 ��ȣ�� Ÿ���� ������ �ִܰŸ�), �迭��ȣ ����ϱ�
        float distance = float.MaxValue;
        int chooseIndex = -1; // �迭 �ʱ�ȭ ���� -1�� ���� ���

        for (int i = 0; i < towers.Length; i++)
        {
            // Ÿ���� ������ �Ÿ��� ���
            float temp = Vector3.Distance(towers[i].transform.position, transform.position);

            // �� �Ÿ��� �ִ� �Ÿ����� �۴ٸ�
            // �ִܰŸ��� �����ϰ�
            // ���� �迭��ȣ�� �����ϰ�ʹ�.
            if(temp < distance)
            {
                distance = temp;
                chooseIndex = i;
            }
        }

        // ���� ���� ����� Ÿ���� ����ϰ�
        // ���� ���¸� Move ���·� �����ϰڴ�.
        if (chooseIndex != -1)
        {
            target = towers[chooseIndex].transform;
            state = State.Move;
        }

    }

    HPBase hpBase;
    public void DiePlz(int dmg)
    {
        // �ϴ� �����ϰ�
        agent.isStopped = true;

        if (hpBase == null)
        {
            hpBase = GetComponent<HPBase>();
            // hpBase = new HPBase(); �̷��� ���ϰ� GetComponent�� ���ִ� ����?
        }

        // ü���� dmg��ŭ ���ҽ�Ű�� �ʹ�.
        hpBase.HP -= dmg;

        if(hpBase.HP <= 0)
        {
            // ���� ���·� �����ϰ�
            state = State.Die;

            // 1�� �Ŀ� �ı�
            Destroy(gameObject, 1);
        }
        else
        {
            // ��ģ ���·� �����ϰ�
            state = State.Damage;
            // currentTime = 0;
            damageMoments = Time.time; // ���� �߻��� �ð��� ����ϱ�
        }


    }
}
