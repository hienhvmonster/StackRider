using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject headStart;
    public GameObject bodyStart;

    public List<TextMeshProUGUI> coins = new List<TextMeshProUGUI>();
    public TextMeshProUGUI level;

    public GameObject loseBoard;
    public GameObject winBoard;

    private void Start()
    {
        this.RegisterListener(EventID.OnCoinChange, param => OnChangeCoinValue());
        this.RegisterListener(EventID.NewGame, param => OnChangeLevel((string)param));

        this.RegisterListener(EventID.Win, param => WinBoardActivate());
        this.RegisterListener(EventID.Lose, param => LoseBoardActivate());
    }

    public void StartGame()
    {
        bodyStart.SetActive(false);
        this.PostEvent(EventID.Run);
    }


    private void OnChangeCoinValue()
    {
        foreach(TextMeshProUGUI coinShow in coins)
        {
            coinShow.SetText(GameManager.instance.GetCoin().ToString());
        }
    }

    private void OnChangeLevel(string levelName)
    {
        level.SetText(levelName);
    }


    public void OnTryAgainPress()
    {
        GameManager.instance.ResetLevel();
        loseBoard.SetActive(false);
    }

    public void OnNoThanksPress()
    {
        GameManager.instance.NextLevel();
        winBoard.SetActive(false);
    }

    public void OnX2Press()
    {
        //Chay Ads


        GameManager.instance.NextLevel();
        winBoard.SetActive(false);
    }

    public void LoseBoardActivate()
    {
        loseBoard.SetActive(true);
    }
    public void WinBoardActivate()
    {
        winBoard.SetActive(true);
    }
}
