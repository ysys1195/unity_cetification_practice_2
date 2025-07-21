using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerAlternative : MonoBehaviour
{
  public static GameManagerAlternative Instance { get; private set; }
  [SerializeField] private GameObject RaceOverCanvas;
  [SerializeField] private TextMeshProUGUI raceTimeText;
  private readonly int FIRST_SCENE_INDEX = 0;

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
  }

  private void OnEnable()
  {
    GameEventAlternative.OnRaceEnd += ShowRaceOverCanvas;
    GameEventAlternative.OnRaceRetry += ReloadScene;
    GameEventAlternative.OnRaceNextLevel += LoadNextScene;
  }

  private void ShowRaceOverCanvas()
  {
    raceTimeText.text = RaceTimerAlternative.Instance.raceTime.ToString("F2") + "sec";
    RaceOverCanvas.SetActive(true);
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
}
