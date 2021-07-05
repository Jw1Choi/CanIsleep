using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAction : MonoBehaviour
{
    EnemyFSM efsm;

    // Start is called before the first frame update
    void Start()
    {
        //�θ� ���� ������Ʈ�κ��� EnemyFSM ������Ʈ�� �����´�.
        efsm = GetComponentInParent<EnemyFSM>();    
    }
    //�� �Լ��� ����: ������ ���� ���ݷ��� ���� ���� �������� ���濡�� ������ �ʹ�.
    public void OnEnemyAttack(float damageRate)
    {
        //EnemyFSM Ŭ�������� �÷��̾ �����´�.
        Transform enemyTarget = efsm.GetTargetTransform();

        //
        PlayerMove pm = enemyTarget.GetComponent<PlayerMove>();
        int finalDamage = (int)(efsm.attackPower * damageRate);

        pm.ApplyDamage(finalDamage);
        print("�÷��̾��" + finalDamage + "�� ���ظ� �Ծ����ϴ�.");
    }
}
