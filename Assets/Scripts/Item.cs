using UnityEngine;

// ��� �������� �⺻ �з� �ڵ�
public enum EquipmentType
{
    Weapon,
    Armor,
    Accessory
}

// ��� ������ Ŭ����
[System.Serializable]
public class EquipmentItem
{
    public string name; // �̸�
    public int grade;   // ���
    public string description; // ����
    public string effect;      // ȿ��
    public EquipmentType type; // �з�

    // ������
    public EquipmentItem(string name, int grade, string description, string effect, EquipmentType type)
    {
        this.name = name;
        this.grade = grade;
        this.description = description;
        this.effect = effect;
        this.type = type;
    }
}

// ��� ������ �Ŵ��� �̱��� Ŭ����
public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    // ��� ������ ����Ʈ
    public EquipmentItem[] equipmentItems;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        // ��� ������ �ʱ�ȭ
        InitializeEquipmentItems();
    }

    // ��� ������ �ʱ�ȭ �Լ�
    void InitializeEquipmentItems()
    {
        // ���⼭ ��� �������� �ʱ�ȭ�� �� �ֽ��ϴ�.
        // ���� ���, JSON �����̳� �ٸ� �ҽ����� �����͸� �о�ͼ� �ʱ�ȭ�� �� �ֽ��ϴ�.
        // �� ���ÿ����� ������ ������� �� ���� ��� �������� ���� ����� �ʱ�ȭ�մϴ�.
        equipmentItems = new EquipmentItem[]
        {
            new EquipmentItem("��", 1, "�⺻ ��", "�Ϲ����� ����", EquipmentType.Weapon),
            new EquipmentItem("����", 1, "�⺻ ����", "���� ����", EquipmentType.Armor),
            new EquipmentItem("����", 1, "�⺻ ����", "ü�� ȸ��", EquipmentType.Accessory)
        };
    }
}
