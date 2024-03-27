using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public static int coin;
    private const string CoinCountKey = "CoinCount";


    // Start is called before the first frame update
    void Start()
    {
                coin = PlayerPrefs.GetInt(CoinCountKey, 0);
    }
    public static void UpdateCoinCount(int score)
    {
        coin += score; // Add the player's score to the coin count
        PlayerPrefs.SetInt(CoinCountKey, coin);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
