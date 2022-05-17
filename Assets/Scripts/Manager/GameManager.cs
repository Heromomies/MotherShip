using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform spawnEnemies;
    public GameObject canvasButton;
    public ParticleSystem particleOnTouch;
    
    private Camera cam;
    
    #region Singleton

    private static GameManager gameManager;
    public static GameManager Instance => gameManager;

    private void Awake()
    {
        gameManager = this;
        cam = Camera.main;
    }

    #endregion

    private void Start()
    {
        particleOnTouch = Instantiate(particleOnTouch, transform.position, Quaternion.identity);
        particleOnTouch.Stop();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var target = cam.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;

            if (particleOnTouch != null)
            {
                particleOnTouch.transform.position = target;
                particleOnTouch.Play();
            }
        }
    }
}
