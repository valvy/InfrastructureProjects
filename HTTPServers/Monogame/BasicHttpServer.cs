using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Collections;

public class BasicHttpServer { 

    Socket socket = null;
    public String htmlSend = "<!DOCTYPE html><html><body><h1>Hello world!</h1></body></html>";
    public BasicHttpServer(int port) {
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any.Address, port);  
        // Create a TCP/IP socket.  
        socket  = new Socket(
            AddressFamily.InterNetwork,  
            SocketType.Stream, 
            ProtocolType.Tcp
        );                        

        try {
            socket.Bind(localEndPoint);  
            socket.Listen(100);  
            socket.BeginAccept(   
                    new AsyncCallback(AcceptCallback),  
                    socket 
            );  
        } catch ( Exception exc ) {
            Console.WriteLine(exc.ToString());  
        }
    }

    public void Stop() {
        if(socket.Connected) {
            socket.Disconnect(false);
            socket.Close();
        }
    }
    private void AcceptCallback(IAsyncResult ar) {
        Socket handler = ((Socket) ar.AsyncState).EndAccept(ar); 
        byte[] bytes;
       
        bytes = new byte[1024];
        
        int byteRec = handler.Receive(bytes);
        String userAgent = Encoding.UTF8.GetString(bytes,0,byteRec); 
        String sendMsg= String.Format(
            "HTTP/1.1 200 OK\r\nServer: MonoGameServer\r\nPragma: no-cache\r\nExpires: 0 \r\nContent-Type: text/html\r\nContent-Length: {0}\r\n\r\n{1}",
            htmlSend.Length + 1,
            htmlSend
        );


        Console.WriteLine(userAgent);    
        handler.Send(Encoding.UTF8.GetBytes(sendMsg));
  

        socket.BeginAccept(   
                new AsyncCallback(AcceptCallback),  
                socket 
        ); 
        handler.Shutdown(SocketShutdown.Both); 
        handler.Close();
    }
}