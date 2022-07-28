using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // public variables
    public GameObject player;

    // private variables
    private Vector3 offset = new Vector3(0, 14, -8.2f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // changes the camera position to the player position with an offset in the Y position
        transform.position = player.transform.position + offset;
    }
}
