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

namespace ReceiveService.BroadcastService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="BroadcastServiceSoap", Namespace="localhost")]
    public partial class BroadcastService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback RegisterOperationCompleted;
        
        private System.Threading.SendOrPostCallback UnregisterOperationCompleted;
        
        private System.Threading.SendOrPostCallback PutInMailboxOperationCompleted;
        
        private System.Threading.SendOrPostCallback CheckMailboxOperationCompleted;
        
        private System.Threading.SendOrPostCallback ClearEverythingOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public BroadcastService() {
            this.Url = global::ReceiveService.Properties.Settings.Default.ReceiveService_BroadcastService_BroadcastService;
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
        public event RegisterCompletedEventHandler RegisterCompleted;
        
        /// <remarks/>
        public event UnregisterCompletedEventHandler UnregisterCompleted;
        
        /// <remarks/>
        public event PutInMailboxCompletedEventHandler PutInMailboxCompleted;
        
        /// <remarks/>
        public event CheckMailboxCompletedEventHandler CheckMailboxCompleted;
        
        /// <remarks/>
        public event ClearEverythingCompletedEventHandler ClearEverythingCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/Register", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int Register() {
            object[] results = this.Invoke("Register", new object[0]);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void RegisterAsync() {
            this.RegisterAsync(null);
        }
        
        /// <remarks/>
        public void RegisterAsync(object userState) {
            if ((this.RegisterOperationCompleted == null)) {
                this.RegisterOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRegisterOperationCompleted);
            }
            this.InvokeAsync("Register", new object[0], this.RegisterOperationCompleted, userState);
        }
        
        private void OnRegisterOperationCompleted(object arg) {
            if ((this.RegisterCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RegisterCompleted(this, new RegisterCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/Unregister", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Unregister(int ID) {
            this.Invoke("Unregister", new object[] {
                        ID});
        }
        
        /// <remarks/>
        public void UnregisterAsync(int ID) {
            this.UnregisterAsync(ID, null);
        }
        
        /// <remarks/>
        public void UnregisterAsync(int ID, object userState) {
            if ((this.UnregisterOperationCompleted == null)) {
                this.UnregisterOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUnregisterOperationCompleted);
            }
            this.InvokeAsync("Unregister", new object[] {
                        ID}, this.UnregisterOperationCompleted, userState);
        }
        
        private void OnUnregisterOperationCompleted(object arg) {
            if ((this.UnregisterCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UnregisterCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/PutInMailbox", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int PutInMailbox(int ID, string message) {
            object[] results = this.Invoke("PutInMailbox", new object[] {
                        ID,
                        message});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void PutInMailboxAsync(int ID, string message) {
            this.PutInMailboxAsync(ID, message, null);
        }
        
        /// <remarks/>
        public void PutInMailboxAsync(int ID, string message, object userState) {
            if ((this.PutInMailboxOperationCompleted == null)) {
                this.PutInMailboxOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPutInMailboxOperationCompleted);
            }
            this.InvokeAsync("PutInMailbox", new object[] {
                        ID,
                        message}, this.PutInMailboxOperationCompleted, userState);
        }
        
        private void OnPutInMailboxOperationCompleted(object arg) {
            if ((this.PutInMailboxCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PutInMailboxCompleted(this, new PutInMailboxCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/CheckMailbox", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] CheckMailbox(int ID) {
            object[] results = this.Invoke("CheckMailbox", new object[] {
                        ID});
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public void CheckMailboxAsync(int ID) {
            this.CheckMailboxAsync(ID, null);
        }
        
        /// <remarks/>
        public void CheckMailboxAsync(int ID, object userState) {
            if ((this.CheckMailboxOperationCompleted == null)) {
                this.CheckMailboxOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckMailboxOperationCompleted);
            }
            this.InvokeAsync("CheckMailbox", new object[] {
                        ID}, this.CheckMailboxOperationCompleted, userState);
        }
        
        private void OnCheckMailboxOperationCompleted(object arg) {
            if ((this.CheckMailboxCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckMailboxCompleted(this, new CheckMailboxCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/ClearEverything", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void ClearEverything() {
            this.Invoke("ClearEverything", new object[0]);
        }
        
        /// <remarks/>
        public void ClearEverythingAsync() {
            this.ClearEverythingAsync(null);
        }
        
        /// <remarks/>
        public void ClearEverythingAsync(object userState) {
            if ((this.ClearEverythingOperationCompleted == null)) {
                this.ClearEverythingOperationCompleted = new System.Threading.SendOrPostCallback(this.OnClearEverythingOperationCompleted);
            }
            this.InvokeAsync("ClearEverything", new object[0], this.ClearEverythingOperationCompleted, userState);
        }
        
        private void OnClearEverythingOperationCompleted(object arg) {
            if ((this.ClearEverythingCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ClearEverythingCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void RegisterCompletedEventHandler(object sender, RegisterCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RegisterCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RegisterCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void UnregisterCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void PutInMailboxCompletedEventHandler(object sender, PutInMailboxCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PutInMailboxCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal PutInMailboxCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void CheckMailboxCompletedEventHandler(object sender, CheckMailboxCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckMailboxCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CheckMailboxCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void ClearEverythingCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591