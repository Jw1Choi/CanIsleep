using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Playermove1 : MonoBehaviour
{
    // �÷��̾ w, s, a, d�� �̿��ؼ� �����¿�� �̵���Ű�� �ʹ�.
    // �ʿ��� : �ӷ�, ����, ������� �Է�
    public float moveSpeed = 9f;



    void Start()
    {

    }

    void Update()
    {
        // ������� �Է��� �޴´�.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // ������ �����Ѵ�.
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();


        // �Էµ� �������� �̵��Ѵ�(p = p0 + vt).
        transform.position += dir * moveSpeed * Time.deltaTime;

        // ������ �����Ѵ�.

    }

}
