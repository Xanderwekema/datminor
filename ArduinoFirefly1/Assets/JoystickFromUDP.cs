using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class JoystickFromUDP : MonoBehaviour
{
    UdpClient client;
    IPEndPoint remoteEndPoint;
    public float speed = 5f;

    void Start()
    {
        client = new UdpClient(5053);
        remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
    }

    void Update()
    {
        if (client.Available > 0)
        {
            byte[] data = client.Receive(ref remoteEndPoint);
            string message = Encoding.UTF8.GetString(data);
            string[] parts = message.Split(',');

            if (parts.Length >= 2 &&
                int.TryParse(parts[0], out int xRaw) &&
                int.TryParse(parts[1], out int yRaw))
            {
                float x = Mathf.InverseLerp(0, 1023, xRaw) * 2 - 1;
                float z = Mathf.InverseLerp(0, 1023, yRaw) * 2 - 1;

                float deadzone = 0.15f;
                if (Mathf.Abs(x) < deadzone) x = 0;
                if (Mathf.Abs(z) < deadzone) z = 0;

                Vector3 move = new Vector3(x, 0, z) * speed * Time.deltaTime;
                transform.Translate(move);
            }
        }
    }

    void OnApplicationQuit()
    {
        client?.Close();
    }
}
