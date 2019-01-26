using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public float maxDistanceToPlayerX, maxDistanceToPlayerY;
    public Vector2 smoothing = new Vector2(3, 3);
    float x, y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = transform.position.x;
        y = transform.position.y;

        if (Mathf.Abs(x - player.position.x) > maxDistanceToPlayerX)
                    x = Mathf.Lerp(x, player.position.x, smoothing.x * Time.deltaTime);
        if (Mathf.Abs(y - player.position.y) > maxDistanceToPlayerY)
                    y = Mathf.Lerp(y, player.position.y, smoothing.y * Time.deltaTime);

                       
        /* if (Mathf.Abs(transform.position.x - playerTransform.position.x) > maxDistanceToPlayerX || Mathf.Abs(transform.position.y - playerTransform.position.y) > maxDistanceToPlayerY) {
            // transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerTransform.position.x, .5f), Mathf.Lerp(transform.position.y, playerTransform.position.y, .5f), transform.position.z);
            transform.parent = playerTransform;
        } 
        if (transform.parent != null) {
            transform.parent = null;
        } */
    }

    void LateUpdate() {
        transform.position = new Vector3(x, y, transform.position.z);     
    }
}
