using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{
    static public bool isGetSyringe; //주사기 먹었는지 안먹었는지 확인. 주사기를 먹은 순간에만 true가 되도록함.
    static public bool OnDamaged; //무적상태인지 확인하는 변수. 아이템스포너 스크립트에서 이용함
    private float timeInterval = 2.9f; //무적 시간
    public float GetSyringeTime; //주사기 아이템을 먹은 시점

    private GameObject Player;
    private SpriteRenderer player_SpriteRenderer; // Player의 SpriteRenderer
    private SpriteRenderer SpriteRenderer; //주사기의 SpriteRenderer
    private Color c;

    private void Start()
    {
        Player = GameObject.Find("Player");
        player_SpriteRenderer = Player.GetComponent<SpriteRenderer>();
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        isGetSyringe = false;
    }
    private void Update()
    {         
        if (isGetSyringe == true)
        {
            Player.layer = 8; //장애물이랑 안부딪치게 함
            player_SpriteRenderer.material.color = new Color(0.0f / 255.0f, 255.0f / 255.0f, 1.0f / 255.0f, 255.0f / 255.0f); // 녹색으로 변경 

            if (Time.time - GetSyringeTime >= (timeInterval - 1))
            {
                player_SpriteRenderer.material.color = new Color(188.0f / 255.0f, 255.0f / 255.0f, 1.0f / 255.0f, 255.0f / 255.0f);// 무적시간 끝나기 1초 전 연두색으로 변경
            }
            if (Time.time - GetSyringeTime >= timeInterval)
            {
                s_DamagedOff();
                isGetSyringe = false;
            }
        }
        else if (isGetSyringe == false)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlayItemSound();
            SpriteRenderer.material.color = Color.clear; //주사기 아이템 투명하게 함(SetActive를 false로 처리하면, 후에 코드가 작동이 안됨)
            GetSyringeTime = Time.time;
            isGetSyringe = true;
        }
    }

    public void s_DamagedOff() // 무적상태 풀림
    {
        Player.layer = 0;
        player_SpriteRenderer.material.color = Color.white; // 색상 원상태로
    }
}


