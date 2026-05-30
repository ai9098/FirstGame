using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject Item1;
    [SerializeField] private GameObject ItemImage1;
    [SerializeField] private GameObject Item2;
    [SerializeField] private GameObject ItemImage2;
    [SerializeField] private Sprite item1Sprite;
    [SerializeField] private Sprite item2Sprite;

    [SerializeField] private string item1Name;
    [SerializeField] private string item1Text;
    [SerializeField] private string item2Name;
    [SerializeField] private string item2Text;

    [SerializeField] private GameObject Panel_msg;
    [SerializeField] private GameObject listItemPrefab;
    public Transform parentTran;
    [SerializeField] private UIText uiText;

    public bool Get1 { get; private set; }
    public bool Get2 { get; private set; }

    void Start()
    {
        // Item1を既に持っている場合
        if (GameManager.Instance.gotItems.Contains(item1Name))
        {
            Item1.SetActive(false);
        }
        // Item2
        if (GameManager.Instance.gotItems.Contains(item2Name))
        {
            Item2.SetActive(false);
        }
    }

    // アイテムを回収したときの処理
    public void ItemClicked(string button)
    {
        if (GameManager.Instance.isUIOpen || uiText.playing || GameManager.Instance.isItemListOpen)
        {
            return;
        }
        Debug.Log("クリック来た");

        // パネル表示
        Panel_msg.SetActive(true);


        if (button == "Item1")
        {
            if (!Move.isMoving)
            {
                Panel_msg.SetActive(false);
                return;
            }

            if (!GameManager.Instance.gotItems.Contains(item1Name))
            {
                if (GameManager.Instance.isUIOpen && !GameManager.Instance.isItemListOpen)
                {
                    Panel_msg.SetActive(false);
                    return;
                }
                GameManager.Instance.gotItems.Add(item1Name);

                // アイテム欄にボタンを生成（拾った時だけ実行）
                GameObject obj = Instantiate(listItemPrefab, parentTran);
                obj.GetComponent<ListItemButton>().Setup(item1Name, item1Sprite, item1Text);
                // ※ボタンへのAddListenerはListItemButton側でやっているので、ここでのbtn.onClick...はいらない
                if (SEManager.Instance != null) SEManager.Instance.ItemGetSE();
                ItemImage1.SetActive(true);
                Get1 = true;
            }
            GameManager.Instance.isUIOpen = true;
            uiText.DrawText(item1Name, item1Text);
            Item1.SetActive(false);
        }

        if (button == "Item2")
        {
            if (!GameManager.Instance.gotItems.Contains(item2Name))
            {
                if (GameManager.Instance.isUIOpen && !GameManager.Instance.isItemListOpen)
                {
                    Panel_msg.SetActive(false);
                    return;
                }
                GameManager.Instance.gotItems.Add(item2Name);

                GameObject obj = Instantiate(listItemPrefab, parentTran);
                obj.GetComponent<ListItemButton>().Setup(item2Name, item2Sprite, item2Text);
                if (SEManager.Instance != null) SEManager.Instance.ItemGetSE();
                ItemImage2.SetActive(true);
                GameManager.Instance.isUIOpen = true;
                Get2 = true;
            }
            GameManager.Instance.isUIOpen = true;
            uiText.DrawText(item2Name, item2Text);
            Item2.SetActive(false);
        }
    }

    // アイテム欄からアイテムをクリックしたときの処理（ListItemButtonから呼ばれる）
    public void ListItemClicked(string name, string desc)
    {
        if (GameManager.Instance.isUIOpen || uiText.playing)
        {
            return;
        }
        Debug.Log("クリック来た");

        // パネル表示
        Panel_msg.SetActive(true);
        GameManager.Instance.isUIOpen = true;

        uiText.DrawText(name, desc);
    }

    //アイテム名からアイテムのスプライトを返す（ListItemButtonから呼ばれる）
    public Sprite GetItemSprite(string itemName)
    {
        if (itemName == item1Name)
            return item1Sprite;

        if (itemName == item2Name)
            return item2Sprite;

        return null;
    }
}
