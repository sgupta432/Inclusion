import socket

# Sanjana's iPhone
UDP_IP = "128.61.60.194"
UDP_PORT = 11000


while True:
    MESSAGE = input()
    b = MESSAGE.encode('utf-8')
    sock = socket.socket(socket.AF_INET, # Internet
                         socket.SOCK_DGRAM) # UDP
    sock.sendto(b, (UDP_IP, UDP_PORT))
    print("sent")
