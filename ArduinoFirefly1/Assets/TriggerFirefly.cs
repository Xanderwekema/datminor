using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TriggerFirefly : MonoBehaviour
{
    private UdpClient client;
    private IPEndPoint arduinoEndPoint;

    private void Start()
    {
        client = new UdpClient();
        arduinoEndPoint = new IPEndPoint(IPAddress.Loopback, 5052); // Must match Python script port
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            byte[] data = Encoding.UTF8.GetBytes("ON");
            client.Send(data, data.Length, arduinoEndPoint);
            Debug.Log("Sent ON command to Arduino via Python bridge.");
        }
    }

    private void OnApplicationQuit()
    {
        client?.Close();
    }
}
