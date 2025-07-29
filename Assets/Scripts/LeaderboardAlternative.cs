using System;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardAlternative : MonoBehaviour
{
  public string[] formattedTimes;
  private List<float> savedTimes = new List<float>(new float[5]);

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
    if (PlayerPrefs.HasKey("fastTime1"))
    {
      savedTimes[0] = PlayerPrefs.GetFloat("fastTime1");
    }

    if (PlayerPrefs.HasKey("fastTime2"))
    {
      savedTimes[1] = PlayerPrefs.GetFloat("fastTime2");
    }

    if (PlayerPrefs.HasKey("fastTime3"))
    {
      savedTimes[2] = PlayerPrefs.GetFloat("fastTime3");
    }

    if (PlayerPrefs.HasKey("fastTime4"))
    {
      savedTimes[3] = PlayerPrefs.GetFloat("fastTime4");
    }

    if (PlayerPrefs.HasKey("fastTime5"))
    {
      savedTimes[4] = PlayerPrefs.GetFloat("fastTime5");
    }

    FormatTimesToString();
  }

  private void FormatTimesToString()
  {
    for (int i = 4; i >= 0; i--)
    {
      TimeSpan t = TimeSpan.FromSeconds(savedTimes[i]);
      formattedTimes[i] = t.ToString("m':'ss':'ff");
    }
  }

  private void SetBestTimes()
  {
    PlayerPrefs.SetFloat("fastTime1", savedTimes[0]);
    PlayerPrefs.SetFloat("fastTime2", savedTimes[1]);
    PlayerPrefs.SetFloat("fastTime3", savedTimes[2]);
    PlayerPrefs.SetFloat("fastTime4", savedTimes[3]);
    PlayerPrefs.SetFloat("fastTime5", savedTimes[4]);

    FormatTimesToString();
  }

  private void CheckRaceTime()
  {
    int scorePosition = int.MaxValue;
    bool hightScore = false;

    for (int i = 4; i >= 0; i--)
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
    for (int i = 0; i <= 5; i++)
    {
      if (!PlayerPrefs.HasKey("fastTime" + i.ToString()))
      {
        PlayerPrefs.SetFloat("fastTime" + i.ToString(), 0);
      }
    }
  }

}