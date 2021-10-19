using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ����, ���� ����, UI ����
public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;

    public static GameManager instance; // �̱����� �Ҵ��� ���� ����

    public bool isGameStart = false; // ���� ��ŸƮ ����
    public bool isPause = false; // �Ͻ����� ����
    public bool isGameOver = false; // ���� ���� ����

    public Text timerText; // Ÿ�̸� �ؽ�Ʈ
    public Text ScoreText; // ���� ǥ�� �ؽ�Ʈ
    public GameObject pauseText; // �Ͻ����� �ؽ�Ʈ
    public GameObject gameoverText; // ���� ���� �ؽ�Ʈ

    //private int Score = 0; // ���� ������ �����ϴ� ����
    //private float repeatTime = 1; // �ݺ��� �ð� (1��)
    //private float Time2 = 1;

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
        Time.timeScale = 0;
        soundManager.AudioPause();
        StartCoroutine(Timer(3));
    }

    void Update()
    {
        /*
        if (!isGameOver && Time2 < Time.time)
        {
            Score += 1;
            repeatTime = 1;
            Time2 = repeatTime + Time.time;
            //scoreText.text = "Score : " + Score.ToString();
        }
        */
        PauseButton();
    }
   
    public void OnPlayerDead()
    {
        isGameOver = true;
        HpBar.DropHP();
        gameoverText.SetActive(true);

        StartCoroutine(SwitchDelayScene());
    }

    public void PauseButton()
    {
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
    IEnumerator Timer(float countTime)
    {
        timerText.enabled = true;

        float lastTime = Time.realtimeSinceStartup;
        float processTime = 0;
        float countDown = 0;

        while (processTime < countTime)
        {
            processTime = Time.realtimeSinceStartup - lastTime;
            countDown = countTime - processTime;
            //Debug.Log(countDown);
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

    /*
    // n�� �� �Ͻ����� ����
    IEnumerator PauseDelay3f()
    {
        yield return new WaitForSecondsRealtime(3f); // timeScale�� ������ ���� ����

        pauseText.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
        soundManager.AudioPlay();
    }
    */
}