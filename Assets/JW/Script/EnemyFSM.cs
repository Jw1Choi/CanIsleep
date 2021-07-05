using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyFSM : MonoBehaviour
{
    //������ ���
    
    //eu
    public enum EnemyState
    {
        //������ �߿� �Ѱ����� true�� �� �� �ִ�.
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
        //ü�� ���� �ʱ�ȭ�ϱ�
        healthPoint = maxHP;

        //������ ���´� ��� �����̴�.
        eState = EnemyState.Idle;

        //�÷��̾ ã�´�.
        player = GameObject.Find("Player").transform;
        cc = transform.GetComponent<CharacterController>();

        //�ڽ� ������Ʈ�κ��� Animator ������Ʈ�� �����´�.
        enemyAnim = GetComponentInChildren<Animator>();
        if(enemyAnim ==null)
        {
            //print("�ڽ� ������Ʈ�� �ִϸ����� ����");

        }
        else
        {
            //print("�ڽ� ������Ʈ�� �ִϸ����� ������");
        }

        smith = GetComponent<NavMeshAgent>();
        smith.speed = 5;
        smith.acceleration = 10.0f;
        smith.stoppingDistance = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        //�� ���º� ó��
        //���Ⱑ ����ϰ� �����ϱ⵵ ����.
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
                
                //if���� else�� �����
            default:
                //to do
                break;
        }

        //�ణ �������� ���
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
            //// ���� ȸ�� ���¸� �����صд�.
            //startRotation = transform.rotation;

            ////ȸ�� ������ ���� rotRate �� 0���� �ʱ�ȭ�Ѵ�.
            //rotRate = 0;
        }
    }

    private void Move()
    {
        //�÷��̾� �������� �̵��Ѵ�.
        
        Vector3 dir = player.position - transform.position;
        float distance = dir.magnitude;

        //����, �÷��̾���� �Ÿ��� ���� ���� �̳��� �����ߴٸ� ���� ���·� ��ȯ�Ѵ�.
        //�ʿ� ��� : ���� ����
        if (distance <= attackRange)
        {
            eState = EnemyState.Attack;
            currentTime = 0;
            //CancelInvoke();

            enemyAnim.SetTrigger("MoveToAttack");

           //���ǿ� �´ٸ� �Լ��� �����Ѵ�.
           //������ ������ �Ʒ� �Լ��� ���� �ʰ� �����Ѵ�.
            return;
        }


        dir.Normalize();
        cc.Move(dir * moveSpeed * Time.deltaTime);

        //�̵� ������ �ٶ󺸵��� ȸ���Ѵ�.(������ ���� ������ �� ����)
        //������ �� �ڵ�� ���� �ʹ� ���� ���Ƽ� �̻���
        //transform.rotation = Quaternion.LookRotation(dir);

        //�������� ������ ���� ������ ������ش�.
        Quaternion startRot = startRotation;
        Quaternion endRot = Quaternion.LookRotation(dir);
        rotRate += Time.deltaTime *2.0f;
        //print(startRotation.eulerAngles);

        //���� ������ �̿��Ͽ� (������, ������, ����)
        transform.rotation = Quaternion.Lerp(startRot, endRot, rotRate);


        //�̵� ������ �ٶ󺸵��� ȸ���ϴ� ��� (����)
        //transform.forward = dir;

     
    }

    void Move2()
    {
        smith.enabled = true;
        //�÷��̾��� ��ġ�� �׺�޽��� �������� �����Ѵ�.
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
        //Ÿ�ٿ��� �������� �ش�.
        //�÷��̾��� HP�� ���ҽ�Ų��.
        //���ݷ¿� �°� ���ҽ�Ų��.
        // �� 1�ʸ��� Ÿ���� ü���� ���� ���ݷ¸�ŭ ���ҽ�Ų��.
       
        currentTime += Time.deltaTime;
        //�����Ÿ� �ȿ� �־�� �ϰ�)
        if (Vector3.Distance(player.position, transform.position) < attackRange)
        {
            if (currentTime > delayTime)
            {
                //HP�� �÷��̾� ���忡�� �ſ� �߿��� �����̱� ������ ���׳��� �ʰ� �� ��� �Ѵ�.
                //HP�� �������� ���ٺ��� �Լ��� �����ϴ°� �Ϲ����̴�.
               
                currentTime = 0;
                //print("����");
                enemyAnim.SetTrigger("DelayToAttack");
            }
        }
        //���� ���� ���̸�
        else
        {
            if (!isBooked)
            {
                //1.5�� �ڿ� �̵� ���·� ��ȯ�Ѵ�.
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

        //�̵� �ִϸ��̼��� �����Ѵ�.
        enemyAnim.SetTrigger("IdleToMove");

        startRotation = transform.rotation;
        //ȸ�� ������ ���� rotRate �� 0���� �ʱ�ȭ�Ѵ�.
        rotRate = 0;

        isBooked = false;
    }

  
    //�ǰ� ó�� �Լ�
   
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

        //�׺���̼� �޽��� �����.
        //smith.isStopped = true;

        //�׺���̼� �޽� ��ü�� 
        smith.enabled = false;

        healthPoint = Mathf.Max(healthPoint - val, 0);

        
        //���� ü���� 0 �����̸� Die ���·� �����Ѵ�.
        if(healthPoint <=0)
        {
            eState = EnemyState.Die;
            //���� �ִϸ��̼��� ȣ���Ѵ�.
            enemyAnim.SetBool("isDie", true);

            //ĳ���� ��Ʈ�ѷ�, ĸ�� �ݶ��̴� üũ�� �����Ѵ�.
            //Ư�� ������Ʈ�� ��Ȱ��
            cc.enabled = false;
            //ĸ�� �ݶ��̴� ��Ȱ��(���� ������ �� �ʿ� ���� �ѹ��� ���� ���̱� ������ �״�� �ҷ�����.
            GetComponent<CapsuleCollider>().enabled = false;


        }
        else { 


        //�׷��� ������, Damaged ���·� �����Ѵ�.

        //�¾��� �� �ǰݸ���� �ְ� �� �����.
        eState = EnemyState.Damaged;

            //�ǰ� �ִϸ��̼��� ȣ���Ѵ�.
            enemyAnim.SetTrigger("OnHit");

          

            //�׸��� �� ���� �־��ش�.


        //�ǰ� ���ϸ��̼� �߿��� ���� �ʵ���
        //Invoke("ReturnState", 0.9f);
        }

    }

    private void CheckClipTime()
    {
        //�ǰ� �ִϸ��̼� Ŭ���� �� ���̸� ���Ѵ�.
        AnimatorStateInfo myStateInfo = enemyAnim.GetCurrentAnimatorStateInfo(0);
        //console ���� 3�̻� �Ѿ�� �͵��� Loop �ִϸ��̼��� �󸶳� �ݺ��ߴ����� �����ִ� �� �����̴�.
        //print("length��" + myStateInfo.length);

        //����, ���� ������ �̸��� "Hit State"���...
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
        //ȸ�� ������ ���� rotRate �� 0���� �ʱ�ȭ�Ѵ�.
        rotRate = 0;
    }

    private void Die()
    {
        ////����, isDie �Ķ���� ���� True ���, false�� ��ȯ���ش�.


        //����,Die �ִϸ��̼��� ���� ���̰�, �ִϸ��̼� ������� 1.0(100%)�� �������� ��
        AnimatorStateInfo myState = enemyAnim.GetCurrentAnimatorStateInfo(0);
        if(myState.IsName("Die State"))
        {
            enemyAnim.SetBool("isDie", false);
            //normalizedTime�� ����� 0 ~ 100%�� 0 ~ 1�� ȯ���Ͽ� ��Ÿ���� ��
            //������ 2���� ���� ���� (Die state �� normalizedTime) 
            if(myState.normalizedTime >= 0.8f)
            { 
            Destroy(gameObject);
        }
        }

        //�ڱ� �ڽ��� �����Ѵ�.
    }
}
