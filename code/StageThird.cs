using UnityEngine;

public class StageThird : MonoBehaviour
{
    [SerializeField] GameObject player;

    // このステージのみ、プレイヤーが透明な状態でスタートする
    void Start()
    {
        var sr = player.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color c = sr.color;
            c.a = 0f; // プレイヤーを完全に透明にする
            sr.color = c;

            // 現在のサイズを取得
            Vector3 s = transform.localScale;
            // サイズを変更する
            s.x = 0.2f;
            s.y = 0.2f;
            s.z = 0.2f;
            // 書き換えたサイズを適用
            transform.localScale = s;
        }

    }
}
