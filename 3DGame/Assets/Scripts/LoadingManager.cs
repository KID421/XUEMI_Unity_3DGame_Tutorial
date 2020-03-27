using UnityEngine;
using UnityEngine.UI;                       // 引用 Unity 介面 API
using UnityEngine.SceneManagement;          // 引用 Unity 場景管理 API
using System.Collections;                   // 引用 系統.集合 API

public class LoadingManager : MonoBehaviour
{
    [Header("載入畫面")]
    public GameObject loadingPanel;
    [Header("載入進度：文字")]
    public Text textLoading;
    [Header("載入進度：圖片")]
    public Image imageLoading;
    [Header("提示文字")]
    public GameObject tip;

    /// <summary>
    /// 載入場景
    /// </summary>
    private IEnumerator Loading()
    {
        loadingPanel.SetActive(true);                                               // 顯示載入畫面

        // 非同步作業資料 ao = 場景管理器.非同步載入場景("場景名稱")
        // 非同步載入場景：不等待其他任務，直接進行載入場景的任務稱之為非同步載入
        AsyncOperation ao = SceneManager.LoadSceneAsync("蓋房子");       
        ao.allowSceneActivation = false;                                            // 非同步作業資料.是否允許啟動場景 = 否 (避免自動進入場景)

        while (!ao.isDone)                                                          // 當 (非同步作業資料.完成 等於 false)
        {
            // 載入文字.文字 = 進度
            // ao.progress 會取得當前載入的進度，值為 0 - 0.9f
            // 為了讓百分比達到 1 所以必須除以 0.9f
            // 乘上 100 是將 1.0 * 100 變成 100
            // ToString("F0") 將數字轉為字串並設定小數點 0 位數
            textLoading.text = (ao.progress / 0.9f * 100).ToString("F0") + " %";
            // 載入圖片.填滿數值 = 進度
            imageLoading.fillAmount = ao.progress / 0.9f;
            // 傳回 null 為等待一個影格，約為 1/60 秒，讓給個影格偵測載入進度
            yield return null;

            if (ao.progress >= 0.9f)                                                // 如果 非同步作業資料.進度 >= 0.9f
            {
                tip.SetActive(true);                                                // 顯示提示文字

                if (Input.anyKey) ao.allowSceneActivation = true;                   // 如果玩家按下任意鍵 允許進入場景
            }
        }
    }

    /// <summary>
    /// 讓按鈕呼叫的方法：啟動載入協程
    /// </summary>
    public void StartLoading()
    {
        StartCoroutine(Loading());      // 啟動載入協程
    }
}
