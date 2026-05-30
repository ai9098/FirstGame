using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ListItemButton : MonoBehaviour
{
    private string itemName;
    public TMP_Text text;
    public Image iconImage;
    private string itemDescription;

    public void Setup(string name, Sprite icon, string description)
    {
        Debug.Log("Setup呼ばれた: " + name);
        Debug.Log("text: " + text);
        Debug.Log("iconImage: " + iconImage);
        Debug.Log("sprite: " + icon);

        if (text == null)
        {
            Debug.Log("returnされた");
            return;
        }
        itemName = name;
        text.text = name;

        if (icon != null)
        {
            iconImage.sprite = icon;
        }
        itemDescription = description;

        //Debug.Log("sprite入れる前");
        Debug.Log(icon);

        iconImage.sprite = icon;
        //Debug.Log("sprite入れた後");

        Button btn = GetComponent<Button>();

        if (btn == null)
        {
            Debug.LogError("Buttonコンポーネントがない！");
            return;
        }

        btn.onClick.RemoveAllListeners(); // 念のため
        btn.onClick.AddListener(() => {
            Debug.Log("クリック検知！！");
            OnClick();
        });
    }

    void OnClick()
    {
        Debug.Log("クリックされた: " + itemName);
        Debug.Log("UIOpen: " + GameManager.Instance.isUIOpen);

        ItemManager manager = FindObjectOfType<ItemManager>();
        Debug.Log(manager);

        if (manager != null)
        {
            manager.ListItemClicked(itemName, itemDescription);
        }
        else
        {
            Debug.LogError("ItemManagerが見つからない！");
        }
    }
}
