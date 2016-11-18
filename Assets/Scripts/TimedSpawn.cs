using UnityEngine;
using System.Collections;

[System.Serializable]
public class spawnObject
{
    public GameObject spawn;
    public Vector3 spawnLoc;
    public float spawnTime;
    private float curTime = 0.0f;

    public void Update()
    {
        curTime -= Time.deltaTime;
        if(curTime <= 0.0f)
        {
            GameObject tmp = (GameObject)GameObject.Instantiate(spawn, spawnLoc, Quaternion.identity);
            curTime = spawnTime;
        }
    }
}

public class TimedSpawn : MonoBehaviour {
    public spawnObject[] spawns;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    foreach(spawnObject spawn in spawns)
        {
            spawn.Update();
        }
	}
}
