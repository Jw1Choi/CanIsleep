using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Chain : MonoBehaviour
{
    float rotSpeed = 300f;
    // chain�� ������ �ʹ�.
    // 1.transform ��rotation�� y���� 1~360 ���� ���� �ø���. 
    // 2. 1���� �ݺ��Ѵ�.
    // 3. �����̼� �ӵ��� �����ϰ� �ʹ�,


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }

}
