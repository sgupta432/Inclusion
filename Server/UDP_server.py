import socket

UDP_IP = "128.61.35.76"
UDP_PORT = 8888

sock = socket.socket(socket.AF_INET, # Internet
                     socket.SOCK_DGRAM) # UDP
sock.bind((UDP_IP, UDP_PORT))

while True:
    data, addr = sock.recvfrom(65507) # buffer size is 1024 bytes
    print("received message:", data)
