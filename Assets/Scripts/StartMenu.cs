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

    public void StartGame()
    {
        bodyStart.SetActive(false);
        this.PostEvent(EventID.Run);
        coins[0].SetText(GameManager.instance.GetCoin().ToString());
        coins[0].SetText("ara");
    }
}
