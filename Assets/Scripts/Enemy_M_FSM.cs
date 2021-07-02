using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_M_FSM : MonoBehaviour
{
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
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
