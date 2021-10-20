using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource playerAudio; // SoundManager 오브젝트
    AudioSource deathSound; // DeathSound 오브젝트 (SoundManager의 자식 오브젝트)

    public AudioClip jumpClip; // 점프 시 재생할 소리
    public AudioClip hitClip; // 다칠 시 재생할 소리

    //public AudioClip coinClip; // 코인 먹을 때 재생할 소리
    //public AudioClip itemClip; // 아이템 먹을 때 재생할 소리
    
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        deathSound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    // 오디오(BGM) 재생
    public void AudioPlay()
    {
        playerAudio.Play();
    }

    // 오디오(BGM) 일시정지
    public void AudioPause()
    {
        playerAudio.Pause();
    }

    // 오디오(BGM) 정지
    public void AudioStop()
    {
        playerAudio.Stop();
    }

    // 점프 사운드
    public void PlayJumpSound()
    {
        playerAudio.PlayOneShot(jumpClip);

    }

    // 장애물 충돌 사운드
    public void PlayHitSound()
    {
        playerAudio.PlayOneShot(hitClip);
    }

    // 사망 사운드
    public void PlayDeathSound()
    {
        playerAudio.Stop();

        deathSound.Play();
    }
}