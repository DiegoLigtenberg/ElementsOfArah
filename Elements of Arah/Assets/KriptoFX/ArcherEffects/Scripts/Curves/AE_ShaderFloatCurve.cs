using UnityEngine;
using System.Collections;
using CreatingCharacters.Abilities;

public class AE_ShaderFloatCurve : MonoBehaviour {

    public AE_ShaderProperties ShaderFloatProperty = AE_ShaderProperties._Cutoff;
    public AnimationCurve FloatCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float GraphTimeMultiplier = 1, GraphIntensityMultiplier = 1;
    public bool IsLoop;
    public int MaterialNumber = 0;
    public bool UseSharedMaterial;
    public Renderer[] UseSharedRenderers;

    private bool canUpdate;
    private float startTime;
    private Material mat;
    private float startFloat;
    private int propertyID;
    private string shaderProperty;
    private bool isInitialized;

    private MaterialPropertyBlock props;
    private Renderer rend;

    private float time;
    private void Awake()
    {
        if (props == null) props = new MaterialPropertyBlock();
        if (rend == null) rend = GetComponent<Renderer>();

        shaderProperty = ShaderFloatProperty.ToString();
        propertyID = Shader.PropertyToID(shaderProperty);
    }

    private void OnEnable()
    {
        startTime = Time.time;
        canUpdate = true;

        rend.GetPropertyBlock(props);

        var eval = FloatCurve.Evaluate(0) * GraphIntensityMultiplier;
        props.SetFloat(propertyID, eval);

        rend.SetPropertyBlock(props);
    }

    private void Update()
    {
        rend.GetPropertyBlock(props);

        //manually added
        if (RapidFireMarco.isFiring_BowEffect && Ability.energy >= 10 && time < GraphTimeMultiplier) { time = (Time.time )- startTime; }
        else {   GraphIntensityMultiplier += (Time.deltaTime /1.1f);  } //1.1f makes it smooth glow out when loosening rapid fire

        if (canUpdate)
        {
            var eval = FloatCurve.Evaluate(time / GraphTimeMultiplier) * GraphIntensityMultiplier;
            props.SetFloat(propertyID, eval);
        }
        if (time >= GraphTimeMultiplier)
        {
            if (IsLoop) startTime = Time.time;
            else canUpdate = false;
        }

        rend.SetPropertyBlock(props);
    }
}
