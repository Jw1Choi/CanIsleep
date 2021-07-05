using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryCalc : MonoBehaviour
{
    //char letter = '박';
    //char[] letters = new char[3];
    //string name = "박원석";
    int num;
    int debuff = 0;

    //public int debuffNumber1 = 0;
    //public int debuffNumber2 = 0;


    public enum DebuffName
    {
        None = 0,
        Poison = 1,
        Electric = 2,
        Frozen = 4,
        Sleep = 8,
        Panic = 16
    }

   public DebuffName debuffName1 = DebuffName.None;
    public DebuffName debuffName2 = DebuffName.None;

    // Start is called before the first frame update
    void Start()
    {

        //debuff = debuff | SetDebuff(debuffNumber1);
        //debuff = debuff | SetDebuff(debuffNumber2);

        debuff = debuff | (int)debuffName1;
        debuff = debuff | (int)debuffName2;

        CheckDebuff(debuff);

        //num = 5;
        //print("원래의 값:" + num.ToString());

        //num = num <<3;
        //print("시프트된 값:" + num.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int SetDebuff(int buffNumber)
    {
        return 1 << buffNumber;

    }

    void CheckDebuff(int myDebuff)
    {
        int checkNum = 1;
        for(int i =0; i < 32; i++)
        {
            int result = myDebuff & checkNum;

            if(result > 0)
            {
                print("디버프" + i.ToString() + "번째 일치!");
                
            }
            checkNum = checkNum << 1;
        }

       
    }

    //float SecondCosineRule(float aSize, float bSize, float theta)
    //{
    //    //c = a^2 + b^2 - 2abCos@

    //    //float result = aSize * aSize + bSize * bSize - 2 * aSize * bSize * Mathf.Cos(Mathf.Deg2Rad * theta);
    //    //같은 뜻
    //    float result = Mathf.Pow(aSize,2) + Mathf.Pow(bSize, 2) - 2 * aSize * bSize * Mathf.Cos(Mathf.Deg2Rad * theta);
    //    return result;
    //}
}
