using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollPhasingLaser : MonoBehaviour
{
    public GameObject[] effect;
    public Transform[] effectTransform;
    private float lastStep, timeBetweenSteps = .5f;


    public GameObject checkplayerinragne;
    public GameObject checkplayerinragnec1;
    public GameObject checkplayerinragnec2;
    public GameObject checkplayerinragnec3;
    public GameObject checkplayerinragnec4;

    public static bool isbeaming;

    private bool offRoutine;

    public Animator anim;

    public int phasenumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        checkplayerinragne = GameObject.Find("checkinplayrange").gameObject;
        checkplayerinragnec1 = GameObject.Find("cheatcube1").gameObject;
        checkplayerinragnec2 = GameObject.Find("cheatcube2").gameObject;
        checkplayerinragnec3 = GameObject.Find("cheatcube3").gameObject;
        checkplayerinragnec4 = GameObject.Find("cheatcube4").gameObject;
        isbeaming = false;
        offRoutine = false;
        phasenumber = 0;
        anim.ResetTrigger("P2_Enter");
        anim.ResetTrigger("P3_Enter");
        Debug.Log(this.gameObject.name);
    }


    public void StartBeam()
    {
        StartCoroutine(Beaming());
        delaycooldown = 5;
    }


    public IEnumerator Beaming()
    {
   
        if (offRoutine == false)
        {
         
            phasenumber++;
            yield return new WaitForSeconds(.1f);
            offRoutine = true;
         
            isbeaming = true;

            //explosion
            Instantiate(effect[2], effectTransform[2].position, effectTransform[2].rotation);   //- new Vector3(0, 1, 0), effectTransform[2].rotation) as GameObject).transform.parent = GameObject.Find("PyramidPrefab").gameObject.transform; // explosion
            yield return new WaitForSeconds(1.0f);
            isbeaming = false;
            yield return new WaitForSeconds(0.3f);
           
       



            yield return new WaitForSeconds(1.1f);

            if (phasenumber == 1) {anim.SetTrigger("P2_Enter");}
            if (phasenumber == 2) {anim.SetTrigger("P3_Enter");}
            if (phasenumber == 3) {anim.SetTrigger("P4_Enter");}
            if (phasenumber == 4) {anim.SetTrigger("P5_Enter");}
            

            offRoutine = true;
           

            yield return new WaitForSeconds(2f);
           
            //misschien hier nog rook explosie

            gameObject.SetActive(false);
     

        }
      

    }
    private int threeshots;
    // Update is called once per frame

        public IEnumerator delayshot()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(effect[1], effectTransform[1].position, effectTransform[1].rotation);
        Instantiate(effect[3], effectTransform[1].position, effectTransform[1].rotation);

    }

    private float delaycooldown;
    private bool resetcooldown;
    private bool earlystop;
    void Update()
    {
       
      
        if (threeshots == 2)
        {
            StartCoroutine(delayshot());
            threeshots++;
          
        }
        if (isbeaming)
        {
            checkplayerinragne.GetComponent<BoxCollider>().enabled = false;
            checkplayerinragnec1.GetComponent <BoxCollider>().enabled = false;
            checkplayerinragnec2.GetComponent<BoxCollider>().enabled = false;
            checkplayerinragnec3.GetComponent<BoxCollider>().enabled = false;
            checkplayerinragnec4.GetComponent<BoxCollider>().enabled = false;
            if (Time.time - lastStep > timeBetweenSteps)
            {

                lastStep = Time.time;

                //laser
                if (!earlystop)
                {
                    Instantiate(effect[0], effectTransform[1].position + new Vector3(0, 0f, 0), effectTransform[1].rotation); //this.transform.rotation);
                }
               
                if (threeshots == 1)
                {
                    earlystop = true;
                }
                if (threeshots < 3)
                {
                    Instantiate(effect[1], effectTransform[1].position, effectTransform[1].rotation);  //dragonfire 
                    Instantiate(effect[3], effectTransform[1].position, effectTransform[1].rotation);
                    threeshots++;
                }
                //Instantiate(effect[1], effectTransform[1].position, effectTransform[1].rotation);  //dragonfire 

                delaycooldown = 5f;
            }
        }

        else
        {
      
         
        
        
        }
   
    }
}
