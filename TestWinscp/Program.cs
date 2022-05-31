using System;
using WinSCP;

namespace TestWinscp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            try
            {
                // Setup session options
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = "10.205.208.85",
                    UserName = "op_ordersz",
                    Password = "lzdlwy01",
                    //SshHostKeyFingerprint = "ssh-rsa 2048 xxxxxxxxxxx...="
                };

                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);

                    // Download files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;

                    TransferOperationResult transferResult;
                    transferResult =
                        session.GetFiles("/home/op_ordersz/cst_bds/op_bds_sh*", @"C:\Toll\", false, transferOptions);

                    // Throw on any error
                    transferResult.Check();

                    // Print results
                    foreach (TransferEventArgs transfer in transferResult.Transfers)
                    {
                        Console.WriteLine("Download of {0} succeeded", transfer.FileName);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                
            }
        }
    }
}
