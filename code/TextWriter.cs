using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    public UIText uitext;
    public ItemManager itemManager;

    public GameObject Panel_msg;

    public GameObject ItemImage1;
    public GameObject ItemImage2;

    void Update()
    {
        // 1. まず「今、文字が再生中（playing）」かどうかを確認
        if (uitext.playing)
        {
            return;
        }

        if (!Panel_msg.activeSelf) return;

        // 2. 再生中でない（終わった）状態で、クリックされたら
        if (Input.GetMouseButtonDown(0))
        {
            // ウィンドウ（このスクリプトがついているオブジェクト）を非表示にする
            Panel_msg.SetActive(false);
            if (itemManager.Get1) ItemImage1.SetActive(false);
            if (itemManager.Get2) ItemImage2.SetActive(false);

            GameManager.Instance.isUIOpen = false;
        }
    }
}
