using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backward : MonoBehaviour
{
    public Button btn;

    public Info InfoHandler;
    public GameObject[] panel;
    public GameObject Main;

    public void onClick()
    {
        for (int i = 0; i < panel.Length; i++)
        {
            if (panel[i].activeSelf)
            {
                panel[i].SetActive(false);
                Main.SetActive(true);
            }
        }

        // 혹시라도 유저가 설명을 숨기는걸 깜빡하고 다른 메뉴로 갈까봐...
        // 숨깁니다.
        InfoHandler.SetPanelShow(false);
        InfoHandler.buttonActive = false;
    }
}