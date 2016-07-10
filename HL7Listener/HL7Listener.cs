using System;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HL7ReportsDA.DataEntities;
using NHapi.Model.V231.Message;

namespace HL7Listener
{
    public class HL7Listener
    {
        public static Func<string, string> GotMessage;
        public static Func<string, string> Log;
        private static string message;

        //  static EventLog evtLog = Common.Log.GetLogObject(HL7ReportsDA.Constant.APP_NAME, HL7ReportsDA.Constant.HL7_Listener_ModuleName);


        public static string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

        public static string Log1
        {
            get
            {
                return log1;
            }

            set
            {
                log1 = value;
            }
        }

        private static string log1;

        static TcpListener server = default(TcpListener);

        public static void HL7StopListen()
        {
            if (server != null)
            {
                server.Stop();
            }
        }

        public static void HL7Listen()
        {
            bool IsMessageCleaned = false;
            server = null;
            try
            {
                HL7ReportsDA.DataAccess.GetAllFieldNames();

                WriteToHL7LogFile("Inside HL7 Server Listener");
                // Set the TcpListener on port 13000.
                Int32 port = 2904;
                IPAddress localAddr = IPAddress.Any;

                server = new TcpListener(localAddr, port);


                // Buffer for reading data
                byte[] bytes = new byte[1025];
                string data = null;

                // Enter the listening loop.
                while (true)
                {
                    WriteToHL7LogFile("Waiting for a connection... ");
                    
                    // Start listening for client requests.
                    server.Start();

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    WriteToHL7LogFile("Connected!");


                    data = string.Empty;

                    // Get a stream object for reading and writing
                    //'WriteToHL7LogFile("Client: Is Data Available? ", client.GetStream.DataAvailable)
                    NetworkStream stream = client.GetStream();
                    Int32 i = default(Int32);
                    PipeParser hl7parser = new PipeParser();

                    bool IsACKSent = false;

                    while (client.Connected)
                    {   
                                        
                        // Loop to receive all the data sent by the client
                        i = stream.Read(bytes, 0, bytes.Length);
                        
                        WriteToHL7LogFile("Client: i = " + i);
                        while (i > 0)
                        {
                            //Translate data bytes to a ASCII string.
                            data = data + System.Text.UTF8Encoding.ASCII.GetString(bytes, 0, i);
                            WriteToHL7LogFile(data);

                            // Process the data sent by the client. 
                            i = stream.Read(bytes, 0, bytes.Length);
                        }

                        if (i < bytes.Length && !IsACKSent)
                        {
                            ACK ackRes = new ACK();
                            String ResMess = hl7parser.Encode(ackRes);
                            Byte[] msg; 
                            msg = Encoding.ASCII.GetBytes(ResMess);

                            WriteToHL7LogFile("Sending ack");
                            stream.Write(msg, 0, msg.Length);
                            IsACKSent = true;
                        }

                        stream.Close();
                        client.Close();
                        WriteToHL7LogFile("ack sent and connection closed.");

                        IsMessageCleaned = false;

                        if (!(data == null))
                        {
                            if (!IsMessageCleaned)
                            {
                                data = data.Remove(0, 1);
                                IsMessageCleaned = true;
                            }

                            string ver = hl7parser.GetVersion(data);
                            IMessage iMsg = hl7parser.Parse(data);

                            WriteToHL7LogFile("Message proces called");
                            Task t = new Task(() => ProcessMessage(iMsg, ver));
                            t.Start();

                           
                        }
                    }
                }
            }
            catch (SocketException e)
            {
                WriteToHL7LogFile("SocketException: {0}", e.Message);
            }
            finally
            {
                WriteToHL7LogFile("HL7 Server stopped");
            }
            WriteToHL7LogFile("Outside HL7 Server Listener");
        }

        private static void ProcessMessage(IMessage iMsg, String ver)
        {
            try
            {
                Report report;
                WriteToHL7LogFile("Message version " + ver);
                switch (ver)
                {
                    case "2.3":
                    case "2.3.1":
                        report = HL7ReportsDA.HL7ReportsBL.GetReportObject_23((NHapi.Model.V23.Message.ORU_R01)iMsg.Message);
                        break;

                    default:
                        WriteToHL7LogFile("Only V 2.3 mesages are supported. still trying out with 2.3 parser");
                        report = HL7ReportsDA.HL7ReportsBL.GetReportObject_23((NHapi.Model.V23.Message.ORU_R01)iMsg.Message);
                        break;
                }
                if (report != null)
                {
                    HL7ReportsDA.HL7ReportsBL.SaveNewReport(report);
                }


            }
            catch (Exception ee)
            {
                WriteToHL7LogFile("Message Ex: {0}", ee.Message);
            }
        }

        private static void WriteToHL7LogFile(string v, string message)
        {

            Common.Log.WriteFilelog(HL7ReportsDA.Constant.APP_NAME, HL7ReportsDA.Constant.HL7_Listener_ModuleName, v + " - " + message);

            log1 += v + " : " + message;

            if (Log != null)
            {
                Log.Invoke(v + " : " + message);
            }
        }

        private static void WriteToHL7LogFile(string v)
        {
            Common.Log.WriteFilelog(HL7ReportsDA.Constant.APP_NAME, HL7ReportsDA.Constant.HL7_Listener_ModuleName, v);
            log1 += v;
            if (Log != null)
            {
                Log.Invoke(v + " : " + v);
            }
        }
        //Main
        
    }
}