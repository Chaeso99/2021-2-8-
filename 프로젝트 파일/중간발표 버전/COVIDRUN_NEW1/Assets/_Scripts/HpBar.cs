using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    PlayerController playerController;

    Image Hp_gauge;
    int maxHp = 1;
    public static float Hp;   // 플레이어 캐릭터의 현재 HP를 저장하는 변수

    bool isZero = false;    // HP가 0임을 체크하는 변수
    float LastReduceHpTime;         // 최근 플레이어 캐릭터의 HP를 줄인 시간을 저장하는 변수
    float ReduceIntervalTime = 1.0f; // 플레이어 캐릭터의 HP를 줄이는 시간 간격

    void Start()
    {
        Hp_gauge = GetComponent<Image>();
        Hp_gauge.fillAmount = maxHp;
        Hp = maxHp;

        LastReduceHpTime = Time.time; //초기화 
    }
    void Update()
    {
        // isZero가 true가 되면 (HP가 0이 되면) 실행 종료
        if (isZero)
        {
            return;
        }

        Hp_gauge.fillAmount = Hp / maxHp;

        // 1초마다 플레이어 캐릭터의 HP를 0.02씩 줄임
        if (ReduceIntervalTime <= Time.time - LastReduceHpTime)  
        {
            Hp -= 0.02f;
            LastReduceHpTime = Time.time;
        }

        // HP가 0 이하가 되면 사망
        if (Hp <= 0f)
        {
            isZero = true;
            // 플레이어의 스크립트를 불러옴
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();

            playerController.Die();
        }
    }

    public static void DropHP()
    {
        // 낙사하면 HP가 0이 됨
        if (GameManager.instance.isGameOver)
        {
            Hp = 0;
        }
    }
}
