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
        // 나는 아래로 내려가고 싶다.
        //transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        //transform.position += new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;

        // 플레이어를 향해서 가고 싶다.
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    // 나와 부딪힌 대상을 제거하고, 나를 제거한다.
    //private void OnCollisionEnter(Collision collision)
    //{
    //    // 부딪힌 대상의 이름이 "Player"라는 글씨를 포함하고 있다면...
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
