using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject Platform1; // ���ù���(��)
    public GameObject Platform2; // ���ù���(��)
    public GameObject Platform3; // �ٴٹ���
    public GameObject EmptyPlatform;

    public int Platform1Percent = 88; //���ù���(��) ����Ȯ��
    public int Platform2Percent = 86; //���ù���(��) ����Ȯ��
    public int Platform3Percent = 85; //�ٴٹ��� ����Ȯ��

    private BoxCollider2D boxc;
    private Vector3 PlatformSpawnPosition = new Vector3 (13.3f, -4.6f, 0.0f);
    public bool isEmptyPlatfromSpawn = false; // �� �÷����� 1�� ���� ������ �ʰ��ϱ� ����

    private void Start()
    {
        boxc = GetComponent<BoxCollider2D>();
    }
    
    // ���� �� �÷���
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

    // ���� �� �÷���
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

    // �ٴ� �÷���
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
    //private bool isEmptyPlatfromSpawn = false; // �� �÷����� 1�� ���� ������ �ʰ��ϱ� ����

    //Vector3 PlatformSpawnPosition = new Vector3(13, -5, 0);

    //void start()
    //{
    //    LastSpawnTime = 0; // ���� ���۵Ǹ� ���� �ʱ�ȭ
    //}

    //void Update()
    //{
    //    if (GameManager.instance.isGameOver)
    //    {
    //        return;
    //    }

    //    if (Time.time >= LastSpawnTime + Timeinterval) // ���� ���� 
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