using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] AudioClip secondsLeftAudio;
    [SerializeField] int time;

    int score;
    int timeLeft;
    float timer;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        score = 0;
        UpdateScoreText();

        timeLeft = time;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timer += Time.deltaTime;
            timeLeft = time - (int)timer;
            UpdateTimeGUI();
        }
        else
        {
            //End Game
            audioSource.Stop();
        }
    }

    public void AddScorePoints(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    void UpdateTimeGUI()
    {
        if (!audioSource.isPlaying && timeLeft ==9)
        {
            audioSource.PlayOneShot(secondsLeftAudio);
        }
        timeText.text = timeLeft.ToString();
    }
}
