using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    PlayerController playerController;

    Image Hp_gauge;
    int maxHp = 1;
    public static float Hp;   // �÷��̾� ĳ������ ���� HP�� �����ϴ� ����

    bool isZero = false;    // HP�� 0���� üũ�ϴ� ����
    float LastReduceHpTime;         // �ֱ� �÷��̾� ĳ������ HP�� ���� �ð��� �����ϴ� ����
    float ReduceIntervalTime = 1.0f; // �÷��̾� ĳ������ HP�� ���̴� �ð� ����

    void Start()
    {
        Hp_gauge = GetComponent<Image>();
        Hp_gauge.fillAmount = maxHp;
        Hp = maxHp;

        LastReduceHpTime = Time.time; //�ʱ�ȭ 
    }
    void Update()
    {
        // isZero�� true�� �Ǹ� (HP�� 0�� �Ǹ�) ���� ����
        if (isZero)
        {
            return;
        }

        Hp_gauge.fillAmount = Hp / maxHp;

        // 1�ʸ��� �÷��̾� ĳ������ HP�� 0.02�� ����
        if (ReduceIntervalTime <= Time.time - LastReduceHpTime)  
        {
            Hp -= 0.02f;
            LastReduceHpTime = Time.time;
        }

        // HP�� 0 ���ϰ� �Ǹ� ���
        if (Hp <= 0f)
        {
            isZero = true;
            // �÷��̾��� ��ũ��Ʈ�� �ҷ���
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();

            playerController.Die();
        }
    }

    public static void DropHP()
    {
        // �����ϸ� HP�� 0�� ��
        if (GameManager.instance.isGameOver)
        {
            Hp = 0;
        }
    }
}
