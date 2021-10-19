using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject Platform;
    public GameObject EmptyPlatform;

    private BoxCollider2D boxc;
    private Vector3 PlatformSpawnPosition = new Vector3(13.0f, -4.0f, 0.0f);
    public bool isEmptyPlatfromSpawn = false; // 빈 플랫폼이 1개 연속 나오지 않게하기 위해

    private void Start()
    {
        boxc = GetComponent<BoxCollider2D>();
    }

    public void SpawnPlatform()
    {
        if(Random.Range(0,6)==0&&isEmptyPlatfromSpawn==false)
        {
            Instantiate(EmptyPlatform, PlatformSpawnPosition, Quaternion.identity);
            isEmptyPlatfromSpawn = true;
        }
        else
        {
            Instantiate(Platform, PlatformSpawnPosition, Quaternion.identity);
            isEmptyPlatfromSpawn = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Platform")
        {
            SpawnPlatform();
        }
    }

    //private float Timeinterval = 1f;
    //private float LastSpawnTime;
    //private bool isEmptyPlatfromSpawn = false; // 빈 플랫폼이 1개 연속 나오지 않게하기 위해

    //Vector3 PlatformSpawnPosition = new Vector3(13, -5, 0);

    //void start()
    //{
    //    LastSpawnTime = 0; // 게임 시작되면 변수 초기화
    //}

    //void Update()
    //{
    //    if (GameManager.instance.isGameOver)
    //    {
    //        return;
    //    }

    //    if (Time.time >= LastSpawnTime + Timeinterval) // 발판 생성 
    //    {
    //        if (Random.Range (0,6) == 0)
    //        {
    //            if (isEmptyPlatfromSpawn == true)
    //            {
    //                Instantiate(Platform, PlatformSpawnPosition, Quaternion.identity);
    //                LastSpawnTime = Time.time;
    //                isEmptyPlatfromSpawn = false;
    //            }

    //            Instantiate(EmptyPlatform,PlatformSpawnPosition,Quaternion.identity);
    //            LastSpawnTime = Time.time;
    //            isEmptyPlatfromSpawn = true;
    //        }
    //        else
    //        {
    //            Instantiate(Platform, PlatformSpawnPosition, Quaternion.identity);
    //            LastSpawnTime = Time.time;
    //            isEmptyPlatfromSpawn = false;
    //        }
    //    }
    //}
}