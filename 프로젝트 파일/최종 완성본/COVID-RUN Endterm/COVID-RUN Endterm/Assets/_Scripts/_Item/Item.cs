using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        // 코인, 아이템
        if (other.CompareTag("Player"))
        {
            // 코인
            if (gameObject.tag == "Coin")
            {
                SoundManager.instance.PlayCoinSound();
                Debug.Log("Coin Sound Play!");
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

            // 경찰봉
            else if (gameObject.tag == "Truncheon")
            {
                SoundManager.instance.PlayItemSound();

                GameObject[] PersonObstacles = GameObject.FindGameObjectsWithTag("Person");
                for (int i = 0; i < PersonObstacles.Length; i++)
                {
                    Destroy(PersonObstacles[i]);
                }

                Destroy(gameObject);
            }

            // 마스크
            else if (gameObject.tag == "Mask")
            {
                SoundManager.instance.PlayItemSound();
                // 체력 회복
                // HpBar.Hp += 0.1f;
                Destroy(gameObject);
            }

            // 주사기
            else if (gameObject.tag == "Syringe")
            {
                SoundManager.instance.PlayItemSound();
                // 3초 무적

                Destroy(gameObject);
            }
        }
    }
}
