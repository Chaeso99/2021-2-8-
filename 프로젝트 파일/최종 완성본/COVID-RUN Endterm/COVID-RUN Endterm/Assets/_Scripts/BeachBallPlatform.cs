using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBallPlatform : MonoBehaviour
{
    public GameObject[] BeachBall_obstacles; // ��ġ�� ��ֹ�

    private int Randvalue; // ���� ���� ������ ����

    private void OnEnable()  // ������Ʈ�� Ȱ��ȭ �� ������ ����
    {
        for (int i = 0; i < BeachBall_obstacles.Length; i++) // i�� ������Ʈ �迭�� ���̺��� ������ for�� ����
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
