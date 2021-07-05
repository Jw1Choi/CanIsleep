using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyFSM : MonoBehaviour
{
    //열거형 상수
    
    //eu
    public enum EnemyState
    {
        //선택지 중에 한가지만 true가 될 수 있다.
        Idle,
        Move,
        Attack,
        AttackToMove,
        Damaged,
        Die,
    }

   public EnemyState eState;
    public float sightRange = 5.0f;
    public float attackRange = 5.0f;
    public int attackPower = 30;
    public float delayTime = 1.0f;
    public float moveSpeed = 9.0f;
    public int maxHP = 100;
    
    Transform player;
    CharacterController cc;
    float rotRate = 0;
    Quaternion startRotation;
    float currentTime = 0;
    bool isBooked = false;
    int healthPoint;
    Animator enemyAnim;
    bool playMoveAni = false;
    NavMeshAgent smith;
    public Transform GetTargetTransform()
    {
        return player;

    }

    public float GetHp()
    {
        return healthPoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        //체력 정보 초기화하기
        healthPoint = maxHP;

        //최초의 상태는 대기 상태이다.
        eState = EnemyState.Idle;

        //플레이어를 찾는다.
        player = GameObject.Find("Player").transform;
        cc = transform.GetComponent<CharacterController>();

        //자식 오브젝트로부터 Animator 컴포넌트를 가져온다.
        enemyAnim = GetComponentInChildren<Animator>();
        if(enemyAnim ==null)
        {
            //print("자식 오브젝트에 애니메이터 없음");

        }
        else
        {
            //print("자식 오브젝트의 애니메이터 가조욤");
        }

        smith = GetComponent<NavMeshAgent>();
        smith.speed = 5;
        smith.acceleration = 10.0f;
        smith.stoppingDistance = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        //각 상태별 처리
        //보기가 깔끔하고 수정하기도 쉽다.
        switch(eState)
        {
            case EnemyState.Idle:
                Idle();
            
              
                break;
            case EnemyState.Move:
                Move2();
                //Move();
                //to do
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damaged:
                CheckClipTime();

                break;
            case EnemyState.Die:
                Die();
                break;
                
                //if문의 else와 비슷함
            default:
                //to do
                break;
        }

        //약간 지저분한 방식
        //if(eState == EnemyState.Idle)
        //{

        //}
        //else if (eState == EnemyState.Move)
        //{

        //}
        //else if (eState == EnemyState.Attack)
        //{

        //}

    }

    private void Idle()
    {
        float distance = (player.position - transform.position).magnitude;

        if (sightRange >= distance)
        {

            SetMoveState();
            //eState = EnemyState.Move;
            //// 현재 회전 상태를 저장해둔다.
            //startRotation = transform.rotation;

            ////회전 보간을 위한 rotRate 도 0으로 초기화한다.
            //rotRate = 0;
        }
    }

    private void Move()
    {
        //플레이어 방향으로 이동한다.
        
        Vector3 dir = player.position - transform.position;
        float distance = dir.magnitude;

        //만일, 플레이어와의 거리가 공격 범위 이내로 접근했다면 공격 상태로 전환한다.
        //필요 요소 : 공격 범위
        if (distance <= attackRange)
        {
            eState = EnemyState.Attack;
            currentTime = 0;
            //CancelInvoke();

            enemyAnim.SetTrigger("MoveToAttack");

           //조건에 맞다면 함수를 종료한다.
           //리턴이 나오면 아래 함수는 보지 않고 종료한다.
            return;
        }


        dir.Normalize();
        cc.Move(dir * moveSpeed * Time.deltaTime);

        //이동 방향을 바라보도록 회전한다.(오류가 생길 여지가 잘 없음)
        //하지만 이 코드는 고개를 너무 빨리 돌아서 이상함
        //transform.rotation = Quaternion.LookRotation(dir);

        //시작점과 마지막 점을 변수로 만들어준다.
        Quaternion startRot = startRotation;
        Quaternion endRot = Quaternion.LookRotation(dir);
        rotRate += Time.deltaTime *2.0f;
        //print(startRotation.eulerAngles);

        //선형 보간을 이용하여 (시작점, 끝지점, 비율)
        transform.rotation = Quaternion.Lerp(startRot, endRot, rotRate);


        //이동 방향을 바라보도록 회전하는 방식 (비추)
        //transform.forward = dir;

     
    }

    void Move2()
    {
        smith.enabled = true;
        //플레이어의 위치를 네브메쉬의 목적지로 설정한다.
        smith.SetDestination(player.position);
        smith.isStopped = false;

        float dist = Vector3.Distance(player.position, transform.position);
        if(dist < attackRange)
        {
            eState = EnemyState.Attack;
            enemyAnim.SetTrigger("MoveToAttack");
            smith.isStopped = true;
        }
    }

    private void Attack()
    {
        //타겟에게 데미지를 준다.
        //플레이어의 HP를 감소시킨다.
        //공격력에 맞게 감소시킨다.
        // 매 1초마다 타겟의 체력을 나의 공격력만큼 감소시킨다.
       
        currentTime += Time.deltaTime;
        //사정거리 안에 있어야 하고)
        if (Vector3.Distance(player.position, transform.position) < attackRange)
        {
            if (currentTime > delayTime)
            {
                //HP는 플레이어 입장에서 매우 중요한 개념이기 때문에 버그나지 않게 잘 써야 한다.
                //HP에 직접적인 접근보다 함수로 접근하는게 일반적이다.
               
                currentTime = 0;
                //print("공격");
                enemyAnim.SetTrigger("DelayToAttack");
            }
        }
        //공격 범위 밖이면
        else
        {
            if (!isBooked)
            {
                //1.5초 뒤에 이동 상태로 전환한다.
                Invoke("SetMoveState", 1.5f);
                isBooked = true;
            }
            //Invoke("SetMoveState", 1.5f);
            eState = EnemyState.AttackToMove;

        }
    }

    void SetMoveState()
    {
        eState = EnemyState.Move;

        //이동 애니메이션을 실행한다.
        enemyAnim.SetTrigger("IdleToMove");

        startRotation = transform.rotation;
        //회전 보간을 위한 rotRate 도 0으로 초기화한다.
        rotRate = 0;

        isBooked = false;
    }

  
    //피격 처리 함수
   
    public void Damaged(int val)
    {
        if(eState == EnemyState.Move)
        {
            playMoveAni = true;
        }
        else
        {
            playMoveAni = false;
        }

        //네비게이션 메쉬를 멈춘다.
        //smith.isStopped = true;

        //네비게이션 메쉬 자체를 
        smith.enabled = false;

        healthPoint = Mathf.Max(healthPoint - val, 0);

        
        //만일 체력이 0 이하이면 Die 상태로 변경한다.
        if(healthPoint <=0)
        {
            eState = EnemyState.Die;
            //죽음 애니메이션을 호출한다.
            enemyAnim.SetBool("isDie", true);

            //캐릭터 컨트롤러, 캡슐 콜라이더 체크를 해제한다.
            //특정 컴포넌트만 비활성
            cc.enabled = false;
            //캡슐 콜라이더 비활성(따로 변수를 줄 필요 없이 한번만 쓰는 것이기 때문에 그대로 불러낸다.
            GetComponent<CapsuleCollider>().enabled = false;


        }
        else { 


        //그렇지 않으면, Damaged 상태로 변경한다.

        //맞았을 때 피격모션이 있게 끔 만든다.
        eState = EnemyState.Damaged;

            //피격 애니메이션을 호출한다.
            enemyAnim.SetTrigger("OnHit");

          

            //그리고 그 값을 넣어준다.


        //피격 에니메이션 중에는 맞지 않도록
        //Invoke("ReturnState", 0.9f);
        }

    }

    private void CheckClipTime()
    {
        //피격 애니메이션 클립의 총 길이를 구한다.
        AnimatorStateInfo myStateInfo = enemyAnim.GetCurrentAnimatorStateInfo(0);
        //console 에서 3이상 넘어가는 것들은 Loop 애니메이션을 얼마나 반복했는지를 보여주는 것 때문이다.
        //print("length는" + myStateInfo.length);

        //만일, 현재 상태의 이름이 "Hit State"라면...
        if(playMoveAni)
        {
            if(myStateInfo.IsName("Hit State"))
            {
                playMoveAni = false;
            }
        }
        else
        {
            if(myStateInfo.IsName("Move_state"))
            {
                ReturnState();
            }
        }
        //if(myStateInfo.IsName("Hit State") && myStateInfo.normalizedTime > 1.0f)
        //{
            
        //    ReturnState();
      
        //}
       
    }

    void ReturnState()
    {
      
        eState = EnemyState.Move;
        startRotation = transform.rotation;
        //회전 보간을 위한 rotRate 도 0으로 초기화한다.
        rotRate = 0;
    }

    private void Die()
    {
        ////만일, isDie 파라미터 값이 True 라면, false로 전환해준다.


        //만일,Die 애니메이션이 실행 중이고, 애니메이션 진행률이 1.0(100%)에 도달했을 때
        AnimatorStateInfo myState = enemyAnim.GetCurrentAnimatorStateInfo(0);
        if(myState.IsName("Die State"))
        {
            enemyAnim.SetBool("isDie", false);
            //normalizedTime은 진행률 0 ~ 100%를 0 ~ 1로 환원하여 나타내는 것
            //조건을 2개로 나눈 것은 (Die state 와 normalizedTime) 
            if(myState.normalizedTime >= 0.8f)
            { 
            Destroy(gameObject);
        }
        }

        //자기 자신을 제거한다.
    }
}
