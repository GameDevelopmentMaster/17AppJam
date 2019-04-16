using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SchoolLogic : MonoBehaviour
{
    public float playTime = 20 * 60 * 60; // 시간

    // School
    public bool Chick;          //true 일시 학기 false 방학
    public bool night;          //true 야쟈 있음 false 야자 없음
    public bool food;           //true 급식질 상승함 false 급식질 상승 안함
    public bool schoolhit;

    public int[] schoolTurnMoney;    // 학기마다 들어오는 돈
    public int schoolBudgetU; // 돈 : 억
    public int schoolBudgetM; // 만

    public int schoolKarma; // 평판
    public int schoolPrice; // 입학비
    public int schoolSecurityNum = 0; // 경보원들

    public int schoolBuildingNum = 1; // 건물
    public int schoolClassNum; // 학급

    public float[] schoolTurnTotalRate;  // 학기마다 변하는 퍼센트 0:진학률 1:취업률
    public float schoolUnivRate = 0.05f; // 진학
    public float schoolJobRate = 0.05f; // 취업

    int schoolhitchick;       //학폭위 횟수 체크
    float schoolCut;              // 입학 컷
   public int[] schoolChick;           //0: 선생님 영입 횟수, 1: 취업교육 횟수 2.캠페인 횟수

    // Student
    public long studentTotal; // 총합 학생
    public long studentGood; // 우수학생
    public long studentNormalNum; // 일반인
    public long studentBullyNum; // 불량학생
    public float studentNeg; // 학생 불만
    public float studentParentNeg; // 부모님의 노여움

    public int turn = 0;

    //Test
    public Text[] StudentPanel;
    public Text[] aext;
    public Text penis;

    public string[] name;
    public string[] turnLUT;

    /*
    *   MISC FUNCS 
    */
    public void allReset()
    {
        turn = 0;

        // money
        schoolBudgetU = 3;
        schoolBudgetM = 0;

        // students
        studentGood = 0;
        studentNormalNum = 250;
        studentBullyNum = 20;

        schoolPrice = 7;

        //StartCoroutine(TimeLimit()); // 시간 코루틴
}

    public IEnumerator TimeLimit ()
    {
        yield return new WaitForSeconds(playTime);

        // ENDING
        Debug.Log("게임 끝. 엔딩");
    }

    /*
    *   SCHOOL FUNCS 
    */
    //public void schoolReset

    // Start is called before the first frame update
    void Start()
    {
        allReset();
        name[0] = "중점 학교";
        name[1] = "장학금 지원 사업";
        name[2] = "취업 지원 사업";
        name[3] = "지킴이 고용";
        name[4] = "캠페인";
        name[5] = "석식 실시";
        name[6] = "급식 질 하락";
        name[7] = "급식 질 상승";
        name[8] = "야간 자율학습";
        name[9] = "취업 교육";
        name[10] = "유명 선생 고용";
        name[11] = "방과 후";
        for(int i = 0; i < schoolChick.Length; i++)
        {
            schoolChick[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

        schoolCut = 1 - (schoolUnivRate + schoolJobRate);
        studentTotal = (studentGood + studentNormalNum + studentBullyNum);
        StudentPanel[0].text = "학생 불만 : " + (studentNeg * 100).ToString() + "%";
        StudentPanel[1].text = "학부모 불만 : " + (studentParentNeg * 100).ToString() + "%";
        StudentPanel[2].text = "학생 수 : " + studentTotal.ToString() + " 명";
        if (schoolBudgetU <= 0)
        {
            StudentPanel[3].text = "학비 : " + schoolBudgetM.ToString() + " 만";
        }
        else
        {
            StudentPanel[3].text = "학비 : " + (((int)studentTotal*schoolPrice)/1000).ToString()+"억"+(((int)studentTotal*schoolPrice)%10000).ToString()+ "만원";
        }
        StudentPanel[4].text = "입학 내신 : " + (schoolCut * 100).ToString() + "%";
        StudentPanel[5].text = "진학률 : " + (schoolUnivRate * 100).ToString() + "%";
        StudentPanel[6].text = "취업률 : " + (schoolJobRate * 100).ToString() + "%";
        StudentPanel[7].text = "보유 자산 : " + schoolBudgetU.ToString() + "억" + schoolBudgetM.ToString() + "만원";
        if (studentBullyNum - (10 + schoolSecurityNum) > 10)
        {
            if (!schoolhit)
            {
                studentParentNeg += 0.1f;
                schoolhit = true;
                schoolhitchick += 1;
            }
        }
    }

    #region 이벤트 함수들
    public void AfterSchool()           // 방과후 이벤트 함수
    {
        long Chike = studentTotal / 200; // 인원 홧수
        if (Chike > 0)
        {
            if (IfInt(3000) == 1)
            {
                schoolTurnTotalRate[0] += 0.05f;
                schoolCut += 0.1f;
                schoolTurnMoney[1] += 1;
            }
            else
            {
                return;
            }
        }

    }

    public void Teacheremploy()         //유명 선생님 고용 이벤트 함수
    {
        if (schoolChick[0] < 3)
        {
            if (IfInt(2000) == 1)
            {
                schoolCut += 0.03f;
                schoolTurnTotalRate[0] += 0.03f;
            }
            else
            {
                return;
            }
            schoolChick[0] += 1;
        }
    }

    public void Eudcation()       //취업 교육 이벤트 함수
    {
        if (schoolChick[1] < 2)
        {
            if (IfInt(2000) == 1)
            {
                schoolTurnTotalRate[1] += 0.05f;
                schoolTurnMoney[1] += 2;
            }
            else
            {
                return;
            }
            schoolChick[1] += 1;
        }
    }

    public void nightSchool() //야자 이벤트 함수
    {
        if (schoolBudgetU >= 2)
        {
            schoolBudgetU -= 2;
            schoolTurnTotalRate[0] += 0.1f;
            schoolCut += 0.1f;
            night = true;
        }
        else
        {
            return;
        }
    }

    public void JobRate()   //취업 지원 사업 이벤트 함수 
    {
        if (IfInt(5000) == 1)
        {
            schoolTurnTotalRate[1] += 0.1f;
            schoolTurnMoney[1] = 5;
        }
        else
        {
            return;
        }
    }

    public void reward()    //장학금 지원 사업 이벤트 함수
    {
        if (studentTotal >= 500 && schoolCut <= 0.2f)
        {
            schoolTurnMoney[3] += 1;
        }
        else
        {
            return;
        }
    }

    public void Focus()     //중점 학교 이벤트 함수
    {
        if (studentGood >= 50)
        {
            schoolTurnMoney[3] += 2;
        }
        else
        {
            return;
        }
    }

    public void security()      //보안관 구매 이벤트 함수
    {
        if (IfInt(1200) == 1)
        {
            schoolSecurityNum += 1;
            schoolTurnMoney[1] -= 1;
            schoolTurnMoney[0] -= 2;
        }
        else
        {
            return;
        }
    }

    public void nightFood()     //석식 제공 이벤수 함수
    {
        if (night)
        {
            if (schoolBudgetU >= 1)
            {
                if (schoolBudgetM >= 5000)
                {
                    schoolBudgetM -= 5000;
                    schoolBudgetU -= 1;
                }
                else if (schoolBudgetU > 1 && schoolBudgetM < 5000)
                {
                    schoolBudgetM += 5000;
                    schoolBudgetU -= 2;
                }
                else
                {
                    return;
                }
                schoolTurnMoney[1] += 5;
                schoolTurnMoney[0] += 5;
            }
        }
    }

    public void Food(int key)      //급식 관련 이벤트 함수
    {
        switch (key)
        {
            case 1:
                if (IfInt(5000) == 1)
                {
                    studentNeg -= 0.05f;
                }
                else
                {
                    break;
                }
                break;

            case 2:
                if (schoolBudgetM + 3000 > 10000)
                {
                    schoolBudgetM -= 3000;
                    schoolBudgetU += 1;
                }
                else
                {
                    schoolBudgetM += 3000;
                }
                studentNeg += 0.05f;
                break;
        }

    }

    public void turnschool() //턴 종류
    {
        turn++;

        if (turn >= 12)
        {
            PlayerPrefs.SetFloat("SCORE_CUT", schoolCut);
            PlayerPrefs.SetFloat("SCORE_STUDENTS", studentTotal);
            SceneManager.LoadScene("Ending");
            return;
        }

        penis.text = turnLUT[turn];
        if (Chick)
        {
            if (schoolhit)
            {
                studentBullyNum -= 10;
                schoolhit = false;
            }
            schoolBudgetM = (int)studentTotal * schoolPrice + schoolTurnMoney[1] * 1000 + schoolTurnMoney[0] * 100;
            schoolBudgetU += schoolTurnMoney[3] * 1;
            if (!food)
            {
                studentNeg += 0.1f;
            }

            if ((schoolUnivRate+schoolTurnTotalRate[0]) + (schoolJobRate+schoolTurnTotalRate[1]) < 100)
            {
                schoolUnivRate += schoolTurnTotalRate[0];
                schoolJobRate += schoolTurnTotalRate[1];
            }
            else
            {
                return;
            }

            for(int i=0; i < schoolChick.Length; i++)
            {
                schoolChick[i] = 0;
            }
        }

    }

    public void schoolbuildingAdd() //건물 생성 이벤트 함수
    {
        if (schoolBudgetU > 30)
        {
            schoolBuildingNum += 1;
            schoolBudgetU -= 30;
        }
        else
        {
            return;
        }
    }

    public void studnetAdd()    //학생수 늘리기
    {
        if (schoolClassNum > schoolBuildingNum * 30)
        {
            if (IfInt(3000) == 1)
            {
                studentGood += 5;
                studentNormalNum += 15;
                studentBullyNum += 10;

            }
            else
            {
                return;
            }
        }
        schoolClassNum += 1;

        StudentHandler shit = GameObject.Find("StudentHandler").GetComponent<StudentHandler>();

        shit.studentNum++;
        shit.UpdateStudentNum();
    }

    public void Campain()//캠페인 이벤트 함수
    {
        if (schoolChick[2] < 1)
        {
            if (IfInt(2000) == 1)
            {
                studentBullyNum -= 10;
                studentNormalNum += 10;
            }
            else
            {
                return;
            }
            schoolChick[2] += 1;
        }
    }

    public void _Meney(int Meney)
    {
        for(int i=0; i < aext.Length; i++)
        {
            if (aext[i].gameObject.activeSelf)
            {
                aext[i].text = name + Meney.ToString() + " 만원";
            }
        }
    }

    int IfInt(int a) //비교 함수
    {
        if (schoolBudgetM < a && schoolBudgetU > 1)
        {
            schoolBudgetU -= 1;
            schoolBudgetM = 10000 + schoolBudgetM - a;
            return 1;
        }
        else if (schoolBudgetM >= a)
        {
            schoolBudgetM -= a;
            return 1;

        }
        else
        {
            return 2;
        }
    }
    #endregion
}
