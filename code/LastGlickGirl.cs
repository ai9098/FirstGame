using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LastClickGirl : MonoBehaviour
{
    //[SerializeField] private GameObject SideGirl1;
    [SerializeField] private GameObject Panel_msg;
    [SerializeField] private UIText uiText;

    [SerializeField] private string Name;

    [SerializeField] private string Text;
    [SerializeField] private string Text2;

    // 画面を暗くするためのオブジェクトとフェード時間
    [SerializeField] private CanvasGroup ItemImage;
    [SerializeField] private CanvasGroup ItemBackGround;
    [SerializeField] private float fadeDuration = 2.0f; // 何秒かけて暗くするか

    // ゲットできるアイテム
    [SerializeField] private string itemName;
    [SerializeField] private string itemText;
    [SerializeField] private Sprite itemSprite;

    // アイテム欄
    [SerializeField] private GameObject listItemPrefab;
    public Transform parentTran;

    // 重要なアイテムの名前（これらをすべて回収しているか確認するため）
    string[] importantItems = { "手紙", "新聞の切れ端", "診断書", "成績表", "母子健康手帳", "遺書" };

    [SerializeField] private Animator girlAnimator;

    void Start()
    {
    }

    public void FinalGirlClicked()
    {
        // まだ回収していないアイテムがある場合は何も起きない
        if (!importantItems.All(item => GameManager.Instance.gotItems.Contains(item))) return;
        // 全シーンで少女の話しかけていない場合は何も起きない
        if (GameManager.Instance.talkedGirls.Count < 4) return;

        // すでに会話中の場合は二重に起動しないようにする
        StartCoroutine(FinalTalkRoutine());
    }

    // エンディングまでの会話と演出を管理するコルーチン
    private IEnumerator FinalTalkRoutine()
    {
        // パネル表示
        Panel_msg.SetActive(true);

        if (GameManager.Instance.isItemListOpen)
        {
            Panel_msg.SetActive(false);
            yield break;
        }

        // 少女がクリックされたら音を鳴らす
        if (SEManager.Instance != null) SEManager.Instance.GirlClickSE();

        // 初めのテキスト(クリックを待つ)
        GameManager.Instance.isUIOpen = true;
        uiText.DrawText(Name, Text);
        yield return new WaitUntil(() => !uiText.playing);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null; // 1フレーム待機（これでクリック判定をリセット）

        // こちらを見るアニメーションを再生
        girlAnimator.SetBool("LookMe", true);

        // 次のテキストを表示(クリックを待つ)
        uiText.DrawText(Name, Text2);
        yield return new WaitUntil(() => !uiText.playing);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;


        // ここから画面が暗くなり、アイテムも表示する(ゆっくりと)
        float elapsed = 0f;
        ItemBackGround.gameObject.SetActive(true); // パネルを有効化
        ItemImage.gameObject.SetActive(true);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            // Alpha値を 0（透明）から 1（真っ黒）へ変化させる
            ItemBackGround.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            ItemImage.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null; // 1フレーム待機
        }

        // 真っ暗になった状態で次のテキストを表示
        uiText.DrawText(itemName, itemText);
        yield return new WaitUntil(() => !uiText.playing);
        GameManager.Instance.isUIOpen = false;

        //　選択肢を表示


        // アイテム欄にボタンを生成（拾った時だけ実行）
        GameObject obj = Instantiate(listItemPrefab, parentTran);
        obj.GetComponent<ListItemButton>().Setup(itemName, itemSprite, itemText);

        // アニメーションを戻す
        girlAnimator.SetBool("LookMe", false);
    }
}
