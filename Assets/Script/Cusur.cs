using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cusur : MonoBehaviour
{
    //���콺 �����ͷ� ����� �ؽ�ó�� �Է¹޽��ϴ�.
    public Texture2D cursorTexture;
    public Vector2 adjustHotSpot = Vector2.zero;
    //���ο��� ����� �ʵ带 �����մϴ�.
    private Vector2 hotSpot;
    public void Start()
    {

        //�ڷ�ƾ�� ����մϴ�. TargetCursor()�Լ��� ȣ���մϴ�.
        StartCoroutine("MyCursor");
    }
    //MyCursor()��� �̸��� �ڷ�ƾ�� ���۵˴ϴ�.
    IEnumerator MyCursor()
    {
        yield return new WaitForEndOfFrame();
        hotSpot.x = cursorTexture.width / 2f;
        hotSpot.y = cursorTexture.height / 2f;
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }
}
