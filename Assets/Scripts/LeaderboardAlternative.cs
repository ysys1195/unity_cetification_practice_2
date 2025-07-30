using System;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardAlternative : MonoBehaviour
{
  public string[] formattedTimes;
  private static int saveNumber = 5; // ベストタイムの数
  private List<float> savedTimes = new List<float>(new float[saveNumber]);


  private void OnEnable()
  {
    GameEventAlternative.OnRaceEnd += CheckRaceTime;
  }

  private void OnDisable()
  {
    GameEventAlternative.OnRaceEnd -= CheckRaceTime;
  }

  private void Start()
  {
    CheckIfPrefsSet();
    GetBestTimes();
    // PlayerPrefs.DeleteAll();
  }

  private void GetBestTimes()
  {
    for (int i = 0; i < saveNumber; i++)
    {
      var key = $"fastTime{i + 1}";
      if (PlayerPrefs.HasKey(key))
      {
        savedTimes[i] = PlayerPrefs.GetFloat(key);
      }
    }

    FormatTimesToString();
  }

  private void FormatTimesToString()
  {
    for (int i = saveNumber - 1; i >= 0; i--)
    {
      TimeSpan t = TimeSpan.FromSeconds(savedTimes[i]);
      formattedTimes[i] = t.ToString("m':'ss':'ff");
    }
  }

  private void SetBestTimes()
  {
    for (int i = 0; i < saveNumber; i++)
    {
      PlayerPrefs.SetFloat($"fastTime{i + 1}", savedTimes[i]);
    }

    FormatTimesToString();
  }

  private void CheckRaceTime()
  {
    int scorePosition = int.MaxValue;
    bool hightScore = false;

    for (int i = saveNumber - 1; i >= 0; i--)
    {
      if (RaceTimerAlternative.Instance.raceTime < savedTimes[i] || savedTimes[i] == 0)
      {
        hightScore = true;
        if (i < scorePosition)
        {
          scorePosition = i;
        }
      }
    }

    if (hightScore)
    {
      savedTimes.Insert(scorePosition, RaceTimerAlternative.Instance.raceTime);
      SetBestTimes();
    }
  }

  private void CheckIfPrefsSet()
  {
    for (int i = 0; i <= saveNumber; i++)
    {
      if (!PlayerPrefs.HasKey("fastTime" + i.ToString()))
      {
        PlayerPrefs.SetFloat("fastTime" + i.ToString(), 0);
      }
    }
  }

}