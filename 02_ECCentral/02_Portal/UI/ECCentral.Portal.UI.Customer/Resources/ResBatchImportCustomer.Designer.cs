﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECCentral.Portal.UI.Customer.Resources {
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
    public class ResBatchImportCustomer {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public ResBatchImportCustomer() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ECCentral.Portal.UI.Customer.Resources.ResBatchImportCustomer", typeof(ResBatchImportCustomer).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 开始导入.
        /// </summary>
        public static string btn_Export {
            get {
                return ResourceManager.GetString("btn_Export", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 批量导入顾客.
        /// </summary>
        public static string Button_BatchImportCustomer {
            get {
                return ResourceManager.GetString("Button_BatchImportCustomer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 关闭.
        /// </summary>
        public static string Button_Close {
            get {
                return ResourceManager.GetString("Button_Close", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to *.xlsx|*.xlsx|*.xls|*.xls|所有文件(*.*)|*.*.
        /// </summary>
        public static string FileUploader_Filter {
            get {
                return ResourceManager.GetString("FileUploader_Filter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 用户批量导入成功!.
        /// </summary>
        public static string Message_BatchImportSuccess {
            get {
                return ResourceManager.GetString("Message_BatchImportSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 您选择的文件不是电子表格类型的文件 !.
        /// </summary>
        public static string Message_UploadExcel_FileTypeError {
            get {
                return ResourceManager.GetString("Message_UploadExcel_FileTypeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 您选择的文件不存在!.
        /// </summary>
        public static string Message_UploadExcel_NoExistFile {
            get {
                return ResourceManager.GetString("Message_UploadExcel_NoExistFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 您尚未选择相关电子表格文件!.
        /// </summary>
        public static string Message_UploadExcel_NoSelectFile {
            get {
                return ResourceManager.GetString("Message_UploadExcel_NoSelectFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 请选择模板类型!.
        /// </summary>
        public static string Message_UploadExcel_NoSelFileType {
            get {
                return ResourceManager.GetString("Message_UploadExcel_NoSelFileType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 文件上传失败!.
        /// </summary>
        public static string Message_UploadFailed {
            get {
                return ResourceManager.GetString("Message_UploadFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 顾客来源不能为空!.
        /// </summary>
        public static string Message_VipFromLinkSource_Null {
            get {
                return ResourceManager.GetString("Message_VipFromLinkSource_Null", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 顾客来源:.
        /// </summary>
        public static string TextBlock_CustomerSource {
            get {
                return ResourceManager.GetString("TextBlock_CustomerSource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 选择Excel:.
        /// </summary>
        public static string TextBlock_Excel {
            get {
                return ResourceManager.GetString("TextBlock_Excel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 模板类型:.
        /// </summary>
        public static string TextBlock_TemplateType {
            get {
                return ResourceManager.GetString("TextBlock_TemplateType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 顾客来源不能为空！.
        /// </summary>
        public static string Validate_FromLinkNotNull {
            get {
                return ResourceManager.GetString("Validate_FromLinkNotNull", resourceCulture);
            }
        }
    }
}
