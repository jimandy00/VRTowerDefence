using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ü�� ǥ��
// ü���� ���� �ٲ�� ǥ��
// �ʿ� ��� : UI, HP, MaxHP
public class HPBase : MonoBehaviour
{
    int hp = 0;
    public int MaxHP = 2;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = MaxHP;
        hp = MaxHP;
    }

    public int HP
    {
        // ������Ƽ
        // ������ ��ȭ�ϱ� ���ؼ� -100, 100
        get { return hp - 100; }
        set 
        { 
            hp = value + 100;
            slider.value = hp;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
