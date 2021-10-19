using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] PlatformObstacles; // �÷��� �� ��ֹ� ����

    private void OnEnable()  // ������Ʈ�� Ȱ��ȭ �� ������ ����
    {
        for (int i = 0; i < PlatformObstacles.Length; i++)
        {
            if ((Random.Range(0, 6) == 0))
            {
                PlatformObstacles[i].SetActive(true);
            }
            else
            {
                PlatformObstacles[i].SetActive(false);
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
}
