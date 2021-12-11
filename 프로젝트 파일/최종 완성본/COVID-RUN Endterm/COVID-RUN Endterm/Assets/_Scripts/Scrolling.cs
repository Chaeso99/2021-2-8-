using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    static float ScrollSpeed = -8f;

    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            transform.Translate(Time.deltaTime * ScrollSpeed, 0, 0);
        }
        else
        {
            ScrollSpeed = -8f; // 게임 오버가 되면 스크롤 속도 초기화
        }

        if (GameManager.instance.stage == 2)
        {
            ScrollSpeed = -8.5f;
        }
        else if (GameManager.instance.stage == 3)
        {
            ScrollSpeed = -9f;
        }

        if (gameObject.transform.position.x <= -50.0f)
        {
            Destroy(gameObject);
        }
    }

    static public void SetScrollSpeed(float speed)
    {
        ScrollSpeed -= speed;
        Debug.Log(ScrollSpeed);
    }
}