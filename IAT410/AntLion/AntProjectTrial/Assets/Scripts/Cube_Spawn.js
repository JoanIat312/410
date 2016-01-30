#pragma strict

var cube : GameObject;
var spawn_position;
var timer = 0.0;

function spawn_cube () 
{

	spawn_position = Vector3(Random.Range(1, 5), Random.Range(1,5), 0);
	var temp_spawn_cube = Instantiate(cube, spawn_position, Quaternion.identity);	

}

function Start () {

}

function Update () {

timer += Time.deltaTime;
	if (timer > 10) 
	{
		spawn_cube();
		timer = 0.0;

	}

}