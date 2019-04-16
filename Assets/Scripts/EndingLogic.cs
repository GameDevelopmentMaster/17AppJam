using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingLogic : MonoBehaviour
{
    public Text UIStudent, UICut;


    // Start is called before the first frame update
    void Start()
    {
        UICut.text = string.Format("{0}%", PlayerPrefs.GetFloat("SCORE_CUT", 0f));
        UIStudent.text = string.Format("{0} 명", PlayerPrefs.GetFloat("SCORE_STUDENTS", 0));
    }

    public void GoBack ()
    {
        SceneManager.LoadScene("Main");
    }
}
