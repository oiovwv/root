using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestLSLService.LSLService;

namespace TestLSLService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LSLFeedback();
        }

        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }

        private static void LSLFeedback()
        {

            zdk_server2Client service = new zdk_server2Client();
            service.ClientCredentials.UserName.UserName = "RFC_CONN";
            service.ClientCredentials.UserName.Password = "dcrfc&*(conn12)";
            ZDATA_TO_SAP_SYNC2 SYNC = new ZDATA_TO_SAP_SYNC2();
            ZDATA_TO_SAP_SYNC2Response sr2 = new ZDATA_TO_SAP_SYNC2Response();
            SYNC.IN_JSON = "";
            ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(RemoteCertificateValidate));
            sr2 = service.ZDATA_TO_SAP_SYNC2(SYNC);
            var res = sr2.OUT_JSON;
        }
    }
}
