using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    public bool isGameStart = false; // 게임 스타트 상태
    public bool isPause = false; // 일시정지 상태
    public bool isGameOver = false; // 게임 오버 상태

    public Text timerText; // 타이머 텍스트
    public Text scoreText; // 점수 표시 텍스트
    public Text pauseGuide; // 일시정지 버튼 안내 텍스트
    public Text pauseText; // 일시정지 텍스트
    public Text gameoverText; // 게임 오버 텍스트

    public GameObject Stage2UI;  // 스테이지 전환시 화면에 띄울 UI들
    public GameObject Stage3UI;

    public int stage = 1; // 현재 진행중인 스테이지
    public int score = 0; // 게임 점수를 저장하는 변수
    public static int maxScore = 0; // 최고 점수를 저장하는 정적 변수 (다시 시작시 초기화를 방지)
    private float startTime = 0; // 게임 시작 시간을 저장할 변수
    private float scoreStartTime = 0; // 스코어용 게임 시작 시간을 저장할 변수
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
        startTime = Time.time; // 시작 시간 저장 (초기화)

        // 3초 뒤 게임 시작
        Time.timeScale = 0;
        SoundManager.instance.AudioPause();
        StartCoroutine(Timer(3));

        StartCoroutine(NextStage());
    }
    
    void Update()
    {
        // 게임 시작이 아니면 실행 종료
        if (!isGameStart)
        {
            return;
        }

        // 게임 오버 상태이면 실행 종료
        if (isGameOver)
        {
            return;
        }

        TextColorChange();

        AddScore();

        PauseButton();
    }
   
    public void OnPlayerDead()
    {
        isGameOver = true;
        HpBar.DropHP();
        SoundManager.instance.AudioStop();

        if (stage == 2)
        {
            gameoverText.text = "<color=#FF2A13>" + "Game Over!!!" + "</color>";
        }
        else
        {
            gameoverText.text = "Game Over!!!";
        }

        MaxScore();

        StartCoroutine(SwitchDelayScene());
    }

    // 텍스트 색상 변경 (점수, 일시정지 안내 텍스트 변경)
    public void TextColorChange()
    {
        if (stage == 2)
        {
            scoreText.text = "<color=#FFFFFF>" + "Score : " + score.ToString() + "</color>";
            pauseGuide.text = "<color=#FFFFFF>" + "ESC:일시정지" + "</color>";
        }
        else
        {
            scoreText.text = "Score : " + score.ToString(); // 점수 출력
            pauseGuide.text = "ESC:일시정지";
        }
    }

    // 3초에 100점 증가시키는 기본 점수 메소드
    public void AddScore()
    {   
        // 게임오버가 아니면서 게임 시작부터 현재까지 측정된 시간이 totalTime보다 작을 때 
        if (!isGameOver && totalTime < Time.time - scoreStartTime)
        {
            score += 100; // 점수 증가
            totalTime = repeatTime + Time.time; // 반복할 시간 (3초) + 게임 시작부터 현재까지 측정된 시간
        }
    }

    // 현재 획득한 점수 (게임오버 씬에서 현재 점수 출력)
    public int CurrntScore()
    {
        return score;
    }

    // 최고 점수 (현재 획득한 점수가 최고 점수보다 높으면 최고 점수 갱신, 최고 점수 출력) 
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
                if (stage == 2)
                {
                    pauseText.text = "<color=#FF2A13>" + "일시정지\n점프 키를 눌러주세요!" + "</color>";
                }
                else
                {
                    pauseText.text = "일시정지\n점프 키를 눌러주세요!";
                }

                Time.timeScale = 0;
                isPause = true;
                SoundManager.instance.AudioPause();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isPause)
            {
                pauseText.text = "";

                StartCoroutine(Timer(3));
            }
        }
    }

    // 3초 뒤 게임오버 씬 전환 코루틴
    IEnumerator SwitchDelayScene()
    {
        if (isGameOver)
        {
            yield return new WaitForSecondsRealtime(3f);

            SceneDirector.GameOverChange();
        }
    }

    // n초 카운트다운을 위한 타이머 코루틴
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

            if (stage == 2)
            {
                timerText.text = "<color=#00FF0F>" + string.Format("{0:f0}", countDown) + "</color>";
            }
            else
            {
                timerText.text = string.Format("{0:f0}", countDown);
            }

            if (countDown <= 0)
            {
                timerText.enabled = false;
                Time.timeScale = 1;
            }

            yield return null;
        }

        SoundManager.instance.AudioPlay();

        if (!isGameStart)
        {
            isGameStart = true;
        }

        if (isPause)
        {
            isPause = false;
        }
    }

    // 일정 시간 이후에 스테이지 변경을 위한 코루틴
    IEnumerator NextStage()
    {
        while (stage < 3)
        {
            yield return new WaitForSeconds(30.0f);
            stage++;
            
            if (stage == 2)
            {
                Stage2UI.SetActive(true);

                Destroy(Stage2UI, 3f);
            }

            if (stage == 3)
            {
                Stage3UI.SetActive(true);

                Destroy(Stage3UI, 3f);
            }
            
        }
    }
}