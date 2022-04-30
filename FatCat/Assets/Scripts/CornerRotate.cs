using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerRotate : MonoBehaviour
{
    private Animator _processingRotate;
    [SerializeField] private string nameBoolDirectRotate;
    private void Start()
    {
        _processingRotate = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _processingRotate.SetBool(nameBoolDirectRotate, true);
            this.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
