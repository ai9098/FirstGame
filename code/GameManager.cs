using UnityEngine;
using System.Collections.Generic;

[System.Serializable] // これを書くとインスペクターに表示されるようになります
public struct ItemData
{
    public string itemName;
    public Sprite itemSprite;
    [TextArea] public string itemDescription; // TextAreaにすると入力しやすくなります
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // シーン毎に女の子をクリックしたかどうかのフラグを管理する
    public HashSet<string> talkedGirls = new HashSet<string>();
    // アイテムを回収したかどうかのフラグを管理するHashSet
    public HashSet<string> gotItems = new HashSet<string>();

    public Vector3 nextPlayerPosition; // player次の出現座標
    public bool useNextPosition = false; // 座標を使うかどうかのフラグ

    // インスペクターで設定するアイテムデータのリスト
    [SerializeField] private List<ItemData> allItemSettings = new List<ItemData>();
    // 内部で検索しやすくするための辞書
    public Dictionary<string, ItemData> itemDatabase = new Dictionary<string, ItemData>();

    // 最初のテキストを再生したかどうかのフラグ
    public bool hasPlayedFirstText = false;

    // UIが開いているかどうかのフラグ
    public bool isUIOpen = false;
    // アイテムリストが開いているかどうかのフラグ
    public bool isItemListOpen = false;

    // アイテムリストを開きたいかどうかのフラグ（Iキーが押されたかどうか）
    public bool wantToOpenItemList;

    private void Awake()
    {

        // シングルトンパターンの実装
        if (Instance != null)
        {
            //CleanUpAndDestroy();
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // インスペクターで入れたデータを辞書に登録
        foreach (var data in allItemSettings)
        {
            if (!string.IsNullOrEmpty(data.itemName))
                itemDatabase[data.itemName] = data;
        }
    }

    void OnLevelWasLoaded(int level)
    {
        // 全フラグをリセット
        isUIOpen = false;
        isItemListOpen = false;

        // クラス名を「UIText」に変更して探し出す
        var canvasUIText = FindObjectOfType<UIText>();
        if (canvasUIText != null)
        {
            canvasUIText.playing = false;
            Debug.Log("シーン移動に伴いテキスト再生フラグをリセットしました");
        }
    }
}
