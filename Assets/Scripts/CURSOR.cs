using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CURSOR : MonoBehaviour
{
    public Vector2 hotSpot = Vector2.zero;
    public Vector2 ChagnehotSpot = Vector2.zero;

    [SerializeField] private Sprite[] cursorSprites; // Multiple 모드로 잘라진 스프라이트 배열
    private Texture2D[] cursorTextures;

    private bool isClicked = false; // 좌클릭 상태 변수

    void Start()
    {
        LoadCursorTextures();

        // 기본 커서 설정
        hotSpot.x = cursorTextures[0].width / 2;
        hotSpot.y = cursorTextures[0].height / 2;
        Cursor.SetCursor(cursorTextures[0], hotSpot, CursorMode.Auto);
    }

    private void Update()
    {
        cursor_Change();

        // 인벤토리나 옵션이 열린 상태에서만 좌클릭 감지
        if ((IngameUI.single.inv_slot_active_bool == true || IngameUI.single.openOption == false) && Input.GetMouseButtonDown(0))
        {
            isClicked = true;
            Cursor.SetCursor(cursorTextures[3], ChagnehotSpot, CursorMode.Auto); // 좌클릭 시 커서 변경
            Debug.Log("좌클릭 감지");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isClicked = false;
        }
    }

    public void cursor_Change()
    {
        // 특정 조건에 따라 슬라이스된 커서를 적용
        if (IngameUI.single.inv_slot_active_bool == true || IngameUI.single.openOption == false)
        {

            if (!isClicked)
            {
                Cursor.SetCursor(cursorTextures[2], ChagnehotSpot, CursorMode.Auto); // 조건에 맞는 기본 커서
            }
        }
        else
        {
            if (!isClicked)
            {
                Cursor.SetCursor(cursorTextures[0], hotSpot, CursorMode.Auto); // 기본 커서
            }
        }
    }

    private void LoadCursorTextures()
    {
        // 잘라진 스프라이트 배열을 Texture2D 배열로 변환
        cursorTextures = new Texture2D[cursorSprites.Length];
        for (int i = 0; i < cursorSprites.Length; i++)
        {
            cursorTextures[i] = SpriteToTexture2D(cursorSprites[i]);
        }
    }

    private Texture2D SpriteToTexture2D(Sprite sprite)
    {
        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);
        texture.alphaIsTransparency = true;  // 투명도 설정
        texture.Apply(); // 설정 적용

        Color[] pixels = sprite.texture.GetPixels(
            (int)sprite.textureRect.x,
            (int)sprite.textureRect.y,
            (int)sprite.textureRect.width,
            (int)sprite.textureRect.height);
        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
}
