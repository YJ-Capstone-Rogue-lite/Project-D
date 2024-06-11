using TMPro;
using UnityEngine;

public class Item_interaction : MonoBehaviour
{ //아이템 상호작용코드

    [SerializeField] private bool pickupActivated = false;  // 아이템 습득 가능할시 True 
    public Item_PickUp item_PickUp;
    [SerializeField] private TMP_Text actionText;  // 행동을 보여 줄 텍스트
    //[SerializeField] private Weapon_Slot Weapon_Slot;
    public GameObject player_postion;
    private Weapon_Slot weaponSlotScript; //웨폰 슬롯의 무기슬롯을 받아오기 위함
    [SerializeField] private IngameUI ingameUI;
    [SerializeField] private Character character;
    [SerializeField] private GameObject pickedUpConsumablePrefab; // 픽업한 소비 아이템의 프리팹을 저장할 변수
    public ConsumableItem currentConsumable; // 현재 슬롯에 있는 소비 아이템
    public GameObject currentBox; // 현재 상자 객체를 저장할 변수
    public Item_drop item_Drop;

    private void Start()
    {
        // actionText를 찾아서 초기화합니다. 예를 들어, 해당 텍스트는 같은 게임 오브젝트 내에 있을 수 있습니다.
        actionText = GameObject.Find("actionText").GetComponent<TMP_Text>();
        // actionText를 비활성화합니다.
        actionText.gameObject.SetActive(false);

    }

    private void Awake()
    {
        weaponSlotScript = FindObjectOfType<Weapon_Slot>(); //웨폰슬롯스크립트는 웨폰슬롯 코드의 값을 가져옴
        ingameUI = FindObjectOfType<IngameUI>();
        character = FindAnyObjectByType<Character>();
        item_Drop = FindAnyObjectByType<Item_drop>();
    }

    private void Update()
    {
        if (pickupActivated && item_PickUp != null) // pickupActivated와 item_PickUp이 초기화된 경우에만 CanPickUp 호출
        {
            CanPickUp();
        }
        else
        {
            Open_box();
        }
        active_Potion();
        active_Shield();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Item"))
        {
            pickupActivated = true;
            item_PickUp = collider2D.gameObject.GetComponent<Item_PickUp>();
            actionText.gameObject.SetActive(true);
            if (item_PickUp.weapon != null) // 아이템 픽업 코드에 무기가 존재한다면
            {
                actionText.text = item_PickUp.weapon.name + "<b>" + " 획득 " + "<color=yellow>" + "[E]" + "</b>" + "</color>";
            }
            else if (item_PickUp.consumable != null)
            {
                actionText.text = item_PickUp.consumable.name + "<b>" + " 획득 " + "<color=yellow>" + "[E]" + "</b>" + "</color>";
            }
        }
        else if (collider2D.gameObject.CompareTag("Box"))
        {
            pickupActivated = true;
            currentBox = collider2D.gameObject; // 현재 상자 객체 저장
            actionText.gameObject.SetActive(true);
            Animator boxAnimator = currentBox.GetComponent<Animator>(); // 현재 상자 객체의 애니메이터 가져오기
            if (!boxAnimator.GetBool("State"))
            {
                actionText.text = "<b>" + " 상자 열기 " + "<color=yellow>" + "[E]" + "</b>" + "</color>";

            }
            else             // 상자가 닫혀 있는 경우에만 실행

            {
                actionText.text = "<b>" + " 이미 열린 상자입니다 " + "<color=yellow>" + "</b>" + "</color>";

            }


        }
    }


    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Item") || (collider2D.gameObject.CompareTag("Box")))
        {
            pickupActivated = false;
            item_PickUp = null;
            currentBox = null; // 상자 객체 초기화
            actionText.gameObject.SetActive(false);
        }

    }

    private void CanPickUp()
    {
        if (Input.GetKeyDown(KeyCode.E) && pickupActivated) // E를 누르고 픽업이 활성화될 때
        {
            if (item_PickUp != null) // 아이템 픽업이 존재할 경우
            {
                if (item_PickUp.weapon != null) // 무기 아이템일 경우
                {
                    PickUp_Weapon_Change();
                    weaponSlotScript.ReceiveWeapon(item_PickUp.weapon);
                    Debug.Log(item_PickUp.weapon.name + " 획득 했습니다.");
                    Destroy(item_PickUp.gameObject);
                }
                else if (item_PickUp.consumable != null) // 소비 아이템일 경우
                {
                    PickUp_Item_Change();
                    pickedUpConsumablePrefab = item_PickUp.consumable.ConsumItemPrefab;
                    currentConsumable = item_PickUp.consumable;

                    if (ingameUI != null && ingameUI.ConsumableItem_Img != null)
                    {
                        ingameUI.ConsumableItem_Img.sprite = item_PickUp.consumable.sprite;
                    }
                    Debug.Log(item_PickUp.consumable.name + " 획득 했습니다.");
                    Destroy(item_PickUp.gameObject);
                }
            }
            
        }
    }

    private void Open_box()
    {
        if (Input.GetKeyDown(KeyCode.E) && pickupActivated)// E를 누르고 픽업이 활성화될 때
        {
            if (currentBox != null) // 상자일 경우
            {
                Debug.Log("상자를 확인했고 E키를 누름");
                tresure_box_open();
            }
        }
           
    }

    public void PickUp_Weapon_Change() //아이템 픽업시 프리펩 재생성 코드
    {
        if (weaponSlotScript != null && weaponSlotScript.activeWeaponSlot != null)
        {
            Fire_Test activeWeaponFireTest = weaponSlotScript.activeWeaponSlot.GetComponent<Fire_Test>();

            if (activeWeaponFireTest != null && activeWeaponFireTest.weapon != null)
            {
                if (weaponSlotScript.weaponSlot2.GetComponent<Fire_Test>().weapon.weaponType != Weapon.WeaponType.None)
                {
                    GameObject itemPrefab = activeWeaponFireTest.weapon.weapnPrefab;

                    if (itemPrefab != null)
                    {
                        Vector3 spawnPosition = player_postion.transform.position;
                        spawnPosition.z = 0f;

                        GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
                        SpriteRenderer itemRenderer = newItem.GetComponent<SpriteRenderer>();
                        if (itemRenderer != null)
                        {
                            itemRenderer.sortingLayerName = "Item";
                        }
                    }
                    else
                    {
                        Debug.LogError("현재 무기의 프리팹이 없습니다.");
                    }
                }
                else
                {
                    Debug.Log("무기 슬롯 2의 무기가 None입니다. 새로운 아이템을 생성하지 않습니다.");
                }
            }
            else
            {
                Debug.LogError("활성화된 무기의 Fire_Test 컴포넌트 또는 무기 정보를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.Log("현재 무기 슬롯에 아이템이 없습니다.");
        }
    }

    public void PickUp_Item_Change()
    {
        if (item_PickUp.consumable != null && ingameUI.ConsumableItem_Img.sprite != null && ingameUI.ConsumableItem_Img.sprite != ingameUI.default_consumableItem.sprite)
        {
            GameObject ConsumItem_Prefab = pickedUpConsumablePrefab;

            if (ConsumItem_Prefab != null)
            {
                Vector3 spawnPosition = player_postion.transform.position;
                spawnPosition.z = 0f;

                GameObject new_Consum_Item = Instantiate(ConsumItem_Prefab, spawnPosition, Quaternion.identity);
                SpriteRenderer itemRenderer = new_Consum_Item.GetComponent<SpriteRenderer>();
                if (itemRenderer != null)
                {
                    itemRenderer.sortingLayerName = "Item";
                }
            }
            else
            {
                Debug.LogWarning("프리팹이 존재하지 않습니다.");
            }
        }
        else
        {
            Debug.LogWarning("소비아이템이나 소비 슬롯 이미지가 존재하지 않습니다.");
        }
    }

    public void active_Potion()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) &&
            ingameUI.ConsumableItem_Img.sprite != null &&
            ingameUI.ConsumableItem_Img.sprite != ingameUI.default_consumableItem.sprite &&
            character.m_health < character.m_maxHealth &&
            currentConsumable != null &&
            currentConsumable.itemType == ConsumableItem.ItemType.Potion)
        {
            character.m_health += currentConsumable.HPHealing;
            if (character.m_health > character.m_maxHealth)
            {
                character.m_health = character.m_maxHealth;
            }
            character.player_hpbar_update();
            ingameUI.ConsumableItem_Img.sprite = ingameUI.default_consumableItem.sprite;
            currentConsumable = null; // 소비 후 현재 소비 아이템을 null로 설정
            Debug.Log("포션 사용");
        }
    }

    public void active_Shield()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) &&
            ingameUI.ConsumableItem_Img.sprite != null &&
            ingameUI.ConsumableItem_Img.sprite != ingameUI.default_consumableItem.sprite &&
            character.m_shield < character.m_maxShield &&
            currentConsumable != null &&
            currentConsumable.itemType == ConsumableItem.ItemType.Shield)
        {
            character.m_shield += currentConsumable.AddShield;
            if (character.m_shield > character.m_maxShield)
            {
                character.m_shield = character.m_maxShield;
            }
            character.player_shieldbar_update();
            ingameUI.ConsumableItem_Img.sprite = ingameUI.default_consumableItem.sprite;
            currentConsumable = null; // 소비 후 현재 소비 아이템을 null로 설정
            Debug.Log("쉴드 사용");
        }
    }

    public void tresure_box_open()
    {
        Animator boxAnimator = currentBox.GetComponent<Animator>(); // 현재 상자 객체의 애니메이터 가져오기
        if (boxAnimator != null)
        {
            if (!boxAnimator.GetBool("State")) // 상자가 닫혀 있는 경우에만 실행
            {
                boxAnimator.SetBool("State", true); // 애니메이션 실행
                Debug.Log("상자를 열었습니다.");
                item_Drop.Box_Open_Item_Drop();
            }
            else
            {
                Debug.Log("상자가 이미 열려 있습니다.");
            }
        }
        else
        {
            Debug.LogWarning("상자에 애니메이터가 없습니다.");
        }
    }

}
