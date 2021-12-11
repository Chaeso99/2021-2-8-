using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxScore : MonoBehaviour
{
    public Text currntScoreText; // ÇöÀç È¹µæ Á¡¼ö ÅØ½ºÆ®
    public Text maxScoreText; // ÃÖ°í È¹µæ Á¡¼ö ÅØ½ºÆ®

    void Start()
    {
        DisplayScore();
    }

    public void DisplayScore()
    {
        currntScoreText.text = GameManager.instance.score.ToString();
        maxScoreText.text = GameManager.instance.MaxScore().ToString();
    }
}
