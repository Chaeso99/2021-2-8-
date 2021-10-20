using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] Person_obstacles; // 플랫폼 위 사람 장애물
    public GameObject[] Brick_obstacles; // 플랫폼 위 벽돌 장애물

    private int Randvalue; // 랜덤 값을 저장할 변수

    private void OnEnable()  // 오브젝트가 활성화 될 때마다 실행
    {
        for (int i = 0; i < Person_obstacles.Length && i < Brick_obstacles.Length; i++) // i가 두 오브젝트 배열의 길이보다 작으면 for문 실행
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
    //};    //장애물 스폰 위치 저장

    //public GameObject P_Platform;  //부모오브젝트가 될 발판
    //public GameObject Obstacle1;
    //public GameObject Obstacle2;

    //private GameObject ob; //객체화된 장애물을 저장하는 변수
    //private int randvalue; //무작위 값을 저장하는 변수

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
    //            Debug.Log("장애물이 생성되지 않습니다.");
    //        }
    //    }
    //}
}



