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
            matneMablagheRial.text = TabdileAdadeFarsi(mablagheRial.ToString()) + " ریال";
    }

    public void NamayeshePanelePiroozi()
    {
        PayaneBazi(true, "MenoyePiroozi");
    }

    public void NamayeshePaneleShekast()
    {
        PayaneBazi(false, "MenoyeShekast");
    }

    private void PayaneBazi(bool pirooziAst, string esmeSceneNatije)
    {
        if (baziTamamShode)
            return;

        baziTamamShode = true;
        pirooziShode = pirooziAst;

        if (modireMoj != null)
            modireMoj.TavaqofSpawn();

        string esmeMarhaleyeFeli = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("AkharinMarhale", esmeMarhaleyeFeli);

        if (pirooziAst && esmeMarhaleyeFeli == "Marhale1")
            PlayerPrefs.SetString("MarhaleyeBadi", "Marhale2");

        PlayerPrefs.Save();

        Time.timeScale = 1f;
        SceneManager.LoadScene(esmeSceneNatije);
    }

    private string TabdileAdadeFarsi(string matn)
    {
        char[] adadeFarsi = { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };

        for (int i = 0; i < adadeFarsi.Length; i++)
            matn = matn.Replace((char)('0' + i), adadeFarsi[i]);

        return matn;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
