﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    #region Singleton

    public static PlayerManager instance;
    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player; //not used since we have multiple players
    public GameObject middle;
    public GameObject north;
    public GameObject south;
    public GameObject southeast;
    public GameObject southwest;
    public GameObject northwest;


     
}
