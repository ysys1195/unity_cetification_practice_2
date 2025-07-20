using UnityEngine;

public class RaceTimerAlternative : MonoBehaviour
{
  public static RaceTimerAlternative Instance { get; private set; }
  public float raceTime { get; private set; }
  private bool isRacing = false;

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
  }

  private void Update()
  {
    if (isRacing == true)
    {
      raceTime += Time.deltaTime; // タイマーを更新
    }
  }

  public void StartRace()
  {
    isRacing = true;
    raceTime = 0f; // タイマーをリセット
    Debug.Log("レースがスタートしました。タイマーをリセットしました。");
  }

  public void StopRace()
  {
    isRacing = false;
    Debug.Log("レースが終わりました。レースタイム： " + raceTime + "秒");
    GameManagerAlternative.Instance.ShowRaceOverCanvas();
  }

  public void AddTime(float timeToAdd)
  {
    if (isRacing)
    {
      raceTime += timeToAdd;
      Debug.Log("合計時間が " + timeToAdd + "秒 増えました。現在の合計時間: " + raceTime + "秒");
    }
  }
}