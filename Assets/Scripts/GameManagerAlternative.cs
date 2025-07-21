using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerAlternative : MonoBehaviour
{
  [SerializeField] private GameObject RaceOverPanel;
  [SerializeField] private TextMeshProUGUI raceTimeText;
  [SerializeField] private GameObject screenOverlay;
  private readonly int FIRST_SCENE_INDEX = 0;

  private void Start()
  {
    RaceOverPanel.SetActive(false);

    // 白カバーをフェードアウト
    screenOverlay.GetComponent<Image>().CrossFadeAlpha(0f, 1f, false);
  }

  private void OnEnable()
  {
    GameEventAlternative.OnRaceEnd += ShowRaceOverCanvas;
    GameEventAlternative.OnRaceRetry += ReloadScene;
    GameEventAlternative.OnRaceNextLevel += LoadNextScene;
    GameEventAlternative.OnQuitGame += QuitGame;
  }

  private void OnDisable()
  {
    GameEventAlternative.OnRaceEnd -= ShowRaceOverCanvas;
    GameEventAlternative.OnRaceRetry -= ReloadScene;
    GameEventAlternative.OnRaceNextLevel -= LoadNextScene;
    GameEventAlternative.OnQuitGame -= QuitGame;
  }

  private void ShowRaceOverCanvas()
  {
    raceTimeText.text = RaceTimerAlternative.Instance.raceTime.ToString("F2") + "sec";
    RaceOverPanel.SetActive(true);
  }

  private void ReloadScene()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    Debug.Log("シーンをリロードしました。");
  }

  private void LoadNextScene()
  {
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    {
      SceneManager.LoadScene(nextSceneIndex);
      Debug.Log("次のシーンをロードしました。");
    }
    else
    {
      Debug.Log("これ以上のシーンはありません。最初のシーンに戻ります。");
      SceneManager.LoadScene(FIRST_SCENE_INDEX);
    }
  }

  private IEnumerator QuitGame()
  {
    // 鳴っている音を全て止める
    AudioListener.pause = true; // 音を一時停止
    AudioListener.volume = 0; // 音量を0にする

    // 白カバーをフェードイン
    screenOverlay.GetComponent<Image>().CrossFadeAlpha(1f, 1f, false);

    yield return new WaitForSeconds(1f); // 1秒待機

    Debug.Log("ゲームを終了します。");
    Application.Quit();

    // エディタでのテスト用
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
  }
}
