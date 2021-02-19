using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip clip;
    public AudioSource audioSource;

    
    public void StartBasicAttack()
    {
        StartCoroutine(BasicAttack());
    }

    public void StartPrepareToDie()
    {
        StartCoroutine(PrePareToDie());
    }


    public void StarPlayerDied()
    {
        StartCoroutine(PlayerDied());
    }

    public IEnumerator PrePareToDie()
    {
        //   yield return new WaitForSeconds(0.3f);
        audioSource.Play();

        yield return null;
    }


    public IEnumerator PlayerDied()
    {
        //   yield return new WaitForSeconds(0.3f);
        audioSource.Play();

        yield return null;
    }

    public IEnumerator BasicAttack()
    {
     //   yield return new WaitForSeconds(0.3f);
        audioSource.Play();

        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
