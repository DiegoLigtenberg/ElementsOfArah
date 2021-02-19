using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerOnCollision : MonoBehaviour
{

    public AudioSource source;
    public AudioSource[] source2;
    public ParticleSystem part;
    public static bool onlyonce;
   public static int count;
    // Start is called before the first frame update
    void Start()
    {
        count = (int)(Random.Range(0, 5));
        onlyonce = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
 
        if (collision.collider.tag != "Trigger")
        {
           // this.gameObject.SetActive(false);
          
        }
     
    }

    public IEnumerator removetimer()
    {
        yield return new WaitForSeconds(0.15f + Random.Range(-0.3f,0.3f));
        onlyonce = false;
    } 

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name.Contains("ArenaFloor") || other.tag == "Player")
        {
            if (!onlyonce)
            {
                count = (int)(Random.Range(0, 5));

                source.pitch = Random.Range(0.40f, .5f);
                source2[count].pitch = Random.Range(.4f, .45f);

                source2[count].volume *= .4f;
                // Debug.Log(other.gameObject.name);
                source.Play();
                if (count <= 5)
                {

                }
                source2[count].Play();
                onlyonce = true;
              
                StartCoroutine(removetimer());
            
           

            }

        }


    }

  
        // Update is called once per frame
        void Update()
    {
       
    }
}
