using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
public class OpenList : MonoBehaviour
{
    // アイテム欄を開いたとき、目玉が動くアニメーション
    [SerializeField] private Animator animater;

    [SerializeField] private GameObject ItemList;

    void Update()
    {
        //Debug.Log("GameManager.Instance.isUIOpen: " + GameManager.Instance.isUIOpen);

        if (!GameManager.Instance.wantToOpenItemList) return;

        GameManager.Instance.wantToOpenItemList = false;

        if (GameManager.Instance.isUIOpen)
        {
            //Debug.Log("テキスト表示中なので、アイテム欄リクエストを無視");
            return;
        }

        if (SEManager.Instance != null)
            SEManager.Instance.ItemListSE();

        // アイテム欄が開いている場合は閉じる、閉じている場合は開く
        if (GameManager.Instance.isItemListOpen)
        {
            ItemList.GetComponent<ItemListUI>().Close();
            GameManager.Instance.isItemListOpen = false;

            // アイテム欄を開く際の目玉アニメーションを停止
            animater.SetBool("Look", false);
            //Debug.Log("アイテム欄を閉じる");
        }
        else
        {
           // 他のUI（isUIOpen）が開いていない時だけ実行
            if (!GameManager.Instance.isUIOpen)
            {
                ItemList.GetComponent<ItemListUI>().Open();
                GameManager.Instance.isItemListOpen = true;
                //Debug.Log("アイテム欄を開く");
            }
            Debug.Log("最終的な状態: isItemListOpen = " + GameManager.Instance.isItemListOpen);
        }
    }

    // ボタンでも操作可能にする
    public void OpenListButton()
    {
        Debug.Log("目玉クリック");

        GameManager.Instance.wantToOpenItemList = false;

        if (GameManager.Instance.isUIOpen)
        {
            //Debug.Log("テキスト表示中なので、アイテム欄リクエストを無視");
            return;
        }

        if (SEManager.Instance != null)
            SEManager.Instance.ItemListSE();

        // アイテム欄が開いている場合は閉じる、閉じている場合は開く
        if (GameManager.Instance.isItemListOpen)
        {
            ItemList.GetComponent<ItemListUI>().Close();
            GameManager.Instance.isItemListOpen = false;

            // アイテム欄を開く際の目玉アニメーションを停止
            animater.SetBool("Look", false);
            //Debug.Log("アイテム欄を閉じる");
        }
        else
        {
            // 他のUI（isUIOpen）が開いていない時だけ実行
            if (!GameManager.Instance.isUIOpen)
            {
                ItemList.GetComponent<ItemListUI>().Open();
                GameManager.Instance.isItemListOpen = true;

                // アイテム欄を開く際の目玉アニメーションを再生
                animater.SetBool("Look", true);
                //Debug.Log("アイテム欄を開く");
            }
            Debug.Log("最終的な状態: isItemListOpen = " + GameManager.Instance.isItemListOpen);
        }
    }
}
