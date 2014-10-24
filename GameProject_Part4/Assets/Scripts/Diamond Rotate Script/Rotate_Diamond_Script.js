#pragma strict

var myDegrees = 100;

function Start () {

}

function Update () {

transform.Rotate(0, 0, myDegrees * Time.deltaTime);
;
}
