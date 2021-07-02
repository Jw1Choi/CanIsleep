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
        //Time.deltaTime�� �����ָ� �ӵ��� �ʹ� �������� ������ moveSpeed�� �����־�� �Ѵ�.

   

        //������ ���ǵ带 moveSpeed�� �ٲ��ش�.
        float finalSpeed = moveSpeed;

        //1.y�ӵ��� �߷��� ��� ���ϰ� �ʹ�.
        yVelcity += gravity * Time.deltaTime;
        
        //2. ���� ����ڰ� ���� ��ư�� ������ y�ӵ��� �ٴ� ���� �����ϰ� �ʹ�.
        if (Input.GetButtonDown("Jump"))
        {
            yVelcity = jumpPower;
        }

        //3. y�ӵ��� ���� dir�� y�� �����ϰ� �ʹ�.

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
