using UnityEngine;
using UnityEngine.UI;           // 引用 介面 API

public class HouseManager : MonoBehaviour
{
    // Transform 可以儲存物件的 Transform 元件，可以取得座標、角度或尺寸資訊
    // Rigidbody 可以儲存物件的 Rigidbody 元件，可以取得物理資訊
    // GameObject 可以儲存預製物或場景上的物件

    [Header("懸吊房子物件")]
    public Transform pointSuspention;
    [Header("晃動位置")]
    public Transform pointShake;
    [Header("晃動位置剛體")]
    public Rigidbody pointShakeRig;
    [Header("房子預製物陣列")]
    public GameObject[] houses;
    [Header("晃動力道"), Range(0.5f, 10)]
    public float shakePower = 2;
    [Header("攝影機")]
    public Transform myCamera;
    [Header("檢查遊戲失敗牆壁")]
    public Transform checkWall;
    [Header("結算畫面")]
    public GameObject final;
    [Header("蓋房子數量文字介面")]
    public Text textHouseCount;
    [Header("最佳數量文字介面")]
    public Text textBest;
    [Header("本次數量文字介面")]
    public Text textCurrent;
    [Header("生成房子音效")]
    public AudioClip soundCreateHouse;
    [Header("遊戲開始背景音樂")]
    public AudioClip soundBGMStatrt;
    [Header("遊戲結束背景音樂")]
    public AudioClip soundBGMGameOVer;

    /// <summary>
    /// 儲存生成出來的房子
    /// </summary>
    private GameObject tempHouse;
    /// <summary>
    /// 開始蓋房子
    /// </summary>
    private bool startHouse;
    /// <summary>
    /// 房子總高度
    /// </summary>
    private float height;
    /// <summary>
    /// 第一個房子
    /// </summary>
    private Transform firstHouse;
    /// <summary>
    /// 房子總數
    /// </summary>
    private int count;
    /// <summary>
    /// 房子管理器
    /// </summary>
    private SoundManager soundManager;
    /// <summary>
    /// 遊戲結束
    /// </summary>
    private bool gameOver;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.PlayBGM(soundBGMStatrt, true);         // 音效管理器.播放背景音樂(開始，要循環)
        CreateHouse();                                      // 呼叫生成房子函式
        InvokeRepeating("Shake", 0, 3);                     // 重複調用函式("函式名稱"，開始時間，重複頻率)
    }

    private void Update()
    {
        Track();
    }

    /// <summary>
    /// 生成房子
    /// </summary>
    private void CreateHouse()
    {
        // 儲存生出來的房子 = 實例化(房子預製物陣列[第一個]，晃動位置)
        // 判斷數量調整生成房子：小於 5 第一個，小於 10 第二個，其他第三個
        if (count < 5)
            tempHouse = Instantiate(houses[0], pointShake);
        else if (count < 10)
            tempHouse = Instantiate(houses[1], pointShake);
        else
            tempHouse = Instantiate(houses[2], pointShake);

        soundManager.PlaySound(soundCreateHouse);
    }

    /// <summary>
    /// 晃動效果
    /// </summary>
    private void Shake()
    {
        // 晃動位置剛體.速度 = 晃動位置.右邊 * 力道
        pointShakeRig.velocity = pointShake.right * shakePower;
    }

    /// <summary>
    /// 蓋房子
    /// </summary>
    public void HouseDown()
    {
        if (gameOver || !tempHouse) return;                         // 如果 遊戲結束 或者 目前沒有房子 跳出

        tempHouse.transform.SetParent(null);                        // 暫存房子.變形.設定父物件(無)
        tempHouse.GetComponent<Rigidbody>().isKinematic = false;    // 暫存房子.取得元件<剛體>().運動學 = false
        tempHouse.GetComponent<House>().down = true;                // 暫存房子.取得元件<房子>().是否降落中 = true
        Invoke("CreateHouse", 1);                                   // 延遲調用函式("函式名稱"，延遲時間)
        startHouse = true;                                          // 開始蓋房子

        // 房子總高度 遞增指定 暫存房子.取得元件<盒形碰撞器>().尺寸.y * 房子尺寸.y
        // 有些房子有縮放，例如大房子縮小到 0.7 所以實際尺寸為碰撞器 * 尺寸
        height += tempHouse.GetComponent<BoxCollider>().size.y * tempHouse.transform.localScale.y;

        if (!firstHouse)                                            // 如果 還沒有第一個房子
        {
            firstHouse = tempHouse.transform;                       // 第一個房子 = 暫存房子.變形
            Invoke("CreateCheckWall", 1.2f);                        // 延遲調用函式("建立檢查遊戲失敗牆壁", 1.2 秒)
            Destroy(firstHouse.GetComponent<House>());              // 刪除(第一個房子.取得元件<房子>())
        }

        count++;                                                    // 房子總數遞增
        textHouseCount.text = "房子數量：" + count;                  // 蓋房子數量文字介面.文字 = "房子數量：" + 房子總數
        tempHouse = null;                                           // 目前沒有房子
    }

    /// <summary>
    /// 懸吊房子物件往上位移
    /// 攝影機追蹤目前高度
    /// </summary>
    private void Track()
    {
        // 如果 (開始蓋房子)
        if (startHouse)
        {
            // 攝影機新座標 = (0，房子總高度，-10)
            Vector3 posCam = new Vector3(0, height, -10);
            // 攝影機.座標 = 三維向量.插值(攝影機.座標，攝影機新座標，0.3 * 速度 * 一個影格時間)
            myCamera.position = Vector3.Lerp(myCamera.position, posCam, 0.3f * 10 * Time.deltaTime);

            // 懸掛房子物件新座標 = (0，房子總高度 + 6，0)
            Vector3 posSus = new Vector3(0, height + 6, 0);
            // 懸掛房子物件.座標 = 三維向量.插值(懸掛房子物件.座標，懸掛房子物件新座標，0.3 * 速度 * 一個影格時間)
            pointSuspention.position = Vector3.Lerp(pointSuspention.position, posSus, 0.3f * 10 * Time.deltaTime);
        }
    }

    /// <summary>
    /// 建立檢查遊戲失敗牆壁
    /// </summary>
    private void CreateCheckWall()
    {
        // 生成(檢查遊戲失敗牆壁，第一個房子.座標，零角度)
        Instantiate(checkWall, firstHouse.position, Quaternion.identity);
    }

    /// <summary>
    /// 遊戲結束：顯示結算畫面
    /// </summary>
    public void GameOver()
    {
        if (gameOver) return;                                         // 如果 遊戲結束 跳出
        gameOver = true;                                              // 遊戲結束

        final.SetActive(true);                                         // 結算畫面.啟動設定(顯示)

        textCurrent.text = "本次數量：" + count;                       // 本次數量文字介面.文字 = "本次數量： + 房子總數

        if (count > PlayerPrefs.GetInt("最佳數量"))                    // 如果 房子總數 > 玩家參考.取得整數("蓋房子最佳數量")
            PlayerPrefs.SetInt("最佳數量", count);                     // 玩家參考.設定整數("蓋房子最佳數量"，房子總數)

        textBest.text = "最佳數量：" + PlayerPrefs.GetInt("最佳數量");  // 最佳數量文字介面.文字 = "最佳數量：" + 玩家參考.取得整數("蓋房子最佳數量")
        soundManager.PlayBGM(soundBGMGameOVer, false);                // 音效管理器.播放背景音樂(結束，不循環)
    }
}
