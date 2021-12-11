using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxScore : MonoBehaviour
{
    public Text currntScoreText; // ���� ȹ�� ���� �ؽ�Ʈ
    public Text maxScoreText; // �ְ� ȹ�� ���� �ؽ�Ʈ

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
