using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private CanvasGroup buttonsPanel;
    [SerializeField] private Button playButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private CanvasGroup helpPanel;
    [SerializeField] private Button helpBackButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private CanvasGroup confirmQuitPanel;
    [SerializeField] private Button confirmYesButton;
    [SerializeField] private Button confirmNoButton;
    [SerializeField] private Image overlay;
    public int LoadSceneID;
    public float fadeDuration;

    // ▼ イースターエッグ（タイトルクリックでバウンド）
    [Header("Title (Easter Egg)")]
    [SerializeField] private Button title;
    [SerializeField] private float bounceDuration = 0.35f; // 1回のバウンド時間
    [SerializeField] private float bounceScale = 1.18f; // 最大スケール倍率
    [SerializeField] private int bounceRepeat = 1; // 何回バウンドさせるか（1推奨）
    private bool isBouncing;

    void Start()
    {
        playButton.onClick.AddListener(() => StartCoroutine(StartGame()));
        helpButton.onClick.AddListener(OpenHelpPanel);
        helpBackButton.onClick.AddListener(HelpToTitle);
        quitButton.onClick.AddListener(ConfirmQuit);
        confirmYesButton.onClick.AddListener(() => StartCoroutine(QuitGame()));
        confirmNoButton.onClick.AddListener(rejectQuitGame);
        title.onClick.AddListener(OnTitleClick);
    }

    private IEnumerator StartGame()
    {
        overlay.DOFade(1, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(LoadSceneID);
    }

    private void OpenHelpPanel()
    {
        helpPanel.DOFade(1, fadeDuration);
        helpPanel.interactable = true;
        helpPanel.blocksRaycasts = true;
        // ボタン群を非表示
        buttonsPanel.DOFade(0, fadeDuration);
        buttonsPanel.interactable = false;
    }

    private void HelpToTitle()
    {
        helpPanel.DOFade(0, fadeDuration);
        helpPanel.interactable = false;
        helpPanel.blocksRaycasts = false;
        // ボタン群を再表示
        buttonsPanel.DOFade(1, fadeDuration);
        buttonsPanel.interactable = true;
    }

    private void ConfirmQuit()
    {
        // ボタン群を非表示
        buttonsPanel.DOFade(0, fadeDuration);
        buttonsPanel.interactable = false;
        // 確認パネルのalphaを表示
        confirmQuitPanel.DOFade(1, fadeDuration);
        confirmQuitPanel.interactable = true;
        confirmQuitPanel.blocksRaycasts = true;
    }

    private IEnumerator QuitGame()
    {
        overlay.DOFade(1, 1f);
        // 少し待ってからアプリケーションを終了
        yield return new WaitForSeconds(1f);
        Application.Quit();
        Debug.Log("アプリケーションを終了しました。");

        // エディタでのテスト用
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void rejectQuitGame()
    {
        // 確認パネルを非表示
        confirmQuitPanel.DOFade(0, fadeDuration);
        confirmQuitPanel.interactable = false;
        confirmQuitPanel.blocksRaycasts = false;
        // ボタン群を再表示
        buttonsPanel.DOFade(1, fadeDuration);
        buttonsPanel.interactable = true;
    }

    private void OnTitleClick()
    {
        var titleText = title.GetComponentInChildren<TextMeshProUGUI>();
        if (!isBouncing && title != null)
            StartCoroutine(Bounce(titleText.rectTransform));
    }

    private IEnumerator Bounce(RectTransform rt)
    {
        isBouncing = true;
        Vector3 originalScale = title.transform.localScale;

        for (int i = 0; i < bounceRepeat; i++)
        {
            // 伸びる（イージング：EaseOut）
            float t = 0f;
            while (t < bounceDuration * 0.45f)
            {
                t += Time.unscaledDeltaTime; // タイトル画面なので unscaled 推奨
                float p = t / (bounceDuration * 0.45f);
                // EaseOutQuad
                float k = 1f - (1f - p) * (1f - p);
                rt.localScale = Vector3.LerpUnclamped(originalScale, originalScale * bounceScale, k);
                yield return null;
            }

            // しぼむ（オーバーシュートで“ポヨン”）
            t = 0f;
            while (t < bounceDuration * 0.55f)
            {
                t += Time.unscaledDeltaTime;
                float p = t / (bounceDuration * 0.55f);
                // EaseOutBack 風（オーバーシュート）
                const float s = 1.70158f;
                float k = 1 + ((p - 1) * (p - 1) * ((s + 1) * (p - 1) + s));
                rt.localScale = Vector3.LerpUnclamped(originalScale * bounceScale, originalScale, k);
                yield return null;
            }
            rt.localScale = originalScale;
        }

        isBouncing = false;
    }
}
