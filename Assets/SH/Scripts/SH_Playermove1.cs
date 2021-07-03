using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Playermove1 : MonoBehaviour
{
    // 플레이어가 w, s, a, d를 이용해서 전후좌우로 이동시키고 싶다.
    // 필요요소 : 속력, 방향, 사용자의 입력
    public float moveSpeed = 9f;



    void Start()
    {

    }

    void Update()
    {
        // 사용자의 입력을 받는다.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // 방향을 설정한다.
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();


        // 입력된 방향으로 이동한다(p = p0 + vt).
        transform.position += dir * moveSpeed * Time.deltaTime;

        // 방향을 설정한다.

    }

}
