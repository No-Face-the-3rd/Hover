using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    public float scoreValue;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        doCollisionStuff(other);
    }
    public virtual void doCollisionStuff(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            ScoreManager scorer = FindObjectOfType<ScoreManager>();
            scorer.addScore(scoreValue);
            Destroy(gameObject);
        }
    }
}


