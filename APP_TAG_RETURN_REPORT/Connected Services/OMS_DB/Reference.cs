﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace APP_TAG_RETURN_REPORT.OMS_DB {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OMS_DB.DataBaseAccessSoap")]
    public interface DataBaseAccessSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetDataSet", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetDataSet(string strSql, string[] parValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetDataSet", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetDataSetAsync(string strSql, string[] parValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPageDataSet", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        APP_TAG_RETURN_REPORT.OMS_DB.GetPageDataSetResponse GetPageDataSet(APP_TAG_RETURN_REPORT.OMS_DB.GetPageDataSetRequest request);
        
        // CODEGEN: 正在生成消息协定，应为该操作具有多个返回值。
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPageDataSet", ReplyAction="*")]
        System.Threading.Tasks.Task<APP_TAG_RETURN_REPORT.OMS_DB.GetPageDataSetResponse> GetPageDataSetAsync(APP_TAG_RETURN_REPORT.OMS_DB.GetPageDataSetRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ExecuteNonQuery", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int ExecuteNonQuery(string strSql, string[] parValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ExecuteNonQuery", ReplyAction="*")]
        System.Threading.Tasks.Task<int> ExecuteNonQueryAsync(string strSql, string[] parValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DoScalar", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object DoScalar(string strSql, string[] parValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DoScalar", ReplyAction="*")]
        System.Threading.Tasks.Task<object> DoScalarAsync(string strSql, string[] parValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DoTransaction", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object DoTransaction(object[] strSql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DoTransaction", ReplyAction="*")]
        System.Threading.Tasks.Task<object> DoTransactionAsync(object[] strSql);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CallExportProgram", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void CallExportProgram();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CallExportProgram", ReplyAction="*")]
        System.Threading.Tasks.Task CallExportProgramAsync();
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetPageDataSet", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetPageDataSetRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string ProduceName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public System.Data.DataTable parms;
        
        public GetPageDataSetRequest() {
        }
        
        public GetPageDataSetRequest(string ProduceName, System.Data.DataTable parms) {
            this.ProduceName = ProduceName;
            this.parms = parms;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetPageDataSetResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetPageDataSetResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.Data.DataSet GetPageDataSetResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public int TotalLine;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public int TotalPage;
        
        public GetPageDataSetResponse() {
        }
        
        public GetPageDataSetResponse(System.Data.DataSet GetPageDataSetResult, int TotalLine, int TotalPage) {
            this.GetPageDataSetResult = GetPageDataSetResult;
            this.TotalLine = TotalLine;
            this.TotalPage = TotalPage;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DataBaseAccessSoapChannel : APP_TAG_RETURN_REPORT.OMS_DB.DataBaseAccessSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DataBaseAccessSoapClient : System.ServiceModel.ClientBase<APP_TAG_RETURN_REPORT.OMS_DB.DataBaseAccessSoap>, APP_TAG_RETURN_REPORT.OMS_DB.DataBaseAccessSoap {
        
        public DataBaseAccessSoapClient() {
        }
        
        public DataBaseAccessSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DataBaseAccessSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataBaseAccessSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataBaseAccessSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet GetDataSet(string strSql, string[] parValue) {
            return base.Channel.GetDataSet(strSql, parValue);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetDataSetAsync(string strSql, string[] parValue) {
            return base.Channel.GetDataSetAsync(strSql, parValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        APP_TAG_RETURN_REPORT.OMS_DB.GetPageDataSetResponse APP_TAG_RETURN_REPORT.OMS_DB.DataBaseAccessSoap.GetPageDataSet(APP_TAG_RETURN_REPORT.OMS_DB.GetPageDataSetRequest request) {
            return base.Channel.GetPageDataSet(request);
        }
        
        public System.Data.DataSet GetPageDataSet(string ProduceName, System.Data.DataTable parms, out int TotalLine, out int TotalPage) {
            APP_TAG_RETURN_REPORT.OMS_DB.GetPageDataSetRequest inValue = new APP_TAG_RETURN_REPORT.OMS_DB.GetPageDataSetRequest();
            inValue.ProduceName = ProduceName;
            inValue.parms = parms;
            APP_TAG_RETURN_REPORT.OMS_DB.GetPageDataSetResponse retVal = ((APP_TAG_RETURN_REPORT.OMS_DB.DataBaseAccessSoap)(this)).GetPageDataSet(inValue);
            TotalLine = retVal.TotalLine;
            TotalPage = retVal.TotalPage;
            return retVal.GetPageDataSetResult;
        }
        
        public System.Threading.Tasks.Task<APP_TAG_RETURN_REPORT.OMS_DB.GetPageDataSetResponse> GetPageDataSetAsync(APP_TAG_RETURN_REPORT.OMS_DB.GetPageDataSetRequest request) {
            return base.Channel.GetPageDataSetAsync(request);
        }
        
        public int ExecuteNonQuery(string strSql, string[] parValue) {
            return base.Channel.ExecuteNonQuery(strSql, parValue);
        }
        
        public System.Threading.Tasks.Task<int> ExecuteNonQueryAsync(string strSql, string[] parValue) {
            return base.Channel.ExecuteNonQueryAsync(strSql, parValue);
        }
        
        public object DoScalar(string strSql, string[] parValue) {
            return base.Channel.DoScalar(strSql, parValue);
        }
        
        public System.Threading.Tasks.Task<object> DoScalarAsync(string strSql, string[] parValue) {
            return base.Channel.DoScalarAsync(strSql, parValue);
        }
        
        public object DoTransaction(object[] strSql) {
            return base.Channel.DoTransaction(strSql);
        }
        
        public System.Threading.Tasks.Task<object> DoTransactionAsync(object[] strSql) {
            return base.Channel.DoTransactionAsync(strSql);
        }
        
        public void CallExportProgram() {
            base.Channel.CallExportProgram();
        }
        
        public System.Threading.Tasks.Task CallExportProgramAsync() {
            return base.Channel.CallExportProgramAsync();
        }
    }
}
