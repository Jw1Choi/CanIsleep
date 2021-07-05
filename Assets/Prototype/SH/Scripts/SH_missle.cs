using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_missle : MonoBehaviour
{
    public float moveSpeed = 6;
    public GameObject player = null;
    public int rate = 60;
    public GameObject explosionFX;

    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {


}

// Update is called once per frame
void Update()
    {
        dir = Vector3.forward;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
