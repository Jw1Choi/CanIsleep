using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Yoshi : MonoBehaviour
{
           public enum State
        {
            Idle,
            Move,
            Attack,
            Die,
        }

        public Animator anim;
        float rotRate = 0;
        Quaternion startRotation;

        public State state;
        float distance;
        public float findDistance = 5;
        GameObject target;
        public float speed = 1;
        public float attackDistance = 1;

        float currentTime;
        float attackTime = 1;

        // Start is called before the first frame update
        void Start()
        {
            state = State.Idle;
            target = GameObject.Find("Player");
        }


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
            else if (state == State.Attack)
            {
                UpdateAttack();

            }

            //1. ���� target �� �Ÿ��� ���Ѵ�.
            Vector3 dir = target.transform.position - transform.position;
            dir.Normalize();
            distance = Vector3.Distance(transform.position, target.transform.position);
            print(distance);

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
            else { state = State.Idle; }

        }



        private void UpdateMove()
        {//target �������� �̵��ϴٰ� target�� ���ݰŸ��ȿ� ������ Attack���� �����ϰ� �ʹ�
         //1. target �������� �̵��ϰ� �ʹ�.
            Vector3 dir = target.transform.position - transform.position;
            dir.Normalize();
            transform.position += dir * speed * Time.deltaTime;
            //2. ���� target�� �Ÿ��� ���ؼ�
            //3. ���� �� �Ÿ��� ���ݰŸ����� ������
            if (distance < attackDistance)
            {
                //4. Attack ���·� �����ϰ� �ʹ�.
                state = State.Attack;

                anim.SetTrigger("Attack");
            }

            // �̵� ������ �ٶ󺸵��� ȸ���Ѵ�.
            //transform.rotation = Quaternion.LookRotation(dir);
            Quaternion startRot = startRotation;
            Quaternion endRot = Quaternion.LookRotation(dir);
            rotRate += Time.deltaTime * 2f;
            // ���� ������ �̿��Ͽ� ȸ���� �Ѵ�.
            transform.rotation = Quaternion.Lerp(startRot, endRot, rotRate);


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
                    state = State.Move;
                    anim.SetTrigger("Move");

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


