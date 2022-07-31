using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerManager : MonoBehaviour
{
    public Transform[] players; //0 = arah       1 = Marco   2 = Melee
    public static string ActivePlayerName;
    public static GameObject ActivePlayerGameObj;
    public static int ActivePlayerNum;
    // Start is called before the first frame update
    void Awake()
    {
        findActivePlayer();
    }

    public void findActivePlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].gameObject.activeSelf)
            {
                ActivePlayerName = players[i].gameObject.name;
                ActivePlayerGameObj = players[i].gameObject;
                ActivePlayerNum = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        findActivePlayer();
    }
}
