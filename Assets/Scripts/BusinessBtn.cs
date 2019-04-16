using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusinessBtn : MonoBehaviour
{
    // 버튼을 눌렀을때 목적지를 열거형에 쑤셔박았습니다.
    // onClick 에서 SWITCH 문을 이용해 느낌표를 표시할지 안할지 정할때 사용됩니다.
    enum ePANEL
    {
        STATUS = 0,
        EDUCATION,
        STUDENT,
        FOOD,
        VIOLENCE,
        BUSINESS,
        SCHOOL
    }

    public GameObject MainPanel;
    public Info InfoHandler;

    public GameObject[] BusineessPanel;
    public int key;

    // 설명 (TMI)
    private Dictionary<ePANEL, string[]> msgTable = new Dictionary<ePANEL, string[]>
    {
        // 상태
        {ePANEL.STATUS, new[]{  "학생이나 학부모의 불만이 가득쌓이면 게임 오버가 되니 주의하자.",
                                "학비는 학기마다 들어온다."} },
        // 학생관리
        {ePANEL.STUDENT, new[]{ "전학을 받으려면 학급을 증설해야 한다.",
                                "건물을 건설하면 30개의 학급이 생긴다.",
                                "학폭2번이 열리면 자퇴권고로 자퇴시킬 수 있다."} },

        // 급식
        {ePANEL.FOOD, new[]{"급식의 질을 올리면 학생들의 불만이 줄어든다.",
                            "석식을 제공하려면 야자를 실시해야 한다."} },

        // 학폭관리
        {ePANEL.VIOLENCE, new[]{"캠페인 실시를 하면 불량학생들 5명을 구제할 수 있다.",
                                "보안관을 고용하면 학폭 조건을 10+(보안관)명으로 증가시킨다."} },

        // 교육관리
        {ePANEL.EDUCATION, new[]{   "방과후는 턴 당 천만원 회수 및 입학 내신1% 진학률2%을 증가시킨다.",
                                    "유명 선생 고용은 입학 내신 3퍼% 진학률 3퍼 증가시킨다. (최대 3번)",
                                    "취업교육은 취업률 5% 상승을 시킬 수 있다. (학기당 2번)",
                                    "야자는 입학 내신 10%, 진힉률 10%상승을 시킨다 (영구)"} },

        // 사업운영
        {ePANEL.BUSINESS, new[]{"취업지원 사업은 취업률 10%상승 시킨다.",
                                "장학금지원사업은 학생수 500명, 입학내신 20퍼 이상일 때 학기당 1억을 받을 수 있다!",
                                "중점학교는 우수학생이 50명일 때 실시할 수 있으며, 교육청에서 학기당 2억씩 받을 수 있다!"} },

        // 학교평가
        {ePANEL.SCHOOL, new[]{  "입학 내신이 10%마다 학비가 10만원씩 상승한다.",
                                "진학률과 취업률이 높으면 입학 내신이 올라간다." } }
    };

    public void onClick(int key)
    {
        MainPanel.SetActive(false);
        BusineessPanel[key].SetActive(true);

        // 혹시라도 유저가 설명을 숨기는걸 깜빡하고 다른 메뉴로 갈까봐...
        // 숨깁니다.
        InfoHandler.SetPanelShow(false);

        // 설명 예외
        switch (key)
        {
            // 느낌표 않표시 예제
            // case ePANEL.STATUS:
            //      InfoHandler.buttonActive = false; <= 이걸로 버튼 비활성화
            //      break;

            // 느낌표 표시
            default:
                InfoHandler.buttonActive = true;
                break;
        }

        // 설명 업데이트
        if (msgTable.ContainsKey((ePANEL)key))
        {
            Debug.Log("OH");

            InfoHandler.messages.Clear();
            InfoHandler.messages.AddRange(msgTable[(ePANEL)key]);
            InfoHandler.UpdatePanel();
        }
        else
        {
            Debug.Log("WTF?");
        }
    }
}