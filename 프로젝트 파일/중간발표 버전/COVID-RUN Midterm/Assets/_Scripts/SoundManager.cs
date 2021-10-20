using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource playerAudio; // SoundManager ������Ʈ
    AudioSource deathSound; // DeathSound ������Ʈ (SoundManager�� �ڽ� ������Ʈ)

    public AudioClip jumpClip; // ���� �� ����� �Ҹ�
    public AudioClip hitClip; // ��ĥ �� ����� �Ҹ�

    //public AudioClip coinClip; // ���� ���� �� ����� �Ҹ�
    //public AudioClip itemClip; // ������ ���� �� ����� �Ҹ�
    
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        deathSound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    // �����(BGM) ���
    public void AudioPlay()
    {
        playerAudio.Play();
    }

    // �����(BGM) �Ͻ�����
    public void AudioPause()
    {
        playerAudio.Pause();
    }

    // �����(BGM) ����
    public void AudioStop()
    {
        playerAudio.Stop();
    }

    // ���� ����
    public void PlayJumpSound()
    {
        playerAudio.PlayOneShot(jumpClip);

    }

    // ��ֹ� �浹 ����
    public void PlayHitSound()
    {
        playerAudio.PlayOneShot(hitClip);
    }

    // ��� ����
    public void PlayDeathSound()
    {
        playerAudio.Stop();

        deathSound.Play();
    }
}