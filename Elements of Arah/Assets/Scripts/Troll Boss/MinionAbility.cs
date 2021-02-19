using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAbility : MonoBehaviour
{


    public GameObject[] effectMinion;
    public Transform[] effectTransformMinion;

    public Health hp;
    public AudioClip clip;
    public AudioSource audioSource;

    public GameObject hpbar;
    private BoxCollider bc;

    public GameObject light;

    public void StartMinionHealTroll()
    {
        StartCoroutine(MinionHealTroll());
        AudioSource audio = GetComponent<AudioSource>();
        first = false;
        bc = GetComponent<BoxCollider>();
    }

    private bool first;
    public IEnumerator PlaySound()
    {
        if (!first)
        {
            hpbar.SetActive(false);
            audioSource.Play();
            first = true;
        }
      
        yield return null;
    }


    private IEnumerator MinionHealTroll()
    {
        yield return new WaitForSeconds(0.7f);
        Instantiate(effectMinion[0], effectTransformMinion[0].position, effectTransformMinion[0].rotation);
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.currentHealth <= 0)
        {
            bc.enabled = false;
            StartCoroutine(PlaySound());
            audioSource.volume -= 0.0015f;
            //audioSource.clip = clip;
       
        }

        if (PhasManager.CurrentPhase == 2)
        {
            light.SetActive(true);
        }
    }
}
