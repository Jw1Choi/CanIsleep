using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSetting : MonoBehaviour
{
    //���� ���۵Ǹ� ���콺 Ŀ���� ȭ�� ���ʿ��� ����� �ʵ��� �ϰ� �ʹ�.

    
    public CursorLockMode myLockMode = CursorLockMode.None;
    // Start is called before the first frame update
    void Start()
    {
     
      
            Cursor.lockState = myLockMode;
          
       
    }

}
