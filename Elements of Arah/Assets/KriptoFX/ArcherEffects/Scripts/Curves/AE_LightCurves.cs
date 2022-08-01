using UnityEngine;
using System.Collections;
using CreatingCharacters.Abilities;


public class AE_LightCurves : MonoBehaviour
{
    public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public Gradient LightColor = new Gradient();
    public float GraphTimeMultiplier = 1, GraphIntensityMultiplier = 1;
    public bool IsLoop;

    [HideInInspector] public bool canUpdate;
    private float startTime;
    Color startColor;
    private Light lightSource;

    private float time;
    private float light_intensity;
    private GameObject parent_delete;
    private float rfc;
    private bool onlyonce;
    private bool no_delete_all_once;
    public void SetStartColor(Color color)
    {
        startColor = color;
    }

    public IEnumerator destroy_self()
    {
        yield return new WaitForSeconds(3);
        parent_delete = transform.parent.parent.gameObject;
        Destroy(parent_delete);
    }

    private void Awake()
    {
        lightSource = GetComponent<Light>();
        startColor = lightSource.color;

        //    lightSource.intensity = LightCurve.Evaluate(0) * GraphIntensityMultiplier;
        lightSource.color = startColor * LightColor.Evaluate(0);

        startTime = Time.time;
        canUpdate = true;
    }

    private void OnEnable()
    {
        startTime = Time.time;
        canUpdate = true;
        if (lightSource != null)
        {
            // lightSource.intensity = LightCurve.Evaluate(0) * GraphIntensityMultiplier;
            lightSource.color = startColor * LightColor.Evaluate(0);
        }
    }

    private void Update()
    {
        light_intensity = GetComponent<Light>().intensity;

        if (RapidFireMarco.rapidFireHits <= 2) { rfc = 1.5f; }
        else if (RapidFireMarco.rapidFireHits > 2) { rfc = 5.5f; }

        if (RapidFireMarco.isFiring_mana && Ability.energy >= 10)
        {
            if (light_intensity < 1.5f) { GetComponent<Light>().intensity += 1.5f * Time.deltaTime; }
            time = (Time.time) - startTime;
        }
        else
        {
            if (light_intensity > 0) { GetComponent<Light>().intensity -= rfc * Time.deltaTime; }
            if (light_intensity < 0.1f && !onlyonce) { if (no_delete_all_once) { StartCoroutine(destroy_self()); no_delete_all_once = true; } } //1.1f makes it smooth glow out when loosening rapid fire

            /*
            if (canUpdate) {
                var eval = LightCurve.Evaluate(time / GraphTimeMultiplier) * GraphIntensityMultiplier;
                lightSource.intensity = eval;
                lightSource.color = startColor * LightColor.Evaluate(time / GraphTimeMultiplier);
            }
            if (time >= GraphTimeMultiplier) {
                if (IsLoop) startTime = Time.time;
                else canUpdate = false;
            }
            */
        }
    }
}