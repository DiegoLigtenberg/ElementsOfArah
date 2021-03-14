using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectForBossAA : MonoBehaviour
{
    public Vector3 offset;
    private float standardizedDistance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        standardizedDistance = (TrollController.distToAgent / 30);
        standardizedDistance = 1.3f - standardizedDistance;

        standardizedDistance = Mathf.Clamp(standardizedDistance, 0.01f, 1);
        if (standardizedDistance <= 0.5f)
        {
            standardizedDistance = 0.5f;
        }

        // Debug.Log(standardizedDistance);
        if (ActivePlayerManager.ActivePlayerName.Contains("heraklios"))
        {
            if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.D) && TrollController.distToAgent < 10))
            {
                this.transform.localPosition = new Vector3(offset.x * standardizedDistance, offset.y, offset.z);
            }
            else
            {
                this.transform.localPosition = new Vector3(0, 0.866f, 0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.D) && TrollController.distToAgent < 10))
            {
                this.transform.localPosition = 100 * new Vector3(offset.x * standardizedDistance, offset.y, offset.z);
            }
            else
            {
                this.transform.localPosition = 100 * new Vector3(0, 0.866f, 0);
            }
        }
    }

}
