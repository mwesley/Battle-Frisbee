using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandleStats : MonoBehaviour
{
    private float playerWins;
    private float playerLosses;
    public Text Wins;
    public Text Losses;
    public Text[] TextArray;
    private GameObject _selectionPrefab;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 4)
        {
            TextArray = GameObject.Find("Canvas1").GetComponentsInChildren<Text>();            
            Wins = TextArray[0];
            Losses = TextArray[1];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(playerWins != 0 && Wins != null)
        {
            Wins.text = "Wins: " + playerWins;
        }
        if(playerLosses != 0 && Losses != null)
        {
            Losses.text = "Losses: " + playerLosses;
        }
    }

    public void GetStats(float wins, float losses)
    {
        playerWins = wins;
        playerLosses = losses;
    }
}
