using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    public float bulletSpeed = 10.0f;
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        ////월드 좌표(위, 아래, 오른쪽)
        //Vector3.up;
        //Vector3.forward;

        ////local 좌표로 (앞,오른쪽,위)
        ////각자의 오브젝트를 표현하기 위해 변수(소문자 transform)를 사용
        ////this가 생략되어 있는데 이건 스크립트의 클래스를 뜻한다.
        //transform.forward;
        //transform.right;
        //transform.up;

    }

    // Update is called once per frame
    void Update()
    {


        transform.position += transform.forward * bulletSpeed * Time.deltaTime;

        #region 카메라 방향으로 총알 발사
        //Vector3 dir = new Vector3(0, 0, 1);
        //dir = Camera.main.transform.TransformDirection(dir);
        //transform.position += dir * bulletSpeed * Time.deltaTime;
        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] cPoint = collision.contacts;
        GameObject go = Instantiate(effect);

        go.transform.position = cPoint[0].point;


        //충돌한 대상의 충돌 지점에 피격 이펙트를 생성한다.
        go.transform.up = cPoint[0].normal;
        // 피격 이펙트의 파티클 방출 방향이 충돌 지점의 노멀 방향으로과 일치하도록 한다.


         
        ParticleSystem ps = go.GetComponent<ParticleSystem>();
        ps.Stop();
        ps.Play();
        //나는 이만 사라지겠소
        Destroy(gameObject);
    }
}
