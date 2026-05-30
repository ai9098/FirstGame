using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField] private AudioSource source_walk; // 歩く音のAudioSource
    [SerializeField] private AudioClip clip_walk; // 歩く音のAoudioClip

    [SerializeField] private AudioSource source_ItemGet; // アイテムゲット音のAudioSource
    [SerializeField] private AudioClip clip_ItemGet; // アイテムゲット音のAudioClip

    [SerializeField] private AudioSource source_ItemListOpen; // アイテムリスト音のAudioSource
    [SerializeField] private AudioClip clip_ItemListOpen; // アイテムリスト音のAudioClip

    [SerializeField] private AudioSource source_GirlClick; // 女の子クリック音のAudioSource
    [SerializeField] private AudioClip clip_GirlClickn; // 女の子クリック音のAudioClip

    [SerializeField] private AudioSource source_Move; // 物移動音のAudioSource
    [SerializeField] private AudioClip clip_Move; // 物移動音のAudioClip

    public static SEManager Instance { get; private set; } // シングルトンインスタンス

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // このインスタンスがnullならば
        if(Instance == null)
        {
            Instance = this; // このインスタンスをシングルトンインスタンスとして設定
        }
        else
        {
            Destroy(gameObject); // すでにインスタンスが存在する場合はこのオブジェクトを破壊
        }
        source_walk.clip = clip_walk; // ClipとSourceを対応付ける
    }

    // これを呼び出せばSEを鳴らすことができる
    public void WalkSE()
    {
        source_walk.Play(); // 歩く音を鳴らす
    }
    public void StopWalk()
    {
        source_walk.Stop(); // 歩く音を止める
    }

    public void ItemGetSE()
    {
        source_ItemGet.Play(); // アイテム取得音を鳴らす
    }

    public void ItemListSE()
    {
        source_ItemListOpen.Play(); // アイテムリストクリック音を鳴らす
    }

    public void GirlClickSE()
    {
        source_GirlClick.Play(); // 女の子クリック音を鳴らす
    }

    public void MoveSE()
    {
        source_Move.Play(); // 女の子クリック音を鳴らす
    }
}
