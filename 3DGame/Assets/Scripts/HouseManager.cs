using UnityEngine;

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

    private void Start()
    {
        CreateHouse();                          // 呼叫生成房子函式
        InvokeRepeating("Shake", 0, 3);         // 重複調用函式("函式名稱"，開始時間，重複頻率)
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
        tempHouse = Instantiate(houses[0], pointShake);
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
        tempHouse.transform.SetParent(null);                        // 暫存房子.變形.設定父物件(無)
        tempHouse.GetComponent<Rigidbody>().isKinematic = false;    // 暫存房子.取得元件<剛體>().運動學 = false
        Invoke("CreateHouse", 1);                                   // 延遲調用函式("函式名稱"，延遲時間)
        startHouse = true;                                          // 開始蓋房子

        // 房子總高度 遞增指定 暫存房子.取得元件<盒形碰撞器>().尺寸.y * 房子尺寸.y
        // 有些房子有縮放，例如大房子縮小到 0.7 所以實際尺寸為碰撞器 * 尺寸
        height += tempHouse.GetComponent<BoxCollider>().size.y * tempHouse.transform.localScale.y;
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
}
