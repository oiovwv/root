using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Models
{
    class Scan_Model
    {

    }
    public class __metadata
    {
        public string id
        {
            get;
            set;
        }

        public string uri
        {
            get;
            set;
        }

        public string type
        {
            get;
            set;
        }
    }
    public class Traceability
    {
        public string Delivery
        {
            get;
            set;
        }

        public string Pcode
        {
            get;
            set;
        }

        public string Rescan
        {
            get;
            set;
        }

        public List<IxOBQtyItem> IxOBQty
        {
            get;
            set;
        }

        public List<ExSaveMsgItem> ExSaveMsg
        {
            get;
            set;
        }
    }
    public class ExOBMsg
    {
        public List<ExOBMsgResultsItem> results
        {
            get;
            set;
        }
    }

    public class ExOBMsg_NonOEM
    {
        public List<OBMsgResultsItem> results
        {
            get;
            set;
        }
    }
    public class ExOBMsgResultsItem
    {
        public __metadata __metadata
        {
            get;
            set;
        }

        public string Delivery
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }
    public class ExOBQty
    {
        public List<ExOBQtyResultsItem> results
        {
            get;
            set;
        }
    }
    public class ExOBQty_NonOEM
    {
        public List<OEMResultsItem> results
        {
            get;
            set;
        }
    }

    public class ExOBQtyResultsItem
    {
        public __metadata __metadata
        {
            get;
            set;
        }

        public string Delivery
        {
            get;
            set;
        }

        public string Material
        {
            get;
            set;
        }

        public int TotalBoxQty
        {
            get;
            set;
        }

        public int CurrentBoxQty
        {
            get;
            set;
        }
    }
    public class ExPcodeMsg
    {
        public List<PcodeResultsItemB> results
        {
            get;
            set;
        }
    }

    public class ExSaveMsg
    {
        public List<ExSaveMsgResultsItem> results
        {
            get;
            set;
        }
    }
    public class ExSaveMsg_NonOEM
    {
        public List<NonOEMResultsItem> results
        {
            get;
            set;
        }
    }
    public class ExSaveMsg_NonOEMItem
    {
    }
    public class ExSaveMsgItem
    {
    }
    public class ExSaveMsgResultsItem
    {
        public __metadata __metadata
        {
            get;
            set;
        }

        public string Delivery
        {
            get;
            set;
        }

        public string Pcode
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }
    public class IxOBQty
    {
        public List<IxOBQtyResultsItem> results
        {
            get;
            set;
        }
    }
    public class IxOBQtyItem
    {
        public string Delivery
        {
            get;
            set;
        }

        public string Material
        {
            get;
            set;
        }

        public int TotalBoxQty
        {
            get;
            set;
        }

        public int CurrentBoxQty
        {
            get;
            set;
        }
    }
    public class IxOBQtyResultsItem
    {
        public __metadata __metadata
        {
            get;
            set;
        }

        public string Delivery
        {
            get;
            set;
        }

        public string Material
        {
            get;
            set;
        }

        public int TotalBoxQty
        {
            get;
            set;
        }

        public int CurrentBoxQty
        {
            get;
            set;
        }
    }
    public class NonOEMD
    {
        public __metadata __metadata
        {
            get;
            set;
        }

        public string Delivery
        {
            get;
            set;
        }

        public string Material
        {
            get;
            set;
        }

        public string AntiFakeNo
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }

        public string ScanUnit
        {
            get;
            set;
        }

        public int QtyScanned
        {
            get;
            set;
        }

        public string BaseUnit
        {
            get;
            set;
        }

        public string Rescan
        {
            get;
            set;
        }

        public ExSaveMsg_NonOEM ExSaveMsg_NonOEM
        {
            get;
            set;
        }
    }
    public class NonOEMDRoot
    {
        public NonOEMD d
        {
            get;
            set;
        }
    }
    public class NonOEMDRootA
    {
        public NonOEMDRoot result
        {
            get;
            set;
        }
    }
    public class NonOEMResultsItem
    {
        public __metadata __metadata
        {
            get;
            set;
        }

        public string Delivery
        {
            get;
            set;
        }

        public string Material
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }
    public class OBMsgResultsItem
    {
        public __metadata __metadata
        {
            get;
            set;
        }

        public string Delivery
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }
    public class ObSetD
    {
        public List<ObSetResultsItem> results
        {
            get;
            set;
        }
    }
    public class ObSetResultsItem
    {
        public __metadata __metadata
        {
            get;
            set;
        }

        public string Delivery
        {
            get;
            set;
        }

        public ExOBMsg ExOBMsg
        {
            get;
            set;
        }

        public ExOBQty ExOBQty
        {
            get;
            set;
        }
    }
    public class ObSetRoot
    {
        public ObSetD d
        {
            get;
            set;
        }
    }
    public class ObSetRootA
    {
        public ObSetRoot result
        {
            get;
            set;
        }
    }
    public class OEMD
    {
        public List<OEMResultsItemA> results
        {
            get;
            set;
        }
    }
    public class OEMResultsItem
    {
        public __metadata __metadata
        {
            get;
            set;
        }

        public string Delivery
        {
            get;
            set;
        }

        public string Material
        {
            get;
            set;
        }

        public string ScanUnit
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }

        public string TotalQty
        {
            get;
            set;
        }

        public string QtyScanned
        {
            get;
            set;
        }

        public string OpenQty
        {
            get;
            set;
        }

        public string SalesUnit
        {
            get;
            set;
        }

        public string BaseUnit
        {
            get;
            set;
        }
    }
    public class OEMResultsItemA
    {
        public __metadata __metadata
        {
            get;
            set;
        }

        public string Delivery
        {
            get;
            set;
        }

        public ExOBQty_NonOEM ExOBQty_NonOEM
        {
            get;
            set;
        }

        public ExOBMsg_NonOEM ExOBMsg_NonOEM
        {
            get;
            set;
        }
    }
    public class OEMRoot
    {
        public OEMD d
        {
            get;
            set;
        }
    }
    public class OEMRootA
    {
        public OEMRoot result
        {
            get;
            set;
        }
    }
    public class OEMTraceability
    {
        public string Delivery
        {
            get;
            set;
        }

        public string Material
        {
            get;
            set;
        }

        public string AntiFakeNo
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }

        public string ScanUnit
        {
            get;
            set;
        }

        public int QtyScanned
        {
            get;
            set;
        }

        public string BaseUnit
        {
            get;
            set;
        }

        public string Rescan
        {
            get;
            set;
        }

        public List<ExSaveMsg_NonOEMItem> ExSaveMsg_NonOEM
        {
            get;
            set;
        }
    }
    public class PcodeD
    {
        public List<PcodeResultsItemA> results
        {
            get;
            set;
        }
    }
    public class PcodeResultsItemA
    {
        public __metadata __metadata
        {
            get;
            set;
        }

        public string Pcode
        {
            get;
            set;
        }

        public string ShipTo
        {
            get;
            set;
        }

        public ExPcodeMsg ExPcodeMsg
        {
            get;
            set;
        }
    }
    public class PcodeResultsItemB
    {
        public __metadata __metadata
        {
            get;
            set;
        }

        public string Pcode
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }
    public class PcodeRoot
    {
        public PcodeD d
        {
            get;
            set;
        }
    }
    public class PcodeRootA
    {
        public PcodeRoot result
        {
            get;
            set;
        }
    }
    public class SaveD
    {
        public __metadata __metadata
        {
            get;
            set;
        }

        public string Delivery
        {
            get;
            set;
        }

        public string Pcode
        {
            get;
            set;
        }

        public string Rescan
        {
            get;
            set;
        }

        public IxOBQty IxOBQty
        {
            get;
            set;
        }

        public ExSaveMsg ExSaveMsg
        {
            get;
            set;
        }
    }
    public class SaveRoot
    {
        public SaveD d
        {
            get;
            set;
        }
    }
    public class SaveRootA
    {
        public SaveRoot result
        {
            get;
            set;
        }
    }

}
