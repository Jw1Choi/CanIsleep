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
        //�������� �����Ѵ�.
        target = GameObject.Find("Player").transform;

        // NavMeshAgent ������Ʈ�� �����´�.
        smith = GetComponent<NavMeshAgent>();



    }

    // Update is called once per frame
    void Update()
    {
        //������Ʈ���� �������� �����Ѵ�.
        //������Ʈ ������ �Ѵ�.
        smith.speed = 8.0f;
        smith.SetDestination(target.position);
    }
}
