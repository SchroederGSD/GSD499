#pragma strict

var myDegrees = 100;

function Start () {

}

function Update () {

transform.Rotate(myDegrees * Time.deltaTime, 0, 0);
;
}
