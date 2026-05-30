using UnityEngine;

public class LineTrigger : MonoBehaviour
{
    // trueならプレイヤーを透明に、falseなら不透明にする
    [SerializeField] bool makeInvisible;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlayerAlpha(other, 1f); // 入ったら出現
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlayerAlpha(other, 0f); // 出たら消える
        }
    }

    void SetPlayerAlpha(Collider2D player, float alphaValue)
    {
        //プレイヤーがトリガーに入ったときの処理
        Debug.Log("プレイヤーがラインに触れました！");

        //プレイヤーの見た目を取り出す
        var sr = player.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            //プレイヤーの透明度を変更する
            Color c = sr.color;
            //makeInvisibleがtrueなら透明に、falseなら不透明に
            c.a = alphaValue;
            //変更した色をプレイヤーに適用する
            sr.color = c;
        }
    }
}
