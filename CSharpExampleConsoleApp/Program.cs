using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExampleConsoleApp
{
    class Program
    {
        TWSLib.TwsClass Tws1 = new TWSLib.TwsClass();

        static void Main(string[] args)
        {
            Program programInstance = new Program();
            
            programInstance.bindEvents();
            programInstance.startConnection();
            programInstance.requestData();
            Console.ReadLine();
        }
        private void bindEvents()
        {
            Tws1.errMsg += new TWSLib._DTwsEvents_errMsgEventHandler(this.OnErrMsg);
            Tws1.tickPrice += new TWSLib._DTwsEvents_tickPriceEventHandler(this.OnTickPrice);
            Tws1.tickSize += new TWSLib._DTwsEvents_tickSizeEventHandler(this.OnTickSize);
            Tws1.tickSnapshotEnd += new TWSLib._DTwsEvents_tickSnapshotEndEventHandler(this.OnTickSnapshotEnd);
        }

        private void startConnection()
        {
            Console.WriteLine("Connecting...");
            Tws1.connect("", 7496, 1);
        }

        private void OnErrMsg(int id, int errorCode, string errorMsg)
        {
            Console.WriteLine(errorCode + " - " + errorMsg);
        }

        private void OnTickPrice(int id, int tickType, double price, int canAutoExecute)
        {
            var TickType = "";
            switch (tickType)
            {
                case 1:
                    TickType = "bid";
                    break;
                case 2:
                    TickType = "ask";
                    break;
                case 4:
                    TickType = "last";
                    break;
                case 6:
                    TickType = "high";
                    break;
                case 7:
                    TickType = "low";
                    break;
                case 9:
                    TickType = "close";
                    break;
            }
            Console.WriteLine(TickType + " - " + price);
        }
        private void OnTickSize(int id, int tickType, int size)
        {
            var TickType = "";
            switch (tickType)
            {
                case 0:
                    TickType = "bid size";
                    break;
                case 3:
                    TickType = "ask size";
                    break;
                case 5:
                    TickType = "last size";
                    break;
                case 8:
                    TickType = "volume";
                    break;
            }
            Console.WriteLine(TickType + " - " + size);
        }

        private void OnTickSnapshotEnd(int id)
        {
            Tws1.disconnect();
        }

        private void requestData()
        {
            //Define contract
            TWSLib.IContract Contract = Tws1.createContract();
            Contract.symbol = "USD";
            Contract.secType = "CASH";
            Contract.exchange = "IDEALPRO";
            Contract.currency = "JPY";
            //Make request
            Tws1.reqMktDataEx(1, Contract, "", 1);
        }
    }
}
