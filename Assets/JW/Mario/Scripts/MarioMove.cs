using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioMove : MonoBehaviour
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
    
    CharacterController cc;
    float yVelocity = 0;
    float moveSpeed = 0.0f;
    public GameObject enemy;
    public GameObject smoke;

    //������� ���콺 �巡�� �Է��� �޾Ƽ� �����¿�� ȸ����Ű�� �ʹ�.

    //float tongtong = 0;
    //bool isJump = false;
    Animator marioAnim;

    // Start is called before the first frame update
    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
        //tongtong = jumpPower;
        marioAnim = GetComponentInChildren<Animator>();
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
        if (dir.magnitude > 0)
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animSpeed = moveSpeed = dir.magnitude * runSpeed;
                GameObject smo = Instantiate(smoke);
                smo.transform.position = transform.position;
                ParticleSystem ps = smo.GetComponent<ParticleSystem>();
                ps.Stop();
                ps.Play();
            }
            else
            {
                animSpeed = moveSpeed = dir.magnitude * walkSpeed;
            }
        }

        //�÷��̾��� ���� �ӵ��� Animator�� "PlayerSpeed" �Ķ���Ϳ� �����Ѵ�.
        //���꽺�ǵ�/�����ǵ�� Normalize ��Ų ���̴�. (0~1������ ��ȯ�ϱ� ���Ͽ�)
        // < 1D ���> �÷��̾��� ���� �ӵ��� Animator�� "PlaySpeed" �Ķ���͸� �����Ѵ�.
        //playerAnim.SetFloat("PlayerSpeed", animSpeed / runSpeed);

        //<2D ���> �÷��̾��� ���Ĺ� ���Ⱚ�� �¿� ���� ���� �����Ѵ�.

        marioAnim.SetFloat("Speed_V", f);
        marioAnim.SetFloat("Speed_H", h);

        //���� ���͸� ī�޶��� ������ �������� �����Ѵ�.
        //���� ī�޶�� �̱���ó�� ������ �ҷ��� �� �ִ�.
        dir = Camera.main.transform.TransformDirection(dir);
        //1. y�ӵ��� �߷��� ��� ���ϰ� �ʹ�.
        yVelocity += gravity * Time.deltaTime;
        //2. ���� ����ڰ� ���� ��ư�� ������ y �ӵ��� �ٴ� ���� �����ϰ� �ʹ�.
      

        //���� ������� ���� Ƚ���� �ʱ�ȭ�Ѵ�.
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            jumpCount = 1;
            yVelocity = 0;
           
        }


        if (Input.GetButtonDown("Jump") && jumpCount >0)
        {
            yVelocity = jumpPower;
            jumpCount = 0;
            marioAnim.SetTrigger("Jump");
        AudioSource bangSound = gameObject.GetComponent<AudioSource>();

            //bangSound�� 1�� �÷����Ѵ�.
            bangSound.Play();
            //bangSound.volume = 0.2f;
      
        }
        //�Էµ� �������� �̵��Ѵ�.
        //transform.position += dir * movespeed * Time.deltaTime;



        //Vector3 speedVec = new Vector3(dir.x, 0, dir.y);



        //�߷�) �߷°��� �����Ѵ�.

        dir.y = yVelocity;


        //moveSpeed = Input.GetKey(KeyCode.LeftShift) == true ? dir.magnitude * runSpeed : dir.magnitude * walkSpeed;

        cc.Move(dir * moveSpeed * Time.deltaTime);



    }
}