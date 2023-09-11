using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 체력 표시
// 체력의 값이 바뀌면 표현
// 필요 요소 : UI, HP, MaxHP
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
        // 프로퍼티
        // 보안을 강화하기 위해서 -100, 100
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
