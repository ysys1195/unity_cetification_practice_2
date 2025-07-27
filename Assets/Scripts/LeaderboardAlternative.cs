using System.Collections.Generic;
using UnityEngine;

public class LeaderboardAlternative : MonoBehaviour
{
  public static LeaderboardAlternative Instance { get; private set; }
  public int TotalRaceCount { get; private set; }
  private const string PLAYER_PREFS_KEY = "TotalRaceCount";
  private const string BEST_TIMES_KEY = "BestTimes";

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
    LoadBestTimes();
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

  public void TryAddBestTime(float newTime)
  {
    BestTimes.Add(newTime);
    BestTimes.Sort();
    if (BestTimes.Count > 5)
    {
      BestTimes = BestTimes.GetRange(0, 5);
    }
    SaveBestTimes();
  }

  private void SaveBestTimes()
  {
    for (int i = 0; i < BestTimes.Count; i++)
    {
      PlayerPrefs.SetFloat($"{BEST_TIMES_KEY}_{i}", BestTimes[i]);
      Debug.Log($"No. {BEST_TIMES_KEY}_{i}, Time: {BestTimes[i]}");
    }
    PlayerPrefs.SetInt($"{BEST_TIMES_KEY}_Count", BestTimes.Count);
    PlayerPrefs.Save();
  }

  private void LoadBestTimes()
  {
    BestTimes.Clear();
    int count = PlayerPrefs.GetInt($"{BEST_TIMES_KEY}_Count", 0);
    for (int i = 0; i < count; i++)
    {
      float time = PlayerPrefs.GetFloat($"{BEST_TIMES_KEY}_{i}", float.MaxValue);
      BestTimes.Add(time);
    }
  }
}