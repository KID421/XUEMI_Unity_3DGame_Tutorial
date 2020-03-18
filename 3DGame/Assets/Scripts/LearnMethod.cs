using UnityEngine;

public class LearnMethod : MonoBehaviour
{
    // 事件：開始 - 播放遊戲時執行一次
    private void Start()
    {
        MyMethod();
        HelloPeople("KID");
        Walk("前方", 30);               // 呼叫函式，必須填入對應的【引數】
        Jump("強力跳躍");               // 選填式參數如果不填入，會以預設值執行 

        Shoot();                       // 使用第一種
        Shoot("炸彈");                 // 使用第二種
    }

    // 修飾詞 傳回類型 函式名稱 (參數) { 敘述 }
    public void MyMethod()
    {
        // 輸出 (輸出訊息);
        print("嗨，我是函式。");
    }

    public void Hello()
    {
        print("哈囉~");
    }

    // (參數)：小括號內可以放參數
    // 參數語法：類型 名稱
    public void HelloPeople(string people)
    {
        print("哈囉：" + people);
    }

    // 多個參數可以用逗號＂，＂隔開，參數沒有上限
    public void Walk(string direction, int speed)
    {
        print("走路方向：" + direction);
        print("走路速度：" + speed);
    }

    // 當參數有預設值時，可以用＂= 值＂給予
    // ※ 有預設值的參數稱之為【選填式參數】必須放在最後面
    public void Jump(string type, float height = 3.5f)
    {
        print("跳躍方式：" + type);
        print("跳躍高度：" + height);
    }


    // 可以定義相同名稱的函式
    // 參數數量不同或者參數類型不同即可
    // 稱之為【函式多載】

    /// <summary>
    /// 發射：子彈
    /// </summary>
    public void Shoot()
    {
        print("發射：子彈");
    }

    /// <summary>
    /// 發射：自行決定物件
    /// </summary>
    /// <param name="prop">想發射的物件</param>
    public void Shoot(string prop)
    {
        print("發射：" + prop);
    }
}
