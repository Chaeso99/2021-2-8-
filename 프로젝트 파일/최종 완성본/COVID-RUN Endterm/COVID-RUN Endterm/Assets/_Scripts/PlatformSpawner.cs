using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject Platform1; // µµ½Ã¹ßÆÇ(³·)
    public GameObject Platform2; // µµ½Ã¹ßÆÇ(¹ã)
    public GameObject Platform3; // ¹Ù´Ù¹ßÆÇ
    public GameObject EmptyPlatform;

    public int Platform1Percent = 88; //µµ½Ã¹ßÆÇ(³·) »ý¼ºÈ®·ü
    public int Platform2Percent = 86; //µµ½Ã¹ßÆÇ(¹ã) »ý¼ºÈ®·ü
    public int Platform3Percent = 85; //¹Ù´Ù¹ßÆÇ »ý¼ºÈ®·ü

    private BoxCollider2D boxc;
    private Vector3 PlatformSpawnPosition = new Vector3 (13.3f, -4.6f, 0.0f);
    public bool isEmptyPlatfromSpawn = false; // ºó ÇÃ·§ÆûÀÌ 1°³ ¿¬¼Ó ³ª¿ÀÁö ¾Ê°ÔÇÏ±â À§ÇØ

    private void Start()
    {
        boxc = GetComponent<BoxCollider2D>();
    }
    
    // µµ½Ã ³· ÇÃ·§Æû
    public void SpawnPlatform1()
    {
        if (Random.Range(1,101) > Platform1Percent && isEmptyPlatfromSpawn == false)
        {
            Instantiate (EmptyPlatform, PlatformSpawnPosition, Quaternion.identity);
            isEmptyPlatfromSpawn = true;
        }
        else
        {
            Instantiate (Platform1, PlatformSpawnPosition, Quaternion.identity);
            isEmptyPlatfromSpawn = false;
        }
    }

    // µµ½Ã ¹ã ÇÃ·§Æû
    public void SpawnPlatform2()
    {
        if (Random.Range(1,101) >Platform2Percent && isEmptyPlatfromSpawn == false)
        {
            Instantiate(EmptyPlatform, PlatformSpawnPosition, Quaternion.identity);
            isEmptyPlatfromSpawn = true;
        }
        else
        {
            Instantiate(Platform2, PlatformSpawnPosition, Quaternion.identity);
            isEmptyPlatfromSpawn = false;
        }
    }

    // ¹Ù´Ù ÇÃ·§Æû
    public void SpawnPlatform3()
    {
        if (Random.Range(1,101) >Platform3Percent && isEmptyPlatfromSpawn == false)
        {
            Instantiate(EmptyPlatform, PlatformSpawnPosition, Quaternion.identity);
            isEmptyPlatfromSpawn = true;
        }
        else
        {
            Instantiate(Platform3, PlatformSpawnPosition, Quaternion.identity);
            isEmptyPlatfromSpawn = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Platform" && GameManager.instance.stage == 1)
        {
            SpawnPlatform1();
        }

        if (collision.tag == "Platform" && GameManager.instance.stage == 2)
        {
            SpawnPlatform2();
        }

        else if (collision.tag == "Platform" && GameManager.instance.stage == 3)
        {
            SpawnPlatform3();
        }
    }

    //private float Timeinterval = 1f;
    //private float LastSpawnTime;
    //private bool isEmptyPlatfromSpawn = false; // ºó ÇÃ·§ÆûÀÌ 1°³ ¿¬¼Ó ³ª¿ÀÁö ¾Ê°ÔÇÏ±â À§ÇØ

    //Vector3 PlatformSpawnPosition = new Vector3(13, -5, 0);

    //void start()
    //{
    //    LastSpawnTime = 0; // °ÔÀÓ ½ÃÀÛµÇ¸é º¯¼ö ÃÊ±âÈ­
    //}

    //void Update()
    //{
    //    if (GameManager.instance.isGameOver)
    //    {
    //        return;
    //    }

    //    if (Time.time >= LastSpawnTime + Timeinterval) // ¹ßÆÇ »ý¼º 
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