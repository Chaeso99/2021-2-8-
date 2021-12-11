using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    PlayerController playerController;

    Image Hp_gauge;
    int maxHp = 1000;
    public static float HP;   // 플레이어 캐릭터의 현재 HP를 저장하는 변수

    bool isZero = false;    // HP가 0임을 체크하는 변수
    float LastReduceHpTime;         // 최근 플레이어 캐릭터의 HP를 줄인 시간을 저장하는 변수
    float ReduceIntervalTime = 1.0f; // 플레이어 캐릭터의 HP를 줄이는 시간 간격

    void Start()
    {
        Hp_gauge = GetComponent<Image>();
        Hp_gauge.fillAmount = maxHp;
        HP = maxHp;

        LastReduceHpTime = Time.time; //초기화 
    }
    void Update()
    {
        // isZero가 true가 되면 (HP가 0이 되면) 실행 종료
        if (isZero)
        {
            return;
        }

        //HP가 1000을 넘어가지 않도록
        if(HP > 1000.0f)
        {
            HP = 1000.0f;
        }

        Hp_gauge.fillAmount = HP / maxHp;
        
        // 1초마다 플레이어 캐릭터의 HP를 10씩 줄임
        if (ReduceIntervalTime <= Time.time - LastReduceHpTime)  
        {
            HP -= 10.0f;
            LastReduceHpTime = Time.time;
        }
        
        // HP가 0 이하가 되면 사망
        if (HP <= 0f)
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
            HP = 0;
        }
    }
}
