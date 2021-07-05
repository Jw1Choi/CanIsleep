using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    //마우스 우 클릭을 하면 총알이 발사되게 하고 싶다.
    //필요 요소: 총알 오브젝트, 우클릭 입력, 발사 위치
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
        //만일, 마우스 우 클릭을 하면..
        if(Input.GetMouseButtonDown(1))
        {

            //총알이 firePosition 위치에 나타나게 한다.
            GameObject go = Instantiate(bullet);
            go.transform.position = firePosition.position;
            //총알이 나가는 각도도 같이 바꿔준다.
            go.transform.rotation = firePosition.rotation;
        }

        //만일, 마우스 좌 클릭을 하면...

        if(Input.GetMouseButtonDown(0))
        {

       
        //레이를 생성하고 카메라의 정면 방향으로 발사하고 싶다.
        //1.레이를 생성한다.
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //2. 레이가 부딪힌 대상의 정보를 담을 변수를 선언한다.
        RaycastHit hitInfo;

            anim.SetTrigger("Fire");
        //3,레이를 발사한다!
        if(Physics.Raycast(ray, out hitInfo))
        {
            //부딪힌 대상의 이름은 콘솔로 출력한다.
            //print(hitInfo.transform.name);

                //Explosion 효과를 불러와서 효과를 실행한다.
                //1. Explosion 효과를 들고오기
                GameObject fx = Instantiate(fireEffect);
                //2. 효과를 Ray가 맞닿은 곳에서 실행한다.
                fx.transform.position = hitInfo.point;

                //생성한 이펙트의 up방향을 레이가 닿은 지점의 노멀 벡터와 일치시킨다.
                fx.transform.up = hitInfo.normal;

                ps = fx.GetComponent<ParticleSystem>();
                ps.Stop();
                ps.Play();

                //방법 1) 부딪힌 대상이 Enemy라면, EnemyFSM 컴포넌트를 가져와서, Damaged 함수를 실행한다.
                if (hitInfo.transform.name.Contains("Enemy"))
                {

                    EnemyFSM efsm = hitInfo.transform.GetComponent<EnemyFSM>();
                    if(efsm.eState != EnemyFSM.EnemyState.Damaged || efsm.eState != EnemyFSM.EnemyState.Die)
                    { 
                    efsm.Damaged(attackPower);
                    }
                }
                //방법1- END




            }
        }
    }
}
