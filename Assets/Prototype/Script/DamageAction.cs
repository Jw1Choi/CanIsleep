using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : MonoBehaviour
{
    EnemyFSM efsm;

    // Start is called before the first frame update
    void Start()
    {
        //부모 게임 오브젝트로부터 EnemyFSM 컴포넌트를 가져온다.
        efsm = GetComponentInParent<EnemyFSM>();    
    }
    //이 함수의 목적: 비율에 따라 공격력의 일정 비율 데미지를 상대방에게 입히고 싶다.
    public void OnEnemyAttack(float damageRate)
    {
        //EnemyFSM 클래스에서 플레이어를 가져온다.
        Transform enemyTarget = efsm.GetTargetTransform();

        //
        PlayerMove pm = enemyTarget.GetComponent<PlayerMove>();
        int finalDamage = (int)(efsm.attackPower * damageRate);

        pm.ApplyDamage(finalDamage);
        print("플레이어는" + finalDamage + "의 피해를 입었습니다.");
    }
}
