using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public GameObject infoButton;
    public GameObject contentParent;

    public Text contentText; // 내용물
    public Button nextButton, prevButton; // 다음/이전 버튼

    public bool isActive; // 보여줄까?
    public bool isButtonActive; // 버튼 클릭가능?
    public List<string> messages; // 보여줄 메시지들; 페이지 단위.
    public int page = 0; // 페이지
    
    public bool buttonActive // 버튼 활성화 비활성화
    {
        // 사용법 : Info.buttonActive(true / false);
        get
        {
            return isButtonActive;
        }
        set
        {
            isButtonActive = value;
            infoButton.SetActive(value);
        }
    }

    public bool contentActive
    {
        // 사용법 : Info.contentActive(true / false);
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
            contentParent.SetActive(value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        contentActive = isActive;
        buttonActive = isButtonActive;
    }

    // 내용 보여주거나 숨기는 함수 -- yey : 보이거나 않보이거나
    public void SetPanelShow (bool yey)
    {
        contentParent.SetActive(yey);
        page = 0;
    }

    // 내용 업데이트
    public void UpdatePanel ()
    {
        // 내용 가져오기
        string current = messages[page];
        contentText.text = current;

        UpdateButtons();
    }

    // 페이지 이동
    public void NextPage ()
    {
        page++;

        UpdateButtons();

        UpdatePanel();
    }

    public void PrevPage()
    {
        page--;

        UpdateButtons();

        UpdatePanel();
    }

    // 버튼 활성화 비화성화 업데이트
    public void UpdateButtons ()
    {
        if (page < messages.Count - 1)
            nextButton.interactable = true;
        else
            nextButton.interactable = false;

        if (page > 0)
            prevButton.interactable = true;
        else
            prevButton.interactable = false;

        page = Mathf.Clamp(page, 0, messages.Count - 1);
    }
}
