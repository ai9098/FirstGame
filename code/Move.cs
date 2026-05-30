using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour
{
    public float speed = 200f;

    // 移動中かどうかを管理するフラグ
    [SerializeField] public static bool isMoving = false;

    [SerializeField] private RectTransform target;

    // インスペクターからシーンごとに目的地をいじれるようにする
    [SerializeField] private Vector2 destination;
    // 向きも変えられるようにする（1なら右、-1なら左）
    [SerializeField] private float directionX = 1f;

    void Start()
    {
        // targetがアサインされていない場合、エラー文を出す
        if (target == null)
        {
            Debug.Log("RectTransform component not found on the GameObject.");
        }
    }

    public void ClickThing()
    {
        // 他のテキストが出ている間は移動処理を無効化
        if (GameManager.Instance.isUIOpen || GameManager.Instance.isItemListOpen) return;

        isMoving = true;

        // 直接数値を書かず、設定した変数（destination）に移動
        target.anchoredPosition = destination;

        // 向き（localScale）も変数で変える
        target.localScale = new Vector3(directionX, 1, 1);

        // SEを鳴らす
        Debug.Log("移動SE再生");
        if (SEManager.Instance != null) SEManager.Instance.MoveSE();

        Debug.Log($"{gameObject.name} が {destination} に移動しました。向きは {directionX}");
    }
}
