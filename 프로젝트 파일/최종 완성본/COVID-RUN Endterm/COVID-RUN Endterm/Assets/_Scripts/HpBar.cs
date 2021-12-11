using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    PlayerController playerController;

    Image Hp_gauge;
    int maxHp = 1000;
    public static float HP;   // �÷��̾� ĳ������ ���� HP�� �����ϴ� ����

    bool isZero = false;    // HP�� 0���� üũ�ϴ� ����
    float LastReduceHpTime;         // �ֱ� �÷��̾� ĳ������ HP�� ���� �ð��� �����ϴ� ����
    float ReduceIntervalTime = 1.0f; // �÷��̾� ĳ������ HP�� ���̴� �ð� ����

    void Start()
    {
        Hp_gauge = GetComponent<Image>();
        Hp_gauge.fillAmount = maxHp;
        HP = maxHp;

        LastReduceHpTime = Time.time; //�ʱ�ȭ 
    }
    void Update()
    {
        // isZero�� true�� �Ǹ� (HP�� 0�� �Ǹ�) ���� ����
        if (isZero)
        {
            return;
        }

        //HP�� 1000�� �Ѿ�� �ʵ���
        if(HP > 1000.0f)
        {
            HP = 1000.0f;
        }

        Hp_gauge.fillAmount = HP / maxHp;
        
        // 1�ʸ��� �÷��̾� ĳ������ HP�� 10�� ����
        if (ReduceIntervalTime <= Time.time - LastReduceHpTime)  
        {
            HP -= 10.0f;
            LastReduceHpTime = Time.time;
        }
        
        // HP�� 0 ���ϰ� �Ǹ� ���
        if (HP <= 0f)
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
            HP = 0;
        }
    }
}
