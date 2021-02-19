using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhasManager : MonoBehaviour
{
    public Health[] hb;
    public GameObject[] gr;

    public GameObject[] go;

    public GameObject[] gf; // fake box

    public GameObject[] pyramiditems;

    public Animator anim;

    private bool onlyonce;
    private bool onlyoncep2;

    public GameObject turnbossOn;
    public static int CurrentPhase;

    public GameObject[] indicator;

    public GameObject[] pyramids;
  

    // Start is called before the first frame update
    void Start()
    {
        phasingToMiddle.Phasecount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //  CurrentPhase = anim.GetInteger("Phase");
        CurrentPhase = phasingToMiddle.Phasecount;
        CurrentPhase = Mathf.Clamp(CurrentPhase, CurrentPhase, 999);
        /*
       if (Input.GetKey(KeyCode.M))
        {
           turnbossOn.SetActive(true);
        }
        */
        if (CurrentPhase == 0)
        {
            //Debug.Log(anim.GetInteger("Phase"));
            gr[0].SetActive(false);
            gr[1].SetActive(false);
            gr[2].SetActive(false);
            gr[3].SetActive(false);
            gr[4].SetActive(false);

            //hp bar
            go[0].SetActive(false);
            go[1].SetActive(false);
            go[2].SetActive(false);
            go[3].SetActive(false);
            go[4].SetActive(false);

            //fake box
            gf[0].SetActive(true);
            gf[1].SetActive(true);
            gf[2].SetActive(true);
            gf[3].SetActive(true);
            gf[4].SetActive(true);

           




        }

        if (CurrentPhase == 1)
        {
            if (!onlyonce)
            {
                gr[0].SetActive(true);
                onlyonce = true;
                pyramids[0].SetActive(true); //pyramid
                pyramids[1].SetActive(true); //pyramid smoke

            }

         // gr[0].SetActive(true); dit doen we 1 x
            gr[1].SetActive(false);
            gr[2].SetActive(false);
            gr[3].SetActive(false);
            gr[4].SetActive(false);

            //hp bar
            go[0].SetActive(true);
            go[1].SetActive(false);
            go[2].SetActive(false);
            go[3].SetActive(false);
            go[4].SetActive(false);
            
            //fake box
            gf[0].SetActive(false);
            gf[1].SetActive(true);
            gf[2].SetActive(true);
            gf[3].SetActive(true);
            gf[4].SetActive(true);

            indicator[0].SetActive(true);
            pyramids[0].SetActive(true);


        }

        if (CurrentPhase == 2)
        {
         
            if (!onlyoncep2)
            {
                gr[4].SetActive(true);
                onlyoncep2 = true;
                pyramids[2].SetActive(true); //pyramid
                pyramids[3].SetActive(true); //pyramid smoke
            }

            gr[0].SetActive(false);
            gr[1].SetActive(false); // dit doen we 1x
            gr[2].SetActive(false);
            gr[3].SetActive(false);
           // gr[4].SetActive(false);

            //hp bar
            go[0].SetActive(false);
            go[1].SetActive(false);
            go[2].SetActive(false);
            go[3].SetActive(false);
            go[4].SetActive(true);
            
            //fake box
            gf[0].SetActive(true);
            gf[1].SetActive(true);
            gf[2].SetActive(true);
            gf[3].SetActive(true);
            gf[4].SetActive(false);

            indicator[1].SetActive(true);
            Debug.Log("indicator 1 activated");

            pyramids[2].SetActive(true);
        }

    }


}
