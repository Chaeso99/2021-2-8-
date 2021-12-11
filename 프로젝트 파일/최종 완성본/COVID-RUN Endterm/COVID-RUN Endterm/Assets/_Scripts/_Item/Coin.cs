using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 코인
            if (gameObject.tag == "Coin")
            {
                SoundManager.instance.PlayCoinSound();
                GameManager.instance.score += 100;
                Destroy(gameObject);
                //other.gameObject.SetActive(false);
            }

            // 달러
            else if (gameObject.tag == "Dollar1")
            {
                SoundManager.instance.PlayCoinSound();
                GameManager.instance.score += 300;
                Destroy(gameObject);
            }

            // 달러 뭉치
            else if (gameObject.tag == "Dollar2")
            {
                SoundManager.instance.PlayCoinSound();
                GameManager.instance.score += 500;
                Destroy(gameObject);
            }
        }
    }
}
