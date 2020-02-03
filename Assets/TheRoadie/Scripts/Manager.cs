using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : Singleton<Manager>
{
    public float pissedIncreaseRate = 0.1f;
    public float pissedDecreaseRate = 0.01f;
    public float breakChance = 0.05f;
    public float survivalTime = 4 * 60f;

    public Player player;
    public SpotlightController[] spotlights;
    public Breakable[] breakables;
    public LayerMask raycastMask;
    public LoadingBar pissedBar;
    public LoadingBar fixingBar;
    public Text timer; 

    private float currentTime = 0.0f;
    private float pissedLevel = 0.0f;
    private float fixingLevel = 0.0f;
    private bool isFixing = false;

    protected Manager() { }

    private void Update()
    {
        this.currentTime += Time.deltaTime;
        this.CalculatePissedLevel();
        this.BreakThings();
        this.UpdateHUD();
        this.CheckLoss();
        this.CheckWin();
    }


    private void BreakThings()
    {
        foreach (Breakable breakable in this.breakables)
            if (!breakable.isBroken && Random.value < this.breakChance * Time.deltaTime)
                breakable.Break();
    }

    private void CheckLoss()
    {
        if (this.pissedLevel == 1.0f)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    private void CheckWin()
    {
        if (this.currentTime >= this.survivalTime)
        {
            SceneManager.LoadScene("Success");
        }
    }

    private void CalculatePissedLevel()
    {
        bool increasing = false;

        foreach (SpotlightController spotlight in this.spotlights)
            if (spotlight.IsInSight(this.player, raycastMask))
            {
                this.pissedLevel += this.pissedIncreaseRate * Time.deltaTime;
                increasing = true;
            }

        foreach (Breakable breakable in this.breakables)
            if (breakable.isBroken)
            {
                this.pissedLevel += this.pissedIncreaseRate * Time.deltaTime;
                increasing = true;
            }

        this.pissedLevel -= increasing ? 0.0f : this.pissedDecreaseRate * Time.deltaTime;
        this.pissedLevel = Mathf.Clamp01(this.pissedLevel);
    }

    private void UpdateHUD()
    {
        this.pissedBar.Set(this.pissedLevel);
        this.fixingBar.gameObject.SetActive(this.isFixing);
        this.fixingBar.Set(this.fixingLevel);


        int totalSeconds = (int)this.survivalTime - (int)this.currentTime;
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds - (60 * minutes);
        string appendZero = seconds < 10 ? "0" : "";
        this.timer.text = $"{minutes}:{appendZero}{seconds}";
    }

    public void UpdateFixingBar(bool isFixing, float fixingLevel)
    {
        this.isFixing = isFixing;
        this.fixingLevel = fixingLevel;
    }
}
