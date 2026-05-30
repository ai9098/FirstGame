using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour
{
    // nameText: 話している人の名前
    // talkText: 話している内容
    public TMP_Text nameText;
    public TMP_Text talkText;

    public bool playing;
    public float textSpeed = 0.5f;

    // クリックで次のページを表示
    public bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }

    // ナレーション用テキストを表示する関数
    public void DrawText(string text)
    { 
        nameText.text = "";
        StartCoroutine("CoDrawText", text);
    }

    // 通常会話用のテキストを生成する関数
    public void DrawText(string name, string text)
    {
        nameText.text = name;
        StartCoroutine("CoDrawText", text);
    }

    // テキストを一文字ずつ表示するコルーチン
    IEnumerator CoDrawText(string text)
    {
        GameManager.Instance.isUIOpen = true;

        playing = true;
        float time = 0;
        while(true)
        {
            GameManager.Instance.isUIOpen = true;
            yield return 0;
            time += Time.deltaTime;

            // クリックでテキストを一気に表示
            if (IsClicked()) break;

            int len = Mathf.FloorToInt (time / textSpeed);
            if (len >= text.Length) break;
            talkText.text = text.Substring(0, len);
        }
        talkText.text = text;
        yield return 0;
        playing = false;
    }
}
