using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private float control_speed;
    private Vector2 direction; 
    private float sideToSide;
    private LevelTemplates _templates;
    private void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("Levels").GetComponent<LevelTemplates>();
    }
    private void Update()
    {
        if (_templates.isStarted)
        {
            direction = new Vector2(0, 0);
            if (Input.touchCount > 0)
            {
                direction = Input.GetTouch(0).deltaPosition;
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        direction = new Vector2(0, 0);
                        break;
                    case TouchPhase.Moved:
                        direction = Input.GetTouch(0).deltaPosition;
                        break;
                    case TouchPhase.Stationary:
                        direction = Input.GetTouch(0).deltaPosition;
                        break;
                }
            }
            sideToSide = direction.x;
            _movement.Move(new Vector3(0, 0, -sideToSide));
        }
    }

}
