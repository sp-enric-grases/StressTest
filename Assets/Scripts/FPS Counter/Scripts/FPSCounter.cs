using System.Collections;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public float update = 0.1f;
    public TMP_Text textPFS;
    public int fpsLimitWarning = 60;

    private WaitForSeconds updateInterval;

    private float timeleft = 0;
    private float frames = 0;
    private float accum = 0;

    public static FPSCounter Instance;

    public float AverageFPS
    {
        get { return Mathf.RoundToInt((accum / frames)); }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    /// <summary>
    /// v1
    /// </summary>
    void OnEnable()
    {
        timeleft = update;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft <= 0.0)
        {
            textPFS.text = AverageFPS.ToString();
            timeleft = update;
            accum = 0;
            frames = 0;
        }
    }

    /// <summary>
    /// v2
    /// </summary>
    //void OnEnable ()
    //{
    //    updateInterval = new WaitForSeconds(update);
    //    StartCoroutine(ShowFPS());
    //}

    //IEnumerator ShowFPS()
    //{
    //    yield return updateInterval;

    //    int fps = Mathf.RoundToInt(1.0f / Time.deltaTime);
    //    textPFS.text = fps.ToString();
    //    textPFS.color = fps <= (fpsLimitWarning - 1) ? Color.red : Color.green;

    //    StartCoroutine(ShowFPS());
    //}

    /// <summary>
    /// v3
    /// </summary>
    //void OnEnable()
    //{
    //    InvokeRepeating("ShowFPS", 0, update);
    //}

    //private void ShowFPS()
    //{
    //    int fps = Mathf.RoundToInt(1.0f / Time.deltaTime);
    //    textPFS.text = fps.ToString();
    //    textPFS.color = fps <= (fpsLimitWarning - 1) ? Color.red : Color.green;
    //}
}
