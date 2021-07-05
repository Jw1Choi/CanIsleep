using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_t : MonoBehaviour
{
    public Animator anim;
    float sightRange = 5.0f;
    float attackRange = 1.0f;
    Transform player;
    public float moveSpeed = 7.0f;
    CharacterController cc;
    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        AttackToMove,
        Damaged,
        Die,
    }
    public EnemyState eState;
    // Start is called before the first frame update
    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
        player = GameObject.Find("Player").transform;
        eState = EnemyState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch(eState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            
            case EnemyState.Move:
                Move();
                break;

            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.AttackToMove:
                AttackToMove();
                break;

            case EnemyState.Die:
                Die();
                break;
        }
    }

  
    private void Idle()
    {
        float distance = (player.transform.position - transform.position).magnitude;

        if(distance <= sightRange)
        {
            eState = EnemyState.Move;
        }
    }

    float gravity = -9.81f;
    float yVelocity;
    private void Move()
    {
        Vector3 dir = player.transform.position - transform.position;
        float distance = dir.magnitude;


        dir.Normalize();

        if(distance <= attackRange)
        {
            eState = EnemyState.Attack;

            return;
        }

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        cc.Move(dir * moveSpeed * Time.deltaTime);

    }

    public float currentTime= 0.0f;
    float attackTime = 1;

    private void Attack()
    {
        currentTime += Time.deltaTime;
        float distance = (player.transform.position - transform.position).magnitude;
        if(distance <= attackRange)
        {
            if(currentTime >= attackTime)
            {
                currentTime = 0;

                
            
            }

        }
        if (distance >= attackRange)
        {
            eState = EnemyState.Move;
        }
    }

    private void AttackToMove()
    {
        throw new NotImplementedException();
    }

    private void Die()
    {
        throw new NotImplementedException();
    }

    public void AddDamage()
    {
        Destroy(gameObject);
    }
}
