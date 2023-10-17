using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    
    [Header("Offset and general components")]
    [SerializeField] private Vector3 camOffSet;
    [SerializeField] private float offsetMultiplyerX;
    [SerializeField] private float offsetMultiplyerY;

    private Vector3 currentOffSet;
    [SerializeField] private GameObject player;
    Vector3 playerPos;

    playerController playerC;

    [SerializeField] private Vector2 MaxOffSet;
    [SerializeField] private Vector2 MinOffSet;
    Vector3 velocity;
    [SerializeField] private float moveTime;

    
    [Header("Zoom Level")]
    private float zoomLevel;
    [SerializeField] private float zoomMultiplyer;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    private float speed;
    [SerializeField] private float zoomTime;


    [SerializeField] private Camera cam;

    private void Start() {
        cam = gameObject.GetComponent<Camera>();
        playerC = player.GetComponent<playerController>();

        zoomLevel = cam.orthographicSize;

    }

    // Update is called once per frame
    void Update()
    {
        zoom();
    }

    private void FixedUpdate() {
        playerPos = player.transform.position;
        
        OffSet(playerPos);
    }

    void zoom(){
        zoomLevel = playerC.speedX * zoomMultiplyer;
        zoomLevel = Mathf.Clamp(zoomLevel, minZoom, maxZoom);
        
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoomLevel, ref speed, zoomTime);

    }

    void OffSet(Vector3 PP){
        currentOffSet = new Vector3 (camOffSet.x + (playerC.speedX * offsetMultiplyerX), camOffSet.y + (playerC.speedX * offsetMultiplyerX), -10);
        currentOffSet = new Vector3 (Mathf.Clamp(currentOffSet.x, MinOffSet.x, MaxOffSet.x), Mathf.Clamp(currentOffSet.y, MinOffSet.y, MaxOffSet.y), -10);
        
        transform.position = Vector3.SmoothDamp(transform.position, (PP + currentOffSet), ref velocity, moveTime);
        // transform.position = PP + currentOffSet;
    }
}
