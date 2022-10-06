using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace vlapp.Models
{
    public class ConnectionModel
    {

        public ObservableCollection<NodeListItem> bList = new ObservableCollection<NodeListItem>();
        private Socket? _clientSocket;
        public bool receiveData { get; set; }
        public bool receievIP { get; set; }
        byte[] res = new byte[1];
        byte[] resData = new byte[255];
        public NodeListItem[] ?BlindList;
        private string ipadd = "192.168.1.48";

        public void Connect()
        {
            try
            {
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _clientSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(ipadd), 6969), new AsyncCallback(ConnectCallback), null);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ConnectCallback(IAsyncResult AR)
        {
            try
            {
                _clientSocket.EndConnect(AR);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //1 = getConnection
        public void getConnection()
        {
            try
            {
                byte[] fileBuffer;

                fileBuffer = new byte[5];
                fileBuffer[0] = 2;
                fileBuffer[1] = 70;
                fileBuffer[2] = 254;
                fileBuffer[3] = (byte)52; //command
                fileBuffer[4] = (byte)0; //value if meron

                _clientSocket.BeginSend(fileBuffer, 0, fileBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);

                _clientSocket.BeginReceive(res, 0, res.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);

                if (res[0] == Convert.ToByte(69))
                {
                    this.receiveData = true;
                    res[0] = 0;
                }
                else
                {
                    this.receiveData = false;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void sendArrange(ObservableCollection<NodeListItem> list)
        {
            int fBuffSize = (list.Count * 6) + 5;
            byte[] fileBuffer2 = new byte[fBuffSize];

            //sort 
            list = new ObservableCollection<NodeListItem>(list.OrderBy(i => i.nodeIndex));
            //Array.Sort(blindList, delegate (BlindListItem x, BlindListItem y) { return x.BlindIndex.CompareTo(y.BlindIndex); });

            fileBuffer2[0] = 2;
            fileBuffer2[1] = 70;
            fileBuffer2[2] = 254;
            fileBuffer2[3] = (byte)53; //command
            fileBuffer2[4] = (byte)(fBuffSize - 5);
            int buffAddr = 5;
            for (int v = 0; v < list.Count; v++)
            {
                fileBuffer2[buffAddr] = list[v].nodeIndex;
                buffAddr++;
                for (int t = 0; t < list[v].BlindIp.Length; t++)
                {
                    fileBuffer2[buffAddr] = list[v].BlindIp[t];
                    buffAddr++;
                }

                //store prev esp blind id
                byte tempPrevBlindId = 0;
                for (int u = 0; u < v; u++)
                {
                    tempPrevBlindId += list[u].BlindNumber;
                }
                fileBuffer2[buffAddr] = tempPrevBlindId;
                buffAddr++;

                //if (v != 0)
                //{
                //    fileBuffer2[buffAddr] = EspList[v - 1].blindNum;
                //    buffAddr++;
                //}
                //else 
                //{
                //    fileBuffer2[buffAddr] = 0;
                //    buffAddr++;
                //}
            }
            buffAddr = 0;

            sendData(fileBuffer2);
        }

        private void sendData(byte[] buffData)
        {
            TcpClient clientSocket = new TcpClient(ipadd, 6969);
            NetworkStream networkStream = clientSocket.GetStream();
            networkStream.Write(buffData, 0, buffData.GetLength(0));
            networkStream.Close();
        }

        public byte[] sendWithResponse()
        {
            byte[] fileBuffer = new byte[5];
            fileBuffer[0] = 2;
            fileBuffer[1] = 70;
            fileBuffer[2] = 254;
            fileBuffer[3] = (byte)55; //command
            fileBuffer[4] = (byte)0; //value if meron

            TcpClient clientSocket = new TcpClient(ipadd, 6969);
            NetworkStream networkStream = clientSocket.GetStream();
            networkStream.Write(fileBuffer, 0, fileBuffer.GetLength(0));
            int x = networkStream.Read(resData, 0, resData.Length);
            networkStream.Close();

            initializeIpAddr(resData);

            return resData;
        }

        public void SendCallback(IAsyncResult AR)
        {
            try
            {
                _clientSocket.EndSend(AR);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
               _clientSocket.EndReceive(AR);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void initializeIpAddr(byte[] src)
        {
            int ipListSize = (src[0] - 1) / 6;
            BlindList = new NodeListItem[ipListSize];
            int tempAddr = 1;
            for (int i = 0; i < ipListSize; i++)
            {
                byte lIndex = src[tempAddr];
                tempAddr++;
                byte lBlindNum = src[tempAddr];
                tempAddr++;
                byte[] lIpAddr = new byte[4];
                for (int j = 0; j < lIpAddr.Length; j++)
                {
                    lIpAddr[j] = src[tempAddr];
                    tempAddr++;
                }

                NodeListItem item = new NodeListItem(lIndex, lBlindNum, lIpAddr);
                BlindList[i] = item;
            }

            //sort 
            Array.Sort(BlindList, delegate (NodeListItem x, NodeListItem y) { return x.nodeIndex.CompareTo(y.nodeIndex); });
            tempAddr = 0;
        }

        public ObservableCollection<NodeListItem> GetBlindList()
        {
            foreach (NodeListItem element in BlindList)
            {
                bList.Add(element);
            }

            return bList;
        }

        //public bool ConnectionCheck()
        //{
        //    string response;
        //    byte[] fileBuffer;
        //    fileBuffer = new byte[5];
        //    fileBuffer[0] = 2;
        //    fileBuffer[1] = 70;
        //    fileBuffer[2] = 254;
        //    fileBuffer[3] = (byte)52; //command
        //    fileBuffer[4] = (byte)0; //value if meron
        //    byte[] res = new byte[1];
        //    SendCommandWithResponse(res, fileBuffer);
        //    response =  res[0].ToString();




        //    if (response == "69")
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        //private void SendCommandWithResponse(byte[] buffFback, byte[] buffData)
        //{
        //    try
        //    {
        //        TcpClient clientSocket = new TcpClient("127.0.0.1", 6969);
        //        NetworkStream networkStream = clientSocket.GetStream();
        //        networkStream.Write(buffData, 0, buffData.GetLength(0));
        //        networkStream.Read(buffFback, 0, buffFback.Length);
        //        networkStream.Close();

        //    }
        //    catch
        //    {

        //    }


        //}
    }
}
