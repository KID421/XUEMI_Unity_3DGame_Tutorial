using UnityEngine;

public class HouseSound : MonoBehaviour
{
    [Header("房子碰撞音效")]
    public AudioClip soundHit;

    /// <summary>
    /// 音效管理器
    /// </summary>
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    /// <summary>
    /// 碰撞開始事件：碰到沒有勾選 Is Trigger 的物件時執行一次
    /// </summary>
    /// <param name="collision">碰到物件的碰撞資訊</param>
    private void OnCollisionEnter(Collision collision)
    {
        // 如果碰到物件.標籤 等於 "房子" 或者 碰到物件.名字 等於 "地板"
        if (collision.gameObject.tag == "房子" || collision.gameObject.name == "地板")
        {
            // 音效管理器.播放音效(房子碰撞音效)
            soundManager.PlaySound(soundHit);
        }
    }
}
