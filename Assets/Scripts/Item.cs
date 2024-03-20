using UnityEngine;

// 장비 아이템의 기본 분류 코드
public enum EquipmentType
{
    Weapon,
    Armor,
    Accessory
}

// 장비 아이템 클래스
[System.Serializable]
public class EquipmentItem
{
    public string name; // 이름
    public int grade;   // 등급
    public string description; // 설명
    public string effect;      // 효과
    public EquipmentType type; // 분류

    // 생성자
    public EquipmentItem(string name, int grade, string description, string effect, EquipmentType type)
    {
        this.name = name;
        this.grade = grade;
        this.description = description;
        this.effect = effect;
        this.type = type;
    }
}

// 장비 아이템 매니저 싱글톤 클래스
public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    // 장비 아이템 리스트
    public EquipmentItem[] equipmentItems;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        // 장비 아이템 초기화
        InitializeEquipmentItems();
    }

    // 장비 아이템 초기화 함수
    void InitializeEquipmentItems()
    {
        // 여기서 장비 아이템을 초기화할 수 있습니다.
        // 예를 들어, JSON 파일이나 다른 소스에서 데이터를 읽어와서 초기화할 수 있습니다.
        // 이 예시에서는 간단한 방법으로 몇 개의 장비 아이템을 직접 만들어 초기화합니다.
        equipmentItems = new EquipmentItem[]
        {
            new EquipmentItem("검", 1, "기본 검", "일반적인 공격", EquipmentType.Weapon),
            new EquipmentItem("갑옷", 1, "기본 갑옷", "방어력 증가", EquipmentType.Armor),
            new EquipmentItem("반지", 1, "기본 반지", "체력 회복", EquipmentType.Accessory)
        };
    }
}
