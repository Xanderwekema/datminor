using System.IO;
using UnityEngine;

public class TriggerLightOn : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            File.WriteAllText("trigger_flag.txt", "ON");
            Debug.Log("Wrote ON to trigger_flag.txt");
        }
    }
}
