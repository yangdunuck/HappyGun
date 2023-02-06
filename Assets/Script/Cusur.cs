using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cusur : MonoBehaviour
{
    //마우스 포인터로 사용할 텍스처를 입력받습니다.
    public Texture2D cursorTexture;
    public Vector2 adjustHotSpot = Vector2.zero;
    //내부에서 사용할 필드를 선업합니다.
    private Vector2 hotSpot;
    public void Start()
    {

        //코루틴을 사용합니다. TargetCursor()함수를 호출합니다.
        StartCoroutine("MyCursor");
    }
    //MyCursor()라는 이름의 코루틴이 시작됩니다.
    IEnumerator MyCursor()
    {
        yield return new WaitForEndOfFrame();
        hotSpot.x = cursorTexture.width / 2f;
        hotSpot.y = cursorTexture.height / 2f;
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }
}
