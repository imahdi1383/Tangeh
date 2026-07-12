using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ModireBazi : MonoBehaviour
{
    public static ModireBazi Instance { get; private set; }

    [Header("Rial")]
    [SerializeField] private int mablagheRialAvalie = 100;

    [FormerlySerializedAs("moneyText")]
    [SerializeField] private TextMeshProUGUI matneMablagheRial;

    [Header("UI")]
    [SerializeField] private GameObject panelePiroozi;
    [SerializeField] private GameObject paneleShekast;
    [SerializeField] private ModireMoj modireMoj;

    private int mablagheRial;
    private bool baziTamamShode;
    private bool pirooziShode;

    public int MablagheRial => mablagheRial;
    public bool BaziTamamShode => baziTamamShode;
    public bool PirooziShode => pirooziShode;
    public bool ShekastKhorde => baziTamamShode && !pirooziShode;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (panelePiroozi != null)
            panelePiroozi.SetActive(false);

        if (paneleShekast != null)
            paneleShekast.SetActive(false);
    }

    private void Start()
    {
        mablagheRial = mablagheRialAvalie;

        if (modireMoj == null)
            modireMoj = FindObjectOfType<ModireMoj>();

        UpdateMablagheRial();
    }

    public bool SpendRial(int hazineyeSakht)
    {
        if (baziTamamShode || mablagheRial < hazineyeSakht)
            return false;

        mablagheRial -= hazineyeSakht;
        UpdateMablagheRial();
        return true;
    }

    private void UpdateMablagheRial()
    {
        if (matneMablagheRial != null)
            matneMablagheRial.text = mablagheRial + " Rial";
    }

    public void NamayeshePanelePiroozi()
    {
        if (baziTamamShode)
            return;

        baziTamamShode = true;
        pirooziShode = true;

        if (panelePiroozi != null)
            panelePiroozi.SetActive(true);

        if (modireMoj != null)
            modireMoj.TavaqofSpawn();
    }

    public void NamayeshePaneleShekast()
    {
        if (baziTamamShode)
            return;

        baziTamamShode = true;
        pirooziShode = false;

        if (paneleShekast != null)
            paneleShekast.SetActive(true);

        if (modireMoj != null)
            modireMoj.TavaqofSpawn();
    }

    public void DokmeyeBaziyeMojadad()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }

    public void DokmeyeKhorooj()
    {
#if UNITY_EDITOR
        Debug.Log("DokmeyeKhorooj: Application.Quit is ignored in the Unity Editor.");
#endif
        Application.Quit();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
