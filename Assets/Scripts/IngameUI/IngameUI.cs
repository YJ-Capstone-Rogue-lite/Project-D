using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{

    public static IngameUI single;
    public Character character;
    public BuffSystem buffsystem;
    [SerializeField] private Weapon_Slot Weapon_Slot;
    

    [Header("SFX")]
    public AudioClip buttonSound;
    public AudioClip bookSound;

    [Header("무기 슬롯 이미지")]
    public Image main_slot_sprite;
    public GameObject selectMainSlot;
    public Image sub_slot_sprite;
    public GameObject selectSubSlot;
    public Weapon nullgun_image;

    //[Header("무기 슬롯 교체 애니메이션")]
    //[SerializeField]
    //private Animator MainWeapon_Swap;
    //[SerializeField]
    //private Animator SubWeapon_Swap;


    [Header("소비 슬롯 이미지")]
    public Image ConsumableItem_Img; //소비 슬롯 이미지
    public ConsumableItem default_consumableItem; // 소비슬롯이 비어있을때 사용할 이미지
    public Image slot1_select_test_img;
    public Image slot2_select_test_img;
    public Image ConsumableItem_Img_2;

    [Header("코인 카운트")]
    public TMP_Text Coin_Count_Text;

    [Header("현재 활성화된 총의 총알 카운트")]
    public TMP_Text activate_Magazine;

    [Header("기타 불 값들")]
    public bool MainWeapon = false; // true면 MainWeapon false면 SubWeapon
    public bool SubWeapon = true; // true면 MainWeapon false면 SubWeapon
    public bool openOption = true;
    public bool openInventory = true;
    public bool minimap_state = false; //false면 미니맵 off true면 미니맵 on
    public bool test_clear_boolCheck = false; //클리어 체크용 임시 불값


    [Header("재장전 이미지 오브젝트")]
    [SerializeField]  private GameObject reload_img;


    [Header("인벤토리 관련")]
    public GameObject inv_slot;
    public GameObject left_inv;
    public GameObject right_inv;
    public bool inv_slot_active_bool;
    public Inventory_Slot inventory_slot;
    public GameObject Buff_list;


    //[Header("체력바 관련")]
    //public Image HPbar; //hp바 관련 이미지
    [Header("플레이어 스크린샷")]
    public Camera screenshotCamera; // 추가 카메라
    private RenderTexture renderTexture;
    private Texture2D screenShot;

    [Header("플레이어 사망UI")]
    public RawImage deathScreenImage; // UI에서 RawImage를 통해 이미지를 표시할 경우
    public GameObject deathScreenUI; // 플레이어 사망 시 표시할 UI

    [Header("플레이어 클리어UI")]
    public GameObject Clear_Screen_UI; //플레이어가 클리어시 표시할 UI
    public RawImage Clear_Screen_Img; //플레이어 클리어시 이미지 표시
    public Image weapon_slot1_icon; //플레이어가 마지막까지 들고있는 슬롯1 무기
    public Image weapon_slot2_icon; //플레이어가 마지막까지 들고있는 슬롯1 무기
    [SerializeField] private TMP_Text Clear_enemyCountText; // UI Text를 연결할 변수
    [SerializeField] private TMP_Text Clear_TitleText;
    [SerializeField] private TMP_Text Clear_playtimeText;
    [SerializeField] private TMP_Text Clear_destory_enemy_count; // enemy count 저장할 텍스트
    [SerializeField] private TMP_Text atkBuff_needcoin; // 공격력 강화비용
    [SerializeField] private TMP_Text hpBuff_needcoin; // 체력 강화비용
    [SerializeField] private TMP_Text movementBuff_needcoin; // 이동속도 강화비용
    [SerializeField] private TMP_Text atkBuff_stacktext; // 공격력 강화비용
    [SerializeField] private TMP_Text hpBuff_stacktext; // 체력 강화비용
    [SerializeField] private TMP_Text movementBuff_stacktext; // 이동속도 강화비용


    [Header("기타등등")]

    [SerializeField] private RectTransform WeaponSlot1;
    [SerializeField] private RectTransform WeaponSlot2;


    [SerializeField] private GameObject ingameOption;
    [SerializeField] private GameObject option_popup;
    [SerializeField] private GameObject quit_popup;
    [SerializeField] private GameObject minimap_camera;
    //[SerializeField] private GameObject mainWeapon;
    //[SerializeField] private GameObject subWeapon;
    [SerializeField] private GameObject buff_BG;

    [SerializeField] private Image fullScreen_Box;
    [SerializeField] private Image windowScreen_Box;

    [SerializeField] private Sprite checkBox;
    [SerializeField] private Sprite emptyBox;


    [SerializeField] private TMP_Text enemyCountText; // UI Text를 연결할 변수
    [SerializeField] private TMP_Text TitleText;
    [SerializeField] private TMP_Text playtimeText;
    [SerializeField] private TMP_Text destory_enemy_count; // enemy count 저장할 텍스트

    private int enemy_count;

    public GameObject action_text; 
    private Coroutine startCoroutine;
    private int haveCoin;


    private void Start() => startCoroutine = StartCoroutine(WaitStart());

    private IEnumerator WaitStart()
    {
        GameObject player = null;
        yield return new WaitUntil(() => {
            // "Player" 태그를 가진 GameObject를 찾습니다.
            player = GameObject.FindWithTag("Player");
            return player != null;
        });
        if (player != null)
        {
            // 캐릭터 컴포넌트를 가져옵니다.
            character = player.GetComponent<Character>();

            if (character == null)
            {
                Debug.LogWarning("Character component is missing. Adding it now.");

                // 필요한 경우 캐릭터 컴포넌트를 추가합니다.
                character = player.AddComponent<Character>();
            }
        }
        else
        {
            Debug.LogWarning("Player object with tag 'Player' not found.");
        }
        inv_slot_active_bool = false;
        character = player.GetComponent<Character>();
        ConsumableItem_Img.sprite = default_consumableItem.sprite;
        ConsumableItem_Img_2.sprite = default_consumableItem.sprite;
        sub_slot_sprite.sprite = default_consumableItem.sprite;
        // 추가 카메라 설정
        screenshotCamera.enabled = false;
        renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        screenshotCamera.targetTexture = renderTexture;
        action_text.SetActive(true);//인게임 UI코드에서 액션 텍스트 활성화
        Weapon_Slot = PlayerChar.single.GetComponent<Weapon_Slot>();
        //MainWeapon_Swap = mainWeapon.GetComponent<Animator>();
        //SubWeapon_Swap = subWeapon.GetComponent<Animator>();
        Coin_Count_Text_Update();
        StopCoroutine(startCoroutine);
        startCoroutine = null;
    }

    private void Awake()
    {

       
    }

    private void Update()
    {

        // if (ConsumableItem_Img.sprite == null)
        // {
        //     ConsumableItem_Img.sprite = default_consumableItem.sprite;
        // }
        // if(sub_slot_sprite.sprite == null)
        // {
        //     sub_slot_sprite.sprite = nullgun_image.sprite;
        // }
        // Singleton 패턴 설정
        if (single == null)
        {
            single = this;
        }
        //작동안해서 급하게 업데이트에 때려넣은 임시 코드
        if (Weapon_Slot == null)
        {
            Weapon_Slot = PlayerChar.single.GetComponent<Weapon_Slot>();

        }
        if (character == null)
        {
            character = GameObject.FindWithTag("Player").GetComponent<Character>();
        }
        if (startCoroutine != null) return;
        main_slot_sprite.sprite = Weapon_Slot.weaponSlot1.GetComponent<Fire_Test>().weapon.sprite;
        sub_slot_sprite.sprite = Weapon_Slot.weaponSlot2.GetComponent<Fire_Test>().weapon.sprite;
        enemy_count = GameManager.Instance.enemyDestoryCount;

        //플레이어 따라다니는 재장전 애니메이션 이미지
        reload_img.transform.position = Weapon_Slot.transform.position + new Vector3(0, 0, 0);
        MinimapOn_off();
        enemy_count_update();

        if (Input.GetKeyDown(KeyCode.Tab)) //탭키를 눌렀을때
        {
            inventory_open(); //인벤토리 오픈
        }
        if (Input.GetButtonDown("Option"))
        {
            option_open();
        }

        if (Input.GetButtonDown("MainWeapon") && MainWeapon == true)
        {
            //MainWeapon_Swap.SetBool("MainWeapon_Up", true);
            //SubWeapon_Swap.SetBool("SubWeapon_Down", true);
            //MainWeapon_Swap.SetBool("MainWeapon_Down", false);
            //SubWeapon_Swap.SetBool("SubWeapon_Up", false);
            selectMainSlot.SetActive(true);
            selectSubSlot.SetActive(false);
            MainWeapon = false;
            SubWeapon = true;
            Debug.Log("MainClick");
            //WeaponSlot1.SetAsLastSibling();
            //WeaponSlot2.SetAsFirstSibling();
        }
        else if (Input.GetButtonDown("SubWeapon") && SubWeapon == true)
        {
            selectMainSlot.SetActive(false);
            selectSubSlot.SetActive(true);
            //MainWeapon_Swap.SetBool("MainWeapon_Down", true);
            //SubWeapon_Swap.SetBool("SubWeapon_Up", true);
            //MainWeapon_Swap.SetBool("MainWeapon_Up", false);
            //SubWeapon_Swap.SetBool("SubWeapon_Down", false);
            MainWeapon = true;
            SubWeapon = false;
            Debug.Log("SubClick");
            //WeaponSlot2.SetAsLastSibling();
            //WeaponSlot1.SetAsFirstSibling();
        }
        
        if(ingameOption.activeSelf == true || inv_slot.activeSelf == true || buff_BG.activeSelf == true)
        {
            IngameTime(false);
        }
        else if(ingameOption.activeSelf == false && inv_slot.activeSelf == false && buff_BG.activeSelf == false)
        {
            IngameTime(true);
        }
        //플레이어가 사망할경우 시간멈춤
        if (!character.GetPlayerState())
        {
            OnPlayerDeath();
            IngameTime(false);
            TitleText.text = "당신은 사망하셨습니다";
            playtimeText.text = GameManager.Instance.time.text;
            destory_enemy_count.text = enemy_count.ToString();
        }

        if (test_clear_boolCheck)
        {
            On_Player_Clear();
            IngameTime(false);
            Clear_TitleText.text = "모든 층을 클리어하셨습니다!";
            Clear_playtimeText.text = GameManager.Instance.time.text;
            Clear_destory_enemy_count.text = enemy_count.ToString();
            
        }

        if (haveCoin != character.Coin_Count) // 코인갯수 확인용
        {
            haveCoin = character.Coin_Count;
            Coin_Count_Text_Update();
        }
        if (buff_BG.activeSelf)
        {
            UpdateNeedCoin();
        }

        activate_Magazine_Update();


    }
    public void IngameTime(bool ingameTime) //false면 멈춤 true면 재생
    {
        if (!ingameTime)  // 시간멈추기
        {
            Time.timeScale = 0f;
            GameManager.Instance.isPlaying = false;
        }
        else if(ingameTime) // 시간흘러가게하기
        {
            Time.timeScale = 1f;
            GameManager.Instance.isPlaying = true;
        }
    }

    public void Resume_Btn()
    {
        SoundManager.PlaySFX(buttonSound);
        ingameOption.SetActive(false);
        option_open();
    }
    public void Option_Btn()
    {
        SoundManager.PlaySFX(buttonSound);
        option_popup.SetActive(true);
    }

    public void Cancle_Btn()
    {
        SoundManager.PlaySFX(buttonSound);
        option_popup.SetActive(false);
        quit_popup.SetActive(false);

    }

    public void FullScreen_Btn()
    {
        SoundManager.PlaySFX(buttonSound);
        fullScreen_Box.sprite = checkBox;
        windowScreen_Box.sprite = emptyBox;

        Screen.SetResolution(1920, 1080, true);

    }
    public void WindowScreen_btn()
    {
        SoundManager.PlaySFX(buttonSound);
        fullScreen_Box.sprite = emptyBox;
        windowScreen_Box.sprite = checkBox;

        Screen.SetResolution(1600, 900, false);
    }
    public void Quit_Btn()
    {
        SoundManager.PlaySFX(buttonSound);
        quit_popup.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void inventory_open() //탭키를 눌렀을때
    {
        if (!inv_slot_active_bool) //만약 인벤슬롯의 불 값이 false면
        {
            inv_slot.SetActive(true); //인벤슬롯 활성화
            StartCoroutine(InvRefreshCoroutine());
        }
        else if (inv_slot_active_bool)
        {
            inv_slot.SetActive(false); //false가 아니라면 인벤토리 슬롯 끄기
            inv_slot_active_bool = false; //인벤슬롯 끄고 다시 false로 바꿔주기
            left_inv.SetActive(false);
            right_inv.SetActive(false);
            Buff_list.SetActive(false);
        }
    }
    private IEnumerator InvRefreshCoroutine()
    {
        SoundManager.PlaySFX(bookSound); //현재 사운드 오류나서 아래쪽 코드까지 못 가서 인벤토리 안불러와짐 잠깐 주석처리
        yield return new WaitForSecondsRealtime(0.6f); // 실제 시간 기준으로 대기
        inv_refresh(); // 인벤토리 갱신 메서드 호출
    }

    public void option_open()
    {
        if (openOption)
        {
            ingameOption.SetActive(true);
            openOption = false;
        }
        else if (!openOption)
        {
            ingameOption.SetActive(false);
            openOption = true;
        }
    }
    public void inv_refresh() //인벤토리 갱신
    {
        inv_slot_active_bool = true; //불값 true

        left_inv.SetActive(true);
        right_inv.SetActive(true);
        Buff_list.SetActive(true);
        inventory_slot.AddWeapon(); //인벤 슬롯에 무기 할당 코드
        inventory_slot.AddConsumableItem();//인벤 소비슬롯에 소비아이템 할당
        inventory_slot.AddBuff_Effect();//인벤토리 버프 아이콘 할당
        Debug.Log("인벤토리 열림");
    }

    public void MinimapOn_off()
    {
        if(!minimap_state && Input.GetButtonDown("Minimap"))
        {
            minimap_camera.SetActive(true);
            minimap_state = true;
        }
        else if(minimap_state && Input.GetButtonDown("Minimap"))
        {
            minimap_camera.SetActive(false);
            minimap_state = false;
        }
    }

    public void enemy_count_update()
    {
        // "enemy" 태그를 가진 오브젝트의 개수를 센다
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // UI 텍스트 업데이트
        enemyCountText.text = enemyCount.ToString();
    }
    

    public void SetMusicVolume(float volume) => SoundManager.SetMusicVolume(volume);
    public void SetSFXVolume(float volume) => SoundManager.SetSFXVolume(volume);
    
    public void OnPlayerDeath()
    {
        // 추가 카메라 활성화 및 렌더링
        screenshotCamera.enabled = true;
        screenshotCamera.Render();

        // Render Texture에서 Texture2D로 변환
        RenderTexture.active = renderTexture;
        screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

        // 추가 카메라 비활성화
        //screenshotCamera.enabled = false;
        RenderTexture.active = null;

        // 사망 UI 표시 및 이미지 설정
        deathScreenUI.SetActive(true);
        deathScreenImage.texture = screenShot;
    }

    public void On_Player_Clear()
    {
        // 추가 카메라 활성화 및 렌더링
        screenshotCamera.enabled = true;
        screenshotCamera.Render();

        // Render Texture에서 Texture2D로 변환
        RenderTexture.active = renderTexture;
        screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

        weapon_slot1_icon.sprite = Weapon_Slot.weaponSlot1.GetComponent<Fire_Test>().weapon.Icon;
        weapon_slot2_icon.sprite = Weapon_Slot.weaponSlot2.GetComponent<Fire_Test>().weapon.Icon;


        // 추가 카메라 비활성화
        //screenshotCamera.enabled = false;
        RenderTexture.active = null;

        // 클리어 UI 표시 및 이미지 설정
        Clear_Screen_UI.SetActive(true);
        Clear_Screen_Img.texture = screenShot;

        DataManager.Instance.SaveGameData(PlayerChar.single.playerdata);
        DataManager.Instance.data.clearTime = GameManager.Instance.timer;
    }

    public void GoTitle()
    {
        DataManager.Instance.SaveGameData(PlayerChar.single.playerdata);
        GameManager.Instance.EndGame();
        DataManager.Instance.RankUp();
        GameManager.LoadScene("Lobby");
        deathScreenUI.SetActive(false);
        Clear_Screen_UI.SetActive(false);
        PlayerChar.single.player_reset();
        test_clear_boolCheck = false;
        // DataManager.Instance.SaveGameData();
    }


    public void Coin_Count_Text_Update()
    {
        Coin_Count_Text.text = "X " + character.Coin_Count.ToString();  
    }

    public void Open_BuffBG()
    {
        buff_BG.SetActive(true);  // BuffBG를 활성화
        IngameTime(false);
    }

    public void Close_BuffBG()
    {
        SoundManager.PlaySFX(buttonSound);
        buff_BG.SetActive(false);  // BuffBG를 비활성화
        IngameTime(true);
    }



    public void Atk_UpgradeBtn()
    {
        int needcoin = buffsystem.ATK_UPgrade();
        SoundManager.PlaySFX(buttonSound);
        if (character.Coin_Count >= needcoin)
        {
            character.Coin_Count -= needcoin;
            character.damageUpStack += 1;
            character.m_passive_buff_damage += 1;
            UpdateNeedCoin();
        }
    }

    public void HP_UpgradeBtn()
    {
        int needcoin = buffsystem.HP_UPgrade();
        SoundManager.PlaySFX(buttonSound);
        if (character.Coin_Count >= needcoin)
        {
            character.Coin_Count -= needcoin;
            character.max_hp_UPStack += 1;
            character.m_maxHealth += 10;
            character.m_health = character.m_maxHealth;
            character.player_hpbar_update();
            UpdateNeedCoin();
        }
    }

    public void Movement_UpgradeBtn()
    {

        int needcoin = buffsystem.MoveMent_UPgrade();
        SoundManager.PlaySFX(buttonSound);
        if (character.Coin_Count >= needcoin)
        {
            character.Coin_Count -= needcoin;
            character.movement_SpeedUpStack += 1;
            character.m_buff_movementSpeed += 0.2f;
            UpdateNeedCoin();
        }
    }
    private void UpdateNeedCoin()
    {
        atkBuff_needcoin.text = buffsystem.ATK_UPgrade().ToString();
        hpBuff_needcoin.text = buffsystem.HP_UPgrade().ToString();
        movementBuff_needcoin.text = buffsystem.MoveMent_UPgrade().ToString();
        atkBuff_stacktext.text = "+" + character.damageUpStack.ToString() + " 강화";
        hpBuff_stacktext.text = "+" + character.max_hp_UPStack.ToString() + " 강화";
        movementBuff_stacktext.text = "+" + character.movement_SpeedUpStack.ToString() + " 강화";
    }


    public void activate_Magazine_Update()
    {
        // Weapon_Slot 싱글톤 인스턴스의 활성화된 슬롯을 가져옵니다.
        GameObject activeSlot = Weapon_Slot.activeWeaponSlot;

        if (activeSlot != null)
        {
            if (activeSlot == Weapon_Slot.weaponSlot1)
            {
                float currentMagazineCapacity = Weapon_Slot.magazineCapacitySlot1;
                activate_Magazine.text = currentMagazineCapacity.ToString() + "/" + Weapon_Slot.activeWeaponSlot_Component.weapon.backup_magazine_capacity;

                // 텍스트 변경 후 UI 강제 갱신
                activate_Magazine.ForceMeshUpdate();

            }
            else if (activeSlot == Weapon_Slot.weaponSlot2)
            {
                float currentMagazineCapacity = Weapon_Slot.magazineCapacitySlot2;
                activate_Magazine.text = currentMagazineCapacity.ToString() + "/" + Weapon_Slot.weapon_slot2_weapon.weapon.backup_magazine_capacity;
                ;

                // 텍스트 변경 후 UI 강제 갱신
                activate_Magazine.ForceMeshUpdate();


            }
            else
            {
                Debug.LogError("활성화된 슬롯을 확인할 수 없습니다.");
                activate_Magazine.text = "N/A";
                activate_Magazine.ForceMeshUpdate();
            }
        }
        else
        {
            Debug.LogError("활성화된 슬롯이 없습니다.");
            activate_Magazine.text = "N/A";
            activate_Magazine.ForceMeshUpdate();
        }
    }


}