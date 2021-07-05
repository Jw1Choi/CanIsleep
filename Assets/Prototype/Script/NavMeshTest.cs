using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshTest : MonoBehaviour
{
    NavMeshAgent smith;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        //목적지를 설정한다.
        target = GameObject.Find("Player").transform;

        // NavMeshAgent 컴포넌트를 가져온다.
        smith = GetComponent<NavMeshAgent>();



    }

    // Update is called once per frame
    void Update()
    {
        //에이전트에게 목적지를 전달한다.
        //에이전트 설정을 한다.
        smith.speed = 8.0f;
        smith.SetDestination(target.position);
    }
}
