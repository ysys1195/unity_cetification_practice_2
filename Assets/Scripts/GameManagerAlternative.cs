using TMPro;
using UnityEngine;

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

  public void ShowRaceOverCanvas()
  {
    raceTimeText.text = RaceTimerAlternative.Instance.raceTime.ToString("F2") + "sec";
    RaceOverCanvas.SetActive(true);
  }
}
