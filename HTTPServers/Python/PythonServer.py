#!/usr/bin/env python
# Example webserver based written in Python
# Author Heiko van der Heijden
import socket

# Some Global Variables
PORT = 8081
HOST = 'localhost'
INPUT_BUFFER = 1024

# HTML string we would like to send to the webbrowser
HTML_RESPONSE = "<!DOCTYPE html><html><body><h1>Hello world!</h1></body></html>"
# The HTTP request
HTTP_RESP = "HTTP/1.1 200 OK\r\nServer: PythonWebServer\r\nPragma: no-cache\r\nExpires: 0 \r\nContent-Type: text/html\r\nContent-Length: %i\r\n\r\n%s"

print ("Starting Server")
print("Please go with your browser to http://%s:%i" % (HOST, PORT))

# Open a TCP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.bind((HOST, PORT))
sock.listen(100) # timeout

try:
    print("Press control c to quit")
    while True:
        # Accept a request
        conn, addr = sock.accept()
        print("Connected by ", addr)
        data = conn.recv(INPUT_BUFFER).decode("utf-8")
        # Print the http request from the web browser.
        print(data)
        
        # Create a response based on the HTML and the http headers
        response = HTTP_RESP % (len(HTML_RESPONSE), HTML_RESPONSE)
        
        # Encode it to bytes
        response = response.encode('utf-8')
        conn.sendall(response) 
        
        conn.close()
except KeyboardInterrupt:
    print("Closing program")
except Exception as ex:
    print("Something unexpected has happened")
    print(ex)


sock.close()

