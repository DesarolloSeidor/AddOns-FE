﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace AddOnFE_Facturatech.FacturatechWSNC {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SERVICES-NOMINA-FACTURATECHBinding", Namespace="urn:https://ws-nomina.facturatech.co/v1/demo/")]
    public partial class SERVICESNOMINAFACTURATECH : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback FtechActionuploadDocumentOperationCompleted;
        
        private System.Threading.SendOrPostCallback FtechActiondocumentStatusOperationCompleted;
        
        private System.Threading.SendOrPostCallback FtechActiondownloadXMLOperationCompleted;
        
        private System.Threading.SendOrPostCallback FtechActiondownloadPDFOperationCompleted;
        
        private System.Threading.SendOrPostCallback FtechActiondownloadCUNEOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SERVICESNOMINAFACTURATECH() {
            this.Url = global::AddOnFE_Facturatech.Properties.Settings.Default.AddOnFE_Facturatech_FacturatechWSNC_SERVICES_NOMINA_FACTURATECH;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event FtechActionuploadDocumentCompletedEventHandler FtechActionuploadDocumentCompleted;
        
        /// <remarks/>
        public event FtechActiondocumentStatusCompletedEventHandler FtechActiondocumentStatusCompleted;
        
        /// <remarks/>
        public event FtechActiondownloadXMLCompletedEventHandler FtechActiondownloadXMLCompleted;
        
        /// <remarks/>
        public event FtechActiondownloadPDFCompletedEventHandler FtechActiondownloadPDFCompleted;
        
        /// <remarks/>
        public event FtechActiondownloadCUNECompletedEventHandler FtechActiondownloadCUNECompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:https://ws-nomina.facturatech.co/v1/demo/#FtechAction.uploadDocument", RequestNamespace="urn:https://ws-nomina.facturatech.co/v1/demo/", ResponseNamespace="urn:https://ws-nomina.facturatech.co/v1/demo/")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public uploadResponse FtechActionuploadDocument(string username, string password, string xmlBase64) {
            object[] results = this.Invoke("FtechActionuploadDocument", new object[] {
                        username,
                        password,
                        xmlBase64});
            return ((uploadResponse)(results[0]));
        }
        
        /// <remarks/>
        public void FtechActionuploadDocumentAsync(string username, string password, string xmlBase64) {
            this.FtechActionuploadDocumentAsync(username, password, xmlBase64, null);
        }
        
        /// <remarks/>
        public void FtechActionuploadDocumentAsync(string username, string password, string xmlBase64, object userState) {
            if ((this.FtechActionuploadDocumentOperationCompleted == null)) {
                this.FtechActionuploadDocumentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFtechActionuploadDocumentOperationCompleted);
            }
            this.InvokeAsync("FtechActionuploadDocument", new object[] {
                        username,
                        password,
                        xmlBase64}, this.FtechActionuploadDocumentOperationCompleted, userState);
        }
        
        private void OnFtechActionuploadDocumentOperationCompleted(object arg) {
            if ((this.FtechActionuploadDocumentCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FtechActionuploadDocumentCompleted(this, new FtechActionuploadDocumentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:https://ws-nomina.facturatech.co/v1/demo/#FtechAction.documentStatus", RequestNamespace="urn:https://ws-nomina.facturatech.co/v1/demo/", ResponseNamespace="urn:https://ws-nomina.facturatech.co/v1/demo/")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public documentStatusResponse FtechActiondocumentStatus(string username, string password, string transaccionID) {
            object[] results = this.Invoke("FtechActiondocumentStatus", new object[] {
                        username,
                        password,
                        transaccionID});
            return ((documentStatusResponse)(results[0]));
        }
        
        /// <remarks/>
        public void FtechActiondocumentStatusAsync(string username, string password, string transaccionID) {
            this.FtechActiondocumentStatusAsync(username, password, transaccionID, null);
        }
        
        /// <remarks/>
        public void FtechActiondocumentStatusAsync(string username, string password, string transaccionID, object userState) {
            if ((this.FtechActiondocumentStatusOperationCompleted == null)) {
                this.FtechActiondocumentStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFtechActiondocumentStatusOperationCompleted);
            }
            this.InvokeAsync("FtechActiondocumentStatus", new object[] {
                        username,
                        password,
                        transaccionID}, this.FtechActiondocumentStatusOperationCompleted, userState);
        }
        
        private void OnFtechActiondocumentStatusOperationCompleted(object arg) {
            if ((this.FtechActiondocumentStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FtechActiondocumentStatusCompleted(this, new FtechActiondocumentStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:https://ws-nomina.facturatech.co/v1/demo/#FtechAction.downloadXML", RequestNamespace="urn:https://ws-nomina.facturatech.co/v1/demo/", ResponseNamespace="urn:https://ws-nomina.facturatech.co/v1/demo/")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public downloadXMLResponse FtechActiondownloadXML(string username, string password, string prefix, [System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string number) {
            object[] results = this.Invoke("FtechActiondownloadXML", new object[] {
                        username,
                        password,
                        prefix,
                        number});
            return ((downloadXMLResponse)(results[0]));
        }
        
        /// <remarks/>
        public void FtechActiondownloadXMLAsync(string username, string password, string prefix, string number) {
            this.FtechActiondownloadXMLAsync(username, password, prefix, number, null);
        }
        
        /// <remarks/>
        public void FtechActiondownloadXMLAsync(string username, string password, string prefix, string number, object userState) {
            if ((this.FtechActiondownloadXMLOperationCompleted == null)) {
                this.FtechActiondownloadXMLOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFtechActiondownloadXMLOperationCompleted);
            }
            this.InvokeAsync("FtechActiondownloadXML", new object[] {
                        username,
                        password,
                        prefix,
                        number}, this.FtechActiondownloadXMLOperationCompleted, userState);
        }
        
        private void OnFtechActiondownloadXMLOperationCompleted(object arg) {
            if ((this.FtechActiondownloadXMLCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FtechActiondownloadXMLCompleted(this, new FtechActiondownloadXMLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:https://ws-nomina.facturatech.co/v1/demo/#FtechAction.downloadPDFResponse", RequestNamespace="urn:https://ws-nomina.facturatech.co/v1/demo/", ResponseNamespace="urn:https://ws-nomina.facturatech.co/v1/demo/")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public downloadPDFResponse FtechActiondownloadPDF(string username, string password, string prefix, [System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string number) {
            object[] results = this.Invoke("FtechActiondownloadPDF", new object[] {
                        username,
                        password,
                        prefix,
                        number});
            return ((downloadPDFResponse)(results[0]));
        }
        
        /// <remarks/>
        public void FtechActiondownloadPDFAsync(string username, string password, string prefix, string number) {
            this.FtechActiondownloadPDFAsync(username, password, prefix, number, null);
        }
        
        /// <remarks/>
        public void FtechActiondownloadPDFAsync(string username, string password, string prefix, string number, object userState) {
            if ((this.FtechActiondownloadPDFOperationCompleted == null)) {
                this.FtechActiondownloadPDFOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFtechActiondownloadPDFOperationCompleted);
            }
            this.InvokeAsync("FtechActiondownloadPDF", new object[] {
                        username,
                        password,
                        prefix,
                        number}, this.FtechActiondownloadPDFOperationCompleted, userState);
        }
        
        private void OnFtechActiondownloadPDFOperationCompleted(object arg) {
            if ((this.FtechActiondownloadPDFCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FtechActiondownloadPDFCompleted(this, new FtechActiondownloadPDFCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("urn:https://ws-nomina.facturatech.co/v1/demo/#FtechAction.downloadCUNEResponse", RequestNamespace="urn:https://ws-nomina.facturatech.co/v1/demo/", ResponseNamespace="urn:https://ws-nomina.facturatech.co/v1/demo/")]
        [return: System.Xml.Serialization.SoapElementAttribute("return")]
        public downloadCUNEResponse FtechActiondownloadCUNE(string username, string password, string prefix, [System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string number) {
            object[] results = this.Invoke("FtechActiondownloadCUNE", new object[] {
                        username,
                        password,
                        prefix,
                        number});
            return ((downloadCUNEResponse)(results[0]));
        }
        
        /// <remarks/>
        public void FtechActiondownloadCUNEAsync(string username, string password, string prefix, string number) {
            this.FtechActiondownloadCUNEAsync(username, password, prefix, number, null);
        }
        
        /// <remarks/>
        public void FtechActiondownloadCUNEAsync(string username, string password, string prefix, string number, object userState) {
            if ((this.FtechActiondownloadCUNEOperationCompleted == null)) {
                this.FtechActiondownloadCUNEOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFtechActiondownloadCUNEOperationCompleted);
            }
            this.InvokeAsync("FtechActiondownloadCUNE", new object[] {
                        username,
                        password,
                        prefix,
                        number}, this.FtechActiondownloadCUNEOperationCompleted, userState);
        }
        
        private void OnFtechActiondownloadCUNEOperationCompleted(object arg) {
            if ((this.FtechActiondownloadCUNECompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FtechActiondownloadCUNECompleted(this, new FtechActiondownloadCUNECompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:https://ws-nomina.facturatech.co/v1/demo/")]
    public partial class uploadResponse {
        
        private string codeField;
        
        private string transactionIDField;
        
        private string errorField;
        
        /// <remarks/>
        public string code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        public string transactionID {
            get {
                return this.transactionIDField;
            }
            set {
                this.transactionIDField = value;
            }
        }
        
        /// <remarks/>
        public string error {
            get {
                return this.errorField;
            }
            set {
                this.errorField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:https://ws-nomina.facturatech.co/v1/demo/")]
    public partial class downloadCUNEResponse {
        
        private string codeField;
        
        private string messageField;
        
        private string resourceDataField;
        
        private string messageErrorField;
        
        /// <remarks/>
        public string code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        public string resourceData {
            get {
                return this.resourceDataField;
            }
            set {
                this.resourceDataField = value;
            }
        }
        
        /// <remarks/>
        public string messageError {
            get {
                return this.messageErrorField;
            }
            set {
                this.messageErrorField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:https://ws-nomina.facturatech.co/v1/demo/")]
    public partial class downloadPDFResponse {
        
        private string codeField;
        
        private string documentBase64Field;
        
        private string messageField;
        
        private string messageErrorField;
        
        /// <remarks/>
        public string code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        public string documentBase64 {
            get {
                return this.documentBase64Field;
            }
            set {
                this.documentBase64Field = value;
            }
        }
        
        /// <remarks/>
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        public string messageError {
            get {
                return this.messageErrorField;
            }
            set {
                this.messageErrorField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:https://ws-nomina.facturatech.co/v1/demo/")]
    public partial class downloadXMLResponse {
        
        private string codeField;
        
        private string documentBase64Field;
        
        private string arDocumentField;
        
        private string messageField;
        
        private string messageErrorField;
        
        /// <remarks/>
        public string code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        public string documentBase64 {
            get {
                return this.documentBase64Field;
            }
            set {
                this.documentBase64Field = value;
            }
        }
        
        /// <remarks/>
        public string arDocument {
            get {
                return this.arDocumentField;
            }
            set {
                this.arDocumentField = value;
            }
        }
        
        /// <remarks/>
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        public string messageError {
            get {
                return this.messageErrorField;
            }
            set {
                this.messageErrorField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:https://ws-nomina.facturatech.co/v1/demo/")]
    public partial class documentStatusResponse {
        
        private string codeField;
        
        private string documentBase64Field;
        
        private string arDocumentField;
        
        private string messageField;
        
        private string messageErrorField;
        
        /// <remarks/>
        public string code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        public string documentBase64 {
            get {
                return this.documentBase64Field;
            }
            set {
                this.documentBase64Field = value;
            }
        }
        
        /// <remarks/>
        public string arDocument {
            get {
                return this.arDocumentField;
            }
            set {
                this.arDocumentField = value;
            }
        }
        
        /// <remarks/>
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        public string messageError {
            get {
                return this.messageErrorField;
            }
            set {
                this.messageErrorField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    public delegate void FtechActionuploadDocumentCompletedEventHandler(object sender, FtechActionuploadDocumentCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FtechActionuploadDocumentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FtechActionuploadDocumentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public uploadResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((uploadResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    public delegate void FtechActiondocumentStatusCompletedEventHandler(object sender, FtechActiondocumentStatusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FtechActiondocumentStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FtechActiondocumentStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public documentStatusResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((documentStatusResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    public delegate void FtechActiondownloadXMLCompletedEventHandler(object sender, FtechActiondownloadXMLCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FtechActiondownloadXMLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FtechActiondownloadXMLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public downloadXMLResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((downloadXMLResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    public delegate void FtechActiondownloadPDFCompletedEventHandler(object sender, FtechActiondownloadPDFCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FtechActiondownloadPDFCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FtechActiondownloadPDFCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public downloadPDFResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((downloadPDFResponse)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    public delegate void FtechActiondownloadCUNECompletedEventHandler(object sender, FtechActiondownloadCUNECompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FtechActiondownloadCUNECompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FtechActiondownloadCUNECompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public downloadCUNEResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((downloadCUNEResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591