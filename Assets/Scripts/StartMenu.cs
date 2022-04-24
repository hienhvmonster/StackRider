using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    private Action<object> _actionCoinChange;
    private Action<object> _actionNewGame;
    private Action<object> _actionDoneGame;
    private Action<object> _actionLose;

    public GameObject headStart;
    public GameObject bodyStart;

    public List<TextMeshProUGUI> coins = new List<TextMeshProUGUI>();
    public TextMeshProUGUI level;

    public GameObject loseBoard;
    public GameObject winBoard;

    private void Start()
    {
        _actionCoinChange = param => OnChangeCoinValue();
        this.RegisterListener(EventID.OnCoinChange, _actionCoinChange);

        _actionNewGame = param => OnChangeLevel((string)param);
        _actionNewGame += param => BodyStartActivate();
        this.RegisterListener(EventID.OnNewGame, _actionNewGame);

        _actionDoneGame = param => WinBoardActivate();
        this.RegisterListener(EventID.OnDoneGame, _actionDoneGame);

        _actionLose = param => LoseBoardActivate();
        this.RegisterListener(EventID.OnLose, _actionLose);
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.OnCoinChange, _actionCoinChange);
        this.RemoveListener(EventID.OnNewGame, _actionNewGame);
        this.RemoveListener(EventID.OnDoneGame, _actionDoneGame);
        this.RemoveListener(EventID.OnLose, _actionLose);
    }

    public void StartGame()
    {
        bodyStart.SetActive(false);
        this.PostEvent(EventID.OnRun);
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

    public void OnSkipLevelPress()
    {
        //Chay Ads

        GameManager.instance.NextLevel();
        loseBoard.SetActive(false);
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

        GameManager.instance.SetCoinX2Game();
        GameManager.instance.NextLevel();
        winBoard.SetActive(false);
    }

    private void LoseBoardActivate()
    {
        loseBoard.SetActive(true);
    }

    private void WinBoardActivate()
    {
        winBoard.SetActive(true);
    }

    private void BodyStartActivate()
    {
        bodyStart.SetActive(true);
    }
}
