using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CURSOR : MonoBehaviour
{
    public Vector2 hotSpot = Vector2.zero;
    public Vector2 ChagnehotSpot = Vector2.zero;

    [SerializeField] private Sprite[] cursorSprites; // Multiple 모드로 잘라진 스프라이트 배열
    private Texture2D[] cursorTextures;

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
    }

    public void cursor_Change()
    {
        // 특정 조건에 따라 슬라이스된 커서를 적용
        if (IngameUI.single.inv_slot_active_bool == true || IngameUI.single.openOption == false)
        {
            Debug.Log("123");
            Cursor.SetCursor(cursorTextures[2], ChagnehotSpot, CursorMode.Auto); // 예: inventory 상태일 때 cursorTextures[1] 사용
        }
        else
        {
            Cursor.SetCursor(cursorTextures[0], hotSpot, CursorMode.Auto); // 기본 커서
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
