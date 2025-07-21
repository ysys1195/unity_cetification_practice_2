using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerAlternative : MonoBehaviour
{
  public static GameManagerAlternative Instance { get; private set; }
  [SerializeField] private GameObject RaceOverCanvas;
  [SerializeField] private TextMeshProUGUI raceTimeText;

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
}
