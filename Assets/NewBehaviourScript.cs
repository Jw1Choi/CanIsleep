using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �Ʒ��� �������� �ʹ�.
        //transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        //transform.position += new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;

        // �÷��̾ ���ؼ� ���� �ʹ�.
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    // ���� �ε��� ����� �����ϰ�, ���� �����Ѵ�.
    //private void OnCollisionEnter(Collision collision)
    //{
    //    // �ε��� ����� �̸��� "Player"��� �۾��� �����ϰ� �ִٸ�...
    //    //if (collision.gameObject.name == "Player")
    //    if (collision.gameObject.name.Contains("Player"))
    //    {
    //        Destroy(collision.gameObject);
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
