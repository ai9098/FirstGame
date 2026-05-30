using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
public class ItemListUI : MonoBehaviour
{
    public RectTransform content;
    public GameObject listItemPrefab;

    private void OnEnable()
    {
        // GameManagerが存在し、かつアイテムデータがあるなら更新する
        if (GameManager.Instance != null)
        {
            Refresh();
        }
    }

    public void Open()
    {
        //Debug.Log("ItemListUI Start 発動");
        gameObject.SetActive(true);
        Refresh();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Refresh()
    {
        if (content == null)
        {
            Debug.LogError("content (RectTransform) がアサインされていないか、破棄されています！");
            return;
        }

        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Transform child = content.GetChild(i);
            if (child != null && child.gameObject != null)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (string itemName in GameManager.Instance.gotItems)
        {
            // 辞書からアイテムデータを丸ごと検索する
            if (GameManager.Instance.itemDatabase.TryGetValue(itemName, out ItemData data))
            {
                //GameObject newObj = Instantiate(listItemPrefab, content, false);
                GameObject newObj = Instantiate(listItemPrefab);
                newObj.transform.SetParent(content, false);

                // 【ここがポイント！】
                // すでに data.itemSprite があるので、わざわざ新しい変数「icon」を
                // 作らなくても、そのまま data を Setup に渡せばOKです！
                newObj.GetComponent<ListItemButton>().Setup(
                    data.itemName,
                    data.itemSprite,
                    data.itemDescription
                );
            }
        }
    }
}