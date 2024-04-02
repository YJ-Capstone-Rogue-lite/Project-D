using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // ����Ʈ ��Ʈ�ѷ�
    [SerializeField]
    private EffectController effectController;

    // ĳ���� ���� ������
    [SerializeField]
    protected CharStateData charStateData;


    // ü��
    protected float m_health;

    // ��ȣ��
    protected float m_sheild;

    // �̵� �ӵ�
    protected float m_movementSpeed;

    // ��ȣ �ð�
    protected float m_protectionTime;

    // ���� �Լ�
    protected virtual void Start()
    {
        // ����Ʈ ��Ʈ�ѷ� �ʱ�ȭ
        effectController = new EffectController();

        // ĳ���� ���� ������ �ʱ�ȭ
        // charStateData = new CharStateData();

        // ü�� �ʱ�ȭ
        m_health = charStateData.health;

        // ��ȣ�� �ʱ�ȭ
        m_health = charStateData.shield;

        // �̵� �ӵ� �ʱ�ȭ
        m_movementSpeed = 0;

        // ��ȣ �ð� �ʱ�ȭ
        m_protectionTime = 0;
    }

    // �������� �Ծ��� �� ȣ��Ǵ� �Լ�
    protected virtual void Damaged(DamageData damageData)
    {
        // ��ȣ�� ����
        m_sheild -= damageData.damage;

        // ü�� ����
        m_health -= m_sheild < 0 ? -m_sheild : 0;

        Debug.Log("������ " + damageData.damage + "����");
    }

    // Ʈ���� �浹�� �߻����� �� ȣ��Ǵ� �Լ�
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // �Ѿ�, ��(�� �浹���� ��
        if (collision.CompareTag("Bullet") || (collision.CompareTag("Enemy")))
        {
            // ������ ������
            Damaged(collision.GetComponent<DamageData>());
            return;

        }

        // �����۰� �浹���� ��
        if (collision.CompareTag("Item"))
            Debug.Log("�����۰� �浹");
        {
            var item = collision.GetComponent<Item>();

            // ���� ������ �������� ���
            if (item is IEquipableItem<Effect>)
            {
                // ����Ʈ ȹ�� �� ó��
                effectController.Operation(((IEquipableItem<Effect>)item).Acquir());
            }
            else
            {
                // ������ ȹ��
                ((IAcquisableItem)item).Acquir();
            }
            return;
        }
    }
}
