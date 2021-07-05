using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 3.5f;

    public float runSpeed = 7.0f;

    //�÷��̾�� �߷��� �����ϰ� �ʹ�.
    //�߷��� ���� , �߷��� ũ��
    public float gravity = -9.8f;

    //������� �Է��� �޾Ƽ� ������ �ϰ� �ʹ�.
    //������� Space Ű �Է��� �޴´�.
    //���� ���������� (������)���� �߰��Ѵ�.
    public float jumpPower = 30.0f;
    //�� 2ȸ������ �����ϰ� �ʹ�.
    public int jumpCount = 2;
    int healthPoint = 100;
    CharacterController cc;
    float yVelocity = 0;
    float moveSpeed = 0.0f;
    public GameObject touchPad;

    public GameObject enemy;

    //������� ���콺 �巡�� �Է��� �޾Ƽ� �����¿�� ȸ����Ű�� �ʹ�.

    float tongtong = 0;
    bool isJump = false;
    Animator playerAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
        tongtong = jumpPower;
        playerAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //��ǥ�� ����
        float f = Input.GetAxis("Horizontal");
        float h = Input.GetAxis("Vertical");
        
        //float up = 0.0f;
        //if(Input.GetKey(KeyCode.E))
        //{
        //    up = 1;
        //}
        //else if(Input.GetKeyDown(KeyCode.E))
        //{

        //    up = 0;
        //}

        //if(Input.GetKey(KeyCode.Q))
        //{
        //    up = -1;
        //}else if(Input.GetKey(KeyCode.Q))
        //{
        //    up = 0;
        //}


        //(���� ����)������ �����Ѵ�.
        Vector3 dir = new Vector3(f, 0, h);
        dir.Normalize();
        //���� ĳ������ ��ǥ�� ����(�ӵ�)�� �־��ָ� ��

        moveSpeed = walkSpeed;
        float animSpeed = 0;
        if(dir.magnitude >0)
        {

        if (Input.GetKey(KeyCode.LeftShift))
        {
                animSpeed = moveSpeed = dir.magnitude * runSpeed;
        }
        else
        {
                animSpeed= moveSpeed = dir.magnitude * walkSpeed;
        }
        }

        //�÷��̾��� ���� �ӵ��� Animator�� "PlayerSpeed" �Ķ���Ϳ� �����Ѵ�.
        //���꽺�ǵ�/�����ǵ�� Normalize ��Ų ���̴�. (0~1������ ��ȯ�ϱ� ���Ͽ�)
        // < 1D ���> �÷��̾��� ���� �ӵ��� Animator�� "PlaySpeed" �Ķ���͸� �����Ѵ�.
        //playerAnim.SetFloat("PlayerSpeed", animSpeed / runSpeed);

        //<2D ���> �÷��̾��� ���Ĺ� ���Ⱚ�� �¿� ���� ���� �����Ѵ�.

        playerAnim.SetFloat("Speed_V", f);
        playerAnim.SetFloat("Speed_H", h);

        //���� ���͸� ī�޶��� ������ �������� �����Ѵ�.
        //���� ī�޶�� �̱���ó�� ������ �ҷ��� �� �ִ�.
        dir = Camera.main.transform.TransformDirection(dir);

        //���� ������� ���� Ƚ���� �ʱ�ȭ�Ѵ�.
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            jumpCount = 2;
            yVelocity = 0;
            if (isJump)
            {
                tongtong *= 0.3f;
            
            if (tongtong > 0.1f)
            {
                yVelocity = tongtong;
            }
            else
            {
                tongtong = jumpPower;
                isJump = false;
            }
            }
        }
     

        if(Input.GetButtonDown("Jump") && jumpCount> 0)
        {
        
            yVelocity = tongtong;
            jumpCount--;
            isJump = true;
        }
        //�Էµ� �������� �̵��Ѵ�.
        //transform.position += dir * movespeed * Time.deltaTime;



        //Vector3 speedVec = new Vector3(dir.x, 0, dir.y);



        //�߷�) �߷°��� �����Ѵ�.
        yVelocity += gravity * Time.deltaTime;

        dir.y = yVelocity;


        //moveSpeed = Input.GetKey(KeyCode.LeftShift) == true ? dir.magnitude * runSpeed : dir.magnitude * walkSpeed;

        cc.Move(dir * moveSpeed * Time.deltaTime);

       

    }

    public void ApplyDamage(int val)
    {
        healthPoint -= val;
        
        //MAX �� �� ��ġ �� �� ū ���� �����ش�.
        //0 ���Ϸ� heathpoint�� �������� 0�� ȣ�� ��
        healthPoint = Mathf.Max(healthPoint, 0);
        print("���� ü��" + healthPoint);    
    }
}
