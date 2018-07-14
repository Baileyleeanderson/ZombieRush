using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object1 : MonoBehaviour {

    [SerializeField] private float objectSpeed = 1;
    [SerializeField] private float resetPosition = -62.33f;
    [SerializeField] private float startPosition = -44.28f;
	
	void Start () {
		
	}
	
	
	protected virtual void Update () {
        if(!GameManager.instance.GameOver){
			PlatformMove();
        }
        else {
            transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
        }
	}

    void PlatformMove(){
        transform.Translate(Vector3.left * (objectSpeed * Time.deltaTime));

        if (transform.localPosition.x <= resetPosition)
        {
            Vector3 newPos = new Vector3(startPosition, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
