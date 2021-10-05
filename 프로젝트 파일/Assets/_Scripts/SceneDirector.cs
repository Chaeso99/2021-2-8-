using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public void TitleSceneChange()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void GameSceneChange()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void HowtoPlayChange()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }

    public void GameOverChange()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void RestartChange()
    {
        SceneManager.LoadScene("GameScene");
    }
}
