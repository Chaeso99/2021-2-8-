using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStage : MonoBehaviour
{
    public GameObject changeBgObject; // ���� �������� ��� ������

    public float fadeTime = 2f; // ���̵� ȿ�� ����ð�

    float start = 1f;
    float end = 0f;
    float time = 0f;
    //bool isPlaying = false; // ���̵� ȿ���� ���� ������ üũ�ϴ� ����

    private SpriteRenderer bgSpriteRenderer; // ��׶��� ������ �����ϱ� ���� ���� (���İ� ����)

    void Start()
    {
        bgSpriteRenderer = GetComponent<SpriteRenderer>();

        // 3��������(�غ���)�� �Ǹ� ��� ��ȯ�� ����
        if (GameManager.instance.stage == 3)
        {
            return;
        }
        
        StartCoroutine(FadeInBackground());
    }

    // ��� ��ȯ�� ���� �ڷ�ƾ (���̵� �� �̿�)
    IEnumerator FadeInBackground()
    {
        yield return new WaitForSeconds(30.0f);

        // ���ӿ��� �����̸� �ڷ�ƾ ���� ����
        if (GameManager.instance.isGameOver)
        {
            yield break;
        }

        // ���� ���������� ����� �����ϰ� Background ������Ʈ�� �ڽ����� �̵���Ŵ
        GameObject tGO = Instantiate(changeBgObject) as GameObject;
        tGO.transform.parent = GameObject.Find("Background").transform;

        // ���� ���������� ���� ���������� ��� ��ġ�� ���� (�� ����� ��ġ�� ��߳��� ����)
        tGO.transform.position = this.gameObject.transform.position;

        // ���� ���������� bgm ��� (�̱���)

        Color bgColor = bgSpriteRenderer.color;
        time = 0f;
        bgColor.a = Mathf.Lerp(start, end, time);

        // �̹��� ������ ���İ��� 0���� ����� �� ���� ��������
        while (bgColor.a > 0f)
        {
            time += Time.deltaTime / fadeTime; // ������ �ð� (FadeTime) ��ŭ ���̵� �� ȿ���� �ֱ� ���� 1�ʸ� ����
            bgColor.a = Mathf.Lerp(start, end, time); // start�� end�� �߰����� ����
            bgSpriteRenderer.color = bgColor;

            yield return null; // ���� �����ӱ��� ���
        }

        // ���� �������� ��� ����
        Destroy(gameObject);
    }
}