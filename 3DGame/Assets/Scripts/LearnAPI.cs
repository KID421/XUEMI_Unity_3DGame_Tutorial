using UnityEngine;

public class LearnAPI : MonoBehaviour
{
    public Light lightA;                    // 使用非靜態成員之前必須先給予一個欄位，語法：類別 欄位名稱

    private void Start()
    {
        print(Random.value);                // 使用靜態屬性：類別.靜態屬性

        print(Random.Range(0f, 10f));       // 使用靜態函式：類別.靜態函式(對應引數)
        print(Random.Range(0, 10));

        lightA.color = Color.red;           // 使用非靜態屬性：欄位名稱.非靜態屬性

        lightA.Reset();                     // 使用非靜態函式：欄位名稱.非靜態函式(對應引數)
    }
}
