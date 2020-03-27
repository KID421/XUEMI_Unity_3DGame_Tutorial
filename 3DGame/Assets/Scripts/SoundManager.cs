using UnityEngine;

// 需求元件(類型(音效來源))：添加此段，會在第一次套用此腳本自動加入音效來源元件
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// 音效來源：喇叭
    /// </summary>
    private AudioSource aud;

    // 事件：喚醒，會在 Start 事件前執行
    private void Awake()
    {
        // 音效來源 = 取得元件<音效來源>()
        aud = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="sound">想要播放的音效</param>
    public void PlaySound(AudioClip sound)
    {
        aud.PlayOneShot(sound);                     // 音效來源.播放一次(音效)
    }

    /// <summary>
    /// 幫放背景音樂
    /// </summary>
    /// <param name="sound">想要播放的背景音樂</param>
    /// <param name="loop">是否要循環</param>
    public void PlayBGM(AudioClip sound, bool loop)
    {
        aud.loop = loop;        // 音效來源.循環 = 參數循環
        aud.clip = sound;       // 音效來源.片段 = 參數背景音樂
        aud.Play();             // 音效來源.播放()
    }
}
