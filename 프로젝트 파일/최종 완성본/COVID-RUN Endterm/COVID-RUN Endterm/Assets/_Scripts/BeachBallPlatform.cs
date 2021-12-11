using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBallPlatform : MonoBehaviour
{
    public GameObject[] BeachBall_obstacles; // 비치볼 장애물

    private int Randvalue; // 랜덤 값을 저장할 변수

    private void OnEnable()  // 오브젝트가 활성화 될 때마다 실행
    {
        for (int i = 0; i < BeachBall_obstacles.Length; i++) // i가 오브젝트 배열의 길이보다 작으면 for문 실행
        {
            Randvalue = Random.Range(0, 10);

            if (Randvalue == 0)
            {
                BeachBall_obstacles[i].SetActive(false);
            }
            else if (Randvalue == 1)
            {
                BeachBall_obstacles[i].SetActive(true);
            }
            else
            {
                BeachBall_obstacles[i].SetActive(false);
            }
        }
    }
}
