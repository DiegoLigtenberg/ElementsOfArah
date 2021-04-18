using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
*/
using CreatingCharacters.Player;
using CreatingCharacters.Abilities;

/*
public class ArahAgentController : Agent
{

  [SerializeField] public Transform player;
  [SerializeField] public Transform boss;
  [SerializeField] public ThirdPersonMovement thirdPerson;
  [SerializeField] public ArahMovementController arahMovement;

  public override void OnEpisodeBegin()
  {
      base.OnEpisodeBegin();
      player.transform.position = new Vector3(3267.48f, 64.927f, -186.8f);
  }

  public override void CollectObservations(VectorSensor sensor)
  {
      sensor.AddObservation(transform.position/3000f);
      sensor.AddObservation(boss.transform.position );
  }

  public override void OnActionReceived(ActionBuffers actions)
  {
      float moveX = actions.ContinuousActions[0];
      float moveZ = actions.ContinuousActions[1];
      Debug.Log(actions.ContinuousActions[0]);
      Vector3 movementinput = new Vector3(moveX, 0, moveZ);
      thirdPerson.setMovement(movementinput);
  }

}
*/
