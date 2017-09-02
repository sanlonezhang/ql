﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3603
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.3603.
// 
#pragma warning disable 1591

namespace ECCentral.Service.Utility
{
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "BasicHttpBinding_IWriteLog", Namespace = "http://tempuri.org/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DefaultDataContract))]
    public partial class LogService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback LogAsyncOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public LogService()
        {
            this.Url = "http://s7dmsq01.buyabs.corp:8080/Newegg/Oversea/Framework/LoggingService/20090214" +
                "01/LogService.svc";
            if ((this.IsLocalFileSystemWebService(this.Url) == true))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public new string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true)
                            && (this.useDefaultCredentialsSetExplicitly == false))
                            && (this.IsLocalFileSystemWebService(value) == false)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public new bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        /// <remarks/>
        public event LogAsyncCompletedEventHandler LogAsyncCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://oversea.newegg.com/LoggingService/ServiceContracts/IWriteLog/LogAsync", RequestNamespace = "http://oversea.newegg.com/LoggingService/ServiceContracts", OneWay = true, Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void LogAsync([System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] LogEntryContract log)
        {
            this.Invoke("LogAsync", new object[] {
                        log});
        }

        /// <remarks/>
        public void LogAsyncAsync(LogEntryContract log)
        {
            this.LogAsyncAsync(log, null);
        }

        /// <remarks/>
        public void LogAsyncAsync(LogEntryContract log, object userState)
        {
            if ((this.LogAsyncOperationCompleted == null))
            {
                this.LogAsyncOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLogAsyncOperationCompleted);
            }
            this.InvokeAsync("LogAsync", new object[] {
                        log}, this.LogAsyncOperationCompleted, userState);
        }

        private void OnLogAsyncOperationCompleted(object arg)
        {
            if ((this.LogAsyncCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LogAsyncCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if (((url == null)
                        || (url == string.Empty)))
            {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024)
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
            {
                return true;
            }
            return false;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://oversea.newegg.com/LoggingService/DataContracts")]
    public partial class LogEntryContract : DefaultDataContract
    {

        private LogEntryBody bodyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public LogEntryBody Body
        {
            get
            {
                return this.bodyField;
            }
            set
            {
                this.bodyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://oversea.newegg.com/LoggingService/DataContracts")]
    public partial class LogEntryBody
    {

        private string categoryNameField;

        private string contentField;

        private ArrayOfKeyValueOfstringstringKeyValueOfstringstring[] extendedPropertiesField;

        private string globalIDField;

        private string globalNameField;

        private string idField;

        private string localIDField;

        private string localNameField;

        private System.DateTime logCreateDateField;

        private bool logCreateDateFieldSpecified;

        private string logServerIPField;

        private string logServerNameField;

        private LogType logTypeField;

        private bool logTypeFieldSpecified;

        private string logUserNameField;

        private string referenceKeyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string CategoryName
        {
            get
            {
                return this.categoryNameField;
            }
            set
            {
                this.categoryNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Content
        {
            get
            {
                return this.contentField;
            }
            set
            {
                this.contentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
        [System.Xml.Serialization.XmlArrayItemAttribute("KeyValueOfstringstring", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays", IsNullable = false)]
        public ArrayOfKeyValueOfstringstringKeyValueOfstringstring[] ExtendedProperties
        {
            get
            {
                return this.extendedPropertiesField;
            }
            set
            {
                this.extendedPropertiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string GlobalID
        {
            get
            {
                return this.globalIDField;
            }
            set
            {
                this.globalIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string GlobalName
        {
            get
            {
                return this.globalNameField;
            }
            set
            {
                this.globalNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string LocalID
        {
            get
            {
                return this.localIDField;
            }
            set
            {
                this.localIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string LocalName
        {
            get
            {
                return this.localNameField;
            }
            set
            {
                this.localNameField = value;
            }
        }

        /// <remarks/>
        public System.DateTime LogCreateDate
        {
            get
            {
                return this.logCreateDateField;
            }
            set
            {
                this.logCreateDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LogCreateDateSpecified
        {
            get
            {
                return this.logCreateDateFieldSpecified;
            }
            set
            {
                this.logCreateDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string LogServerIP
        {
            get
            {
                return this.logServerIPField;
            }
            set
            {
                this.logServerIPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string LogServerName
        {
            get
            {
                return this.logServerNameField;
            }
            set
            {
                this.logServerNameField = value;
            }
        }

        /// <remarks/>
        public LogType LogType
        {
            get
            {
                return this.logTypeField;
            }
            set
            {
                this.logTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LogTypeSpecified
        {
            get
            {
                return this.logTypeFieldSpecified;
            }
            set
            {
                this.logTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string LogUserName
        {
            get
            {
                return this.logUserNameField;
            }
            set
            {
                this.logUserNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string ReferenceKey
        {
            get
            {
                return this.referenceKeyField;
            }
            set
            {
                this.referenceKeyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
    public partial class ArrayOfKeyValueOfstringstringKeyValueOfstringstring
    {

        private string keyField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://oversea.newegg.com/Framework/Common/Contract")]
    public partial class OperationUser
    {

        private string companyCodeField;

        private string fullNameField;

        private string logUserNameField;

        private string sourceDirectoryKeyField;

        private string sourceUserNameField;

        private string uniqueUserNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string CompanyCode
        {
            get
            {
                return this.companyCodeField;
            }
            set
            {
                this.companyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string FullName
        {
            get
            {
                return this.fullNameField;
            }
            set
            {
                this.fullNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string LogUserName
        {
            get
            {
                return this.logUserNameField;
            }
            set
            {
                this.logUserNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string SourceDirectoryKey
        {
            get
            {
                return this.sourceDirectoryKeyField;
            }
            set
            {
                this.sourceDirectoryKeyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string SourceUserName
        {
            get
            {
                return this.sourceUserNameField;
            }
            set
            {
                this.sourceUserNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string UniqueUserName
        {
            get
            {
                return this.uniqueUserNameField;
            }
            set
            {
                this.uniqueUserNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://oversea.newegg.com/Framework/Common/Contract")]
    public partial class MessageHeader
    {

        private string actionField;

        private string callbackAddressField;

        private string companyCodeField;

        private string countryCodeField;

        private string descriptionField;

        private string fromField;

        private string fromSystemField;

        private GlobalBusinessType globalBusinessTypeField;

        private bool globalBusinessTypeFieldSpecified;

        private string languageField;

        private string nameSpaceField;

        private OperationUser operationUserField;

        private string originalGUIDField;

        private string originalSenderField;

        private System.Data.DataSet[] preferencesField;

        private string senderField;

        private string storeCompanyCodeField;

        private string tagField;

        private string timeZoneField;

        private string toField;

        private string toSystemField;

        private string transactionCodeField;

        private string typeField;

        private string versionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Action
        {
            get
            {
                return this.actionField;
            }
            set
            {
                this.actionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string CallbackAddress
        {
            get
            {
                return this.callbackAddressField;
            }
            set
            {
                this.callbackAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string CompanyCode
        {
            get
            {
                return this.companyCodeField;
            }
            set
            {
                this.companyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string From
        {
            get
            {
                return this.fromField;
            }
            set
            {
                this.fromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string FromSystem
        {
            get
            {
                return this.fromSystemField;
            }
            set
            {
                this.fromSystemField = value;
            }
        }

        /// <remarks/>
        public GlobalBusinessType GlobalBusinessType
        {
            get
            {
                return this.globalBusinessTypeField;
            }
            set
            {
                this.globalBusinessTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool GlobalBusinessTypeSpecified
        {
            get
            {
                return this.globalBusinessTypeFieldSpecified;
            }
            set
            {
                this.globalBusinessTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string NameSpace
        {
            get
            {
                return this.nameSpaceField;
            }
            set
            {
                this.nameSpaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public OperationUser OperationUser
        {
            get
            {
                return this.operationUserField;
            }
            set
            {
                this.operationUserField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string OriginalGUID
        {
            get
            {
                return this.originalGUIDField;
            }
            set
            {
                this.originalGUIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string OriginalSender
        {
            get
            {
                return this.originalSenderField;
            }
            set
            {
                this.originalSenderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Preferences", Namespace = "http://schemas.datacontract.org/2004/07/Newegg.Oversea.Framework.Contract")]
        public System.Data.DataSet[] Preferences
        {
            get
            {
                return this.preferencesField;
            }
            set
            {
                this.preferencesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Sender
        {
            get
            {
                return this.senderField;
            }
            set
            {
                this.senderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string StoreCompanyCode
        {
            get
            {
                return this.storeCompanyCodeField;
            }
            set
            {
                this.storeCompanyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Tag
        {
            get
            {
                return this.tagField;
            }
            set
            {
                this.tagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string TimeZone
        {
            get
            {
                return this.timeZoneField;
            }
            set
            {
                this.timeZoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string To
        {
            get
            {
                return this.toField;
            }
            set
            {
                this.toField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string ToSystem
        {
            get
            {
                return this.toSystemField;
            }
            set
            {
                this.toSystemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string TransactionCode
        {
            get
            {
                return this.transactionCodeField;
            }
            set
            {
                this.transactionCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://oversea.newegg.com/Framework/Common/Contract")]
    public enum GlobalBusinessType
    {

        /// <remarks/>
        VF,

        /// <remarks/>
        Normal,

        /// <remarks/>
        Listing,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://oversea.newegg.com/Framework/Common/Contract")]
    public partial class MessageFault
    {

        private string errorCodeField;

        private string errorDescriptionField;

        private string errorDetailField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string ErrorCode
        {
            get
            {
                return this.errorCodeField;
            }
            set
            {
                this.errorCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string ErrorDescription
        {
            get
            {
                return this.errorDescriptionField;
            }
            set
            {
                this.errorDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string ErrorDetail
        {
            get
            {
                return this.errorDetailField;
            }
            set
            {
                this.errorDetailField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://oversea.newegg.com/Framework/Common/Contract")]
    public partial class MessageEvent
    {

        private string actionField;

        private string companyCodeField;

        private string descriptionField;

        private string eventIDField;

        private string processorField;

        private System.DateTime timeStampField;

        private bool timeStampFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Action
        {
            get
            {
                return this.actionField;
            }
            set
            {
                this.actionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string CompanyCode
        {
            get
            {
                return this.companyCodeField;
            }
            set
            {
                this.companyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string EventID
        {
            get
            {
                return this.eventIDField;
            }
            set
            {
                this.eventIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Processor
        {
            get
            {
                return this.processorField;
            }
            set
            {
                this.processorField = value;
            }
        }

        /// <remarks/>
        public System.DateTime TimeStamp
        {
            get
            {
                return this.timeStampField;
            }
            set
            {
                this.timeStampField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TimeStampSpecified
        {
            get
            {
                return this.timeStampFieldSpecified;
            }
            set
            {
                this.timeStampFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LogEntryContract))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://oversea.newegg.com/Framework/Common/Contract")]
    public partial class DefaultDataContract
    {

        private MessageEvent[] eventsField;

        private MessageFault[] faultsField;

        private MessageHeader headerField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
        public MessageEvent[] Events
        {
            get
            {
                return this.eventsField;
            }
            set
            {
                this.eventsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
        public MessageFault[] Faults
        {
            get
            {
                return this.faultsField;
            }
            set
            {
                this.faultsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public MessageHeader Header
        {
            get
            {
                return this.headerField;
            }
            set
            {
                this.headerField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://oversea.newegg.com/LoggingService/DataContracts")]
    public enum LogType
    {

        /// <remarks/>
        Info,

        /// <remarks/>
        Trace,

        /// <remarks/>
        Error,

        /// <remarks/>
        Debug,

        /// <remarks/>
        Audit,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void LogAsyncCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591
