using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Eaterplant : MonoBehaviour
{
    Vector3 pos; //현재위치
    float delta = 1.0f; // 상하로 이동가능한 (x)최대값
    float speed = 2.0f; // 이동속도


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
