#pragma strict

var weight: float = 10;

function OnControllerColliderHit(hit: ControllerColliderHit){
  if (hit.rigidbody){
    hit.rigidbody.AddForceAtPosition(-Vector3.up * weight, hit.point);
  }
}