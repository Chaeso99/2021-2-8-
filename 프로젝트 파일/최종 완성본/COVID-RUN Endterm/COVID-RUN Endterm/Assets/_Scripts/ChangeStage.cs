using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStage : MonoBehaviour
{
    public GameObject changeBgObject; // 다음 스테이지 배경 프리팹

    public float fadeTime = 2f; // 페이드 효과 재생시간

    float start = 1f;
    float end = 0f;
    float time = 0f;
    //bool isPlaying = false; // 페이드 효과가 진행 중임을 체크하는 변수

    private SpriteRenderer bgSpriteRenderer; // 백그라운드 색상을 변경하기 위한 변수 (알파값 변경)

    void Start()
    {
        bgSpriteRenderer = GetComponent<SpriteRenderer>();

        // 3스테이지(해변가)가 되면 배경 전환을 안함
        if (GameManager.instance.stage == 3)
        {
            return;
        }
        
        StartCoroutine(FadeInBackground());
    }

    // 배경 전환을 위한 코루틴 (페이드 인 이용)
    IEnumerator FadeInBackground()
    {
        yield return new WaitForSeconds(30.0f);

        // 게임오버 상태이면 코루틴 실행 종료
        if (GameManager.instance.isGameOver)
        {
            yield break;
        }

        // 다음 스테이지의 배경을 생성하고 Background 오브젝트의 자식으로 이동시킴
        GameObject tGO = Instantiate(changeBgObject) as GameObject;
        tGO.transform.parent = GameObject.Find("Background").transform;

        // 현재 스테이지와 다음 스테이지의 배경 위치를 맞춤 (두 배경의 위치가 어긋남을 방지)
        tGO.transform.position = this.gameObject.transform.position;

        // 다음 스테이지의 bgm 재생 (미구현)

        Color bgColor = bgSpriteRenderer.color;
        time = 0f;
        bgColor.a = Mathf.Lerp(start, end, time);

        // 이미지 색상의 알파값이 0으로 가까워 질 수록 투명해짐
        while (bgColor.a > 0f)
        {
            time += Time.deltaTime / fadeTime; // 지정한 시간 (FadeTime) 만큼 페이드 인 효과를 주기 위해 1초를 나눔
            bgColor.a = Mathf.Lerp(start, end, time); // start와 end의 중간값을 리턴
            bgSpriteRenderer.color = bgColor;

            yield return null; // 다음 프레임까지 대기
        }

        // 이전 스테이지 배경 제거
        Destroy(gameObject);
    }
}