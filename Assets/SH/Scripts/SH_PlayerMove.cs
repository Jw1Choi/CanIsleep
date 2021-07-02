using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float gravity = -9.81f;
    public float jumpPower = 5.0f;
    float yVelcity;
    CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0, y);
        //Time.deltaTime을 곱해주면 속도가 너무 느려지기 때문에 moveSpeed를 높여주어야 한다.

   

        //마지막 스피드를 moveSpeed로 바꿔준다.
        float finalSpeed = moveSpeed;

        //1.y속도에 중력을 계속 더하고 싶다.
        yVelcity += gravity * Time.deltaTime;
        
        //2. 만약 사용자가 점프 버튼을 누르면 y속도에 뛰는 힘을 대입하고 싶다.
        if (Input.GetButtonDown("Jump"))
        {
            yVelcity = jumpPower;
        }

        //3. y속도를 최종 dir의 y에 대입하고 싶다.

        dir.Normalize();
        dir.y = yVelcity;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            finalSpeed = moveSpeed * 2;
        }
        print(finalSpeed);

        cc.Move(dir * finalSpeed * Time.deltaTime);
    }
}
