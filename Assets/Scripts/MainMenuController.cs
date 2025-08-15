using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private CanvasGroup buttonsPanel;
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private CanvasGroup confirmQuitPanel;
    [SerializeField] private Button confirmYesButton;
    [SerializeField] private Button confirmNoButton;

    void Start()
    {
        playButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(ConfirmQuit);
        confirmYesButton.onClick.AddListener(QuitGame);
        confirmNoButton.onClick.AddListener(rejectQuitGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("SkiCorrectScene");
    }

    private void ConfirmQuit()
    {
        // ボタン群を非表示
        buttonsPanel.alpha = 0f;
        buttonsPanel.interactable = false;
        // 確認パネルのalphaを表示
        confirmQuitPanel.alpha = 1f;
        confirmQuitPanel.interactable = true;
    }

    private void QuitGame()
    {
        Application.Quit();

        // エディタでのテスト用
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void rejectQuitGame()
    {
        // 確認パネルを非表示
        confirmQuitPanel.alpha = 0f;
        confirmQuitPanel.interactable = false;
        // ボタン群を再表示
        buttonsPanel.alpha = 1f;
        buttonsPanel.interactable = true;
    }
}
