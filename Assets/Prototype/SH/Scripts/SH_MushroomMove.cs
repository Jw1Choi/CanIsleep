using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_MushroomMove : MonoBehaviour
{

    public float gravity = -9.81f;
    public float yVelocity = 0;
    public float mushroomSpeed = 6.0f;
    CharacterController cc;
    public enum State
    {
        Idle,
        Move,
        Run,
        Attack,
        Die,
    }


    GameObject target;

    public State state;

    public float speed = 1;
    public float findDistance = 5;
    public float runDistance = 3;
    public float attackDistance = 1;
    float distance;
    float currentTime;
    float attackTime = 1;

    public Animator anim;
    float rotRate = 0;

    Quaternion startRotation;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        state = State.Idle;
        target = GameObject.Find("Player");
    }


    // Update is called once per frame
    void Update()
    {
        if (state == State.Idle)
        {
            UpdateIdele();
        }
        else if (state == State.Move)
        {
            UpdateMove();
        }
        else if (state == State.Run)
        {
            UpdateRun();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();

        }

        //1. ���� target �� �Ÿ��� ���Ѵ�.
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();
        distance = Vector3.Distance(transform.position, target.transform.position);

        // �̵� ������ �ٶ󺸵��� ȸ���Ѵ�.
        //transform.rotation = Quaternion.LookRotation(dir);
        Quaternion startRot = startRotation;
        Quaternion endRot = Quaternion.LookRotation(dir);
        rotRate += Time.deltaTime * 2f;
        // ���� ������ �̿��Ͽ� ȸ���� �Ѵ�.
        transform.rotation = Quaternion.Lerp(startRot, endRot, rotRate);

    }


    private void UpdateIdele()
    {
        //target�� �����Ÿ��ȿ� ������ Move�� �����ϰ� �ʹ�.
        //1. ���� target �� �Ÿ��� ���ؼ�
        //2. ���� �� �Ÿ��� �����Ÿ����� ������
        if (distance < findDistance)
        {
            //3. Move���·� �����ϰ� �ʹ�.
            state = State.Move;
            anim.SetTrigger("Move");
        }

    }

    private void UpdateMove()
    {    //1. target �������� �̵��ϰ� �ʹ�.
        Vector3 dir = target.transform.position - transform.position;
        yVelocity += gravity * Time.deltaTime;

        dir.y = yVelocity;
        dir.Normalize();
        cc.Move(dir * mushroomSpeed * Time.deltaTime);
        //target�� Run�����Ÿ��ȿ� ������ Run�� �����ϰ� �ʹ�.
        //1. ���� target �� �Ÿ��� ���ؼ�
        //2. ���� �� �Ÿ��� �����Ÿ����� ������
        if (distance < runDistance)
        {
            //3. Move���·� �����ϰ� �ʹ�.
            state = State.Run;
            anim.SetTrigger("Run");
        }

    }

    private void UpdateRun()
    {//target �������� �̵��ϴٰ� target�� ���ݰŸ��ȿ� ������ Attack���� �����ϰ� �ʹ�
        //1. target �������� �̵��ϰ� �ʹ�.
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();
        transform.position += dir * speed *2* Time.deltaTime;
        //2. ���� target�� �Ÿ��� ���ؼ�
        //3. ���� �� �Ÿ��� ���ݰŸ����� ������
        if (distance < attackDistance)
        {
            //4. Attack ���·� �����ϰ� �ʹ�.
            state = State.Attack;

            anim.SetTrigger("Attack");
        }
        if (distance > runDistance)
        {
            //7. Move ���·� �����ϰ� �ʹ�.
            state = State.Move;
            anim.SetTrigger("Move");

            startRotation = transform.rotation;
            //ȸ�� ������ ���� rotRate �� 0���� �ʱ�ȭ�Ѵ�.
            rotRate = 0;

        }

    }

    private void UpdateAttack()
    { //�����ð����� ������ �ϵ� ���ݽ����� target�� ���ݰŸ� �ۿ� ������ Move���·� �����ϰ� �ʹ� �׷��� ������ ��� �ݺ��ؼ� ����!
      //1. �ð��� �帣�ٰ�
        currentTime += Time.deltaTime;
        //2. ����ð��� ���ݽð��� �Ǹ�
        if (currentTime > attackTime)
        {
            //3. ����ð��� �ʱ�ȭ�ϰ�
            currentTime = 0;
            //4. �÷��̾ �����ϰ�
            //target.AddDamage();
            //5. ���� target�� �Ÿ��� ���ؼ�
            //6. ���� �� �Ÿ��� ���ݰŸ����� ũ�ٸ�
            if (distance > attackDistance)
            {
                //7. Move ���·� �����ϰ� �ʹ�.
                state = State.Run;
                anim.SetTrigger("Run");

                startRotation = transform.rotation;
                //ȸ�� ������ ���� rotRate �� 0���� �ʱ�ȭ�Ѵ�.
                rotRate = 0;

            }
        }
    }

    public void AddDamage(int damage)
    {
        Destroy(gameObject);

    }
    private void OnDestroy()
    {

    }
}
