using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
  public Camera Camera;
  public bool Active = true;
  public bool AutoInitCamera = true;

  private GameObject myContainer;
  private Transform t, camT, contT;

  private void Awake()
  {
    if (AutoInitCamera) {
      Camera = Camera.main;
      Active = true;
    }

    t = transform;
    camT = Camera.transform;
    var parent = t.parent;
    myContainer = new GameObject { name = "Billboard_" + t.gameObject.name };
    contT = myContainer.transform;
    contT.position = t.position;
    t.SetParent(myContainer.transform);
    contT.parent = parent;
    if (myContainer.name == "Billboard_Floating Text(Clone)") { DestroyObject(myContainer, 0.8f); }
    }

  private void Update()
  {
    if (Active)
      contT.LookAt(contT.position + camT.rotation * Vector3.back, camT.rotation * Vector3.up);
  }
}