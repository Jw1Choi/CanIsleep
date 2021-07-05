using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 3.5f;

    public float runSpeed = 7.0f;

    //플레이어게 중력을 적용하고 싶다.
    //중력의 방향 , 중력의 크기
    public float gravity = -9.8f;

    //사용자의 입력을 받아서 점프를 하고 싶다.
    //사용자의 Space 키 입력을 받는다.
    //위쪽 방향으로의 (점프력)힘을 추가한다.
    public float jumpPower = 30.0f;
    //단 2회까지만 점프하고 싶다.
    public int jumpCount = 2;
    int healthPoint = 100;
    CharacterController cc;
    float yVelocity = 0;
    float moveSpeed = 0.0f;
    public GameObject touchPad;

    public GameObject enemy;

    //사용자의 마우스 드래그 입력을 받아서 상하좌우로 회전시키고 싶다.

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
        //좌표값 설정
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


        //(월드 기준)방향을 설정한다.
        Vector3 dir = new Vector3(f, 0, h);
        dir.Normalize();
        //게임 캐릭터의 좌표로 설정(속도)만 넣어주면 됨

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

        //플레이어의 현재 속도를 Animator의 "PlayerSpeed" 파라미터에 전달한다.
        //무브스피드/런스피드는 Normalize 시킨 것이다. (0~1값으로 전환하기 위하여)
        // < 1D 방식> 플레이어의 현재 속도를 Animator의 "PlaySpeed" 파라미터를 전달한다.
        //playerAnim.SetFloat("PlayerSpeed", animSpeed / runSpeed);

        //<2D 방식> 플레이어의 전후방 방향값과 좌우 방향 값을 전달한다.

        playerAnim.SetFloat("Speed_V", f);
        playerAnim.SetFloat("Speed_H", h);

        //방향 벡터를 카메라의 방향을 기준으로 재계산한다.
        //메인 카메라는 싱글톤처럼 언제든 불러올 수 있다.
        dir = Camera.main.transform.TransformDirection(dir);

        //땅에 닿았으면 점프 횟수가 초기화한다.
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
        //입력된 방향으로 이동한다.
        //transform.position += dir * movespeed * Time.deltaTime;



        //Vector3 speedVec = new Vector3(dir.x, 0, dir.y);



        //중력) 중력값을 적용한다.
        yVelocity += gravity * Time.deltaTime;

        dir.y = yVelocity;


        //moveSpeed = Input.GetKey(KeyCode.LeftShift) == true ? dir.magnitude * runSpeed : dir.magnitude * walkSpeed;

        cc.Move(dir * moveSpeed * Time.deltaTime);

       

    }

    public void ApplyDamage(int val)
    {
        healthPoint -= val;
        
        //MAX 는 비교 수치 중 더 큰 값을 보여준다.
        //0 이하로 heathpoint가 떨어지면 0만 호출 됨
        healthPoint = Mathf.Max(healthPoint, 0);
        print("현재 체력" + healthPoint);    
    }
}
