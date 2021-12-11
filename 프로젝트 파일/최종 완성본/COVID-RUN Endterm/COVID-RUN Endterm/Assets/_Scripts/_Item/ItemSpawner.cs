using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject Maskitem; // ����ũ������
    public GameObject syringe;  // �ֻ�� ������
    public GameObject Policrod; // ������ ������
    public GameObject Coin1;
    public GameObject coin2;
    public GameObject coin3;

    private int Randnum; // ���� ���� �����ϴ� ����
    private float timeinterval = 3.0f; // ������ ���� �ð� ����
    private float LastItemspawntime; // �ֱ� ������ ������ �ð� ����
    private float LastMaskSpawnTime; // �ֱ� ����ũ ������ �ð� ����

    private bool IsSpawnMaskInItemSpawner = false; // �����۽����ʿ��� ����ũ�������� �����Ǿ��°�?

    private Vector3 Itemspawnposition = new Vector3(13.4f, 1.0f, 0.0f); // ������ ���� ��ġ

    private void Start()
    {
        LastItemspawntime = Time.time;
        Randnum = Random.Range(0, 10); // ������ ���� Ȯ�� ����
    }
    void Update()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }

        if (Time.time >= LastItemspawntime + timeinterval)
        {
            if (Randnum == 0)  // ����ũ ������ ����
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
                    //Debug.Log("�ֻ�� �������¶� �ֻ�Ⱑ �������� �ʾҽ��ϴ�.");
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
                Randnum = Random.Range(0, 10);  //������ ����Ȯ�� ����
            }
        }

        if (Time.time >= LastMaskSpawnTime + 15.0f && IsSpawnMaskInItemSpawner == false) //�����۽����ʿ��� ����ũ�� �Ƚ������� 15�ʰ� ������
        {
            //Debug.Log("����ũ �������� 15�ʰ� �������ϴ�");
            Instantiate(Maskitem, Itemspawnposition, Quaternion.identity);
            LastMaskSpawnTime = Time.time;
        }
    }

    public void SetRandnum()
    {
        LastItemspawntime = Time.time;
        Randnum = Random.Range(0, 10); //������ ����Ȯ�� ����
        IsSpawnMaskInItemSpawner = false;
    }
}
