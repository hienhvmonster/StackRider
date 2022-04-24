using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Action<object> _actionDoneGame;
    private Action<object> _actionLose;

    private int inGameCoin;
    private int realCoin;
    private int coinGetThisLevel;

    [SerializeField] private List<GameObject> levels = new List<GameObject>();
    [SerializeField] private Transform levelFather;
    private int curLevel;


    private void Awake()
    {
        if (instance != null) DestroyImmediate(this);
        instance = this;
        DontDestroyOnLoad(this);
        realCoin = inGameCoin = 0;
        curLevel = 0;
    }

    private void Start()
    {
        _actionDoneGame = param => SetCoinWinGame();
        this.RegisterListener(EventID.OnDoneGame, _actionDoneGame);

        _actionLose = param => SetCoinLoseGame();
        this.RegisterListener(EventID.OnLose, _actionLose);

        LoadLevel(0);
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.OnDoneGame, _actionDoneGame);
        this.RemoveListener(EventID.OnLose, _actionLose);
    }

    public void AddCoin(int coin)
    {
        inGameCoin += coin;
        this.PostEvent(EventID.OnCoinChange);
    }

    public int GetCoin()
    {
        return inGameCoin;
    }

    public void SetCoinX2Game()
    {
        inGameCoin += coinGetThisLevel;
        realCoin = inGameCoin;
    }

    private void SetCoinWinGame()
    {
        coinGetThisLevel = inGameCoin - realCoin;
        realCoin = inGameCoin;
    }

    private void SetCoinLoseGame()
    {
        inGameCoin = realCoin;
    }

    private void LoadLevel(int levelIndex)
    {
        if (levelIndex >= levels.Count - 1)
        {
            curLevel = levels.Count - 1;
        }
        else curLevel = levelIndex;
        string levelName = "LEVEL " + (curLevel + 1).ToString();

        if (levelFather.childCount > 0)
        {
            DestroyImmediate(levelFather.GetChild(0).gameObject);
        }

        Instantiate(levels[curLevel], levelFather, true);

        this.PostEvent(EventID.OnNewGame, levelName);
    }

    public void NextLevel()
    {
        LoadLevel(curLevel + 1);
    }

    public void ResetLevel()
    {
        LoadLevel(curLevel);
    }

}
