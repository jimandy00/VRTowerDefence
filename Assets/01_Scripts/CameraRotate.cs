using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ī�޶� ȸ��
// ������� ���콺 �Է¿� ���� x, y���� ȸ���ϰ�ʹ�.
public class CameraRotate : MonoBehaviour
{
    float rx, ry; // ���콺�� ������
    public float rotSpeed = 200f; // ����
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. ������� ���콺 �Է��� ����
        // 2. ���������� x, y���� ȸ��
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        rx += my * rotSpeed * Time.deltaTime;
        rx = Mathf.Clamp(rx, -75, 75);

        ry += mx * rotSpeed * Time.deltaTime;

        // �� �ݴ�� �����ϳ���?
        // �¿츦 ���� ���� �¿�� -> y��
        // ���Ʒ��� ���� ���� ���Ϸ� -> x��

        transform.eulerAngles = new Vector3(-rx, ry, 0);

        // -rx�� ���ִ� ����
        // ���� ����
        // ȸ���� �ð����? �̷��� �����ϼ̴µ� ^^



        

    }
}
