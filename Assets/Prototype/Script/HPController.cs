using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    // 대상에 체력 정보를 읽어와서 체력 슬라이더의 값에 반영한다.
    // 필요 요소 : 대상, 체력 슬라이더

    public EnemyFSM enemy;
    public Image hpSlider;


    // Start is called before the first frame update
 
     
    // Update is called once per frame
    void Update()
    {

        hpSlider.fillAmount = (float)enemy.GetHp() / (float)enemy.maxHP;
    }
}
