using UnityEngine;
using System.Collections;       // 引用 系統.集合 API：提供協程所需的內容

public class LearnCoroutineAndWhile : MonoBehaviour
{
    // 協同程式方法：注意必須將傳回類型設定為 IEnumerator 注意這個關鍵字喔！
    private IEnumerator CoroutineMethod()
    {
        print("嗨，我是協程的第一段程式。");

        // yield 讓 
        // return new WaitForSeconds(秒數)
        yield return new WaitForSeconds(2);         // 讓協程等待兩秒鐘再執行下方的敘述

        print("我在兩秒後會執行喔~");
    }

    private void Start()
    {
        StartCoroutine(CoroutineMethod());          // 啟動協程：讓協程開始執行

        int count = 1;          // 區域變數 僅在此區塊內允許存取 (區塊指的是大括號)

        // 語法：while (布林值) { 敘述：當布林值為 true 時重複執行 }
        // 當 count 小於 10 時執行敘述
        while (count < 10)
        {
            // 輸出當前數字
            print(count);
            // 數字遞增
            count++;
        }

        StartCoroutine(CreateEnemy(10));                // 啟動協程
    }

    /// <summary>
    /// 每個 0.5 秒生成一隻怪物
    /// </summary>
    /// <param name="enemyCount">要生成的怪物數量</param>
    private IEnumerator CreateEnemy(int enemyCount)
    {
        int enemy = 1;                                  // 目前怪物編號

        while (enemy <= enemyCount)                     // 當怪物編號 <= 要生成的怪物數量
        {
            print("生成怪物，編號為：" + enemy);          // 輸出怪物編號
            enemy++;                                    // 編號遞增
            yield return new WaitForSeconds(0.5f);      // 等待 0.5 秒
        }
    }
}
