using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject Maskitem; // 마스크아이템
    public GameObject syringe;  // 주사기 아이템
    public GameObject Policrod; // 경찰봉 아이템
    public GameObject Coin1;
    public GameObject coin2;
    public GameObject coin3;

    private int Randnum; // 랜덤 숫자 저장하는 변수
    private float timeinterval = 3.0f; // 아이템 생성 시간 간격
    private float LastItemspawntime; // 최근 아이템 생성한 시간 저장
    private float LastMaskSpawnTime; // 최근 마스크 생성한 시간 저장

    private bool IsSpawnMaskInItemSpawner = false; // 아이템스포너에서 마스크아이템이 스폰되었는가?

    private Vector3 Itemspawnposition = new Vector3(13.4f, 1.0f, 0.0f); // 아이템 스폰 위치

    private void Start()
    {
        LastItemspawntime = Time.time;
        Randnum = Random.Range(0, 10); // 아이템 생성 확률 설정
    }
    void Update()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }

        if (Time.time >= LastItemspawntime + timeinterval)
        {
            if (Randnum == 0)  // 마스크 아이템 생성
            {
                LastMaskSpawnTime = Time.time;
                Instantiate(Maskitem, Itemspawnposition, Quaternion.identity);
                SetRandnum();
                IsSpawnMaskInItemSpawner = true;
            }
            else if (Randnum == 1)
            {
                if(Syringe.isGetSyringe==true)
                {
                    //Debug.Log("주사기 무적상태라 주사기가 스폰되지 않았습니다.");
                    SetRandnum();
                    return;
                }
                else if(Syringe.isGetSyringe==false)
                {
                    Instantiate(syringe, Itemspawnposition, Quaternion.identity);
                    SetRandnum();
                }
            }
            else if (Randnum == 2)
            {
                Instantiate(Coin1, Itemspawnposition, Quaternion.identity);
                SetRandnum();
            }
            else if (Randnum == 3)
            {
                Instantiate(coin2, Itemspawnposition, Quaternion.identity);
                SetRandnum();
            }
            else if (Randnum == 4)
            {
                Instantiate(coin3, Itemspawnposition, Quaternion.identity);
                SetRandnum();
            }
            else if (Randnum == 5)
            {
                Instantiate(Policrod, Itemspawnposition, Quaternion.identity);
                SetRandnum();
            }

            else
            {
                Randnum = Random.Range(0, 10);  //아이템 생성확률 설정
            }
        }

        if (Time.time >= LastMaskSpawnTime + 15.0f && IsSpawnMaskInItemSpawner == false) //아이템스포너에서 마스크가 안스폰된지 15초가 지나면
        {
            //Debug.Log("마스크 생성한지 15초가 지났습니다");
            Instantiate(Maskitem, Itemspawnposition, Quaternion.identity);
            LastMaskSpawnTime = Time.time;
        }
    }

    public void SetRandnum()
    {
        LastItemspawntime = Time.time;
        Randnum = Random.Range(0, 10); //아이템 생성확률 설정
        IsSpawnMaskInItemSpawner = false;
    }
}
