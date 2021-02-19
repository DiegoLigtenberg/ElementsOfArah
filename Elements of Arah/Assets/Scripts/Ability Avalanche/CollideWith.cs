using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWith : MonoBehaviour
{

    // Update is called once per frame

    private void Awake()
    {
      //  RFX1_TransformMotion.turnoff = false;
    }

    void Update()
    {
     
        if (RFX1_TransformMotion.turnoff)
        {
            Debug.Log(RFX1_TransformMotion.turnoff);
            //deleteme.SetActive(false);
            this.gameObject.SetActiveRecursively(false);
        }
    }
}
