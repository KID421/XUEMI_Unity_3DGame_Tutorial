using UnityEngine;

public class LearnIf : MonoBehaviour
{
    public bool isOpen;
    public int score = 100;

    private void Start()
    {
        // 如果 (布林值) { 敘述 }
        if (true)
        {
            print("當布林值為 true 會執行此敘述。");
        }
    }

    private void Update()
    {
        if (isOpen)
        {
            print("開門");        // 布林值為 true 執行此區塊
        }
        else
        {
            print("關門");        // 布林值為 false 執行此區塊
        }

        if (score >= 60)
        {
            print("及格");
        }
        else if (score >= 40)
        {
            print("補考");
        }
        else
        {
            print("當掉");
        }
    }
}
