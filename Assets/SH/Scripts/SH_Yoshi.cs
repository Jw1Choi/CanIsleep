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

            //1. 나와 target 의 거리를 구한다.
            Vector3 dir = target.transform.position - transform.position;
            dir.Normalize();
            distance = Vector3.Distance(transform.position, target.transform.position);
            print(distance);

        }



        private void UpdateIdele()
        {
            //target이 감지거리안에 들어오면 Move로 전이하고 싶다.
            //1. 나와 target 의 거리를 구해서
            //2. 만약 그 거리가 감지거리보다 작으면
            if (distance < findDistance)
            {
                //3. Move상태로 전이하고 싶다.
                state = State.Move;
                anim.SetTrigger("Move");
            }
            else { state = State.Idle; }

        }



        private void UpdateMove()
        {//target 방향으로 이동하다가 target이 공격거리안에 들어오면 Attack으로 전이하고 싶다
         //1. target 방향으로 이동하고 싶다.
            Vector3 dir = target.transform.position - transform.position;
            dir.Normalize();
            transform.position += dir * speed * Time.deltaTime;
            //2. 나와 target의 거리를 구해서
            //3. 만약 그 거리가 공격거리보다 작으면
            if (distance < attackDistance)
            {
                //4. Attack 상태로 전이하고 싶다.
                state = State.Attack;

                anim.SetTrigger("Attack");
            }

            // 이동 방향을 바라보도록 회전한다.
            //transform.rotation = Quaternion.LookRotation(dir);
            Quaternion startRot = startRotation;
            Quaternion endRot = Quaternion.LookRotation(dir);
            rotRate += Time.deltaTime * 2f;
            // 선형 보간을 이용하여 회전을 한다.
            transform.rotation = Quaternion.Lerp(startRot, endRot, rotRate);


        }

        private void UpdateAttack()
        { //일정시간마다 공격을 하되 공격시점에 target이 공격거리 밖에 있으면 Move상태로 전이하고 싶다 그렇지 않으면 계속 반복해서 공격!
          //1. 시간이 흐르다가
            currentTime += Time.deltaTime;
            //2. 현재시간이 공격시간이 되면
            if (currentTime > attackTime)
            {
                //3. 현재시간을 초기화하고
                currentTime = 0;
                //4. 플레이어를 공격하고
                //target.AddDamage();
                //5. 나와 target의 거리를 구해서
                //6. 만약 그 거리가 공격거리보다 크다면
                if (distance > attackDistance)
                {
                    //7. Move 상태로 전이하고 싶다.
                    state = State.Move;
                    anim.SetTrigger("Move");

                    startRotation = transform.rotation;
                    //회전 보간을 위한 rotRate 도 0으로 초기화한다.
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


