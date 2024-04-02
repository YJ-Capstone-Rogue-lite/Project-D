using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ������ �����ϴ� ����ü
[System.Serializable]
public struct DamageData
{
    public Transform ownerTransform; // �������� ���� ��ü�� Transform ����
    public LayerMask ignoreLayer; // �������� ���� �� ������ ���̾� ����
    public float damage; // ���� ������ ��
    public EffectData effectData; // �������� ������ ȿ�� ������
}
