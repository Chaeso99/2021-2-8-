using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱����� �Ҵ��� ���� ����

    public bool isGameStart = false; // ���� ��ŸƮ ����
    public bool isPause = false; // �Ͻ����� ����
    public bool isGameOver = false; // ���� ���� ����

    public Text timerText; // Ÿ�̸� �ؽ�Ʈ
    public Text scoreText; // ���� ǥ�� �ؽ�Ʈ
    public Text pauseGuide; // �Ͻ����� ��ư �ȳ� �ؽ�Ʈ
    public Text pauseText; // �Ͻ����� �ؽ�Ʈ
    public Text gameoverText; // ���� ���� �ؽ�Ʈ

    public GameObject Stage2UI;  // �������� ��ȯ�� ȭ�鿡 ��� UI��
    public GameObject Stage3UI;

    public int stage = 1; // ���� �������� ��������
    public int score = 0; // ���� ������ �����ϴ� ����
    public static int maxScore = 0; // �ְ� ������ �����ϴ� ���� ���� (�ٽ� ���۽� �ʱ�ȭ�� ����)
    private float startTime = 0; // ���� ���� �ð��� ������ ����
    private float scoreStartTime = 0; // ���ھ�� ���� ���� �ð��� ������ ����
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
        startTime = Time.time; // ���� �ð� ���� (�ʱ�ȭ)

        // 3�� �� ���� ����
        Time.timeScale = 0;
        SoundManager.instance.AudioPause();
        StartCoroutine(Timer(3));

        StartCoroutine(NextStage());
    }
    
    void Update()
    {
        // ���� ������ �ƴϸ� ���� ����
        if (!isGameStart)
        {
            return;
        }

        // ���� ���� �����̸� ���� ����
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

    // �ؽ�Ʈ ���� ���� (����, �Ͻ����� �ȳ� �ؽ�Ʈ ����)
    public void TextColorChange()
    {
        if (stage == 2)
        {
            scoreText.text = "<color=#FFFFFF>" + "Score : " + score.ToString() + "</color>";
            pauseGuide.text = "<color=#FFFFFF>" + "ESC:�Ͻ�����" + "</color>";
        }
        else
        {
            scoreText.text = "Score : " + score.ToString(); // ���� ���
            pauseGuide.text = "ESC:�Ͻ�����";
        }
    }

    // 3�ʿ� 100�� ������Ű�� �⺻ ���� �޼ҵ�
    public void AddScore()
    {   
        // ���ӿ����� �ƴϸ鼭 ���� ���ۺ��� ������� ������ �ð��� totalTime���� ���� �� 
        if (!isGameOver && totalTime < Time.time - scoreStartTime)
        {
            score += 100; // ���� ����
            totalTime = repeatTime + Time.time; // �ݺ��� �ð� (3��) + ���� ���ۺ��� ������� ������ �ð�
        }
    }

    // ���� ȹ���� ���� (���ӿ��� ������ ���� ���� ���)
    public int CurrntScore()
    {
        return score;
    }

    // �ְ� ���� (���� ȹ���� ������ �ְ� �������� ������ �ְ� ���� ����, �ְ� ���� ���) 
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
                if (stage == 2)
                {
                    pauseText.text = "<color=#FF2A13>" + "�Ͻ�����\n���� Ű�� �����ּ���!" + "</color>";
                }
                else
                {
                    pauseText.text = "�Ͻ�����\n���� Ű�� �����ּ���!";
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

    // 3�� �� ���ӿ��� �� ��ȯ �ڷ�ƾ
    IEnumerator SwitchDelayScene()
    {
        if (isGameOver)
        {
            yield return new WaitForSecondsRealtime(3f);

            SceneDirector.GameOverChange();
        }
    }

    // n�� ī��Ʈ�ٿ��� ���� Ÿ�̸� �ڷ�ƾ
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

    // ���� �ð� ���Ŀ� �������� ������ ���� �ڷ�ƾ
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