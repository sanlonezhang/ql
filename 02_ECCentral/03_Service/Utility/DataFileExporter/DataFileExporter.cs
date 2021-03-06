﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Data;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace ECCentral.Service.Utility
{
    internal static class DataFileExporter
    {
        public static FileExportResult ExportFile(ForwardRequestData requestData, string exporterName)
        {
            string svcHost = System.Configuration.ConfigurationManager.AppSettings["SVCHost"];
            string url = string.Format("http://" + svcHost + "{0}/{1}",
                (requestData.Port.HasValue ? (":" + requestData.Port) : string.Empty), requestData.Url.TrimStart('\\', '/'));
            string method = requestData.HttpMethod.ToUpper();
            if (method == "GET" && requestData.Parameters != null && requestData.Parameters.Count > 0)
            {
                string firstChar = url.Contains("?") ? "&" : "?";
                int i = 0;
                foreach (var p in requestData.Parameters)
                {
                    if (i == 0)
                    {
                        url = url + firstChar;
                    }
                    else
                    {
                        url = url + "&";
                    }
                    url = url + p.Code + "=" + HttpUtility.UrlEncode(p.Name);
                    i++;
                }
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            if (method == "POST")
            {
                request.ContentType = requestData.HttpContentType;
                if (requestData.Parameters != null && requestData.Parameters.Count > 0)
                {
                    foreach (var p in requestData.Parameters)
                    {
                        request.Headers[p.Code] = p.Name;
                    }
                }
                if (requestData.Content != null && requestData.Content.Trim().Length > 0)
                {
                    using (StreamWriter stream = new StreamWriter(request.GetRequestStream()))
                    {
                        stream.Write(requestData.Content.Trim());
                    }
                }
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream receiveStream = response.GetResponseStream())
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        string dataString = readStream.ReadToEnd();
                        readStream.Close();
                        receiveStream.Close();
                        response.Close();
                        #region 特殊字符替换 有待补充
                        if (dataString.IndexOf("&gt;") != -1)
                            dataString = dataString.Replace("&gt;", ">");
                        if (dataString.IndexOf("&lt;") != -1)
                            dataString = dataString.Replace("&lt;", "<");
                        if (dataString.IndexOf("&quot;") != -1)
                            dataString = dataString.Replace("&quot;", "\"");
                        if (dataString.IndexOf("&amp;") != -1)
                            dataString = dataString.Replace("&amp;", "&");

                        #endregion
                        FileExportResult result = new FileExportResult();
                        //if (dataString.Contains("ServiceError") && dataString.Contains("Faults")
                        //    && dataString.Contains("StatusCode") && dataString.Contains("StatusDescription"))

                        //导出的时候抛出BizException没有包含"ServiceError"
                        if (dataString.Contains("Faults")
                            && dataString.Contains("StatusCode") && dataString.Contains("StatusDescription"))
                        {
                            result.RestServiceError = dataString;
                        }
                        else
                        {
                            List<List<ColumnData>> clist;
                            if (requestData == null || requestData.ColumnSetting == null)
                            {
                                clist = new List<List<ColumnData>>(0);
                            }
                            else
                            {
                                clist = requestData.ColumnSetting;
                            }
                            result.DownloadUrl = SaveFile(dataString, exporterName, clist, requestData.TextInfoList, requestData.Url);
                        }
                        return result;
                    }
                }
            }
        }

        private static void CleanOldFiles(string folderPath)
        {
            try
            {
                TimeSpan time = FileExporterConfig.GetSetting().GetExpiryTime();
                DirectoryInfo directory = new DirectoryInfo(folderPath);
                FileInfo[] files = directory.GetFiles();
                foreach (FileInfo file in files)
                {
                    if (file.LastWriteTime.Date.Add(time) < DateTime.Now)
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.HandleException(ex);
            }
        }

        private static List<DataTable> GetData(string data)
        {
            if (data == null)
            {
                return new List<DataTable>(0);
            }
            else
            {
                List<DataTable> list = new List<DataTable>();
                var obj = DynamicXml.Parse(data);
                if (obj.TotalCount == 0)
                {
                    return new List<DataTable>(0);
                }
                if (obj.IsArray)
                {
                    bool isQueryResultList = false;
                    foreach (var item in obj)
                    {
                        isQueryResultList = item.IsDefined("TotalCount") && item.IsDefined("Rows");
                        break;
                    }
                    if (isQueryResultList) // QueryResultList
                    {
                        foreach (var item in obj)
                        {
                            list.Add(ConvertToDataTable(item.Rows));
                        }
                    }
                    else // DataTable
                    {
                        list.Add(ConvertToDataTable(obj));
                    }
                }
                else // QueryResult
                {
                    list.Add(ConvertToDataTable(obj.Rows));
                }
                return list;
            }
        }

        private static string SaveFile(string data, string exporterName, List<List<ColumnData>> columnList, List<TextInfo> textInfoList,string url)
        {
            List<DataTable> list = GetData(data);
            IFileExport exporter = FileExporterFactory.CreateExporter(exporterName);
            string fileName;
            string FileTitle = "";

            string path = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Configuration/ExportFileNameUrlPair.Config");
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Can't find the ExportFileNameUrlPair config file: " + path + "!");
            }
            XElement doc = XElement.Load(path);
            foreach (var x in doc.Descendants("ExportFileNameUrlPair"))
            {
                if (x.Attribute("URL") == null || x.Attribute("URL").Value == null || x.Attribute("URL").Value.Trim().Length <= 0)
                {
                    throw new ApplicationException("There are some 'ExportFileNameUrlPair' node without attribute 'URL' in the ExportFileNameUrlPair config file: " + path + ", please check it!");
                }

                if (x.Attribute("FileName") == null || x.Attribute("FileName").Value == null || x.Attribute("FileName").Value.Trim().Length <= 0)
                {
                    throw new ApplicationException("There are some 'ExportFileNameUrlPair' node without attribute 'FileName' in the ExportFileNameUrlPair config file: " + path + ", please check it!");
                }

                if (x.Attribute("URL").Value.Equals(url))
                {
                    FileTitle = x.Attribute("FileName").Value;
                    break;
                }
            }
            
            byte[] fileData = exporter.CreateFile(list, columnList, textInfoList, out fileName, FileTitle);
            return BringUrl(fileName, fileData);
        }

        internal static string BringUrl(string fileName, byte[] fileData)
        {
            string folderPath = FileExporterConfig.GetSetting().BaseFolder;

            const string TmpFileFolder = "TempFiles";
            if (folderPath == null || folderPath.Trim().Length <= 0)
            {
                folderPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, TmpFileFolder);
            }
            else
            {
                string p = Path.GetPathRoot(folderPath);
                if (p == null || p.Trim().Length <= 0) // 说明是相对路径
                {
                    folderPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, folderPath);
                }
            }
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            else
            {
                CleanOldFiles(folderPath);
            }
            string filePath = Path.Combine(folderPath, fileName);
            using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                stream.Write(fileData, 0, fileData.Length);
            }
            string vp = FileExporterConfig.GetSetting().VirtualPath;
            if (vp == null || vp.Trim().Length <= 0)
            {
                vp = TmpFileFolder;
            }
            else
            {
                vp = vp.Trim().Trim('/', '\\');
            }
            return vp + "/" + HttpUtility.UrlEncode(fileName);
        }

        private static DataTable ConvertToDataTable(dynamic table)
        {
            if (table == null)
            {
                return new DataTable();
            }
            IEnumerable<string> columns = null;
            dynamic firstRow = null;
            foreach (var row in table)
            {
                firstRow = row;
                columns = row.GetDynamicMemberNames();
                break;
            }
            if (columns == null)
            {
                return new DataTable();
            }
            DataTable rst = new DataTable();
            foreach (string name in columns)
            {
                //dynamic t = firstRow[name];
                //Type type = t == null ? typeof(object) : t.GetType();
                //Type tmp = type;
                //while (tmp.IsGenericType && tmp.GetGenericTypeDefinition() == typeof(Nullable<>)
                //        && tmp.GetGenericArguments() != null
                //        && tmp.GetGenericArguments().Length == 1)
                //{
                //    tmp = tmp.GetGenericArguments()[0];
                //}
                //if (tmp.IsEnum || tmp == typeof(bool)) // 因为枚举和bool值会被转化为显示的文本
                //{
                //    type = typeof(string);
                //}
                rst.Columns.Add(name, typeof(object));
            }
            foreach (var row in table)
            {
                DataRow dataRow = rst.NewRow();
                foreach (string name in columns)
                {
                    object t = row[name];
                    if (t == null || t == DBNull.Value)
                    {
                        dataRow[name] = DBNull.Value;
                    }
                    else
                    {
                        Type type = t.GetType();
                        while (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)
                                && type.GetGenericArguments() != null
                                && type.GetGenericArguments().Length == 1)
                        {
                            type = type.GetGenericArguments()[0];
                        }
                        if (type.IsEnum)
                        {
                            Enum x = (Enum)t;
                            dataRow[name] = x.ToDisplayText();
                        }
                        else if (type == typeof(bool))
                        {
                            bool r = Convert.ToBoolean(t);
                            dataRow[name] = r ? ECCentral.Service.Utility.Resources.ErrorMsg.Boolean_True : ECCentral.Service.Utility.Resources.ErrorMsg.Boolean_False;
                        }
                        else
                        {
                            dataRow[name] = t;
                        }
                    }
                }
                rst.Rows.Add(dataRow);
            }
            return rst;
        }
    }
}
