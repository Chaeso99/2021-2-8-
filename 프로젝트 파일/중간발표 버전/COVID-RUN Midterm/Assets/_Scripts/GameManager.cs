using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;

    public static GameManager instance; // �̱����� �Ҵ��� ���� ����

    public bool isGameStart = false; // ���� ��ŸƮ ����
    public bool isPause = false; // �Ͻ����� ����
    public bool isGameOver = false; // ���� ���� ����

    public Text timerText; // Ÿ�̸� �ؽ�Ʈ
    public Text scoreText; // ���� ǥ�� �ؽ�Ʈ

    public GameObject pauseText; // �Ͻ����� �ؽ�Ʈ
    public GameObject gameoverText; // ���� ���� �ؽ�Ʈ

    public int score = 0; // ���� ������ �����ϴ� ����
    public static int maxScore = 0; // �ְ� ������ �����ϴ� ���� ���� (�ٽ� ���۽� �ʱ�ȭ�� ����)
    private float repeatTime = 3; // �ݺ��� �ð� (3��)
    private float totalTime = 1; // �ݺ��� �ð� + ���� ���ۺ��� ������� �帥 �ð��� ������ ���� 

    // �̱���
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Debug.LogError("Scene�� �� �� �̻��� GameManager�� �����մϴ�!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 3�� �� ���� ����
        Time.timeScale = 0;
        soundManager.AudioPause();
        StartCoroutine(Timer(3));
    }

    void Update()
    {
        // ���� ������ �ƴϸ� ���� ����
        if (!isGameStart)
        {
            return;
        }

        AddScore();

        PauseButton();
    }
   
    public void OnPlayerDead()
    {
        isGameOver = true;
        HpBar.DropHP();
        gameoverText.SetActive(true);
        MaxScore();

        StartCoroutine(SwitchDelayScene());
    }

    // 3�ʿ� 100�� ������Ű�� �⺻ ���ھ� �޼ҵ�
    public void AddScore()
    {
        // ���ӿ����� �ƴϸ鼭 ���� ���ۺ��� ������� ������ �ð��� totalTime���� ���� �� 
        if (!isGameOver && totalTime < Time.time)
        {
            score += 100; // ���� ����
            totalTime = repeatTime + Time.time; // �ݺ��� �ð� (3��) + ���� ���ۺ��� ������� ������ �ð�
            scoreText.text = "Score : " + score.ToString(); // ���� ���
        }

        //score += 1;
        //scoreText.text = "Score : " + score.ToString();
    }

    // ���� ȹ���� ����
    public int CurrntScore()
    {
        return score;
    }

    // �ְ� ���� (���� ȹ���� ������ �ְ� �������� ������ �ְ� ���� ����) 
    public int MaxScore()
    {
        if (score >= maxScore)
        {
            maxScore = score;
        }

        return maxScore;
    }

    public void PauseButton()
    {
        // ���� ������ �ƴϸ� ���� ����
        if (!isGameStart)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (!isPause)
            {
                pauseText.SetActive(true);
                Time.timeScale = 0;
                isPause = true;
                soundManager.AudioPause();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isPause)
            {
                pauseText.SetActive(false);

                StartCoroutine(Timer(3));
            }
        }
    }

    // 3�� �� ���ӿ��� �� ��ȯ
    IEnumerator SwitchDelayScene()
    {
        if (isGameOver)
        {
            yield return new WaitForSeconds(3f);

            SceneDirector.GameOverChange();
        }
    }

    // n�� ī��Ʈ�ٿ��� ���� Ÿ�̸�
    IEnumerator Timer(float countTime) // ���� �ð�
    {
        timerText.enabled = true;

        float lastTime = Time.realtimeSinceStartup; // ������ ���۵� �ð����κ��� ����� ���� �ð� ���� (�뷫 1.45��)
        float processTime = 0; 
        float countDown = 0;

        // ���� �ð� - ���� ����� ���� �ð��� ���� �ð����� ���� ������
        while (processTime < countTime)
        {
            processTime = Time.realtimeSinceStartup - lastTime; // ���� �ð� - ���� ����� ���� �ð�
            countDown = countTime - processTime; // ���� �ð� - ������ ���۵� �ð����κ��� ����� ���� �ð�
            timerText.text = string.Format("{0:f0}", countDown);

            if (countDown <= 0)
            {
                timerText.enabled = false;
                Time.timeScale = 1;
            }

            yield return null;
        }

        soundManager.AudioPlay();

        if (!isGameStart)
        {
            isGameStart = true;
        }

        if (isPause)
        {
            isPause = false;
        }
    }
}