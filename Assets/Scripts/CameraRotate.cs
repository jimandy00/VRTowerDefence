using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 카메라 회전
// 사용자의 마우스 입력에 따라 x, y축을 회전하고싶다.
public class CameraRotate : MonoBehaviour
{
    float rx, ry; // 마우스는 누적값
    public float rotSpeed = 200f; // 감도
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 사용자의 마우스 입력을 누적
        // 2. 누적값으로 x, y축을 회전
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        rx += my * rotSpeed * Time.deltaTime;
        rx = Mathf.Clamp(rx, -75, 75);

        ry += mx * rotSpeed * Time.deltaTime;

        // 왜 반대로 누적하나요?
        // 좌우를 보면 고개를 좌우로 -> y축
        // 위아래를 보면 고개를 상하로 -> x축

        transform.eulerAngles = new Vector3(-rx, ry, 0);

        // -rx를 해주는 이유
        // 이해 못함
        // 회전은 시계방향? 이렇게 말씀하셨는데 ^^



        

    }
}
