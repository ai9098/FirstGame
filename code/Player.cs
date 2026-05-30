using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    // 移動スピード
    [SerializeField] private float moveSpeed = 10.0f;

    //[SerializeField] private GameObject msgPanel;
    //[SerializeField] private GameObject ItemList;
    //[SerializeField] private UIText uiText;

    // プレイヤーのをRigidBody2D入れる箱
    private Rigidbody2D rb;
    // プレイヤーのSpriteRenderを入れる箱
    private SpriteRenderer sr;

    // 前フレームの移動状態を記憶（長押し判定）
    private bool wasMoving = false;
    public bool ItemLook { get; private set; }


    // プレイヤーが移動しているかどうかのフラグ
    public static bool isMoving;

    // Animatiorコンポネント
    private Animator animator;

    // ゲーム開始時１回呼ばれるメソッド
    private void Awake()
    {
        if (GameManager.Instance != null && GameManager.Instance.useNextPosition)
        {
            // Startより早いAwakeで位置を確定させる
            transform.position = GameManager.Instance.nextPlayerPosition;
            GameManager.Instance.useNextPosition = false;
        }
    }

    void Start()
    {
        // プレイヤーのRigidBody2Dを取得してrbに入れる
        rb = GetComponent<Rigidbody2D>();
        // プレイヤーのSpriteRendererを取得してrbに入れる
        sr = GetComponent<SpriteRenderer>();
        // プレイヤーのAnimatorを取得してanimatorに入れる
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Spaceキーが押されたらアイテム欄を開閉
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.wantToOpenItemList = true;
        }

        // メッセージ表示中なら歩行処理を無効化
        if (GameManager.Instance.isUIOpen)
        {
            if (rb != null) rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }
        Walk();
    }

    
    private void Walk()
    {
        // 他のテキストが出ている間は移動処理を無効化
        if (GameManager.Instance.isUIOpen || GameManager.Instance.isItemListOpen) return;

        // 入力された方向を調べる（右なら1/左なら-1/何も押していなければ0）
        float direction = Input.GetAxisRaw("Horizontal");
        isMoving = Mathf.Abs(direction) > 0f;

        // 状態遷移でのみ再生／停止を呼ぶ（長押し中は継続再生）
        if (isMoving && !wasMoving)
        {
            if (SEManager.Instance != null) SEManager.Instance.WalkSE();
            animator.SetBool("Walk", true);
        }
        else if (!isMoving && wasMoving)
        {
            if (SEManager.Instance != null) SEManager.Instance.StopWalk();
            animator.SetBool("Walk", false);
        }

        wasMoving = isMoving;

        // RigidBody2D（物理演算）の「速度」を使って左右に動かす
        rb.linearVelocityX = direction * moveSpeed;

        // 右に進んだら
        if (direction > 0)
        {
            // 左右反転させる
            sr.flipX = true;
        }
        // 右に進んだら
        else if (direction < 0)
        {
            // デフォルトのまま
            sr.flipX = false;
        }
    }
}
