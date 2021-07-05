using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    //���콺 �� Ŭ���� �ϸ� �Ѿ��� �߻�ǰ� �ϰ� �ʹ�.
    //�ʿ� ���: �Ѿ� ������Ʈ, ��Ŭ�� �Է�, �߻� ��ġ
    public GameObject bullet;
    public Transform firePosition;
    public GameObject fireEffect;
    public int attackPower = 3;
    public Animator anim;

    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //����, ���콺 �� Ŭ���� �ϸ�..
        if(Input.GetMouseButtonDown(1))
        {

            //�Ѿ��� firePosition ��ġ�� ��Ÿ���� �Ѵ�.
            GameObject go = Instantiate(bullet);
            go.transform.position = firePosition.position;
            //�Ѿ��� ������ ������ ���� �ٲ��ش�.
            go.transform.rotation = firePosition.rotation;
        }

        //����, ���콺 �� Ŭ���� �ϸ�...

        if(Input.GetMouseButtonDown(0))
        {

       
        //���̸� �����ϰ� ī�޶��� ���� �������� �߻��ϰ� �ʹ�.
        //1.���̸� �����Ѵ�.
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //2. ���̰� �ε��� ����� ������ ���� ������ �����Ѵ�.
        RaycastHit hitInfo;

            anim.SetTrigger("Fire");
        //3,���̸� �߻��Ѵ�!
        if(Physics.Raycast(ray, out hitInfo))
        {
            //�ε��� ����� �̸��� �ַܼ� ����Ѵ�.
            //print(hitInfo.transform.name);

                //Explosion ȿ���� �ҷ��ͼ� ȿ���� �����Ѵ�.
                //1. Explosion ȿ���� ������
                GameObject fx = Instantiate(fireEffect);
                //2. ȿ���� Ray�� �´��� ������ �����Ѵ�.
                fx.transform.position = hitInfo.point;

                //������ ����Ʈ�� up������ ���̰� ���� ������ ��� ���Ϳ� ��ġ��Ų��.
                fx.transform.up = hitInfo.normal;

                ps = fx.GetComponent<ParticleSystem>();
                ps.Stop();
                ps.Play();

                //��� 1) �ε��� ����� Enemy���, EnemyFSM ������Ʈ�� �����ͼ�, Damaged �Լ��� �����Ѵ�.
                if (hitInfo.transform.name.Contains("Enemy"))
                {

                    EnemyFSM efsm = hitInfo.transform.GetComponent<EnemyFSM>();
                    if(efsm.eState != EnemyFSM.EnemyState.Damaged || efsm.eState != EnemyFSM.EnemyState.Die)
                    { 
                    efsm.Damaged(attackPower);
                    }
                }
                //���1- END




            }
        }
    }
}
