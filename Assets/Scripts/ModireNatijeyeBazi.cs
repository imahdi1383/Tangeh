using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModireNatijeyeBazi : MonoBehaviour
{
    [SerializeField] private Button dokmeyeMarhaleyeBadi;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        if (dokmeyeMarhaleyeBadi == null)
            return;

        string esmeMarhaleyeBadi = PlayerPrefs.GetString("MarhaleyeBadi", string.Empty);
        dokmeyeMarhaleyeBadi.interactable =
            Application.CanStreamedLevelBeLoaded(esmeMarhaleyeBadi);
    }

    public void BaziyeMojadad()
    {
        string esmeMarhale = PlayerPrefs.GetString("AkharinMarhale", "Marhale1");
        BargozariyeScene(esmeMarhale);
    }

    public void MarhaleyeBadi()
    {
        string esmeMarhale = PlayerPrefs.GetString("MarhaleyeBadi", string.Empty);
        BargozariyeScene(esmeMarhale);
    }

    public void Khorooj()
    {
        #if UNITY_EDITOR
            Debug.Log("Khorooj az bazi dar Unity Editor ejra nemishavad.");
        #endif
        Application.Quit();
    }

    private void BargozariyeScene(string esmeScene)
    {
        if (!Application.CanStreamedLevelBeLoaded(esmeScene))
        {
            Debug.LogWarning("Scene morede nazar dar Build Settings mojood nist: " + esmeScene);
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(esmeScene);
    }
}
