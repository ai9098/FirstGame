using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SeneChanger : MonoBehaviour
{
    public string sceneToLoad;
    public Animator fadeAnim;
    public float fadeTime = .5f;

    // 次のシーンでの出現座標をインスペクターで設定できるようにする
    public Vector2 nextSpawnPosition;
    private bool isTransitioning = false; // 遷移中フラグ


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{gameObject.name} が {collision.gameObject.name} と接触しました！");

        if (collision.CompareTag("Player"))
        {
            // もし遷移中なら、何もしない
            if (isTransitioning) return;

            isTransitioning = true;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.nextPlayerPosition = nextSpawnPosition;
                GameManager.Instance.useNextPosition = true;
            }

            fadeAnim.Play("FadeToBlack");
            StartCoroutine(DelayFade());
        }
    }

    IEnumerator DelayFade()
    {
        yield return new WaitForSeconds(fadeTime); // フェードアウトの時間に合わせて待機
        SceneManager.LoadScene(sceneToLoad);
    }
}
