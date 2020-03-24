using UnityEngine;

public class LearnOperator : MonoBehaviour
{
    public int A = 10, B = 3;

    private void Start()
    {
        print(A > B);           // 大　　於：結果為 true
        print(A < B);           // 小　　於：結果為 false
        print(A >= B);          // 大於等於：結果為 true
        print(A <= B);          // 小於等於：結果為 false
        print(A == B);          // 等　　於：結果為 false
        print(A != B);          // 不　等於：結果為 true

        // 並且：只要有一個 false 結果為 false
        print(true && true);    // 結果為：true
        print(true && false);   // 結果為：false
        print(false && true);   // 結果為：false
        print(false && false);  // 結果為：false

        // 或者：只要有一個 true 結果為 true
        print(true || true);    // 結果為：true
        print(true || false);   // 結果為：true
        print(false || true);   // 結果為：true
        print(false || false);  // 結果為：false

        // 顛倒：將布林值給為相反得值
        print(!true);           // 結果為：false
        print(!false);          // 結果為：true
    }
}
