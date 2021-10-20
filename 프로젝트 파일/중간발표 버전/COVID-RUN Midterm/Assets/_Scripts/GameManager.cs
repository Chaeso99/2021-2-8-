using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;

    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    public bool isGameStart = false; // 게임 스타트 상태
    public bool isPause = false; // 일시정지 상태
    public bool isGameOver = false; // 게임 오버 상태

    public Text timerText; // 타이머 텍스트
    public Text scoreText; // 점수 표시 텍스트

    public GameObject pauseText; // 일시정지 텍스트
    public GameObject gameoverText; // 게임 오버 텍스트

    public int score = 0; // 게임 점수를 저장하는 변수
    public static int maxScore = 0; // 최고 점수를 저장하는 정적 변수 (다시 시작시 초기화를 방지)
    private float repeatTime = 3; // 반복할 시간 (3초)
    private float totalTime = 1; // 반복할 시간 + 게임 시작부터 현재까지 흐른 시간을 저장할 변수 

    // 싱글톤
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Debug.LogError("Scene에 두 개 이상의 GameManager가 존재합니다!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 3초 뒤 게임 시작
        Time.timeScale = 0;
        soundManager.AudioPause();
        StartCoroutine(Timer(3));
    }

    void Update()
    {
        // 게임 시작이 아니면 실행 종료
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

    // 3초에 100점 증가시키는 기본 스코어 메소드
    public void AddScore()
    {
        // 게임오버가 아니면서 게임 시작부터 현재까지 측정된 시간이 totalTime보다 작을 때 
        if (!isGameOver && totalTime < Time.time)
        {
            score += 100; // 점수 증가
            totalTime = repeatTime + Time.time; // 반복할 시간 (3초) + 게임 시작부터 현재까지 측정된 시간
            scoreText.text = "Score : " + score.ToString(); // 점수 출력
        }

        //score += 1;
        //scoreText.text = "Score : " + score.ToString();
    }

    // 현재 획득한 점수
    public int CurrntScore()
    {
        return score;
    }

    // 최고 점수 (현재 획득한 점수가 최고 점수보다 높으면 최고 점수 갱신) 
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
        // 게임 시작이 아니면 실행 종료
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

    // 3초 뒤 게임오버 씬 전환
    IEnumerator SwitchDelayScene()
    {
        if (isGameOver)
        {
            yield return new WaitForSeconds(3f);

            SceneDirector.GameOverChange();
        }
    }

    // n초 카운트다운을 위한 타이머
    IEnumerator Timer(float countTime) // 멈출 시간
    {
        timerText.enabled = true;

        float lastTime = Time.realtimeSinceStartup; // 게임이 시작된 시간으로부터 경과한 실제 시간 저장 (대략 1.45초)
        float processTime = 0; 
        float countDown = 0;

        // 실제 시간 - 위에 저장된 실제 시간이 멈출 시간보다 작을 때까지
        while (processTime < countTime)
        {
            processTime = Time.realtimeSinceStartup - lastTime; // 실제 시간 - 위에 저장된 실제 시간
            countDown = countTime - processTime; // 멈출 시간 - 게임이 시작된 시간으로부터 경과한 실제 시간
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