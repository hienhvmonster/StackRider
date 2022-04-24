using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Action<object> _actionWin;
    private Action<object> _actionLose;

    private int inGameCoin;
    private int realCoin;

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
        _actionWin = param => SetCoinWinGame();
        this.RegisterListener(EventID.OnWin, _actionWin);

        _actionLose = param => SetCoinLoseGame();
        this.RegisterListener(EventID.OnLose, _actionLose);

        LoadLevel(2);
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.OnWin, _actionWin);
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

    private void SetCoinWinGame()
    {
        realCoin = inGameCoin;
    }

    private void SetCoinLoseGame()
    {
        inGameCoin = realCoin;
    }

    private void LoadLevel(int levelIndex)
    {
        curLevel = levelIndex;
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
        if (curLevel >= levels.Count - 1)
        {
            LoadLevel(levels.Count - 1);
        }
        else
        {
            LoadLevel(curLevel + 1);
        }
    }

    public void ResetLevel()
    {
        LoadLevel(curLevel);
    }

}
