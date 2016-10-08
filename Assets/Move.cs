using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    public Vector3 direction = Vector3.forward;
    public float speed = 1.0f;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.rotation * (direction.normalized * speed * Time.deltaTime);   // Time.deltaTime is usually ~1/60s, so this moves 1 unit per sec
    }
}
