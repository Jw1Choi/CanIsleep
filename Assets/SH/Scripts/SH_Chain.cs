using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Chain : MonoBehaviour
{
    float rotSpeed = 300f;
    // chain을 돌리고 싶다.
    // 1.transform 의rotation중 y축을 1~360 까지 점점 늘린다. 
    // 2. 1번을 반복한다.
    // 3. 로테이션 속도를 조절하고 싶다,


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
