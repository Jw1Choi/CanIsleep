using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    // ��� ü�� ������ �о�ͼ� ü�� �����̴��� ���� �ݿ��Ѵ�.
    // �ʿ� ��� : ���, ü�� �����̴�

    public EnemyFSM enemy;
    public Image hpSlider;


    // Start is called before the first frame update
 
     
    // Update is called once per frame
    void Update()
    {

        hpSlider.fillAmount = (float)enemy.GetHp() / (float)enemy.maxHP;
    }
}
