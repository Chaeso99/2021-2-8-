using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 게임 오버, 게임 점수, UI 관리
public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;

    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    public bool isGameStart = false; // 게임 스타트 상태
    public bool isPause = false; // 일시정지 상태
    public bool isGameOver = false; // 게임 오버 상태

    public Text timerText; // 타이머 텍스트
    public Text ScoreText; // 점수 표시 텍스트
    public GameObject pauseText; // 일시정지 텍스트
    public GameObject gameoverText; // 게임 오버 텍스트

    //private int Score = 0; // 게임 점수를 저장하는 변수
    //private float repeatTime = 1; // 반복할 시간 (1초)
    //private float Time2 = 1;

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
    // n초 뒤 일시정지 해제
    IEnumerator PauseDelay3f()
    {
        yield return new WaitForSecondsRealtime(3f); // timeScale에 영향을 받지 않음

        pauseText.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
        soundManager.AudioPlay();
    }
    */
}