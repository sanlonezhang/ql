﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECCentral.Portal.UI.Customer.Resources
{
    using System;


    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResSendSMS
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public ResSendSMS()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ECCentral.Portal.UI.Customer.Resources.ResSendSMS", typeof(ResSendSMS).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 关闭.
        /// </summary>
        public static string Button_Close
        {
            get
            {
                return ResourceManager.GetString("Button_Close", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 发送.
        /// </summary>
        public static string Button_Send
        {
            get
            {
                return ResourceManager.GetString("Button_Send", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 号码以&apos;,&apos;号或者换行分隔.
        /// </summary>
        public static string Label_message
        {
            get
            {
                return ResourceManager.GetString("Label_message", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 号码:.
        /// </summary>
        public static string Label_Number
        {
            get
            {
                return ResourceManager.GetString("Label_Number", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 号码类型:.
        /// </summary>
        public static string Label_NumberType
        {
            get
            {
                return ResourceManager.GetString("Label_NumberType", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 发送结果:.
        /// </summary>
        public static string Label_SendResults
        {
            get
            {
                return ResourceManager.GetString("Label_SendResults", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 短信内容:.
        /// </summary>
        public static string Label_SMSContent
        {
            get
            {
                return ResourceManager.GetString("Label_SMSContent", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 发送失败！.
        /// </summary>
        public static string msg_Fail
        {
            get
            {
                return ResourceManager.GetString("msg_Fail", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 格式错误！.
        /// </summary>
        public static string msg_FormateError
        {
            get
            {
                return ResourceManager.GetString("msg_FormateError", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 发送成功！.
        /// </summary>
        public static string msg_Success
        {
            get
            {
                return ResourceManager.GetString("msg_Success", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 按手机号发送.
        /// </summary>
        public static string Rbtn_ByPhoneNumber
        {
            get
            {
                return ResourceManager.GetString("Rbtn_ByPhoneNumber", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 按订单号发送.
        /// </summary>
        public static string Rbtn_BySoSysNo
        {
            get
            {
                return ResourceManager.GetString("Rbtn_BySoSysNo", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 请填写手机号码！.
        /// </summary>
        public static string validmsg_Cellphone
        {
            get
            {
                return ResourceManager.GetString("validmsg_Cellphone", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 请填写短信内容！.
        /// </summary>
        public static string validmsg_SMSMessage
        {
            get
            {
                return ResourceManager.GetString("validmsg_SMSMessage", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to 请填写订单号！.
        /// </summary>
        public static string validmsg_SoSysNo
        {
            get
            {
                return ResourceManager.GetString("validmsg_SoSysNo", resourceCulture);
            }
        }
    }
}
