using UnityEngine;

public class House : MonoBehaviour
{
    /// <summary>
    /// 是否降落中
    /// </summary>
    [HideInInspector]
    public bool down;

    /// <summary>
    /// 房子管理器
    /// </summary>
    private HouseManager houseManaher;

    private void Start()
    {
        // 房子管理器 = 透過類型尋找物件<類型>()
        houseManaher = FindObjectOfType<HouseManager>();
    }

    /// <summary>
    /// 觸發開始事件：碰到碰撞器勾選 IsTrigger 的物件會執行一次
    /// </summary>
    /// <param name="other">儲存碰到物件的碰撞資訊</param>
    private void OnTriggerEnter(Collider other)
    {
        // 如果 降落中 並且 碰到物件.標籤 等於 "失敗區域"
        if (down && other.tag == "失敗區域")
        {
            // 房子管理器.延遲調用函式("遊戲結束"，0.5 秒)
            houseManaher.Invoke("GameOver", 0.5f);
        }
    }
}
