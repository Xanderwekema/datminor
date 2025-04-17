import serial
import socket

ser = serial.Serial('/dev/cu.usbmodem14101', 9600)  # Use the correct port
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
sock.bind(('127.0.0.1', 5052))  # Must match Unity script

print("Bridge started. Waiting for UDP command...")

while True:
    data, addr = sock.recvfrom(1024)
    message = data.decode('utf-8').strip()
    print(f"Received from Unity: {message}")
    
    if message == "ON":
        ser.write(b'ON\n')
        print("Sent ON to Arduino")
