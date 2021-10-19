using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    private float ScrollSpeed = -10f;

    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            transform.Translate(Time.deltaTime * ScrollSpeed, 0, 0);
        }
    }

    public void SetScrollSpeed(float speed)
    {
        ScrollSpeed = speed;
    }
}

