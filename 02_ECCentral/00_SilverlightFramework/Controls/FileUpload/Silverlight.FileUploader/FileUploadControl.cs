﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using Newegg.Oversea.Silverlight.Controls;
using Newegg.Oversea.Silverlight.FileUploader.Resource;
using Newegg.Oversea.Silverlight.GifUtility;

namespace Newegg.Oversea.Silverlight.FileUploader
{
    public class FileUploadControl : ContentControl
    {
        public event UploadStartedEvent UploadStarted;
        public event UploadCompletedEvent UploadCompleted;
        public event AllUploadCompletedEvent AllFileUploadCompleted;//所有文件上传完成，不管成功失败都会触发该事件
        public event EachFilePreStartUploadEvent EachFilePreStartUpload;
        public event EventHandler ClearFilesCompleted;
        public event CheckUploadFileEvent CheckUploadFilesEvent;
        public event UploadCanceledEvent UploadCanceled;



        #region Const String

        private const string AddFilesButtonName = "AddFilesButton";
        private const string ClearFilesButtonName = "ClearFilesButton";
        private const string UploadFilesButtonName = "UploadFilesButton";
        private const string DisplayThumbailCheckBoxName = "DisplayThumbailCheckBox";
        private const string TimeLeftTextBlockName = "TimeLeftTextBlock";
        private const string FileListItemsControlName = "FileListItemsControl";
        private const string UploadProgressBarName = "UploadProgressBar";
        private const string TotalSizeTextBlockName = "TotalSizeTextBlock";
        private const string TotalCountBlockName = "TotalCountTextBlock";
        private const string NameTextBlockName = "NameTextBlock";
        private const string StatusTextBlockName = "StatusTextBlock";
        private const string SizeTextBlockName = "SizeTextBlock";
        private const string TextBlockTipsName = "TextBlockTips";
        private const string ProgressTextBlockName = "ProgressTextBlock";
        private const string FilesScrollViewerName = "FilesScrollViewer";
        private const string TotalUploadProgressGridName = "TotalUploadProgressGrid";
        private const string BorderBackgroundName = "BorderBackground";


        #endregion

        #region Private Fields

        private DateTime m_start;
        private int m_count = 0;
        private bool m_uploading;
        private ObservableCollection<FileUpload> m_files;
        private Button m_addFilesButton;
        private Button m_clearFilesButton;
        private Button m_uploadFilesButton;
        private CheckBox m_displayThumbailCheckBox;
        private ItemsControl m_fileListItemsControl;
        private TextBlock m_timeLeftTextBlock;
        private ProgressBar m_uploadProgressBar;
        private TextBlock m_totalSizeTextBlock;
        private TextBlock m_totalCountTextBlock;
        private ScrollViewer m_filesScrollViewer;
        private Grid m_totalUploadProgressGrid;
        private Border m_borderBackground;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(FileUploadControl), null);

        public static readonly DependencyProperty MaxConcurrentUploadsProperty =
            DependencyProperty.Register("MaxConcurrentUploads", typeof(int), typeof(FileUploadControl), new PropertyMetadata(10));

        public static readonly DependencyProperty UploadChunkSizeProperty =
            DependencyProperty.Register("UploadChunkSize", typeof(int), typeof(FileUploadControl), new PropertyMetadata(2 * 1024 * 1024));

        public static readonly DependencyProperty MultiSelectProperty =
            DependencyProperty.Register("MultiSelect", typeof(bool), typeof(FileUploadControl), new PropertyMetadata(true));

        public static readonly DependencyProperty MultiUploadProperty =
            DependencyProperty.Register("MultiUpload", typeof(bool), typeof(FileUploadControl), new PropertyMetadata(true));

        public static readonly DependencyProperty UploadUrlProperty =
            DependencyProperty.Register("UploadUrl", typeof(Uri), typeof(FileUploadControl), null);

        public static readonly DependencyProperty MaximumTotalUploadSizeProperty =
           DependencyProperty.Register("MaximumTotalUploadSize", typeof(int), typeof(FileUploadControl), new PropertyMetadata(100));

        public static readonly DependencyProperty MaximumUploadSizeProperty =
           DependencyProperty.Register("MaximumUploadSize", typeof(int), typeof(FileUploadControl), new PropertyMetadata(10));

        public static readonly DependencyProperty MaxNumberToUploadProperty =
           DependencyProperty.Register("MaxNumberToUpload", typeof(int), typeof(FileUploadControl), new PropertyMetadata(10));

        public static readonly DependencyProperty AllowThumbnailProperty =
            DependencyProperty.Register("AllowThumbnail", typeof(bool), typeof(FileUploadControl), new PropertyMetadata(true));        
        
        public static readonly DependencyProperty DisplayThumbnailsProperty =
            DependencyProperty.Register("DisplayThumbnails", typeof(bool), typeof(FileUploadControl), new PropertyMetadata(true));       
        
        public static readonly DependencyProperty IsShowTotalUploadProgressProperty =
            DependencyProperty.Register("IsShowTotalUploadProgress", typeof(bool), typeof(FileUploadControl), new PropertyMetadata(false));


        public static readonly DependencyProperty CanEditFileNameProperty =
             DependencyProperty.Register("CanEditFileName", typeof(bool), typeof(FileUploadControl), new PropertyMetadata(false));

        /// <summary>
        /// 上传的文件类型,如:Images (*.jpg;*.gif)|*.jpg;*.gif
        /// </summary>
        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        /// <summary>
        ///并发请求Handler的最大数
        /// </summary>
        public int MaxConcurrentUploads
        {
            get { return (int)GetValue(MaxConcurrentUploadsProperty); }
            set { SetValue(MaxConcurrentUploadsProperty, value); }
        }

        /// <summary>
        /// 单次http post stream最大值,默认为256k
        /// </summary>
        public int UploadChunkSize
        {
            get { return (int)GetValue(UploadChunkSizeProperty); }
            set { SetValue(UploadChunkSizeProperty, value); }
        }

        public bool MultiSelect
        {
            get { return (bool)GetValue(MultiSelectProperty); }
            set { SetValue(MultiSelectProperty, value); }
        }

        public bool MultiUpload
        {
            get { return (bool)GetValue(MultiUploadProperty); }
            set { SetValue(MultiUploadProperty, value); }
        }

        public Uri UploadUrl
        {
            get { return (Uri)GetValue(UploadUrlProperty); }
            set { SetValue(UploadUrlProperty, value); }
        }

        public int MaximumTotalUploadSize
        {
            get { return (int)GetValue(MaximumTotalUploadSizeProperty); }
            set { SetValue(MaximumTotalUploadSizeProperty, value); }
        }

        public int MaximumUploadSize
        {
            get { return (int)GetValue(MaximumUploadSizeProperty); }
            set { SetValue(MaximumUploadSizeProperty, value); }
        }

        public bool AllowThumbnail
        {
            get { return (bool)GetValue(AllowThumbnailProperty); }
            set { SetValue(AllowThumbnailProperty, value); }
        }

        public int MaxNumberToUpload
        {
            get { return (int)GetValue(MaxNumberToUploadProperty); }
            set { SetValue(MaxNumberToUploadProperty, value); }
        }

        public bool DisplayThumbnails
        {
            get { return (bool)GetValue(DisplayThumbnailsProperty); }
            set { SetValue(DisplayThumbnailsProperty, value); }
        }

        public bool IsShowTotalUploadProgress
        {
            get { return (bool)GetValue(IsShowTotalUploadProgressProperty); }
            set { SetValue(IsShowTotalUploadProgressProperty, value); }

        }

        public bool CanEditFileName
        {
            get { return (bool)GetValue(CanEditFileNameProperty); }
            set { SetValue(CanEditFileNameProperty, value); }

        }

        #endregion

        #region CLR Properties

        public DimensionsMatchType DimensionsMatch { get; set; }

        public int ImageWidth { get; set; }

        public int ImageHeight { get; set; }

        public int MaximumFileNameLength { get; set; }

        private bool Uploading
        {
            get
            {
                return m_uploading;
            }
            set
            {
                m_uploading = value;
                if (m_addFilesButton != null)
                {
                    this.Dispatcher.BeginInvoke(() =>
                        {
                            m_addFilesButton.IsEnabled = !value;
                        });
                }
            }
        }

        private long TotalUploadSize { get; set; }

        private long TotalUploaded { get; set; }

        internal bool Canceld { get; private set; }

        private long MaximumTotalUploadBytes
        {
            get
            {
                return (long)MaximumTotalUploadSize * 1024 * 1024;
            }
        }

        private long MaximumUploadBytes
        {
            get
            {
                return (long)MaximumUploadSize * 1024 * 1024;
            }
        }
        
        public IPage Page { get; set; }


        public Dictionary<string, string> UploadParams { get; set; }


        private AllUploadCompletedEventArgs m_AllUploadCompletedEventArgs;


        #endregion       

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
                     
            this.m_totalUploadProgressGrid = GetTemplateChild(TotalUploadProgressGridName) as Grid;
            this.m_totalUploadProgressGrid.Visibility = this.IsShowTotalUploadProgress ? Visibility.Visible : Visibility.Collapsed;
            this.m_totalCountTextBlock = GetTemplateChild(TotalCountBlockName) as TextBlock;
            this.m_totalCountTextBlock.Text = string.Format(MessageResource.Uploader_TotalCount, 0);
            this.m_totalSizeTextBlock = GetTemplateChild(TotalSizeTextBlockName) as TextBlock;
            this.m_totalSizeTextBlock.Text = MessageResource.Uploader_TotalSize;
            this.m_timeLeftTextBlock = GetTemplateChild(TimeLeftTextBlockName) as TextBlock;
            this.m_fileListItemsControl = GetTemplateChild(FileListItemsControlName) as ItemsControl;
            this.m_uploadProgressBar = GetTemplateChild(UploadProgressBarName) as ProgressBar;
            this.m_addFilesButton = GetTemplateChild(AddFilesButtonName) as Button;
            this.m_addFilesButton.Content = MessageResource.Uploader_Browse;
            this.m_clearFilesButton = GetTemplateChild(ClearFilesButtonName) as Button;
            this.m_clearFilesButton.Content = MessageResource.Uploader_ClearFiles;
            this.m_uploadFilesButton = GetTemplateChild(UploadFilesButtonName) as Button;
            this.m_uploadFilesButton.Content = MessageResource.Uploader_UploadFiles;
            this.m_displayThumbailCheckBox = GetTemplateChild(DisplayThumbailCheckBoxName) as CheckBox;
            this.m_displayThumbailCheckBox.Visibility = this.AllowThumbnail ? Visibility.Visible : Visibility.Collapsed;
            this.m_displayThumbailCheckBox.IsChecked = this.DisplayThumbnails;
            this.m_displayThumbailCheckBox.Content = MessageResource.Uploader_DisplayThumbnails;
            this.m_borderBackground = GetTemplateChild(BorderBackgroundName) as Border;

            this.m_filesScrollViewer = GetTemplateChild(FilesScrollViewerName) as ScrollViewer;

            string tip = string.Empty;
            if (MultiUpload)
            {
                tip = string.Format(MessageResource.Uploader_TipsInfo, this.MaximumUploadSize, this.MaximumTotalUploadSize);
            }
            else
            {
                tip = string.Format(MessageResource.Uploader_SingleFileTipsInfo, this.MaximumUploadSize);
            }
            ToolTipService.SetToolTip(this.m_addFilesButton,tip);
         
            if (this.m_addFilesButton != null)
            {
                this.m_addFilesButton.Click += new RoutedEventHandler(m_addFilesButton_Click);                
            }
            if (this.m_clearFilesButton != null)
            {
                this.m_clearFilesButton.Click += new RoutedEventHandler(m_clearFilesButton_Click);                
            }
            if (this.m_uploadFilesButton != null)
            {
                this.m_uploadFilesButton.Click += new RoutedEventHandler(m_uploadFilesButton_Click);
            }
            if (this.m_displayThumbailCheckBox != null)
            {
                this.m_displayThumbailCheckBox.Checked += new RoutedEventHandler(m_displayThumbailCheckBox_Checked);
                this.m_displayThumbailCheckBox.Unchecked += new RoutedEventHandler(m_displayThumbailCheckBox_Checked);               
            }
            if (!MultiUpload)
            {
                this.m_clearFilesButton.Visibility = Visibility.Collapsed;
                this.m_displayThumbailCheckBox.IsChecked = false;
                this.m_displayThumbailCheckBox.Visibility = Visibility.Collapsed;
                this.m_clearFilesButton.Visibility = Visibility.Collapsed;
                this.m_filesScrollViewer.VerticalAlignment = VerticalAlignment.Top;
                this.m_borderBackground.VerticalAlignment = VerticalAlignment.Top;
                this.m_borderBackground.Height = 23d;
                this.m_borderBackground.Margin = new Thickness(0, 1, 0, 0);

                Grid.SetRow(this.m_uploadFilesButton, 0);
                Grid.SetColumn(this.m_uploadFilesButton, 2);
                this.m_addFilesButton.Margin = new Thickness(10, 1, 0, 0);
                this.m_uploadFilesButton.Margin = new Thickness(10, 1, 0, 0);
                this.MinHeight = 32;
                this.m_filesScrollViewer.MinHeight = 34;
                this.Height = Double.NaN;
            }
            Loaded += new RoutedEventHandler(FileUploadControl_Loaded);
        }

        public FileUploadControl()
        {
            DefaultStyleKey = typeof(FileUploadControl);
          
            m_files = new ObservableCollection<FileUpload>();
            this.m_files.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(files_CollectionChanged);

            Uploading = false;

            this.DimensionsMatch = DimensionsMatchType.None;
        }
       
        public List<FileInfo> GetFiles()
        {
            var files = new List<FileInfo>();
            if (this.m_files != null)
            {
                foreach (var upload in this.m_files)
                {
                    files.Add(upload.File);
                }                     
            }

            return files;
        }

        public List<FileInfo> GetFiles(bool unComplete)
        {
            var files = new List<FileInfo>();
            if (this.m_files != null)
            {
                foreach (var upload in this.m_files)
                {
                    if (unComplete)
                    {
                        if (upload.Status != FileUploadStatus.Complete)
                        {
                            files.Add(upload.File);
                        }
                    }
                    else
                    {
                        if (upload.Status == FileUploadStatus.Complete)
                        {
                            files.Add(upload.File);
                        }
                    }
                }
            }

            return files;
        }


        public void Clear()
        {
            m_clearFilesButton_Click(null, null);
        }

        #region Private Methods

        void files_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.m_totalCountTextBlock.Text = string.Format(MessageResource.Uploader_TotalCount, m_files.Count.ToString());
            TotalUploadSize = m_files.Sum(f => f.FileLength);
            TotalUploaded = m_files.Sum(f => f.BytesUploaded);
            this.m_totalSizeTextBlock.Text = string.Format("{0} of {1}",
                new ByteConverter().Convert(TotalUploaded, this.GetType(), null, null).ToString(),
                new ByteConverter().Convert(TotalUploadSize, this.GetType(), null, null).ToString());
            this.m_uploadProgressBar.Maximum = TotalUploadSize;
            this.m_uploadProgressBar.Value = TotalUploaded;

            //当ScrollViewer的VerticalScrollBarVisibility设置为Auto的时候，必须要显示的赋值ItemsSource,ItemsControl才能正常的显示子控件
            this.m_fileListItemsControl.ItemsSource = m_files;
        }

        void m_uploadFilesButton_Click(object sender, RoutedEventArgs e)
        {            
            if (!this.Uploading)
            {
                if (m_files.Count(f => f.Status != FileUploadStatus.Complete && f.Status != FileUploadStatus.Uploading && f.Status != FileUploadStatus.Error) > 0)
                {
                    if (this.UploadStarted != null)
                    {
                        this.UploadStarted(this, e);
                    }
                }

                m_AllUploadCompletedEventArgs = new AllUploadCompletedEventArgs();

                m_uploadFilesButton.Content = MessageResource.Uploader_Cancel;
                m_start = DateTime.Now;
                Uploading = true;                
                UploadFiles();
            }
            else
            {
                List<FileInfo> canceledFiles = new List<FileInfo>();
                var q = m_files.Where(f => f.Status == FileUploadStatus.Uploading);
                foreach (FileUpload fu in q)
                {
                    fu.CancelUpload(); 
                    
                    canceledFiles.Add(fu.File);
                }
                Uploading = false;
                Canceld = true;
                m_uploadFilesButton.Content = MessageResource.Uploader_UploadFiles;
                if (UploadCanceled != null)
                {

                    UploadCanceled(this, new UploadCanceledEventArgs(canceledFiles));
                }
            }            
        }

        internal void ShowMessage(string message, Newegg.Oversea.Silverlight.Controls.Components.MessageType type)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                if (this.Page != null)
                {
                    this.Page.Context.Window.Alert(message, type);
                }
                else
                {
                    MessageBox.Show(message);
                }
            });
        }

        #region m_clearFilesButton_Click
        void m_clearFilesButton_Click(object sender, RoutedEventArgs e)
        {
            var q = m_files.Where(f => f.Status == FileUploadStatus.Uploading);
            foreach (FileUpload fu in q)
            {
                fu.CancelUpload();
            }
            this.m_timeLeftTextBlock.Text = "";
            this.m_totalSizeTextBlock.Text = "";
            this.m_uploadProgressBar.Value = 0;
            m_count = 0;
            m_files.Clear();
            this.Uploading = false;
            this.Canceld = false;
            this.m_uploadFilesButton.Content = MessageResource.Uploader_UploadFiles;
            this.m_borderBackground.Visibility = System.Windows.Visibility.Visible;
            if (this.ClearFilesCompleted != null)
            {
                this.ClearFilesCompleted(this, e);
            }
        }
        #endregion

        void m_displayThumbailCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkbox = sender as CheckBox;

            foreach (FileUpload fu in m_files)
            {
                if (fu.DisplayThumbnail != chkbox.IsChecked)
                {
                    fu.DisplayThumbnail = (bool)chkbox.IsChecked;
                }
            }
        }

        #region m_addFilesButton_Click
        void m_addFilesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Filter;
            dlg.Multiselect = MultiSelect;
            if (!MultiUpload)
            {
                dlg.Multiselect = false;                
            }

            if ((bool)dlg.ShowDialog())
            {
                if (!MultiUpload)
                {
                    m_files.Clear();
                }

                if (CheckUploadFilesEvent != null)
                {
                    CheckUploadFileEventArgs checkArgs = new CheckUploadFileEventArgs(new List<FileInfo>(dlg.Files));
                    CheckUploadFilesEvent(this, checkArgs);
                    if (checkArgs.CheckResult == false)
                    {
                        return;
                    }
                }

                BitmapSource imageSource = null;

                foreach (FileInfo file in dlg.Files)
                {
                    string fileName = file.Name.ToLower().Trim();
                    var suffix = file.Extension;
                    //如果文件没有后缀名，需要提示User
                    if (string.IsNullOrEmpty(suffix))
                    {
                        this.ShowMessage(MessageResource.Uploader_InvalidFileExtension, Newegg.Oversea.Silverlight.Controls.Components.MessageType.Warning);
                        continue;
                    }

                    if (!this.Filter.Contains("*.*") && !this.Filter.ToLower().Contains(suffix.ToLower()))
                    {
                        this.ShowMessage(MessageResource.Uploader_InvalidFileExtension, Newegg.Oversea.Silverlight.Controls.Components.MessageType.Warning);
                        continue;
                    }
                    try
                    {
                        using (file.OpenRead())
                        {
                            if (fileName.EndsWith("jpg") || fileName.EndsWith("jpeg") || fileName.EndsWith("png"))
                            {
                                imageSource = new BitmapImage();
                                using (var stream = file.OpenRead())
                                {
                                    try
                                    {
                                        imageSource.SetSource(stream);
                                    }
                                    catch
                                    {
                                        //捕获特定异常，比如把a.xls改成a.jpg,此时需要给一个友好提示
                                        this.ShowMessage(MessageResource.Uploader_InvalidImageType, Newegg.Oversea.Silverlight.Controls.Components.MessageType.Warning);
                                        continue;
                                    }
                                }
                            }
                            else if (fileName.EndsWith("gif"))
                            {
                                GifImage gif = null;
                                using (var stream = file.OpenRead())
                                {
                                    try
                                    {
                                        gif = GIFDecoder.Decode(stream);
                                    }
                                    catch
                                    {
                                        //捕获特定异常，比如把a.jpg改成a.gif,此时需要给一个友好提示
                                        this.ShowMessage(MessageResource.Uploader_InvalidImageType, Newegg.Oversea.Silverlight.Controls.Components.MessageType.Warning);
                                        continue;
                                    }
                                    if (gif.Frames.Count > 0)
                                    {
                                        imageSource = gif.Frames[0].Image;
                                    }//如果user把jpg文件的后缀修改为.gif,需要抛异常提示User
                                    else
                                    {
                                        this.ShowMessage(MessageResource.Uploader_InvalidImageType, Newegg.Oversea.Silverlight.Controls.Components.MessageType.Warning);
                                        continue;
                                    }
                                }
                            }
                        }
                    }//检查文件是否正在使用
                    catch (IOException)
                    {
                        this.ShowMessage(MessageResource.Uploader_FileInUsed, Newegg.Oversea.Silverlight.Controls.Components.MessageType.Error);
                        continue;
                    }
                    //Catch other exception
                    catch (Exception ex)
                    {
                        this.ShowMessage(ex.Message, Newegg.Oversea.Silverlight.Controls.Components.MessageType.Error);
                        continue;
                    }

                    //check image width and height
                    
                    if (this.DimensionsMatch != DimensionsMatchType.None)
                    {
                        if (fileName.EndsWith("jpg") || fileName.EndsWith("png") || fileName.EndsWith("gif") ||
                            fileName.EndsWith("jpeg") || fileName.EndsWith("bmp"))
                        {
                            try
                            {                                
                                if (imageSource != null)
                                {
                                    CheckImageDimensions(imageSource, file);
                                }
                            }
                            // Catch check exception
                            catch (ArgumentException ex)
                            {
                                this.ShowMessage(ex.Message, Newegg.Oversea.Silverlight.Controls.Components.MessageType.Warning);
                                continue;
                            }
                        }
                    }

                    FileUpload upload = new FileUpload(this.Dispatcher, UploadUrl, file, this);
                    upload.UploadCanceled += (o, b) =>
                        {
                            if (this.UploadCanceled != null)
                            {
                                UploadCanceled(this, b);
                            }
                        };

                    upload.CanEditFileName = CanEditFileName;  
          
                    if (UploadChunkSize > 0)
                    {
                        upload.ChunkSize = UploadChunkSize;
                    }
                    if (MaximumFileNameLength > 0 && upload.File.Name.Length > MaximumFileNameLength)
                    {
                        this.ShowMessage(string.Format(MessageResource.Uploader_GreaterThanMaximumFileNameLength, upload.Name, upload.File.Name.Length, this.MaximumFileNameLength), Newegg.Oversea.Silverlight.Controls.Components.MessageType.Warning);
                        continue;
                    } 
                    
                    //单个文件超过限制的大小
                    if (MaximumUploadBytes >= 0 && upload.FileLength > MaximumUploadBytes)
                    {
                        this.ShowMessage(string.Format(MessageResource.Uploader_OutOfSingleAvailableUploadSize, upload.Name, this.MaximumUploadSize), Newegg.Oversea.Silverlight.Controls.Components.MessageType.Warning);
                        continue;
                    }
                   
                    //全部的文件Size超过限制大小
                    if (MaximumTotalUploadBytes >= 0 && TotalUploadSize + upload.FileLength > MaximumTotalUploadBytes)
                    {
                        this.ShowMessage(string.Format(MessageResource.Uploader_OutOfTotalAvailableUploadSize, this.MaximumTotalUploadSize), Newegg.Oversea.Silverlight.Controls.Components.MessageType.Warning);
                        break;
                    }

                    //多文件上传的时候才作此Check
                    if (MaxNumberToUpload > -1 && MultiUpload)
                    {          
                        if (++m_count > MaxNumberToUpload)
                        {
                            this.ShowMessage(string.Format(MessageResource.Uploader_OutOfMaximumNumber, this.MaxNumberToUpload), Newegg.Oversea.Silverlight.Controls.Components.MessageType.Warning);
                            break;
                        }
        
                    }
                    upload.DisplayThumbnail = (bool)this.m_displayThumbailCheckBox.IsChecked;
                    if (!this.AllowThumbnail)
                    {
                        upload.DisplayThumbnail = false;
                    }
                    upload.StatusChanged += new EventHandler(upload_StatusChanged);
                    upload.UploadProgressChanged += new ProgressChangedEvent(upload_UploadProgressChanged);
                    upload.UploadCompleted += new UploadCompletedEvent(upload_UploadCompleted);
                    m_files.Add(upload);
                    this.m_borderBackground.Visibility = System.Windows.Visibility.Collapsed;
                }                
            }
        }
        #endregion


        #region CheckImageDimensions
        void CheckImageDimensions(BitmapSource image, FileInfo file)
        {
            switch (this.DimensionsMatch)
            {
                case DimensionsMatchType.GreaterThanOrEqual:
                    if (this.ImageWidth > 0 && image.PixelWidth < this.ImageWidth)
                    {
                        throw new ArgumentException(string.Format(MessageResource.Uploader_LessThanMinimumImageWidth, file.Name, image.PixelWidth, this.ImageWidth));
                    }
                    if (this.ImageHeight > 0 && image.PixelHeight < this.ImageHeight)
                    {
                        throw new ArgumentException(string.Format(MessageResource.Uploader_LessThanMinimumImageHeight, file.Name, image.PixelHeight, this.ImageHeight));
                    }
                    break;
                case DimensionsMatchType.Equal:
                    if ((this.ImageWidth > 0 && image.PixelWidth != this.ImageWidth) || 
                        (this.ImageHeight > 0 && image.PixelHeight != this.ImageHeight))
                    {
                        throw new ArgumentException(string.Format(MessageResource.Uploader_NotEqualImageDimension, this.ImageWidth, this.ImageHeight));
                    }
                    break;
                case DimensionsMatchType.None:
                    break;
            }
        }
        #endregion 


        void upload_UploadCompleted(object sender, UploadCompletedEventArgs args)
        {
            if (UploadCompleted != null)
            {
                this.Dispatcher.BeginInvoke(() =>
                {
                    UploadCompleted(this, args);
                });
            }

            m_AllUploadCompletedEventArgs.UploadInfo.Add(args);

            int c = m_files.Count(p => (p.Status == FileUploadStatus.Canceled)
                || (p.Status == FileUploadStatus.Complete)
                || (p.Status == FileUploadStatus.Error)
                || (p.Status == FileUploadStatus.Removed));
            //所有文件都上传完成了（无论成功还是失败）
            if (m_files.Count == c)
            {
                this.Dispatcher.BeginInvoke(() =>
                {   
                    Uploading = false;
                    this.m_uploadFilesButton.Content = MessageResource.Uploader_UploadFiles;
                    if (this.AllFileUploadCompleted != null)
                    {
                        this.AllFileUploadCompleted(this, m_AllUploadCompletedEventArgs);
                    }
                });
            }
        }


        void upload_UploadProgressChanged(object sender, UploadProgressChangedEventArgs args)
        {
            //如果User Clear了文件,则不应该更新上传进度,如果只是Cancel，则需要更新上传进度
            if (!this.Uploading && !Canceld)
            {
                return;
            }
            TotalUploaded += args.BytesUploaded;
            m_uploadProgressBar.Value = TotalUploaded;
            m_totalSizeTextBlock.Text = string.Format("{0} of {1}",
                 new ByteConverter().Convert(TotalUploaded, this.GetType(), null, null).ToString(),
                 new ByteConverter().Convert(TotalUploadSize, this.GetType(), null, null).ToString());

            double ByteProcessTime = 0;
            double EstimatedTime = 0;

            try
            {
                TimeSpan timeSpan = DateTime.Now - m_start;
                ByteProcessTime = TotalUploaded / timeSpan.TotalSeconds;
                EstimatedTime = (TotalUploadSize - TotalUploaded) / ByteProcessTime;
                timeSpan = TimeSpan.FromSeconds(EstimatedTime);
                this.m_timeLeftTextBlock.Text = string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            }
            catch { }
        }

        void upload_StatusChanged(object sender, EventArgs e)
        {
            FileUpload fu = sender as FileUpload;
            if (fu.Status == FileUploadStatus.Complete)
            {
                if (Uploading)
                {
                    UploadFiles();
                }
            }
            else if (fu.Status == FileUploadStatus.Removed)
            {
                m_files.Remove(fu);
                m_count--;
                if (m_files.Count == 0)
                {
                    this.m_borderBackground.Visibility = System.Windows.Visibility.Visible;
                }
                if (Uploading)
                {
                    UploadFiles();
                }
            }
            else if (fu.Status == FileUploadStatus.Uploading && m_files.Count(f => f.Status == FileUploadStatus.Uploading) < MaxConcurrentUploads)
            {
                UploadFiles();
            }
        }


        void FileUploadControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_fileListItemsControl.ItemsSource = m_files;
        }

        void UploadFiles()
        {
            while (m_files.Count(f => f.Status == FileUploadStatus.Uploading) < MaxConcurrentUploads && Uploading)
            {
                if (m_files.Count(f => f.Status != FileUploadStatus.Complete && f.Status != FileUploadStatus.Uploading && f.Status != FileUploadStatus.Error) > 0)
                {
                    FileUpload fu = m_files.FirstOrDefault(f => f.Status != FileUploadStatus.Complete && f.Status != FileUploadStatus.Uploading);

                    if (fu != null)
                    {
                        if (this.EachFilePreStartUpload != null)
                        {
                            this.EachFilePreStartUpload(this, new UploadPreStartEventArgs(fu.File.Name, fu.File));
                        }
                        fu.UploadParams = this.UploadParams;
                        fu.Upload();
                    }
                }
                else if (m_files.Count(f => f.Status == FileUploadStatus.Uploading) == 0)
                {
                    Uploading = false;
                    m_uploadFilesButton.Content = MessageResource.Uploader_UploadFiles;
                }
                else
                {
                    break;
                }
            }
        }        

        #endregion
    }
}
