using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBox : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<Player>().isCrouching == true)
            {
                other.GetComponent<Player>().isHiding = true;
            }
            else
            {
                other.GetComponent<Player>().isHiding = false;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().isHiding = false;
        }
    }
}
