using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Eaterplant : MonoBehaviour
{
    Vector3 pos; //������ġ
    float delta = 1.0f; // ���Ϸ� �̵������� (x)�ִ밪
    float speed = 2.0f; // �̵��ӵ�


    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = pos;
        v.y += delta * Mathf.Sin(Time.time * speed);

        transform.position = v;
    }
}
