using DilmerGames.Core.Singletons;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private int score;

    public void IncrementScore()
    {
        score++;
        scoreText.text = $"{score}";
    }
}
