﻿// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Newegg.Oversea.Silverlight.Controls.Resources;

namespace Newegg.Oversea.Silverlight.Controls
{
    /// <summary>
    /// Provides a window that can be displayed over a parent window and blocks
    /// interaction with the parent window.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    [TemplatePart(Name = PART_Chrome, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_CloseButton, Type = typeof(ButtonBase))]
    [TemplatePart(Name = PART_ContentPresenter, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_ContentRoot, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_Overlay, Type = typeof(Panel))]
    [TemplatePart(Name = PART_Root, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_Resizer, Type = typeof(FrameworkElement))]
    [TemplateVisualState(Name = VSMSTATE_StateClosed, GroupName = VSMGROUP_Window)]
    [TemplateVisualState(Name = VSMSTATE_StateOpen, GroupName = VSMGROUP_Window)]
    public class FloatableWindow : ContentControl
    {
        #region Static Fields and Constants

        /// <summary>
        /// The name of the Chrome template part.
        /// </summary>
        private const string PART_Chrome = "Chrome";

        /// <summary>
        /// The name of the Resizer template part.
        /// </summary>
        private const string PART_Resizer = "Resizer";

        /// <summary>
        /// The name of the CloseButton template part.
        /// </summary>
        private const string PART_CloseButton = "CloseButton";

        /// <summary>
        /// The name of the ContentPresenter template part.
        /// </summary>
        private const string PART_ContentPresenter = "ContentPresenter";

        /// <summary>
        /// The name of the ContentRoot template part.
        /// </summary>
        private const string PART_ContentRoot = "ContentRoot";

        /// <summary>
        /// The name of the Overlay template part.
        /// </summary>
        private const string PART_Overlay = "Overlay";

        /// <summary>
        /// The name of the Root template part.
        /// </summary>
        private const string PART_Root = "Root";

        /// <summary>
        /// The name of the WindowStates VSM group.
        /// </summary>
        private const string VSMGROUP_Window = "WindowStates";

        /// <summary>
        /// The name of the Closing VSM state.
        /// </summary>
        private const string VSMSTATE_StateClosed = "Closed";

        /// <summary>
        /// The name of the Opening VSM state.
        /// </summary>
        private const string VSMSTATE_StateOpen = "Open";

        #region public bool HasCloseButton

        /// <summary>
        /// Gets or sets a value indicating whether the
        /// <see cref="T:System.Windows.Controls.FloatableWindow" /> has a close
        /// button.
        /// </summary>
        /// <value>
        /// True if the child window has a close button; otherwise, false. The
        /// default is true.
        /// </value>
        public bool HasCloseButton
        {
            get { return (bool)GetValue(HasCloseButtonProperty); }
            set { SetValue(HasCloseButtonProperty, value); }
        }

        /// <summary>
        /// Identifies the
        /// <see cref="P:System.Windows.Controls.FloatableWindow.HasCloseButton" />
        /// dependency property.
        /// </summary>
        /// <value>
        /// The identifier for the
        /// <see cref="P:System.Windows.Controls.FloatableWindow.HasCloseButton" />
        /// dependency property.
        /// </value>
        public static readonly DependencyProperty HasCloseButtonProperty =
            DependencyProperty.Register(
            "HasCloseButton",
            typeof(bool),
            typeof(FloatableWindow),
            new PropertyMetadata(true, OnHasCloseButtonPropertyChanged));

        /// <summary>
        /// HasCloseButtonProperty PropertyChangedCallback call back static function.
        /// </summary>
        /// <param name="d">FloatableWindow object whose HasCloseButton property is changed.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs which contains the old and new values.</param>
        private static void OnHasCloseButtonPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FloatableWindow cw = (FloatableWindow)d;

            if (cw.CloseButton != null)
            {
                if ((bool)e.NewValue)
                {
                    cw.CloseButton.Visibility = Visibility.Visible;
                }
                else
                {
                    cw.CloseButton.Visibility = Visibility.Collapsed;
                }
            }
        }

        #endregion public bool HasCloseButton

        #region public Brush OverlayBrush

        /// <summary>
        /// Gets or sets the visual brush that is used to cover the parent
        /// window when the child window is open.
        /// </summary>
        /// <value>
        /// The visual brush that is used to cover the parent window when the
        /// <see cref="T:System.Windows.Controls.FloatableWindow" /> is open. The
        /// default is null.
        /// </value>
        public Brush OverlayBrush
        {
            get { return (Brush)GetValue(OverlayBrushProperty); }
            set { SetValue(OverlayBrushProperty, value); }
        }

        /// <summary>
        /// Identifies the
        /// <see cref="P:System.Windows.Controls.FloatableWindow.OverlayBrush" />
        /// dependency property.
        /// </summary>
        /// <value>
        /// The identifier for the
        /// <see cref="P:System.Windows.Controls.FloatableWindow.OverlayBrush" />
        /// dependency property.
        /// </value>
        public static readonly DependencyProperty OverlayBrushProperty =
            DependencyProperty.Register(
            "OverlayBrush",
            typeof(Brush),
            typeof(FloatableWindow),
            new PropertyMetadata(OnOverlayBrushPropertyChanged));

        /// <summary>
        /// OverlayBrushProperty PropertyChangedCallback call back static function.
        /// </summary>
        /// <param name="d">FloatableWindow object whose OverlayBrush property is changed.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs which contains the old and new values.</param>
        private static void OnOverlayBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FloatableWindow cw = (FloatableWindow)d;

            if (cw.Overlay != null)
            {
                cw.Overlay.Background = (Brush)e.NewValue;
            }
        }

        #endregion public Brush OverlayBrush

        #region public double OverlayOpacity

        /// <summary>
        /// Gets or sets the opacity of the overlay brush that is used to cover
        /// the parent window when the child window is open.
        /// </summary>
        /// <value>
        /// The opacity of the overlay brush that is used to cover the parent
        /// window when the <see cref="T:System.Windows.Controls.FloatableWindow" />
        /// is open. The default is 1.0.
        /// </value>
        public double OverlayOpacity
        {
            get { return (double)GetValue(OverlayOpacityProperty); }
            set { SetValue(OverlayOpacityProperty, value); }
        }

        /// <summary>
        /// Identifies the
        /// <see cref="P:System.Windows.Controls.FloatableWindow.OverlayOpacity" />
        /// dependency property.
        /// </summary>
        /// <value>
        /// The identifier for the
        /// <see cref="P:System.Windows.Controls.FloatableWindow.OverlayOpacity" />
        /// dependency property.
        /// </value>
        public static readonly DependencyProperty OverlayOpacityProperty =
            DependencyProperty.Register(
            "OverlayOpacity",
            typeof(double),
            typeof(FloatableWindow),
            new PropertyMetadata(OnOverlayOpacityPropertyChanged));

        /// <summary>
        /// OverlayOpacityProperty PropertyChangedCallback call back static function.
        /// </summary>
        /// <param name="d">FloatableWindow object whose OverlayOpacity property is changed.</param>
        /// <param name="e">DependencyPropertyChangedEventArgs which contains the old and new values.</param>
        private static void OnOverlayOpacityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FloatableWindow cw = (FloatableWindow)d;

            if (cw.Overlay != null)
            {
                cw.Overlay.Opacity = (double)e.NewValue;
            }
        }

        #endregion public double OverlayOpacity

        #region private static Control RootVisual

        /// <summary>
        /// Gets the root visual element.
        /// </summary>
        private static Control RootVisual
        {
            get
            {
                return Application.Current == null ? null : (Application.Current.RootVisual as Control);
            }
        }

        #endregion private static Control RootVisual

        #region public object Title

        /// <summary>
        /// Gets or sets the title that is displayed in the frame of the
        /// <see cref="T:System.Windows.Controls.FloatableWindow" />.
        /// </summary>
        /// <value>
        /// The title displayed at the top of the window. The default is null.
        /// </value>
        public object Title
        {
            get { return GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Identifies the
        /// <see cref="P:System.Windows.Controls.FloatableWindow.Title" />
        /// dependency property.
        /// </summary>
        /// <value>
        /// The identifier for the
        /// <see cref="P:System.Windows.Controls.FloatableWindow.Title" />
        /// dependency property.
        /// </value>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
            "Title",
            typeof(object),
            typeof(FloatableWindow),
            null);

        #endregion public object Title

        #endregion Static Fields and Constants

        #region Member Fields
        /// <summary>
        /// Set in the overloaded Show method.  Offsets the Popup vertically from the top left corner of the browser window by this amount.
        /// </summary>
        private double _verticalOffset;

        /// <summary>
        /// Set in the overloaded Show method.  Offsets the Popup horizontally from the top left corner of the browser window by this amount.
        /// </summary>
        private double _horizontalOffset;

        /// <summary>
        /// Private accessor for the Resizer.
        /// </summary>
        private FrameworkElement _resizer;

        /// <summary>
        /// Private accessor for the IsModal
        /// </summary>
        [DefaultValue(false)]
        private bool _modal;

        /// <summary>
        /// Private accessor for the Chrome.
        /// </summary>
        private FrameworkElement _chrome;

        /// <summary>
        /// Private accessor for the click point on the chrome.
        /// </summary>
        private Point _clickPoint;

        /// <summary>
        /// Private accessor for the Closing storyboard.
        /// </summary>
        private Storyboard _closed;

        /// <summary>
        /// Private accessor for the ContentPresenter.
        /// </summary>
        private FrameworkElement _contentPresenter;

        /// <summary>
        /// Private accessor for the translate transform that needs to be applied on to the ContentRoot.
        /// </summary>
        private TranslateTransform _contentRootTransform;

        /// <summary>
        /// Content area desired width.
        /// </summary>
        private double _desiredContentWidth;

        /// <summary>
        /// Content area desired height.
        /// </summary>
        private double _desiredContentHeight;

        /// <summary>
        /// Desired margin for the window.
        /// </summary>
        private Thickness _desiredMargin;

        /// <summary>
        /// Private accessor for the Dialog Result property.
        /// </summary>
        private bool? _dialogresult;

        /// <summary>
        /// Private accessor for the FloatableWindow InteractionState.
        /// </summary>
        private WindowInteractionState _interactionState;

        /// <summary>
        /// Boolean value that specifies whether the application is exit or not.
        /// </summary>
        private bool _isAppExit;

        /// <summary>
        /// Boolean value that specifies whether the window is in closing state or not.
        /// </summary>
        private bool _isClosing;

        /// <summary>
        /// Boolean value that specifies whether the window is opened.
        /// </summary>
        private bool _isOpen;

        /// <summary>
        /// Private accessor for the Opening storyboard.
        /// </summary>
        private Storyboard _opened;

        /// <summary>
        /// Boolean value that specifies whether the mouse is captured or not.
        /// </summary>
        private bool _isMouseCaptured;

        /// <summary>
        /// Private accessor for the Root of the window.
        /// </summary>
        private FrameworkElement _root;

        /// <summary>
        /// Private accessor for the position of the window with respect to RootVisual.
        /// </summary>
        private Point _windowPosition;

        private static int z;

        #endregion Member Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:System.Windows.Controls.FloatableWindow" /> class.
        /// </summary>
        public FloatableWindow()
        {
            this.DefaultStyleKey = typeof(FloatableWindow);
            this.InteractionState = WindowInteractionState.NotResponding;
            this.ResizeMode = ResizeMode.CanResize;
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Occurs when the <see cref="T:System.Windows.Controls.FloatableWindow" />
        /// is closed.
        /// </summary>
        public event EventHandler Closed;

        /// <summary>
        /// Occurs when the <see cref="T:System.Windows.Controls.FloatableWindow" />
        /// is closing.
        /// </summary>
        public event EventHandler<CancelEventArgs> Closing;

        #endregion Events

        #region Properties

        public Panel ParentLayoutRoot
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the internal accessor for the ContentRoot of the window.
        /// </summary>
        internal FrameworkElement ContentRoot
        {
            get;
            private set;
        }

        /// <summary>
        /// Setting for the horizontal positioning offset for start position
        /// </summary>
        public double HorizontalOffset
        {
            get { return _horizontalOffset; }
            set { _horizontalOffset = value; }
        }

        /// <summary>
        /// Setting for the vertical positioning offset for start position
        /// </summary>
        public double VerticalOffset
        {
            get { return _verticalOffset; }
            set { _verticalOffset = value; }
        }

        /// <summary>
        /// Gets the internal accessor for the modal of the window.
        /// </summary>
        public bool IsModal
        {
            get
            {
                return _modal;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the
        /// <see cref="T:System.Windows.Controls.FloatableWindow" /> was accepted or
        /// canceled.
        /// </summary>
        /// <value>
        /// True if the child window was accepted; false if the child window was
        /// canceled. The default is null.
        /// </value>
        [TypeConverter(typeof(NullableBoolConverter))]
        public bool? DialogResult
        {
            get
            {
                return this._dialogresult;
            }

            set
            {
                if (this._dialogresult != value)
                {
                    this._dialogresult = value;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Gets the internal accessor for the PopUp of the window.
        /// </summary>
        internal Popup ChildWindowPopup
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the internal accessor for the close button of the window.
        /// </summary>
        internal ButtonBase CloseButton
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the InteractionState for the FloatableWindow.
        /// </summary>
        internal WindowInteractionState InteractionState
        {
            get
            {
                return this._interactionState;
            }
            private set
            {
                if (this._interactionState != value)
                {
                    WindowInteractionState oldValue = this._interactionState;
                    this._interactionState = value;
                    FloatableWindowAutomationPeer peer = FloatableWindowAutomationPeer.FromElement(this) as FloatableWindowAutomationPeer;

                    if (peer != null)
                    {
                        peer.RaiseInteractionStatePropertyChangedEvent(oldValue, this._interactionState);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the PopUp is open or not.
        /// </summary>
        private bool IsOpen
        {
            get
            {
                return (this.ChildWindowPopup != null && this.ChildWindowPopup.IsOpen) ||
                    ((ParentLayoutRoot != null) && (ParentLayoutRoot.Children.Contains(this)));
            }
        }

        /// <summary>
        /// Gets the internal accessor for the overlay of the window.
        /// </summary>
        internal Panel Overlay
        {
            get;
            private set;
        }

        public ResizeMode ResizeMode
        {
            get;
            set;
        }

        #endregion Properties

        #region Static Methods

        /// <summary>
        /// Inverts the input matrix.
        /// </summary>
        /// <param name="matrix">The matrix values that is to be inverted.</param>
        /// <returns>Returns a value indicating whether the inversion was successful or not.</returns>
        private static bool InvertMatrix(ref Matrix matrix)
        {
            double determinant = (matrix.M11 * matrix.M22) - (matrix.M12 * matrix.M21);

            if (determinant == 0.0)
            {
                return false;
            }

            Matrix matCopy = matrix;
            matrix.M11 = matCopy.M22 / determinant;
            matrix.M12 = -1 * matCopy.M12 / determinant;
            matrix.M21 = -1 * matCopy.M21 / determinant;
            matrix.M22 = matCopy.M11 / determinant;
            matrix.OffsetX = ((matCopy.OffsetY * matCopy.M21) - (matCopy.OffsetX * matCopy.M22)) / determinant;
            matrix.OffsetY = ((matCopy.OffsetX * matCopy.M12) - (matCopy.OffsetY * matCopy.M11)) / determinant;

            return true;
        }

        #endregion Static Methods

        #region Methods

        /// <summary>
        /// Executed when the application is exited.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event args.</param>
        internal void Application_Exit(object sender, EventArgs e)
        {
            if (this.IsOpen)
            {
                this._isAppExit = true;
                try
                {
                    this.Close();
                }
                finally
                {
                    this._isAppExit = false;
                }
            }
        }

        /// <summary>
        /// Executed when focus is given to the window via a click.  Attempts to bring current 
        /// window to the front in the event there are more windows.
        /// </summary>
        internal void BringToFront()
        {
            z++;
            Canvas.SetZIndex(this, z);
        }

        /// <summary>
        /// Changes the visual state of the FloatableWindow.
        /// </summary>
        private void ChangeVisualState()
        {
            if (this._isClosing)
            {
                VisualStateManager.GoToState(this, VSMSTATE_StateClosed, true);
            }
            else
            {
                VisualStateManager.GoToState(this, VSMSTATE_StateOpen, true);
                BringToFront();
            }
        }

        /// <summary>
        /// Executed when FloatableWindow size is changed.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Size changed event args.</param>
        private void ChildWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_modal)
            {
                if (this.Overlay != null)
                {
                    if (e.NewSize.Height != this.Overlay.Height)
                    {
                        this._desiredContentHeight = e.NewSize.Height;
                    }

                    if (e.NewSize.Width != this.Overlay.Width)
                    {
                        this._desiredContentWidth = e.NewSize.Width;
                    }
                }

                if (this.IsOpen)
                {
                    this.UpdateOverlaySize();
                }
            }
        }

        /// <summary>
        /// Closes a <see cref="T:System.Windows.Controls.FloatableWindow" />.
        /// </summary>
        public void Close()
        {
            // AutomationPeer returns "Closing" when Close() is called
            // but the window is not closed completely:
            this.InteractionState = WindowInteractionState.Closing;
            CancelEventArgs e = new CancelEventArgs();
            this.OnClosing(e);

            // On ApplicationExit, close() cannot be cancelled
            if (!e.Cancel || this._isAppExit)
            {
                if (RootVisual != null)
                {
                    RootVisual.IsEnabled = true;
                }

                // Close Popup
                if (this.IsOpen)
                {
                    if (this._closed != null)
                    {
                        // Popup will be closed when the storyboard ends
                        this._isClosing = true;
                        try
                        {
                            var sb = GetVisualStateStoryboard("WindowStates", "Closed");
                            sb.Completed += (s, args) =>
                            {
                                if (this.ParentLayoutRoot != null)
                                {
                                    this.ParentLayoutRoot.Children.Remove(this);
                                }
                                this.OnClosed(EventArgs.Empty);
                                this.UnSubscribeFromEvents();
                                this.UnsubscribeFromTemplatePartEvents();

                                if (Application.Current.RootVisual != null)
                                {
                                    Application.Current.RootVisual.GotFocus -= new RoutedEventHandler(this.RootVisual_GotFocus);
                                }
                            };
                            this.ChangeVisualState();
                        }
                        finally
                        {
                            this._isClosing = false;
                        }
                    }
                    else
                    {
                        // If no closing storyboard is defined, close the Popup
                        this.ChildWindowPopup.IsOpen = false;
                    }

                    if (!this._dialogresult.HasValue)
                    {
                        // If close action is not happening because of DialogResult property change action,
                        // Dialogresult is always false:
                        this._dialogresult = false;
                    }

                    //this.OnClosed(EventArgs.Empty);
                    //this.UnSubscribeFromEvents();
                    //this.UnsubscribeFromTemplatePartEvents();

                    //if (Application.Current.RootVisual != null)
                    //{
                    //    Application.Current.RootVisual.GotFocus -= new RoutedEventHandler(this.RootVisual_GotFocus);
                    //}
                }
            }
            else
            {
                // If the Close is cancelled, DialogResult should always be NULL:
                this._dialogresult = null;
                this.InteractionState = WindowInteractionState.Running;
            }
        }

        /// <summary>
        /// Brings the window to the front of others
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ContentRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BringToFront();
        }

        /// <summary>
        /// Executed when the CloseButton is clicked.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Routed event args.</param>
        internal void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Executed when the Closing storyboard ends.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event args.</param>
        private void Closing_Completed(object sender, EventArgs e)
        {
            if (this.ChildWindowPopup != null)
            {
                this.ChildWindowPopup.IsOpen = false;
            }

            // AutomationPeer returns "NotResponding" when the FloatableWindow is closed:
            this.InteractionState = WindowInteractionState.NotResponding;

            if (this._closed != null)
            {
                this._closed.Completed -= new EventHandler(this.Closing_Completed);
            }
        }

        /// <summary>
        /// Executed when the a key is presses when the window is open.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Key event args.</param>
        private void ChildWindow_KeyDown(object sender, KeyEventArgs e)
        {
            FloatableWindow ew = sender as FloatableWindow;
            Debug.Assert(ew != null, "FloatableWindow instance is null.");

            // Ctrl+Shift+F4 closes the FloatableWindow
            if (e != null && !e.Handled && e.Key == Key.F4 &&
                ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) &&
                ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift))
            {
                ew.Close();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Executed when the window loses focus.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Routed event args.</param>
        private void ChildWindow_LostFocus(object sender, RoutedEventArgs e)
        {
            // If the FloatableWindow loses focus but the popup is still open,
            // it means another popup is opened. To get the focus back when the
            // popup is closed, we handle GotFocus on the RootVisual
            // TODO: Something else could get focus and handle the GotFocus event right.  
            // Try listening to routed events that were Handled (new SL 3 feature)
            // Blocked by Jolt bug #29419
            if (this.IsOpen && Application.Current != null && Application.Current.RootVisual != null)
            {
                this.InteractionState = WindowInteractionState.BlockedByModalWindow;
                Application.Current.RootVisual.GotFocus += new RoutedEventHandler(this.RootVisual_GotFocus);
            }
        }

        /// <summary>
        /// Executed when mouse left button is down on the chrome.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Mouse button event args.</param>
        private void Chrome_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this._chrome != null)
            {
                e.Handled = true;

                this.BringToFront();

                if (this.CloseButton != null && !this.CloseButton.IsTabStop)
                {
                    this.CloseButton.IsTabStop = true;
                    try
                    {
                        this.Focus();
                    }
                    finally
                    {
                        this.CloseButton.IsTabStop = false;
                    }
                }
                else
                {
                    this.Focus();
                }
                this._chrome.CaptureMouse();
                this._isMouseCaptured = true;
                this._clickPoint = e.GetPosition(sender as UIElement);
            }
        }

        /// <summary>
        /// Executed when mouse left button is up on the chrome.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Mouse button event args.</param>
        private void Chrome_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this._chrome != null)
            {
                //e.Handled = true;
                this._chrome.ReleaseMouseCapture();
                this._isMouseCaptured = false;
            }
        }

        /// <summary>
        /// Executed when mouse moves on the chrome.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Mouse event args.</param>
        private void Chrome_MouseMove(object sender, MouseEventArgs e)
        {
            #region New ChildWindow Code not working
            //if (this._isMouseCaptured && this.ContentRoot != null && Application.Current != null && Application.Current.RootVisual != null)
            //{
            //    Point position = e.GetPosition(Application.Current.RootVisual);

            //    GeneralTransform gt = this.ContentRoot.TransformToVisual(Application.Current.RootVisual);

            //    if (gt != null)
            //    {
            //        Point p = gt.Transform(this._clickPoint);
            //        this._windowPosition = gt.Transform(new Point(0, 0));

            //        if (position.X < 0)
            //        {
            //            double Y = FindPositionY(p, position, 0);
            //            position = new Point(0, Y);
            //        }

            //        if (position.X > this.Width)
            //        {
            //            double Y = FindPositionY(p, position, this.Width);
            //            position = new Point(this.Width, Y);
            //        }

            //        if (position.Y < 0)
            //        {
            //            double X = FindPositionX(p, position, 0);
            //            position = new Point(X, 0);
            //        }

            //        if (position.Y > this.Height)
            //        {
            //            double X = FindPositionX(p, position, this.Height);
            //            position = new Point(X, this.Height);
            //        }

            //        double x = position.X - p.X;
            //        double y = position.Y - p.Y;
            //        UpdateContentRootTransform(x, y);
            //    }
            //} 
            #endregion
            if (this._isMouseCaptured && this.ContentRoot != null)
            {
                // If the child window is dragged out of the page, return
                if (Application.Current != null && Application.Current.RootVisual != null &&
                    (e.GetPosition(Application.Current.RootVisual).X < 0 || e.GetPosition(Application.Current.RootVisual).Y < 0))
                {
                    return;
                }

                TransformGroup transformGroup = this.ContentRoot.RenderTransform as TransformGroup;

                if (transformGroup == null)
                {
                    transformGroup = new TransformGroup();
                    transformGroup.Children.Add(this.ContentRoot.RenderTransform);
                }

                TranslateTransform t = new TranslateTransform();
                t.X = e.GetPosition(this.ContentRoot).X - this._clickPoint.X;
                t.Y = e.GetPosition(this.ContentRoot).Y - this._clickPoint.Y;
                if (transformGroup != null)
                {
                    transformGroup.Children.Add(t);
                    this.ContentRoot.RenderTransform = transformGroup;
                }
            }
        }

        /// <summary>
        /// Executed when the ContentPresenter size changes.
        /// </summary>
        /// <param name="sender">Content Presenter object.</param>
        /// <param name="e">SizeChanged event args.</param>
        private void ContentPresenter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // timheuer: not sure really why this is here?
            //if (this.ContentRoot != null && Application.Current != null && Application.Current.RootVisual != null && _isOpen)
            //{
            //    GeneralTransform gt = this.ContentRoot.TransformToVisual(Application.Current.RootVisual);

            //    if (gt != null)
            //    {
            //        Point p = gt.Transform(new Point(0, 0));

            //        double x = this._windowPosition.X - p.X;
            //        double y = this._windowPosition.Y - p.Y;
            //        UpdateContentRootTransform(x, y);
            //    }
            //}

            //RectangleGeometry rg = new RectangleGeometry();
            //rg.Rect = new Rect(0, 0, this._contentPresenter.ActualWidth, this._contentPresenter.ActualHeight);
            //this._contentPresenter.Clip = rg;
            //this.UpdatePosition();
        }

        /// <summary>
        /// Finds the X coordinate of a point that is defined by a line.
        /// </summary>
        /// <param name="p1">Starting point of the line.</param>
        /// <param name="p2">Ending point of the line.</param>
        /// <param name="y">Y coordinate of the point.</param>
        /// <returns>X coordinate of the point.</returns>
        private static double FindPositionX(Point p1, Point p2, double y)
        {
            if (y == p1.Y || p1.X == p2.X)
            {
                return p2.X;
            }

            Debug.Assert(p1.Y != p2.Y, "Unexpected equal Y coordinates");

            return (((y - p1.Y) * (p1.X - p2.X)) / (p1.Y - p2.Y)) + p1.X;
        }

        /// <summary>
        /// Finds the Y coordinate of a point that is defined by a line.
        /// </summary>
        /// <param name="p1">Starting point of the line.</param>
        /// <param name="p2">Ending point of the line.</param>
        /// <param name="x">X coordinate of the point.</param>
        /// <returns>Y coordinate of the point.</returns>
        private static double FindPositionY(Point p1, Point p2, double x)
        {
            if (p1.Y == p2.Y || x == p1.X)
            {
                return p2.Y;
            }

            Debug.Assert(p1.X != p2.X, "Unexpected equal X coordinates");

            return (((p1.Y - p2.Y) * (x - p1.X)) / (p1.X - p2.X)) + p1.Y;
        }

        /// <summary>
        /// Builds the visual tree for the
        /// <see cref="T:System.Windows.Controls.FloatableWindow" /> control when a
        /// new template is applied.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "No need to split the code into two parts.")]
        public override void OnApplyTemplate()
        {
            this.UnsubscribeFromTemplatePartEvents();

            base.OnApplyTemplate();

            this.CloseButton = GetTemplateChild(PART_CloseButton) as ButtonBase;

            if (this.CloseButton != null)
            {
                if (this.HasCloseButton)
                {
                    this.CloseButton.Visibility = Visibility.Visible;
                }
                else
                {
                    this.CloseButton.Visibility = Visibility.Collapsed;
                }
            }

            if (this._closed != null)
            {
                this._closed.Completed -= new EventHandler(this.Closing_Completed);
            }

            if (this._opened != null)
            {
                this._opened.Completed -= new EventHandler(this.Opening_Completed);
            }

            this._root = GetTemplateChild(PART_Root) as FrameworkElement;
            this._resizer = GetTemplateChild(PART_Resizer) as FrameworkElement;

            if (this._root != null)
            {
                IList groups = VisualStateManager.GetVisualStateGroups(this._root);

                if (groups != null)
                {
                    IList states = null;

                    foreach (VisualStateGroup vsg in groups)
                    {
                        if (vsg.Name == FloatableWindow.VSMGROUP_Window)
                        {
                            states = vsg.States;
                            break;
                        }
                    }

                    if (states != null)
                    {
                        foreach (VisualState state in states)
                        {
                            if (state.Name == FloatableWindow.VSMSTATE_StateClosed)
                            {
                                this._closed = state.Storyboard;
                            }

                            if (state.Name == FloatableWindow.VSMSTATE_StateOpen)
                            {
                                this._opened = state.Storyboard;
                            }
                        }
                    }
                }
                //TODO: Figure out why I can't wire up the event below in SubscribeToTemplatePartEvents
                this._root.MouseLeftButtonDown += new MouseButtonEventHandler(this.ContentRoot_MouseLeftButtonDown);

                if (this.ResizeMode == ResizeMode.CanResize)
                {
                    this._resizer.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(Resizer_MouseLeftButtonDown);
                    this._resizer.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(Resizer_MouseLeftButtonUp);
                    this._resizer.MouseMove += new System.Windows.Input.MouseEventHandler(Resizer_MouseMove);
                    this._resizer.MouseEnter += new MouseEventHandler(Resizer_MouseEnter);
                    this._resizer.MouseLeave += new MouseEventHandler(Resizer_MouseLeave);
                }
                else
                {
                    this._resizer.Opacity = 0;
                }
            }

            this.ContentRoot = GetTemplateChild(PART_ContentRoot) as FrameworkElement;

            this._chrome = GetTemplateChild(PART_Chrome) as FrameworkElement;

            this.Overlay = GetTemplateChild(PART_Overlay) as Panel;

            this._contentPresenter = GetTemplateChild(PART_ContentPresenter) as FrameworkElement;

            this.SubscribeToTemplatePartEvents();
            this.SubscribeToStoryBoardEvents();
            this._desiredMargin = this.Margin;
            this.Margin = new Thickness(0);

            // Update overlay size
            if (this.IsOpen && (this.ChildWindowPopup != null))
            {
                this._desiredContentHeight = this.Height;
                this._desiredContentWidth = this.Width;
                this.UpdateOverlaySize();
                this.UpdateRenderTransform();
                this.ChangeVisualState();
            }
        }

        void Resizer_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!this._isMouseCaptured)
            {
                this._resizer.Opacity = .25;
            }
        }

        void Resizer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!this._isMouseCaptured)
            {
                this._resizer.Opacity = 1;
            }
        }

        /// <summary>
        /// Raises the
        /// <see cref="E:System.Windows.Controls.FloatableWindow.Closed" /> event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnClosed(EventArgs e)
        {
            EventHandler handler = this.Closed;

            if (null != handler)
            {
                handler(this, e);
            }

            this._isOpen = false;
            if (!_modal)
            {
                this.ParentLayoutRoot.Children.Remove(this);
            }
        }

        /// <summary>
        /// Raises the
        /// <see cref="E:System.Windows.Controls.FloatableWindow.Closing" /> event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected virtual void OnClosing(CancelEventArgs e)
        {
            EventHandler<CancelEventArgs> handler = this.Closing;

            if (null != handler)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Returns a
        /// <see cref="T:System.Windows.Automation.Peers.FloatableWindowAutomationPeer" />
        /// for use by the Silverlight automation infrastructure.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Windows.Automation.Peers.FloatableWindowAutomationPeer" />
        /// for the <see cref="T:System.Windows.Controls.FloatableWindow" /> object.
        /// </returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new FloatableWindowAutomationPeer(this);
        }

        /// <summary>
        /// This method is called every time a
        /// <see cref="T:System.Windows.Controls.FloatableWindow" /> is displayed.
        /// </summary>
        protected virtual void OnOpened()
        {
            this.UpdatePosition();
            this._isOpen = true;

            if (this.Overlay != null)
            {
                this.Overlay.Opacity = this.OverlayOpacity;
                this.Overlay.Background = this.OverlayBrush;
            }

            if (!this.Focus())
            {
                // If the Focus() fails it means there is no focusable element in the 
                // FloatableWindow. In this case we set IsTabStop to true to have the keyboard functionality
                this.IsTabStop = true;
                this.Focus();
            }
        }

        /// <summary>
        /// Executed when the opening storyboard finishes.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event args.</param>
        private void Opening_Completed(object sender, EventArgs e)
        {
            if (this._opened != null)
            {
                this._opened.Completed -= new EventHandler(this.Opening_Completed);
            }
            // AutomationPeer returns "ReadyForUserInteraction" when the FloatableWindow 
            // is open and all animations have been completed.
            this.InteractionState = WindowInteractionState.ReadyForUserInteraction;
            this.OnOpened();
        }

        /// <summary>
        /// Executed when the page resizes.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event args.</param>
        private void Page_Resized(object sender, EventArgs e)
        {
            if (this.ChildWindowPopup != null)
            {
                this.UpdateOverlaySize();
            }
        }

        /// <summary>
        /// Executed when the root visual gets focus.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Routed event args.</param>
        private void RootVisual_GotFocus(object sender, RoutedEventArgs e)
        {
            this.Focus();
            this.InteractionState = WindowInteractionState.ReadyForUserInteraction;
        }

        public void Show()
        {
            ShowWindow(false);
        }

        public void ShowDialog()
        {
            _verticalOffset = 0;
            _horizontalOffset = 0;
            ShowWindow(true);
        }

        public void Show(double horizontalOffset, double verticalOffset)
        {
            _horizontalOffset = horizontalOffset;
            _verticalOffset = verticalOffset;
            ShowWindow(false);
        }

        /// <summary>
        /// Opens a <see cref="T:System.Windows.Controls.FloatableWindow" /> and
        /// returns without waiting for the
        /// <see cref="T:System.Windows.Controls.FloatableWindow" /> to close.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">
        /// The child window is already in the visual tree.
        /// </exception>
        internal void ShowWindow(bool isModal)
        {
            _modal = isModal;

            // AutomationPeer returns "Running" when Show() is called
            // but the FloatableWindow is not ready for user interaction:
            this.InteractionState = WindowInteractionState.Running;

            this.SubscribeToEvents();
            this.SubscribeToTemplatePartEvents();
            this.SubscribeToStoryBoardEvents();


            // MaxHeight and MinHeight properties should not be overwritten:
            this.MaxHeight = double.PositiveInfinity;
            this.MaxWidth = double.PositiveInfinity;

            if (_modal)
            {
                if (this.ChildWindowPopup == null)
                {
                    this.ChildWindowPopup = new Popup();

                    try
                    {
                        this.ChildWindowPopup.Child = this;
                    }
                    catch (ArgumentException)
                    {
                        // If the FloatableWindow is already in the visualtree, we cannot set it to be the child of the popup
                        // we are throwing a friendlier exception for this case:
                        this.InteractionState = WindowInteractionState.NotResponding;
                        throw new InvalidOperationException(MessageResource.ChildWindow_InvalidOperation);
                    }
                }

                if (this.ChildWindowPopup != null && Application.Current.RootVisual != null)
                {
                    this.ChildWindowPopup.IsOpen = true;

                    this.ChildWindowPopup.HorizontalOffset = _horizontalOffset;
                    this.ChildWindowPopup.VerticalOffset = _verticalOffset;

                    // while the FloatableWindow is open, the DialogResult is always NULL:
                    this._dialogresult = null;
                }
            }
            else
            {
                if (ParentLayoutRoot != null)
                {
                    this.SetValue(Canvas.TopProperty, _verticalOffset);
                    this.SetValue(Canvas.LeftProperty, _horizontalOffset);
                    this.ParentLayoutRoot.Children.Add(this);
                    //this.BringToFront();
                }
                else
                {
                    throw new ArgumentNullException("ParentLayoutRoot", "You need to specify a root Panel element to add the window elements to.");
                }
            }

            // disable the underlying UI
            //if (RootVisual != null && _modal)
            //{
            //    RootVisual.IsEnabled = false;
            //}

            // if the template is already loaded, display loading visuals animation
            if (this.ContentRoot == null)
            {
                this.Loaded += (s, args) =>
                {
                    if (this.ContentRoot != null)
                    {
                        this.ChangeVisualState();
                    }
                };
            }
            else
            {
                this.ChangeVisualState();
            }
        }

        /// <summary>
        /// Subscribes to events when the FloatableWindow is opened.
        /// </summary>
        private void SubscribeToEvents()
        {
            if (Application.Current != null && Application.Current.Host != null && Application.Current.Host.Content != null)
            {
                Application.Current.Exit += new EventHandler(this.Application_Exit);
                Application.Current.Host.Content.Resized += new EventHandler(this.Page_Resized);
            }

            this.KeyDown += new KeyEventHandler(this.ChildWindow_KeyDown);
            if (_modal)
            {
                this.LostFocus += new RoutedEventHandler(this.ChildWindow_LostFocus);
            }
            this.SizeChanged += new SizeChangedEventHandler(this.ChildWindow_SizeChanged);
        }

        /// <summary>
        /// Subscribes to events that are on the storyboards. 
        /// Unsubscribing from these events happen in the event handlers individually.
        /// </summary>
        private void SubscribeToStoryBoardEvents()
        {
            if (this._closed != null)
            {
                this._closed.Completed += new EventHandler(this.Closing_Completed);
            }

            if (this._opened != null)
            {
                this._opened.Completed += new EventHandler(this.Opening_Completed);
            }
        }

        /// <summary>
        /// Subscribes to events on the template parts.
        /// </summary>
        private void SubscribeToTemplatePartEvents()
        {
            if (this.CloseButton != null)
            {
                this.CloseButton.Click += new RoutedEventHandler(this.CloseButton_Click);
            }

            if (this._chrome != null)
            {
                this._chrome.MouseLeftButtonDown += new MouseButtonEventHandler(this.Chrome_MouseLeftButtonDown);
                this._chrome.MouseLeftButtonUp += new MouseButtonEventHandler(this.Chrome_MouseLeftButtonUp);
                this._chrome.MouseMove += new MouseEventHandler(this.Chrome_MouseMove);
            }

            if (this._contentPresenter != null)
            {
                this._contentPresenter.SizeChanged += new SizeChangedEventHandler(this.ContentPresenter_SizeChanged);
            }
        }

        /// <summary>
        /// Unsubscribe from events when the FloatableWindow is closed.
        /// </summary>
        private void UnSubscribeFromEvents()
        {
            if (Application.Current != null && Application.Current.Host != null && Application.Current.Host.Content != null)
            {
                Application.Current.Exit -= new EventHandler(this.Application_Exit);
                Application.Current.Host.Content.Resized -= new EventHandler(this.Page_Resized);
            }

            this.KeyDown -= new KeyEventHandler(this.ChildWindow_KeyDown);
            if (_modal)
            {
                this.LostFocus -= new RoutedEventHandler(this.ChildWindow_LostFocus);
            }
            this.SizeChanged -= new SizeChangedEventHandler(this.ChildWindow_SizeChanged);
        }

        /// <summary>
        /// Unsubscribe from the events that are subscribed on the template part elements.
        /// </summary>
        private void UnsubscribeFromTemplatePartEvents()
        {
            if (this.CloseButton != null)
            {
                this.CloseButton.Click -= new RoutedEventHandler(this.CloseButton_Click);
            }

            if (this._chrome != null)
            {
                this._chrome.MouseLeftButtonDown -= new MouseButtonEventHandler(this.Chrome_MouseLeftButtonDown);
                this._chrome.MouseLeftButtonUp -= new MouseButtonEventHandler(this.Chrome_MouseLeftButtonUp);
                this._chrome.MouseMove -= new MouseEventHandler(this.Chrome_MouseMove);
            }

            if (this._contentPresenter != null)
            {
                this._contentPresenter.SizeChanged -= new SizeChangedEventHandler(this.ContentPresenter_SizeChanged);
            }
        }

        /// <summary>
        /// Updates the size of the overlay of the window.
        /// </summary>
        private void UpdateOverlaySize()
        {
            if (_modal)
            {
                if (this.Overlay != null && Application.Current != null && Application.Current.Host != null && Application.Current.Host.Content != null)
                {
                    this.Height = Application.Current.Host.Content.ActualHeight;
                    this.Width = Application.Current.Host.Content.ActualWidth;
                    this.Overlay.Height = this.Height;
                    this.Overlay.Width = this.Width;

                    if (this.ContentRoot != null)
                    {
                        this.ContentRoot.Width = this._desiredContentWidth;
                        this.ContentRoot.Height = this._desiredContentHeight;
                        this.ContentRoot.Margin = this._desiredMargin;
                    }
                }
            }
            else
            {
                if (this.Overlay != null)
                {
                    this.Overlay.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Updates the position of the window in case the size of the content changes.
        /// This allows FloatableWindow only scale from right and bottom.
        /// </summary>
        private void UpdatePosition()
        {
            if (this.ContentRoot != null && Application.Current != null && Application.Current.RootVisual != null)
            {
                GeneralTransform gt = this.ContentRoot.TransformToVisual(Application.Current.RootVisual);

                if (gt != null)
                {
                    this._windowPosition = gt.Transform(new Point(0, 0));
                }
            }
        }

        /// <summary>
        /// Updates the render transform applied on the overlay.
        /// </summary>
        private void UpdateRenderTransform()
        {
            if (this._root != null && this.ContentRoot != null)
            {
                // The Overlay part should not be affected by the render transform applied on the
                // FloatableWindow. In order to achieve this, we adjust an identity matrix to represent
                // the _root's transformation, invert it, apply the inverted matrix on the _root, so that 
                // nothing is affected by the rendertransform, and apply the original transform only on the Content
                GeneralTransform gt = this._root.TransformToVisual(null);
                if (gt != null)
                {
                    Point p10 = new Point(1, 0);
                    Point p01 = new Point(0, 1);
                    Point transform10 = gt.Transform(p10);
                    Point transform01 = gt.Transform(p01);

                    Matrix transformToRootMatrix = Matrix.Identity;
                    transformToRootMatrix.M11 = transform10.X;
                    transformToRootMatrix.M12 = transform10.Y;
                    transformToRootMatrix.M21 = transform01.X;
                    transformToRootMatrix.M22 = transform01.Y;

                    MatrixTransform original = new MatrixTransform();
                    original.Matrix = transformToRootMatrix;

                    InvertMatrix(ref transformToRootMatrix);
                    MatrixTransform mt = new MatrixTransform();
                    mt.Matrix = transformToRootMatrix;

                    TransformGroup tg = this._root.RenderTransform as TransformGroup;

                    if (tg != null)
                    {
                        tg.Children.Add(mt);
                    }
                    else
                    {
                        this._root.RenderTransform = mt;
                    }

                    tg = this.ContentRoot.RenderTransform as TransformGroup;

                    if (tg != null)
                    {
                        tg.Children.Add(original);
                    }
                    else
                    {
                        this.ContentRoot.RenderTransform = original;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the ContentRootTranslateTransform.
        /// </summary>
        /// <param name="X">X coordinate of the transform.</param>
        /// <param name="Y">Y coordinate of the transform.</param>
        private void UpdateContentRootTransform(double X, double Y)
        {
            if (this._contentRootTransform == null)
            {
                this._contentRootTransform = new TranslateTransform();
                this._contentRootTransform.X = X;
                this._contentRootTransform.Y = Y;

                TransformGroup transformGroup = this.ContentRoot.RenderTransform as TransformGroup;

                if (transformGroup == null)
                {
                    transformGroup = new TransformGroup();
                    transformGroup.Children.Add(this.ContentRoot.RenderTransform);
                }
                transformGroup.Children.Add(this._contentRootTransform);
                this.ContentRoot.RenderTransform = transformGroup;
            }
            else
            {
                this._contentRootTransform.X += X;
                this._contentRootTransform.Y += Y;
            }
        }

        private void Resizer_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._resizer.CaptureMouse();
            this._isMouseCaptured = true;
            this._clickPoint = e.GetPosition(sender as UIElement);

        }

        private void Resizer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._resizer.ReleaseMouseCapture();
            this._isMouseCaptured = false;
            this._resizer.Opacity = 0.25;
        }

        private void Resizer_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this._isMouseCaptured && this.ContentRoot != null)
            {
                // If the child window is dragged out of the page, return
                if (Application.Current != null && Application.Current.RootVisual != null &&
                    (e.GetPosition(Application.Current.RootVisual).X < 0 || e.GetPosition(Application.Current.RootVisual).Y < 0))
                {
                    return;
                }

                Point p = e.GetPosition(this.ContentRoot);

                if ((p.X > this._clickPoint.X) && (p.Y > this._clickPoint.Y))
                {
                    this.Width = (double)(p.X - (12 - this._clickPoint.X));
                    this.Height = (double)(p.Y - (12 - this._clickPoint.Y));
                }
            }
        }

        private Storyboard GetVisualStateStoryboard(string visualStateGroupName, string visualStateName)
        {
            foreach (VisualStateGroup g in VisualStateManager.GetVisualStateGroups((FrameworkElement)this.ContentRoot.Parent))
            {
                if (g.Name != visualStateGroupName) continue;
                foreach (VisualState s in g.States)
                {
                    if (s.Name != visualStateName) continue;
                    return s.Storyboard;
                }
            }
            return null;
        }

        #endregion Methods
    }
}