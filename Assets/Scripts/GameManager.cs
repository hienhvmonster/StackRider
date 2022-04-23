using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        this.RegisterListener(EventID.Win, param => SetCoinWinGame());
        this.RegisterListener(EventID.Lose, param => SetCoinLoseGame());

        this.RegisterListener(EventID.Win, param => NextLevel());
    }

    public void AddCoin(int coin)
    {
        inGameCoin += coin;
        Debug.Log("coin: " + inGameCoin);
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
        string levelName = "LEVEL " + (curLevel + 1);
        Destroy(levelFather.GetChild(0));

        Instantiate<GameObject>(levels[curLevel], levelFather);

        this.PostEvent(EventID.NewGame, levelName);
    }

    public void NextLevel()
    {
        if (curLevel >= levels.Count - 1)
        {
            curLevel = levels.Count - 1;
        }
        else
        {
            curLevel++;
        }

        ResetLevel();
    }

    public void ResetLevel()
    {
        LoadLevel(curLevel);
    }

}
