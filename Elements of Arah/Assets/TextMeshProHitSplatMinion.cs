using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using CreatingCharacters.Abilities;


public class TextMeshProHitSplatMinion : MonoBehaviour
{

    public Health hp;
    public TMP_Text textHpNumber;

    float lastStep, timeBetweenSteps = .15f;
    public MeshRenderer mr;
    public Color colorText;

    // Start is called before the first frame update
    void Awake()
    {
        textHpNumber = GetComponent<TMP_Text>();


        hp = GameObject.Find("Healing Minion").GetComponent<Health>();


        this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        //this.transform.localPosition = new Vector3(this.transform.localPosition.x + Health.transformmover, this.transform.localPosition.y, this.transform.localPosition.z);

        textHpNumber.text = Mathf.Abs(Health.tookThisDmg).ToString();
        textHpNumber.color = colorText;

        mr.enabled = false;
        Invoke("turnon", 0.1f);

    }

    public void turnon()
    {
        mr.enabled = true;
    }


    private bool onlyonce;
    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastStep > timeBetweenSteps && !onlyonce)
        {
            onlyonce = true;
            lastStep = Time.time;


            //makes it invisible by scaling after .15 sec
            if (textHpNumber == null)
            {
                this.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        float scaler = (Gun.fromCenterPLayerDistance / 30) + 0.6f;
        scaler = Mathf.Clamp(scaler, 0.6f, 1.4f);

        if (textHpNumber != null)
        {
            this.transform.localScale = new Vector3(0.2f * scaler, 0.2f * scaler, 0.2f * scaler);

        }

    }


}
