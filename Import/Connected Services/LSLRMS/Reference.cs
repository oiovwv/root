﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Import.LSLRMS {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://webservice.common.biz.com.cn", ConfigurationName="LSLRMS.MiddleServicePortType")]
    public interface MiddleServicePortType {
        
        // CODEGEN: 参数“return”需要其他方案信息，使用参数模式无法捕获这些信息。特定特性为“System.Xml.Serialization.XmlElementAttribute”。
        [System.ServiceModel.OperationContractAttribute(Action="urn:restMiddle", ReplyAction="urn:restMiddleResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        Import.LSLRMS.restMiddleResponse restMiddle(Import.LSLRMS.restMiddleRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:restMiddle", ReplyAction="urn:restMiddleResponse")]
        System.Threading.Tasks.Task<Import.LSLRMS.restMiddleResponse> restMiddleAsync(Import.LSLRMS.restMiddleRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="restMiddle", WrapperNamespace="http://webservice.common.biz.com.cn", IsWrapped=true)]
    public partial class restMiddleRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://webservice.common.biz.com.cn", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string sapData;
        
        public restMiddleRequest() {
        }
        
        public restMiddleRequest(string sapData) {
            this.sapData = sapData;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="restMiddleResponse", WrapperNamespace="http://webservice.common.biz.com.cn", IsWrapped=true)]
    public partial class restMiddleResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://webservice.common.biz.com.cn", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string @return;
        
        public restMiddleResponse() {
        }
        
        public restMiddleResponse(string @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface MiddleServicePortTypeChannel : Import.LSLRMS.MiddleServicePortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MiddleServicePortTypeClient : System.ServiceModel.ClientBase<Import.LSLRMS.MiddleServicePortType>, Import.LSLRMS.MiddleServicePortType {
        
        public MiddleServicePortTypeClient() {
        }
        
        public MiddleServicePortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MiddleServicePortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MiddleServicePortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MiddleServicePortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Import.LSLRMS.restMiddleResponse Import.LSLRMS.MiddleServicePortType.restMiddle(Import.LSLRMS.restMiddleRequest request) {
            return base.Channel.restMiddle(request);
        }
        
        public string restMiddle(string sapData) {
            Import.LSLRMS.restMiddleRequest inValue = new Import.LSLRMS.restMiddleRequest();
            inValue.sapData = sapData;
            Import.LSLRMS.restMiddleResponse retVal = ((Import.LSLRMS.MiddleServicePortType)(this)).restMiddle(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Import.LSLRMS.restMiddleResponse> Import.LSLRMS.MiddleServicePortType.restMiddleAsync(Import.LSLRMS.restMiddleRequest request) {
            return base.Channel.restMiddleAsync(request);
        }
        
        public System.Threading.Tasks.Task<Import.LSLRMS.restMiddleResponse> restMiddleAsync(string sapData) {
            Import.LSLRMS.restMiddleRequest inValue = new Import.LSLRMS.restMiddleRequest();
            inValue.sapData = sapData;
            return ((Import.LSLRMS.MiddleServicePortType)(this)).restMiddleAsync(inValue);
        }
    }
}