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

namespace WebChat.ReceiveService {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="ReceiveServiceSoap", Namespace="localhost")]
    public partial class ReceiveService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback LogonOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendMessageOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendPrivateMessageOperationCompleted;
        
        private System.Threading.SendOrPostCallback LogoutOperationCompleted;
        
        private System.Threading.SendOrPostCallback ClearEverythingOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ReceiveService() {
            this.Url = global::WebChat.Properties.Settings.Default.WebChat_ReceiveService_ReceiveService;
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
        public event LogonCompletedEventHandler LogonCompleted;
        
        /// <remarks/>
        public event SendMessageCompletedEventHandler SendMessageCompleted;
        
        /// <remarks/>
        public event SendPrivateMessageCompletedEventHandler SendPrivateMessageCompleted;
        
        /// <remarks/>
        public event LogoutCompletedEventHandler LogoutCompleted;
        
        /// <remarks/>
        public event ClearEverythingCompletedEventHandler ClearEverythingCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/Logon", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int Logon(string nickname) {
            object[] results = this.Invoke("Logon", new object[] {
                        nickname});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void LogonAsync(string nickname) {
            this.LogonAsync(nickname, null);
        }
        
        /// <remarks/>
        public void LogonAsync(string nickname, object userState) {
            if ((this.LogonOperationCompleted == null)) {
                this.LogonOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLogonOperationCompleted);
            }
            this.InvokeAsync("Logon", new object[] {
                        nickname}, this.LogonOperationCompleted, userState);
        }
        
        private void OnLogonOperationCompleted(object arg) {
            if ((this.LogonCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LogonCompleted(this, new LogonCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/SendMessage", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SendMessage(string message) {
            this.Invoke("SendMessage", new object[] {
                        message});
        }
        
        /// <remarks/>
        public void SendMessageAsync(string message) {
            this.SendMessageAsync(message, null);
        }
        
        /// <remarks/>
        public void SendMessageAsync(string message, object userState) {
            if ((this.SendMessageOperationCompleted == null)) {
                this.SendMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendMessageOperationCompleted);
            }
            this.InvokeAsync("SendMessage", new object[] {
                        message}, this.SendMessageOperationCompleted, userState);
        }
        
        private void OnSendMessageOperationCompleted(object arg) {
            if ((this.SendMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendMessageCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/SendPrivateMessage", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int SendPrivateMessage(string toNickname, string message) {
            object[] results = this.Invoke("SendPrivateMessage", new object[] {
                        toNickname,
                        message});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void SendPrivateMessageAsync(string toNickname, string message) {
            this.SendPrivateMessageAsync(toNickname, message, null);
        }
        
        /// <remarks/>
        public void SendPrivateMessageAsync(string toNickname, string message, object userState) {
            if ((this.SendPrivateMessageOperationCompleted == null)) {
                this.SendPrivateMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendPrivateMessageOperationCompleted);
            }
            this.InvokeAsync("SendPrivateMessage", new object[] {
                        toNickname,
                        message}, this.SendPrivateMessageOperationCompleted, userState);
        }
        
        private void OnSendPrivateMessageOperationCompleted(object arg) {
            if ((this.SendPrivateMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendPrivateMessageCompleted(this, new SendPrivateMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/Logout", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Logout() {
            this.Invoke("Logout", new object[0]);
        }
        
        /// <remarks/>
        public void LogoutAsync() {
            this.LogoutAsync(null);
        }
        
        /// <remarks/>
        public void LogoutAsync(object userState) {
            if ((this.LogoutOperationCompleted == null)) {
                this.LogoutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLogoutOperationCompleted);
            }
            this.InvokeAsync("Logout", new object[0], this.LogoutOperationCompleted, userState);
        }
        
        private void OnLogoutOperationCompleted(object arg) {
            if ((this.LogoutCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LogoutCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/ClearEverything", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ClearEverything() {
            object[] results = this.Invoke("ClearEverything", new object[0]);
            return ((string)(results[0]));
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
                this.ClearEverythingCompleted(this, new ClearEverythingCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void LogonCompletedEventHandler(object sender, LogonCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LogonCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LogonCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void SendMessageCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void SendPrivateMessageCompletedEventHandler(object sender, SendPrivateMessageCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendPrivateMessageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendPrivateMessageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void LogoutCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void ClearEverythingCompletedEventHandler(object sender, ClearEverythingCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ClearEverythingCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ClearEverythingCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591