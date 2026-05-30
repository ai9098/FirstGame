using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class FirstText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tmp_mes; // メッセージを表示するTextMeshProUGUI
    [SerializeField] 
    private GameObject Panel_msg;
    [SerializeField]
    private UIText uiText;
    [SerializeField]
    private TextWriter TextWriter;

    [SerializeField]
    public string[] moji;

    [SerializeField]
    private int kazu;
    [SerializeField]
    public int j;

    [SerializeField]
    private float mojiwait;
    [SerializeField]
    private float ti;

    public static bool IsMessageActive { get; private set; } // メッセージが表示中かどうか
    public bool IsFinished { get; private set; } // メッセージが完全に表示されたかどうか

    private GameObject gmj;
    private RectTransform rect;

    void Start()
    {
        TextWriter.enabled = false;
        StartCoroutine(ShowTexts());
    }

    IEnumerator ShowTexts()
    {
        if (GameManager.Instance != null && GameManager.Instance.hasPlayedFirstText)
        {
            // パネルを消しておく（念のため）
            if (Panel_msg != null) Panel_msg.SetActive(false);
            yield break;
        }

        Panel_msg.SetActive(true);

        moji = new string[]
           {
                "移動：A/D　または　←/→\nアイテム欄：Spaceキー\n気になるところを左クリック",
                "大切なものを\"すべて\"見つけて、少女を助けてあげましょう。",
                "これはテストです。",
                "これはテストです。",
                "これはテストです。"
           };

        GameManager.Instance.isUIOpen = true;
        uiText.DrawText("-操作方法-", moji[0]);
        yield return new WaitUntil(() => !uiText.playing);
        for (int i = 1; i < moji.Length; i++)
        {
            // クリック待ち
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            uiText.DrawText("???",moji[i]);
            yield return new WaitUntil(() => !uiText.playing);
        }

        Panel_msg.SetActive(true);
        GameManager.Instance.hasPlayedFirstText = true;
        TextWriter.enabled = true;
        GameManager.Instance.isUIOpen = false;
    }
}