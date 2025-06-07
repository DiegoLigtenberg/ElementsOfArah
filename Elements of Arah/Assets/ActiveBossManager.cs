using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBossManager : MonoBehaviour
{
    public Transform[] bosses; //0 = arah       1 = Marco   2 = Melee
    public static string ActiveBossName;
    public static GameObject ActiveBossGameObj;
    public static int ActiveBossNum;
    public static string[] ActiveBossNames;
    
    // Start is called before the first frame update
    void Awake()
    {
        findActiveBoss();

    }

    public void findActiveBoss()
    {
        // Reset before searching
        ActiveBossName = null;
        ActiveBossGameObj = null;
        ActiveBossNum = -1;
        ActiveBossNames = new string[99];



        for (int i = 0; i < bosses.Length; i++)
        {
            if (bosses[i].gameObject.activeSelf)
            {
                ActiveBossName = bosses[i].gameObject.name;
                ActiveBossGameObj = bosses[i].gameObject;
                ActiveBossNum = i;
                ActiveBossNames[i] = bosses[i].gameObject.name;
                //break; // Stop at the first active boss (optional)
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        findActiveBoss();
    }
}
