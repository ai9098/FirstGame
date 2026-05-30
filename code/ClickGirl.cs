using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ClickGirl : MonoBehaviour
{
    //[SerializeField] private GameObject SideGirl1;
    [SerializeField] private GameObject Panel_msg;
    [SerializeField] private UIText uiText;

    [SerializeField] private string Name;
    [SerializeField] private string Text;

    //[SerializeField] private Animator girlAnimator;
    [SerializeField] private string girlID;　// シーン毎に女の子をクリックしたかどうかのフラグ

    public void GirlClicked()
    {
        // すでにUIが開いている場合は何もしない
        if (GameManager.Instance.isUIOpen) return;

        // パネル表示
        Panel_msg.SetActive(true);

        if (GameManager.Instance.isItemListOpen)
        {
            Panel_msg.SetActive(false);
            return;
        }

        // 少女がクリックされたら音を鳴らす
        if (SEManager.Instance != null) SEManager.Instance.GirlClickSE();

        // このシーンでは女の子をクリックした
        if (!GameManager.Instance.talkedGirls.Contains(girlID))
        {
            // まだこの女の子をクリックしていない場合のみフラグを追加
            GameManager.Instance.talkedGirls.Add(girlID);
        }
        uiText.DrawText(Name, Text);
    }
}
