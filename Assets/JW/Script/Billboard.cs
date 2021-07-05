using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    public bool isUI = false;

    float direction;

    //�÷��̾��� ���� ���ϰ� �ʹ�.
    // Start is called before the first frame update
    void Start()
    {
        direction = isUI == true ? 1.0f : -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.main.transform.forward  * direction;
    }
}
