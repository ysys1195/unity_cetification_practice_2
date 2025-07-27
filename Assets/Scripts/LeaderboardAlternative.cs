using System.Collections.Generic;
using UnityEngine;

public class LeaderboardAlternative : MonoBehaviour
{
  public static LeaderboardAlternative Instance { get; private set; }
  public int TotalRaceCount { get; private set; }
  private const string PLAYER_PREFS_KEY = "TotalRaceCount";

  public List<float> BestTimes { get; private set; } = new List<float>();

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    LoadRaceCount();
    DontDestroyOnLoad(gameObject);
  }

  public void AddRaceCount()
  {
    TotalRaceCount++;
    SaveRaceCount();
    Debug.Log($"レースの回数が増えました。現在のレース回数: {TotalRaceCount}");
  }

  public void SaveRaceCount()
  {
    PlayerPrefs.SetInt(PLAYER_PREFS_KEY, TotalRaceCount);
    PlayerPrefs.Save();
  }

  private void LoadRaceCount()
  {
    TotalRaceCount = PlayerPrefs.GetInt(PLAYER_PREFS_KEY, 0);
  }

  private void OnDestroy()
  {
    Debug.Log("LeaderboardAlternativeが破棄されました。レース回数を保存しました。");
  }
}