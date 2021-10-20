using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] Person_obstacles; // �÷��� �� ��� ��ֹ�
    public GameObject[] Brick_obstacles; // �÷��� �� ���� ��ֹ�

    private int Randvalue; // ���� ���� ������ ����

    private void OnEnable()  // ������Ʈ�� Ȱ��ȭ �� ������ ����
    {
        for (int i = 0; i < Person_obstacles.Length && i < Brick_obstacles.Length; i++) // i�� �� ������Ʈ �迭�� ���̺��� ������ for�� ����
        {
            Randvalue = Random.Range(0, 10);

            if (Randvalue == 0)
            {
                Person_obstacles[i].SetActive(true);
                Brick_obstacles[i].SetActive(false);
            }
            else if (Randvalue == 1)
            {
                Brick_obstacles[i].SetActive(true);
                Person_obstacles[i].SetActive(false);
            }
            else
            {
                Brick_obstacles[i].SetActive(false);
                Person_obstacles[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (gameObject.transform.position.x <= -20.0f)
        {
            Destroy(gameObject);
        }
    }

    //public Vector3[] ObstaclesPosition =
    //{
    //    new Vector3(-2.0f,0f),
    //    new Vector3(0f,0f),
    //    new Vector3(2.0f,0f)
    //};    //��ֹ� ���� ��ġ ����

    //public GameObject P_Platform;  //�θ������Ʈ�� �� ����
    //public GameObject Obstacle1;
    //public GameObject Obstacle2;

    //private GameObject ob; //��üȭ�� ��ֹ��� �����ϴ� ����
    //private int randvalue; //������ ���� �����ϴ� ����

    //private void OnEnable()
    //{
    //    for(int i=0;i<ObstaclesPosition.Length;i++)
    //    {
    //        randvalue = Random.Range(0, 2);

    //        if(randvalue==0)
    //        {
    //            ob= Instantiate(Obstacle1,ObstaclesPosition[i],Quaternion.identity);
    //            //ob.transform.parent = P_Platform.transform;
    //            ob.transform.SetParent(P_Platform.transform);
    //        }
    //        else if(randvalue==1)
    //        {
    //            ob = Instantiate(Obstacle2, ObstaclesPosition[i], Quaternion.identity);
    //           // ob.transform.SetParent(P_Platform.transform);
    //        }
    //        else
    //        {
    //            Debug.Log("��ֹ��� �������� �ʽ��ϴ�.");
    //        }
    //    }
    //}
}



