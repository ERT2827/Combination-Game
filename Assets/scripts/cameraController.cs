using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] private Vector3 camOffSet;
    [SerializeField] private GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + camOffSet;
    }
}
