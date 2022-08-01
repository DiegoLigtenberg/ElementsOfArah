using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragon_circle : MonoBehaviour
{
    private Transform[] bones;
    private Quaternion[] target_rotations;
    private Quaternion[] previous_rotations;
    private Vector3[] previous_positions;

    public float circle_speed = 1f;
    public float circle_rad = 4f;

    public float up_down_amplitude = 0.5f;
    public float up_down_speed = 1f;

    public float update_delay = 0.1f;

    private float bone_size = 0.6f;
    private float circle_time = 0f;
    private float up_down_time = 0f;
    private float update_timer = 0f;

    void Awake()
    {
     
        bones = transform.GetChild(0).GetChild(0).GetComponentsInChildren<Transform>();
        previous_rotations = new Quaternion[bones.Length - 4];
        target_rotations = new Quaternion[bones.Length - 4];
        previous_positions = new Vector3[bones.Length - 4];

        bones[0].position = transform.position + new Vector3(circle_rad * Mathf.Cos(circle_time), up_down_amplitude * Mathf.Sin(up_down_time), circle_rad * Mathf.Sin(circle_time));
        bones[0].rotation = Quaternion.Euler(-90f, 0f, 0f);

        float angle = 360f * bone_size / (circle_rad * Mathf.PI * 2f);
        for (int i = 1; i < bones.Length - 5; i++)
        { bones[i].localRotation = Quaternion.Euler(0f, 0f, angle); }
    }

    private IEnumerator myfunc()
    {
        if (update_timer <= 0f)
        {
            update_timer = update_delay;
            for (int i = 1; i < bones.Length - 5; i++)
            {
                if (i % 2 == 0) { yield return new WaitForEndOfFrame(); }
                previous_rotations[i] = bones[i].rotation;
                previous_positions[i] = bones[i].position;
                target_rotations[i] = correct_rotation(Quaternion.LookRotation((bones[i - 1].position - previous_positions[i + 1]), Vector3.up));
            }
        }
        yield return null;
    }

    void Update()
    {
        up_down_time += Time.deltaTime * up_down_speed;
        circle_time += Time.deltaTime * circle_speed;// * (2f + Mathf.Cos(up_down_time + 1f)) * 0.5f;

        Vector3 next_position = transform.position + new Vector3(circle_rad * Mathf.Cos(circle_time), up_down_amplitude * Mathf.Sin(up_down_time), circle_rad * Mathf.Sin(circle_time));
        bones[0].rotation = correct_rotation(Quaternion.LookRotation((next_position - bones[0].position), Vector3.up));
        bones[0].position = next_position;

        update_timer -= Time.deltaTime;
        StartCoroutine(myfunc());
        for (int i = 1; i < bones.Length - 5; i++) { bones[i].rotation = target_rotations[i]; }//Quaternion.Lerp(target_rotations[i], previous_rotations[i], update_timer / update_delay); }        
    }

    private Quaternion correct_rotation(Quaternion rotation)
    { return Quaternion.Euler(rotation.eulerAngles + new Vector3(-90f, 0f, 0f)); }
}