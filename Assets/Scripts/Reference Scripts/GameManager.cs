﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Race Over Popup UI
    public GameObject raceOverUI;
    // Overlay UI Image for screen transitions
    public GameObject overlayScreen;
    //The Scene Build Index for our next level
    public int nextScendIndex;


    private void OnEnable()
    {
        GameEvents.OnRaceStop += ShowRaceOverUI;
        GameEvents.OnRetryRace += RestartRace;
        GameEvents.OnQuitGame += ShutDownGame;
        GameEvents.OnNextLevel += NextLevel;
    }

    private void OnDisable()
    {
        GameEvents.OnRaceStop -= ShowRaceOverUI;
        GameEvents.OnRetryRace -= RestartRace;
        GameEvents.OnQuitGame -= ShutDownGame;
        GameEvents.OnNextLevel -= NextLevel;

    }

    // Start is called before the first frame update
    void Start()
    {
        //At the start of our scene we will fade out the Overlay
        overlayScreen.GetComponent<Image>().CrossFadeAlpha(0, 1, false);
    }

    private void ShowRaceOverUI()
    {
        //turn on the game object
        raceOverUI.SetActive(true);

        GameData.Instance.completedRaces++;
        print ($"Completed Races: {GameData.Instance.completedRaces}");
    }

    private void RestartRace()
    {
        // reloads the game scene after pressing the start button (only after the player has reached the finish line
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextLevel()
    {
        StartCoroutine(ChangeLevel());
    }


    private IEnumerator ChangeLevel()
    {
        overlayScreen.GetComponent<Image>().CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextScendIndex);
    }


    private IEnumerator ShutDownGame()
    {
        //to simulate the game quitting we
        overlayScreen.GetComponent<Image>().CrossFadeColor(new Color(0, 0, 0, 1), 1, false, true);
        yield return new WaitForSeconds(1);
        // closes the game entirely (only when playing the build)
        Application.Quit();
        print("The Game Has Quit");
    }

 
}
