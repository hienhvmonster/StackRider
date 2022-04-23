using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int coins;
    public int curLevel;

    [SerializeField] private List<GameObject> levels = new List<GameObject>();

    private void Awake()
    {
        if (instance != null) DestroyImmediate(this);
        instance = this;
        DontDestroyOnLoad(this);
        coins = 0;
    }

    public void AddCoin(int coin)
    {
        coins += coin;
        Debug.Log("coin: " + coins);
        this.PostEvent(EventID.AddCoin);
    }

    public int GetCoin()
    {
        return coins;
    }
}
