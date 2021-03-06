﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.34014.
// 
#pragma warning disable 1591

namespace EmployeeClient.EmployeeService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="EmployeeServiceSoap", Namespace="localhost")]
    public partial class EmployeeService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback CreateEmployeeOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddManagerOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddEmployeeOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetManagerOfOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetEmployeesOfOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public EmployeeService() {
            this.Url = global::EmployeeClient.Properties.Settings.Default.EmployeeClient_EmployeeService_EmployeeService;
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
        public event CreateEmployeeCompletedEventHandler CreateEmployeeCompleted;
        
        /// <remarks/>
        public event AddManagerCompletedEventHandler AddManagerCompleted;
        
        /// <remarks/>
        public event AddEmployeeCompletedEventHandler AddEmployeeCompleted;
        
        /// <remarks/>
        public event GetManagerOfCompletedEventHandler GetManagerOfCompleted;
        
        /// <remarks/>
        public event GetEmployeesOfCompletedEventHandler GetEmployeesOfCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/CreateEmployee", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Employee CreateEmployee(int ID, string Name, string SSN) {
            object[] results = this.Invoke("CreateEmployee", new object[] {
                        ID,
                        Name,
                        SSN});
            return ((Employee)(results[0]));
        }
        
        /// <remarks/>
        public void CreateEmployeeAsync(int ID, string Name, string SSN) {
            this.CreateEmployeeAsync(ID, Name, SSN, null);
        }
        
        /// <remarks/>
        public void CreateEmployeeAsync(int ID, string Name, string SSN, object userState) {
            if ((this.CreateEmployeeOperationCompleted == null)) {
                this.CreateEmployeeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateEmployeeOperationCompleted);
            }
            this.InvokeAsync("CreateEmployee", new object[] {
                        ID,
                        Name,
                        SSN}, this.CreateEmployeeOperationCompleted, userState);
        }
        
        private void OnCreateEmployeeOperationCompleted(object arg) {
            if ((this.CreateEmployeeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateEmployeeCompleted(this, new CreateEmployeeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/AddManager", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void AddManager(Employee e) {
            this.Invoke("AddManager", new object[] {
                        e});
        }
        
        /// <remarks/>
        public void AddManagerAsync(Employee e) {
            this.AddManagerAsync(e, null);
        }
        
        /// <remarks/>
        public void AddManagerAsync(Employee e, object userState) {
            if ((this.AddManagerOperationCompleted == null)) {
                this.AddManagerOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddManagerOperationCompleted);
            }
            this.InvokeAsync("AddManager", new object[] {
                        e}, this.AddManagerOperationCompleted, userState);
        }
        
        private void OnAddManagerOperationCompleted(object arg) {
            if ((this.AddManagerCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddManagerCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/AddEmployee", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void AddEmployee(Employee m, Employee e) {
            this.Invoke("AddEmployee", new object[] {
                        m,
                        e});
        }
        
        /// <remarks/>
        public void AddEmployeeAsync(Employee m, Employee e) {
            this.AddEmployeeAsync(m, e, null);
        }
        
        /// <remarks/>
        public void AddEmployeeAsync(Employee m, Employee e, object userState) {
            if ((this.AddEmployeeOperationCompleted == null)) {
                this.AddEmployeeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddEmployeeOperationCompleted);
            }
            this.InvokeAsync("AddEmployee", new object[] {
                        m,
                        e}, this.AddEmployeeOperationCompleted, userState);
        }
        
        private void OnAddEmployeeOperationCompleted(object arg) {
            if ((this.AddEmployeeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddEmployeeCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/GetManagerOf", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Employee GetManagerOf(Employee e) {
            object[] results = this.Invoke("GetManagerOf", new object[] {
                        e});
            return ((Employee)(results[0]));
        }
        
        /// <remarks/>
        public void GetManagerOfAsync(Employee e) {
            this.GetManagerOfAsync(e, null);
        }
        
        /// <remarks/>
        public void GetManagerOfAsync(Employee e, object userState) {
            if ((this.GetManagerOfOperationCompleted == null)) {
                this.GetManagerOfOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetManagerOfOperationCompleted);
            }
            this.InvokeAsync("GetManagerOf", new object[] {
                        e}, this.GetManagerOfOperationCompleted, userState);
        }
        
        private void OnGetManagerOfOperationCompleted(object arg) {
            if ((this.GetManagerOfCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetManagerOfCompleted(this, new GetManagerOfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("localhost/GetEmployeesOf", RequestNamespace="localhost", ResponseNamespace="localhost", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Employee[] GetEmployeesOf(Employee manager) {
            object[] results = this.Invoke("GetEmployeesOf", new object[] {
                        manager});
            return ((Employee[])(results[0]));
        }
        
        /// <remarks/>
        public void GetEmployeesOfAsync(Employee manager) {
            this.GetEmployeesOfAsync(manager, null);
        }
        
        /// <remarks/>
        public void GetEmployeesOfAsync(Employee manager, object userState) {
            if ((this.GetEmployeesOfOperationCompleted == null)) {
                this.GetEmployeesOfOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetEmployeesOfOperationCompleted);
            }
            this.InvokeAsync("GetEmployeesOf", new object[] {
                        manager}, this.GetEmployeesOfOperationCompleted, userState);
        }
        
        private void OnGetEmployeesOfOperationCompleted(object arg) {
            if ((this.GetEmployeesOfCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetEmployeesOfCompleted(this, new GetEmployeesOfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="localhost")]
    public partial class Employee {
        
        private string nameField;
        
        private string sSNField;
        
        private int idField;
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string SSN {
            get {
                return this.sSNField;
            }
            set {
                this.sSNField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void CreateEmployeeCompletedEventHandler(object sender, CreateEmployeeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateEmployeeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateEmployeeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Employee Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Employee)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void AddManagerCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void AddEmployeeCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void GetManagerOfCompletedEventHandler(object sender, GetManagerOfCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetManagerOfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetManagerOfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Employee Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Employee)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void GetEmployeesOfCompletedEventHandler(object sender, GetEmployeesOfCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetEmployeesOfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetEmployeesOfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Employee[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Employee[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591