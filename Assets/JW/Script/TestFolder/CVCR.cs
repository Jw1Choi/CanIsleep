using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CVCR : MonoBehaviour
{

    public string bestStudent;
    public string worstStudent;
    public string bestTeacher;
    // Start is called before the first frame update
    void Start()
    {
        string result1;
        int temp = 10;
        result1 = temp % 2 == 0 ? "짝수" : "홀수";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SwapStudentsValue(ref bestStudent, ref worstStudent);
            print("최고의 학생:" + bestStudent + "최악의 학생:" + worstStudent);

            AllocateName(out bestTeacher);
            print(bestTeacher);
        }
        
    }

   

    void SwapStudents(ref string AStudent,ref string BStudent)
    {
        string temp = AStudent;
        AStudent = BStudent;
        BStudent = temp;
    }

    // 변수를 전달하면 그 변수에 "박원석"이라는 값을 넣어준다.
    void AllocateName(out string teacherName)
    {

        teacherName = "박원석";

    }

    void SwapStudentsValue(ref string AStudent, ref string BStudent)
    {
        string temp = AStudent;
        AStudent = BStudent;
        BStudent = temp;
    }
}
