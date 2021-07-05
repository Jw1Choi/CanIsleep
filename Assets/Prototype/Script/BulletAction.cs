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
        ////���� ��ǥ(��, �Ʒ�, ������)
        //Vector3.up;
        //Vector3.forward;

        ////local ��ǥ�� (��,������,��)
        ////������ ������Ʈ�� ǥ���ϱ� ���� ����(�ҹ��� transform)�� ���
        ////this�� �����Ǿ� �ִµ� �̰� ��ũ��Ʈ�� Ŭ������ ���Ѵ�.
        //transform.forward;
        //transform.right;
        //transform.up;

    }

    // Update is called once per frame
    void Update()
    {


        transform.position += transform.forward * bulletSpeed * Time.deltaTime;

        #region ī�޶� �������� �Ѿ� �߻�
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


        //�浹�� ����� �浹 ������ �ǰ� ����Ʈ�� �����Ѵ�.
        go.transform.up = cPoint[0].normal;
        // �ǰ� ����Ʈ�� ��ƼŬ ���� ������ �浹 ������ ��� �������ΰ� ��ġ�ϵ��� �Ѵ�.


         
        ParticleSystem ps = go.GetComponent<ParticleSystem>();
        ps.Stop();
        ps.Play();
        //���� �̸� ������ڼ�
        Destroy(gameObject);
    }
}
