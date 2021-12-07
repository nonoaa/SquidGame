using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckWinner : MonoBehaviour
{
    int winner = 0;
    public Text WinnerText;
    // Start is called before the first frame update
    void Start()
    {
        WinnerText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (winner == 0)
            WinnerText.text = "";
        else if (winner == 1)
            WinnerText.text = "Player 1 Win!";
        else if (winner == 2)
            WinnerText.text = "Player 2 Win!";
    }
    public void SetWinner(int num)
    {
        winner = num;
    }

    public int GetWinner()
    {
        return winner;
    }
}
