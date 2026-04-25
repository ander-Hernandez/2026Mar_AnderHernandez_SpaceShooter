using TMPro;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] private int totalPoints = 0;
    [SerializeField] private TextMeshProUGUI displayText;

    private void Start()
    {
        totalPoints = 0;
    }

    public void AddPoints(int points)
    {
        totalPoints += points;
        displayText.text = "POINTS: "+ totalPoints.ToString();
    }
}
