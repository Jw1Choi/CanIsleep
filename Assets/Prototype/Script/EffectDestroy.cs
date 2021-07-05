using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    //2초 뒤에 나를 제거하고 싶다!
   public float elapsedTime = 2;
    
    float currentTime = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>elapsedTime)
        {
            Destroy(gameObject);
        }
        
    }
}
