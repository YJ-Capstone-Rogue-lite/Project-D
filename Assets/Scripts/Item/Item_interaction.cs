using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Item_interaction : MonoBehaviour
{ 
    //아이템 상호작용코드

    [SerializeField] private bool pickupActivated = false;  // 아이템 습득 가능할시 True 
    public Item_PickUp item_PickUp;
    [SerializeField] private TMP_Text actionText;  // 행동을 보여 줄 텍스트 //인게임 UI코드에서 액션 텍스트 활성화
    //[SerializeField] private Weapon_Slot Weapon_Slot;
    public GameObject player_postion;
    private Weapon_Slot weaponSlotScript; //웨폰 슬롯의 무기슬롯을 받아오기 위함
    [SerializeField] private IngameUI ingameUI;
    [SerializeField] private Character character;
    [SerializeField] private GameObject pickedUpConsumablePrefab; // 픽업한 소비 아이템의 프리팹을 저장할 변수
    [SerializeField] private Inventory_Slot Inventory_Slot;
    public ConsumableItem currentConsumable1; // 현재 1 슬롯에 있는 소비 아이템
    public ConsumableItem currentConsumable2; // 현재 2 슬롯에 있는 소비 아이템

    public GameObject currentBox; // 현재 상자 객체를 저장할 변수
    public Item_drop item_Drop;
    public Buff buff; //현재 획득할려고 하는 버프
    public Coin coin;
    private Rigidbody2D rg;
    [SerializeField] private bool canUseItem = false;  // 아이템 사용 가능 여부
    [SerializeField] private Coroutine itemUseCoroutine;  // 코루틴을 추적하기 위한 변수

    public UnityEvent onPeekup;





    private void Awake()
    {
        // Start 메서드에서 actionText를 초기화합니다.
        StartCoroutine(FindActionText());
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 시 이벤트 핸들러 등록

        weaponSlotScript = FindObjectOfType<Weapon_Slot>(); //웨폰슬롯스크립트는 웨폰슬롯 코드의 값을 가져옴
        ingameUI = FindObjectOfType<IngameUI>();
        character = FindAnyObjectByType<Character>();
        // item_Drop = FindAnyObjectByType<Item_drop>();
        rg = transform.parent.GetComponent<Rigidbody2D>();


    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(ActivateActionTextAfterSceneLoad());
    }

    private IEnumerator ActivateActionTextAfterSceneLoad()
    {
        yield return null; // 한 프레임 대기

        if (actionText != null)
        {
            actionText.gameObject.SetActive(true); // 씬이 로드된 후 actionText 활성화
            actionText.text = "";
            Debug.Log("actionText를 찾았고 내용을 비움.");
        }
        else
        {
            Debug.LogWarning("actionText를 찾을 수 없어 씬 전환 후 활성화에 실패했습니다.");
        }
    }

    private IEnumerator FindActionText()
    {
        Debug.Log("액션 텍스트 찾는중");
        while (true)
        {
            // actionText가 null이면 찾으려 시도
            if (actionText == null)
            {
                GameObject textObject = GameObject.Find("actionText");
                if (textObject != null)
                {
                    actionText = textObject.GetComponent<TMP_Text>(); // 적절한 컴포넌트 타입을 사용해야 함
                    if (actionText != null)
                    {
                        actionText.text = "";
                        Debug.Log("actionText를 찾았고 내용을 비움.");
                    }
                    else
                    {
                        Debug.LogWarning("actionText 컴포넌트를 찾을 수 없습니다.");
                    }
                }
                else
                {
                    Debug.LogWarning("actionText를 찾을 수 없습니다. GameObject가 씬에 있는지 확인하세요.");
                }
            }

            // actionText가 null이 아니면 코루틴 종료
            if (actionText != null)
            {
                yield break;
            }

            yield return new WaitForSeconds(0.1f); // 일정 시간 대기 후 다시 시도
        }
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

        // active_Consum 호출 전에 canUseItem이 true인지 확인
        if (canUseItem)
        {
            active_Consum();  // 아이템을 사용할 수 있을 때만 호출
        }

        // actionText가 씬 전환 등으로 파괴된 경우 다시 찾기
        if (actionText == null)
        {
            StartCoroutine(FindActionText());
        }
    }

   // private void LateUpdate() => pickupActivated = false;

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (!pickupActivated && collider2D.gameObject.CompareTag("Box") && !collider2D.gameObject.GetComponent<Animator>().GetBool("State"))
        {
            pickupActivated = true;
            actionText.text = "<b>" + " 상자 열기 " + "<color=yellow>" + "[F]" + "</b>" + "</color>";
            actionText.gameObject.SetActive(true);
            currentBox = collider2D.gameObject; // 현재 상자 객체 저장
            item_Drop = currentBox.GetComponent<Item_drop>();
        }
        else if (!pickupActivated && collider2D.gameObject.CompareTag("Item"))
        {
            pickupActivated = true;
            item_PickUp = collider2D.gameObject.GetComponent<Item_PickUp>();
            actionText.gameObject.SetActive(true);
            if (item_PickUp.weapon != null) // 아이템 픽업 코드에 무기가 존재한다면
            {
                actionText.text = item_PickUp.weapon.name + "<b>" + " 획득 " + "<color=yellow>" + "[F]" + "</b>" + "</color>";
            }
            else if(item_PickUp.consumable != null)
            {
                actionText.text = item_PickUp.consumable.name + "<b>" + " 획득 " + "<color=yellow>" + "[F]" + "</b>" + "</color>";
            }
            else if(item_PickUp.buff != null)
            {
                actionText.text = item_PickUp.buff.name + "<b>" + " 획득 " + "<color=yellow>" + "[F]" + "</b>" + "</color>";
            }
            else if(item_PickUp.coin != null)
            {
                actionText.text = item_PickUp.coin.name + "<b>" + " 획득 " + "<color=yellow>" + "[F]" + "</b>" + "</color>";
            }
        }
        else if(!pickupActivated && collider2D.gameObject.CompareTag("Box") && collider2D.gameObject.GetComponent<Animator>().GetBool("State"))
        {
            // pickupActivated = true;
            // actionText.text = "<b>" + " 상자 열기 " + "<color=yellow>" + "[E]" + "</b>" + "</color>";
            actionText.gameObject.SetActive(true);
            actionText.text = "<b>" + " 이미 열린 상자입니다 " + "<color=yellow>" + "</b>" + "</color>";
        }
     

    }


    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Item") || collider2D.gameObject.CompareTag("Box"))
        {
            pickupActivated = false;
            item_PickUp = null;
            currentBox = null; // 상자 객체 초기화
            buff = null;
            coin = null;
            actionText.gameObject.SetActive(false);
        }

    }

    private void CanPickUp()
    {
        // F 키를 누를 때 픽업을 활성화
        if (Input.GetKeyDown(KeyCode.F))
        {
            onPeekup?.Invoke();
            pickupActivated = false;
            rg.WakeUp();

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
                    // 픽업한 아이템 프리팹 정보 저장
                    pickedUpConsumablePrefab = item_PickUp.consumable.ConsumItemPrefab;

                    // 소비 슬롯 선택 UI를 활성화
                    ingameUI.slot1_select_test_img.gameObject.SetActive(true);
                    ingameUI.slot2_select_test_img.gameObject.SetActive(true);
                    Debug.Log("소비 슬롯 선택창 활성화");
                }
                else if (item_PickUp.buff != null) // 버프 아이템일 경우
                {
                    ApplyBuff();
                    Debug.Log(item_PickUp.buff.Buff_name + " 버프 적용됨.");
                    Destroy(item_PickUp.gameObject);
                }
                else if (item_PickUp.coin != null) // 코인 아이템일 경우
                {
                    Coin_Count();
                    Debug.Log(item_PickUp.coin.Coin_name + " 코인 획득함.");
                    ingameUI.Coin_Count_Text_Update();
                    Destroy(item_PickUp.gameObject);
                }
            }
        }

        // 소비 슬롯 선택 UI가 활성화된 상태에서 3번 키를 누르면
        if (ingameUI.slot1_select_test_img.gameObject.activeSelf && ingameUI.slot2_select_test_img.gameObject.activeSelf)
        {
            actionText.text = "아이템을 획득하시려면 [3]번 또는 [4]번을 눌러주십시오";

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("3번 누름");
                // 3번 슬롯에 소비아이템 저장
                StoreItemInSlot1();

                // 아이템을 슬롯에 넣은 후 코루틴 시작
                StartItemUseCoroutine();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("4번 누름");
                // 4번 슬롯에 소비아이템 저장
                StoreItemInSlot2();

                // 아이템을 슬롯에 넣은 후 코루틴 시작
                StartItemUseCoroutine();
            }
        }
    }
    private void StartItemUseCoroutine()
    {
        // 이미 코루틴이 실행 중이라면 중단
        if (itemUseCoroutine != null)
        {
            StopCoroutine(itemUseCoroutine);
        }

        // 코루틴 실행
        itemUseCoroutine = StartCoroutine(EnableItemUseAfterDelay(0.5f));  // 0.5초 후 아이템 사용 가능
    }
    private IEnumerator EnableItemUseAfterDelay(float delay)
    {
        canUseItem = false;  // 아이템 사용 불가능 상태로 시작
        yield return new WaitForSeconds(delay);  // 지정된 시간만큼 기다림
        canUseItem = true;  // 아이템 사용 가능
        Debug.Log("아이템 사용 가능");
    }

    private void StoreItemInSlot1()
    {
        // 슬롯 1에 아이템 저장
        if (currentConsumable1 != null)
        {
            PickUp_Item_Change_Slot_1();
            Debug.Log("픽업 아이템 재생성함");
        }
        else
        {
            currentConsumable1 = item_PickUp.consumable;
            ingameUI.ConsumableItem_Img.sprite = currentConsumable1.sprite;
            Debug.Log("소비 슬롯 1 아이템 저장");
        }

        // 슬롯 1 선택 UI 비활성화
        ingameUI.slot1_select_test_img.gameObject.SetActive(false);
        ingameUI.slot2_select_test_img.gameObject.SetActive(false);
        Debug.Log("소비 슬롯 선택창 비활성화");

        actionText.text = "";
        Destroy(item_PickUp.gameObject);
        Debug.Log("아이템 삭제");
    }

    private void StoreItemInSlot2()
    {
        // 슬롯 2에 아이템 저장
        if (currentConsumable2 != null)
        {
            PickUp_Item_Change_Slot_2();
            Debug.Log("픽업 아이템 재생성함");
        }
        else
        {
            currentConsumable2 = item_PickUp.consumable;
            ingameUI.ConsumableItem_Img_2.sprite = currentConsumable2.sprite;
            Debug.Log("소비 슬롯 2 아이템 저장");
        }

        // 슬롯 2 선택 UI 비활성화
        ingameUI.slot1_select_test_img.gameObject.SetActive(false);
        ingameUI.slot2_select_test_img.gameObject.SetActive(false);
        Debug.Log("소비 슬롯 선택창 비활성화");

        actionText.text = "";
        Destroy(item_PickUp.gameObject);
        Debug.Log("아이템 삭제");
    }

    private void Open_box()
    {
        if (Input.GetKeyDown(KeyCode.F) && pickupActivated)// E를 누르고 픽업이 활성화될 때
        {
            if (currentBox != null) // 상자일 경우
            {
                Debug.Log("상자를 확인했고 F키를 누름");
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

    // 소비 아이템을 바닥에 생성하는 공통 함수
    private void DropConsumableItem(GameObject consumablePrefab, Vector3 playerPosition)
    {
        if (consumablePrefab != null)
        {
            Debug.Log("바닥에 아이템을 생성합니다.");

            // 아이템을 생성할 위치를 설정
            Vector3 spawnPosition = playerPosition;
            spawnPosition.z = 0f; // z축은 0으로 설정하여 2D 게임에서 위치가 겹치지 않도록 설정

            // 소비 아이템을 해당 위치에 인스턴스화(생성)
            GameObject newConsumableItem = Instantiate(consumablePrefab, spawnPosition, Quaternion.identity);

            // 생성된 아이템의 SpriteRenderer를 설정
            SpriteRenderer itemRenderer = newConsumableItem.GetComponent<SpriteRenderer>();
            if (itemRenderer != null)
            {
                // 아이템의 sortingLayer를 "Item"으로 설정하여, 다른 게임 오브젝트와 겹치지 않도록 순서 설정
                itemRenderer.sortingLayerName = "Item";
                Debug.Log("아이템의 sortingLayer를 'Item'으로 설정했습니다.");
            }
            else
            {
                Debug.LogWarning("생성된 아이템에 SpriteRenderer가 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("DropConsumableItem: 프리팹이 존재하지 않습니다.");
        }
    }

    public void PickUp_Item_Change_Slot_1()
    {
        Debug.Log("소비 아이템 슬롯 1 교체 시작");


        // 아이템을 생성할 위치를 설정
        Vector3 spawnPosition = player_postion.transform.position;
        spawnPosition.z = 0f; // z축은 0으로 설정하여 2D 게임에서 위치가 겹치지 않도록 설정
        // 현재 슬롯 1에 있는 아이템을 바닥에 생성
        DropConsumableItem(currentConsumable1.ConsumItemPrefab, spawnPosition);
        Debug.Log("아이템 바닥에 드랍.");

        currentConsumable1 = item_PickUp.consumable; // 3번 슬롯에 소비아이템 저장하고
        ingameUI.ConsumableItem_Img.sprite = currentConsumable1.sprite; //인게임 ui 스프라이트 바꾸고
        Debug.Log("소비 슬롯 1 아이템 저장");


    }

    public void PickUp_Item_Change_Slot_2()
    {
        Debug.Log("소비 아이템 슬롯 2 교체 시작");

        // 아이템을 생성할 위치를 설정
        Vector3 spawnPosition = player_postion.transform.position;
        spawnPosition.z = 0f; // z축은 0으로 설정하여 2D 게임에서 위치가 겹치지 않도록 설정
        // 현재 슬롯 2에 있는 아이템을 바닥에 생성
        DropConsumableItem(currentConsumable2.ConsumItemPrefab, spawnPosition);
        Debug.Log("아이템 바닥에 드랍.");


        currentConsumable2 = item_PickUp.consumable; // 3번 슬롯에 소비아이템 저장하고
        ingameUI.ConsumableItem_Img_2.sprite = currentConsumable2.sprite; //인게임 ui 스프라이트 바꾸고
        Debug.Log("소비 슬롯 2 아이템 저장");
    }


    public void active_Consum()
    {
        // 슬롯 선택 화면이 꺼져있는 상태에서 3번 또는 4번 키를 눌렀을 시
        if (!ingameUI.slot1_select_test_img.gameObject.activeSelf && // 슬롯 1 선택 화면이 비활성화되어 있는지 확인
            !ingameUI.slot2_select_test_img.gameObject.activeSelf && // 슬롯 2 선택 화면이 비활성화되어 있는지 확인
            (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))) // 3번 또는 4번 키가 눌렸는지 확인
        {
            // 3번 키가 눌린 경우
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                // 슬롯 1 아이템이 포션인 경우 포션 사용
                if (currentConsumable1 != null && currentConsumable1.itemType == ConsumableItem.ItemType.Potion)
                {
                    active_HP_Potion(currentConsumable1, 1);  // 슬롯 1 포션 사용
                }
                // 슬롯 1 아이템이 쉴드인 경우 쉴드 사용
                else if (currentConsumable1 != null && currentConsumable1.itemType == ConsumableItem.ItemType.Shield)
                {
                    active_Shield(currentConsumable1, 1);  // 슬롯 1 쉴드 사용
                }
            }

            // 4번 키가 눌린 경우
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                // 슬롯 2 아이템이 포션인 경우 포션 사용
                if (currentConsumable2 != null && currentConsumable2.itemType == ConsumableItem.ItemType.Potion)
                {
                    active_HP_Potion(currentConsumable2, 2);  // 슬롯 2 포션 사용
                }
                // 슬롯 2 아이템이 쉴드인 경우 쉴드 사용
                else if (currentConsumable2 != null && currentConsumable2.itemType == ConsumableItem.ItemType.Shield)
                {
                    active_Shield(currentConsumable2, 2);  // 슬롯 2 쉴드 사용
                }
            }
        }
    }


    // 포션 사용 코드 (슬롯 1 또는 2를 인자로 전달받음)
    public void active_HP_Potion(ConsumableItem consumable, int slot)
    {
        // 포션 사용 조건 체크
        if (character.m_health < character.m_maxHealth && consumable != null && consumable.itemType == ConsumableItem.ItemType.Potion)
        {
            // 포션 사용
            character.m_health += consumable.HPHealing;

            // 체력이 최대 체력보다 커지지 않도록 설정
            if (character.m_health > character.m_maxHealth)
            {
                character.m_health = character.m_maxHealth;
            }

            // HP 바 업데이트
            character.player_hpbar_update();

            // 슬롯에 따라 포션 소진 처리
            if (slot == 1)
            {
                ingameUI.ConsumableItem_Img.sprite = ingameUI.default_consumableItem.sprite;
                currentConsumable1 = null; // 슬롯 1 포션 사용 후 null로 설정
            }
            else if (slot == 2)
            {
                ingameUI.ConsumableItem_Img_2.sprite = ingameUI.default_consumableItem.sprite;
                currentConsumable2 = null; // 슬롯 2 포션 사용 후 null로 설정
            }

            Debug.Log("포션 사용");
        }
        else
        {
            Debug.LogWarning("포션 사용 조건을 충족하지 못했습니다.");
        }
    }

    // 쉴드 사용 코드 (슬롯 1 또는 2를 인자로 전달받음)
    public void active_Shield(ConsumableItem consumable, int slot)
    {
        // 쉴드 사용 조건 체크
        if (character.m_shield < character.m_maxShield && consumable != null && consumable.itemType == ConsumableItem.ItemType.Shield)
        {
            // 쉴드 사용
            character.m_shield += consumable.AddShield;

            // 쉴드가 최대 쉴드를 초과하지 않도록 설정
            if (character.m_shield > character.m_maxShield)
            {
                character.m_shield = character.m_maxShield;
            }

            // 쉴드 바 업데이트
            character.player_shieldbar_update();

            // 슬롯에 따라 쉴드 소진 처리
            if (slot == 1)
            {
                ingameUI.ConsumableItem_Img.sprite = ingameUI.default_consumableItem.sprite;
                currentConsumable1 = null; // 슬롯 1 쉴드 사용 후 null로 설정
            }
            else if (slot == 2)
            {
                ingameUI.ConsumableItem_Img_2.sprite = ingameUI.default_consumableItem.sprite;
                currentConsumable2 = null; // 슬롯 2 쉴드 사용 후 null로 설정
            }

            Debug.Log("쉴드 사용");
        }
        else
        {
            Debug.LogWarning("쉴드 사용 조건을 충족하지 못했습니다.");
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

                StartCoroutine(DelayedItemDrop());
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


    private IEnumerator DelayedItemDrop()
    {
        // item_Drop = FindAnyObjectByType<Item_drop>();

        yield return new WaitForSeconds(0.35f); // 0.초 대기
        item_Drop.Box_Open_Item_Drop(); // 아이템 드롭 실행
    }
    private void ApplyBuff()
    {
        if (buff == null)
        {
            Debug.LogWarning("버프 객체가 null입니다.");
            return;
        }

        if (buff.buff_Time == Buff.Buff_Time.passive)
        {
            switch (buff.buffType)
            {
                case Buff.BuffType.Attack:
                    Damage_Up_Passive_Buff(); // 데미지 업 버프 적용
                    break;

                case Buff.BuffType.MoveSpeed:
                    Movement_Speed_Up_Passive_Buff(); // 이동 속도 업 버프 적용
                    break;


                case Buff.BuffType.Health_Up:
                    MaxHP_Up_Passive_Buff(); // 최대체력 업 버프 적용
                    break;

                default:
                    Debug.LogWarning("알 수 없는 버프 타입입니다: " + buff.buffType);
                    break;
            }

        }

        else if (buff.buff_Time == Buff.Buff_Time.duration)
        {
            Debug.Log("시간형 버프는 아직 구현되지 않았습니다.");
        }
    }
    //데미지 업 패시브 버프
    public void Damage_Up_Passive_Buff()
    {
        //버프는 최대 5스택까지, 1스택 올라갈때마다 아이콘 오른쪽 하단 숫자 표기 변경
        if (character.damageUpStack < 5) // 최대 5스택까지 증가 가능
        {
            character.damageUpStack++;
            //캐릭터의 버프 데미지 값에 아이템 픽업의 버프 데미지 값 받아와서 더해주기
            character.m_passive_buff_damage += item_PickUp.buff.damage_up;

            Debug.Log("데미지업 패시브 버프 적용");
            UpdateBuffIcon(); // 아이콘에 스택 수 업데이트
        }
    }

    //스피드 업 패시브 버프
    public void Movement_Speed_Up_Passive_Buff()
    {
        //버프는 최대 5스택까지, 1스택 올라갈때마다 아이콘 오른쪽 하단 숫자 표기 변경
        //이동속도 증가 스택이 5보다 작거나 캐릭터의 현재 이동속도가 최대칩다 작거나 같다면
        if (character.movement_SpeedUpStack < 5 || character.m_movementSpeed <= character.m_max_movementSpeed)
        {
            character.movement_SpeedUpStack++;
            //캐릭터의 이동속도 값 더해주기
            character.m_movementSpeed += item_PickUp.buff.movement_Speed_up;

            UpdateBuffIcon(); // 아이콘에 스택 수 업데이트
        }
    }
    //최대체력 업 패시브 버프
    public void MaxHP_Up_Passive_Buff()
    {
        //버프는 최대 5스택까지, 1스택 올라갈때마다 아이콘 오른쪽 하단 숫자 표기 변경
        //
        if (character.max_hp_UPStack < 5)
        {
            character.max_hp_UPStack++;
            //최대체력 값 더해주기
            character.m_maxHealth += item_PickUp.buff.health_up;
            character.player_hpbar_update(); //hp바 업데이트
            UpdateBuffIcon(); // 아이콘에 스택 수 업데이트
        }
    }

    public void Coin_Count()
    {
        //캐릭터 코인 변수 증가시켜주고
        character.Coin_Count++;
        Debug.Log("플레이어 코인 추가" + "현재 코인 갯수 : " + character.Coin_Count);
    }

    


    private void UpdateBuffIcon()
    {
        // 버프 아이콘 오른쪽 하단에 숫자 표기 변경 로직 추가
    }
}
