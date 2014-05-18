/*

 

    -----------------------

    UDP-Receive (send to)

    -----------------------

    // [url]http://msdn.microsoft.com/de-de/library/bb979228.aspx#ID0E3BAC[/url]

    

    

    // > receive 

    // 127.0.0.1 : 8051

    

    // send

    // nc -u 127.0.0.1 8051

 

*/

using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

 

public class UDPReceive : MonoBehaviour {
	    
    // receiving Thread
    Thread receiveThread; 
	
    // udpclient object
    UdpClient client; 

    // public
    // public string IP = "127.0.0.1"; default local
    public int port; // define > init

    // infos
    public string lastReceivedUDPPacket="";
    public string allReceivedUDPPackets=""; // clean up this from time to time!

	// FaceAPI
	public float xPos;
	public float yPos;
	public float zPos;
	public float pitch;
	public float yaw;
	public float roll;

	// start from shell
    private static void Main() 
    {
       UDPReceive receiveObj=new UDPReceive();
       receiveObj.init(); 

        string text="";

        do
        {
             text = Console.ReadLine();
        }
		while(!text.Equals("exit"));
    }

    // start from unity3d
    public void Start()
    {
        init(); 
    }

    // OnGUI
//    void OnGUI()
//    {
//        Rect rectObj=new Rect(40,10,200,400);
//        GUIStyle style = new GUIStyle();
//        style.alignment = TextAnchor.UpperLeft;
//
//        GUI.Box(rectObj,"# UDPReceive\n127.0.0.1 "+port+" #\n"
//                    + "shell> nc -u 127.0.0.1 : "+port+" \n"
//                    + "\nLast Packet: \n"+ lastReceivedUDPPacket
//                    + "\n\nAll Messages: \n"+allReceivedUDPPackets
//                ,style);
//    }

	// init
    private void init()
    {
		xPos = 0;
		yPos = 0;
		zPos = 0;
		pitch = 0;// rotation around y
		yaw = 0; // rotation around z
		roll = 0;// rotation around x

		// Terminator point define by which the news is sent
        print("UDPSend.init()");

		// define port
		port = 29129;

		// status
        print("Sending to 127.0.0.1 : "+port);
        print("Test-Sending to this Port: nc -u 127.0.0.1  "+port+"");

		// ----------------------------

        // Monitor

        // ----------------------------

		// Local terminator point define (where news is received).
		// A new Thread created for the reception of incoming info.

        receiveThread = new Thread(
            new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }
    // receive thread 
    private  void ReceiveData() 
    {
        client = new UdpClient(port);
		//print("ReceiveData function ");
		while (true) 
        {
			//print("ReceiveData function ");

//			try 
//            {
                // Bytes.
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 29129);
//				if (anyIP.Equals(null))
				//print("anyIP " + port );

                byte[] data = client.Receive(ref anyIP);
//				if (data.Equals(null))
					//print("data ok ");


				// Bytes with the ASCII coding in the text format code.
                string text = Encoding.ASCII.GetString(data, 0, data.Length);
				//print("data "+text);
				parseString(text);
//				string[] words = text.Split('	');
////				// The recalled text indication
//                print(">> " + text);
//				// translation x
//				pz = (float)Convert.ToDouble(words[3]);
//				// rotation y
//				ry = (float)Convert.ToDouble(words[4]);
//

//				var floatArray = new float[data.Length/4];
//				Buffer.BlockCopy(data, 0, floatArray , 0, data.Length);

				// latest UDPpacket
                lastReceivedUDPPacket=text;

//				allReceivedUDPPackets=allReceivedUDPPackets+text;

//            }
//            catch (Exception err) 
//            {
//                print(err.ToString());
//            }
        }
    }
	private void parseString(String text)
	{
		String[] str = text.Split(' ');
		int index = 0;
		xPos = float.Parse(str[index++]);
		yPos = float.Parse(str[index++]);
		zPos = float.Parse(str[index++]);
		pitch = float.Parse(str[index++]);
		yaw = float.Parse(str[index++]);
		roll = float.Parse(str[index++]);
		//print("xpos = "+ xPos + "ypos = " + yPos);
	}
	// getLatestUDPPacket
    // cleans up the rest
    public string getLatestUDPPacket()
    {
        allReceivedUDPPackets="";
        return lastReceivedUDPPacket;
    }
	void OnDisable()
	{
		if ( receiveThread!= null)
			receiveThread.Abort();
		
		client.Close();
	} 
}