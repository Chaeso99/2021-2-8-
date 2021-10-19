using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] PlatformObstacles; // 플랫폼 위 장애물 저장

    private void OnEnable()  // 오브젝트가 활성화 될 때마다 실행
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
