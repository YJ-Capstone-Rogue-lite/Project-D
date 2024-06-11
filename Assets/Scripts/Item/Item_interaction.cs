using TMPro;
using UnityEngine;

public class Item_interaction : MonoBehaviour
{ //아이템 상호작용코드

    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 
    public Item_PickUp item_PickUp;
    [SerializeField] private TMP_Text actionText;  // 행동을 보여 줄 텍스트
    //[SerializeField] private Weapon_Slot Weapon_Slot;
    public GameObject player_postion;
    private Weapon_Slot weaponSlotScript; //웨폰 슬롯의 무기슬롯을 받아오기 위함
    [SerializeField] private IngameUI ingameUI;
    [SerializeField] private Character character;
    [SerializeField] private GameObject pickedUpConsumablePrefab; // 픽업한 소비 아이템의 프리팹을 저장할 변수
    [SerializeField] private ConsumableItem currentConsumable; // 현재 슬롯에 있는 소비 아이템

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
    }

    private void Update()
    {
        if (pickupActivated && item_PickUp != null) // pickupActivated와 item_PickUp이 초기화된 경우에만 CanPickUp 호출
        {
            CanPickUp();
        }
        active_Potion();
        active_Shield();
    }

    private void OnTriggerEnter2D(Collider2D collider2D) //만약 트리거 범위에 닿은 거의 태그가 아이템이 포함되어 있으면
    {
        if (collider2D.gameObject.CompareTag("Item"))
        {
            pickupActivated = true;
            item_PickUp = collider2D.gameObject.GetComponent<Item_PickUp>();
            // 충돌한 객체가 "Item" 태그를 가지고 있으면 행동 텍스트를 활성화합니다.
            actionText.gameObject.SetActive(true);
            if (item_PickUp.weapon != null) //만약 아이템 픽업의 코드에 웨폰이 존재한다면
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
            actionText.gameObject.SetActive(true);
            actionText.text = "<b>" + " 상자 열기  " + "<color=yellow>" + "[E]" + "</b>" + "</color>";
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Item") || (collider2D.gameObject.CompareTag("Box")))
        {
            pickupActivated = false;
            actionText.gameObject.SetActive(false);
        }
    }

    private void CanPickUp()
    {
        if (Input.GetKeyDown(KeyCode.E) && pickupActivated) // E를 누르고 픽업이 활성화될 때
        {
            if (item_PickUp == null) return;

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
        Animator boxAnimator = gameObject.GetComponent<Animator>();
        if (boxAnimator != null)
        {
            boxAnimator.SetBool("State", true);
            Debug.Log("상자를 열음");
        }
    }
}
