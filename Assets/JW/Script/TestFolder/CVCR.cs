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
        result1 = temp % 2 == 0 ? "¦��" : "Ȧ��";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SwapStudentsValue(ref bestStudent, ref worstStudent);
            print("�ְ��� �л�:" + bestStudent + "�־��� �л�:" + worstStudent);

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

    // ������ �����ϸ� �� ������ "�ڿ���"�̶�� ���� �־��ش�.
    void AllocateName(out string teacherName)
    {

        teacherName = "�ڿ���";

    }

    void SwapStudentsValue(ref string AStudent, ref string BStudent)
    {
        string temp = AStudent;
        AStudent = BStudent;
        BStudent = temp;
    }
}
