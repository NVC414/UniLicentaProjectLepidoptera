using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public sealed class HUDController : MonoBehaviour
{
    public static HUDController Instance;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI scoreText;

    public int totalEnemies;

    float timeElapsed;
    int killedEnemies;
    bool finished;

    void Awake()
    {
        Instance = this;
        enemyText.text = "0 / " + totalEnemies;
        if (scoreText != null) scoreText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (finished) return;

        timeElapsed += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeElapsed / 60f);
        int seconds = Mathf.FloorToInt(timeElapsed % 60f);
        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void AddKill()
    {
        if (finished) return;

        killedEnemies++;
        enemyText.text = killedEnemies + " / " + totalEnemies;

        if (killedEnemies >= totalEnemies)
            FinishRun();
    }

    void FinishRun()
    {
        if (finished) return;
        finished = true;

        int seconds = Mathf.RoundToInt(timeElapsed);
        int timeScore = seconds * 100;
        int enemyScore = totalEnemies * 500;
        int totalScore = timeScore + enemyScore;
        //technically works
        scoreText.text =
            "Scorul tau este:\n" +
            "Timp: " + seconds+" secunde" + " * 100\n" +
            "Enemy: " + totalEnemies + " * 500\n" +
            "Total: " + totalScore;

        scoreText.gameObject.SetActive(true);

        StartCoroutine(BackToMenuAfterDelay());
    }


    IEnumerator BackToMenuAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
}