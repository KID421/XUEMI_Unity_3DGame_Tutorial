using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void Quit()
    {
        Application.Quit();                 // 應用程式.離開
    }

    /// <summary>
    /// 重新遊戲
    /// </summary>
    /// <param name="scene">場景名稱</param>
    public void Replay(string scene)
    {
        SceneManager.LoadScene(scene);      // 場景管理器.載入場景(場景名稱)
    }
}
