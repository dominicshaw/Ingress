using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Ingress.WPF.Views.Controls
{
    public enum MessageType
    {
        Success,
        Error,
        Notice
    }

    public class FlashMessage : Control
    {
        private readonly DispatcherTimer _fadeOutTimer;
        
        //private static readonly ResourceDictionary _resource;

        static FlashMessage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlashMessage), new FrameworkPropertyMetadata(typeof(FlashMessage)));

            //_resource = new ResourceDictionary
            //{
            //    Source = new Uri("/Ingress.WPF;component/Styles/FlashMessageStyles.xaml",
            //        UriKind.RelativeOrAbsolute)
            //};
        }

        public FlashMessage()
        {
            _fadeOutTimer = new DispatcherTimer { Interval = FadeOutTime.TimeSpan };
            _fadeOutTimer.Tick += FadeOutTimerTick;

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, CloseCommandHandler));
        }

        #region ContentDataTemplate
        public static readonly DependencyProperty ContentDataTemplateProperty =
            DependencyProperty.Register(nameof(ContentDataTemplate), typeof(DataTemplate), typeof(FlashMessage));

        public DataTemplate ContentDataTemplate
        {
            get { return (DataTemplate)GetValue(ContentDataTemplateProperty); }
            set { SetValue(ContentDataTemplateProperty, value); }
        }
        #endregion

        #region FadesOutAutomatically
        public static readonly DependencyProperty FadesOutAutomaticallyProperty =
            DependencyProperty.Register(nameof(FadesOutAutomatically), typeof(bool), typeof(FlashMessage), new PropertyMetadata(true));

        public bool FadesOutAutomatically
        {
            get { return (bool)GetValue(FadesOutAutomaticallyProperty); }
            set { SetValue(FadesOutAutomaticallyProperty, value); }
        }
        #endregion

        #region FadeOutTime
        public static readonly DependencyProperty FadeOutTimeProperty =
            DependencyProperty.Register(nameof(FadeOutTime), typeof(Duration), typeof(FlashMessage),
                                        new PropertyMetadata(new Duration(TimeSpan.FromSeconds(10)), OnFadeOutTimeChanged));

        public Duration FadeOutTime
        {
            get { return (Duration)GetValue(FadeOutTimeProperty); }
            set { SetValue(FadeOutTimeProperty, value); }
        }

        private static void OnFadeOutTimeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FlashMessage)o).OnFadeOutTimeChanged();
        }

        private void OnFadeOutTimeChanged()
        {
            _fadeOutTimer.Interval = FadeOutTime.TimeSpan;
            StopTimerIfRunning();
        }
        #endregion

        #region Message
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(nameof(Message), typeof(string), typeof(FlashMessage), new PropertyMetadata(OnMessageChanged));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        private static void OnMessageChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FlashMessage)o).OnMessageChanged();
        }

        private void OnMessageChanged()
        {
            SetCurrentValue(IsFlashMessageVisibleProperty, !string.IsNullOrEmpty(Message));
        }
        #endregion

        #region MessageType
        public static readonly DependencyProperty MessageTypeProperty =
            DependencyProperty.Register(nameof(MessageType), typeof(MessageType), typeof(FlashMessage));

        public MessageType MessageType
        {
            get { return (MessageType)GetValue(MessageTypeProperty); }
            set { SetValue(MessageTypeProperty, value); }
        }
        #endregion

        #region Reset
        internal static readonly DependencyProperty ResetProperty =
            DependencyProperty.Register(nameof(Reset), typeof(bool), typeof(FlashMessage), new PropertyMetadata(OnResetChanged));

        internal bool Reset
        {
            get { return (bool)GetValue(ResetProperty); }
            set { SetValue(ResetProperty, value); }
        }

        private static void OnResetChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FlashMessage)o).OnResetChanged();
        }

        private void OnResetChanged()
        {
            if (Reset)
            {
                SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
                SetCurrentValue(MessageProperty, null);
            }
        }
        #endregion

        #region IsFlashMessageVisible
        internal static readonly DependencyProperty IsFlashMessageVisibleProperty =
            DependencyProperty.Register(nameof(IsFlashMessageVisible), typeof(bool), typeof(FlashMessage), new PropertyMetadata(OnIsFlashMessageVisibleChanged));
        
        internal bool IsFlashMessageVisible
        {
            get { return (bool)GetValue(IsFlashMessageVisibleProperty); }
            set { SetValue(IsFlashMessageVisibleProperty, value); }
        }

        private static void OnIsFlashMessageVisibleChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FlashMessage)o).OnIsFlashMessageVisibleChanged();
        }

        private void OnIsFlashMessageVisibleChanged()
        {
            if (IsFlashMessageVisible)
            {
                SetCurrentValue(VisibilityProperty, Visibility.Visible);
                StartTimerIfAutomatically();
            }
            else
                StartFadeOutAnimation();
        }
        #endregion

        public void Hide()
        {
            StopTimerIfRunning();
            SetCurrentValue(IsFlashMessageVisibleProperty, false);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (ContentDataTemplate == null)
            {
                //SetCurrentValue(ContentDataTemplateProperty, (DataTemplate) _resource["FlashMessageTemplate"]);
                SetCurrentValue(ContentDataTemplateProperty, (DataTemplate) FindResource("FlashMessageTemplate"));
            }
        }

        private void FadeOutTimerTick(object sender, EventArgs e)
        {
            Hide();
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Hide();
        }

        private void StopTimerIfRunning()
        {
            if (_fadeOutTimer.IsEnabled)
                _fadeOutTimer.Stop();
        }

        private void StartTimerIfAutomatically()
        {
            StopTimerIfRunning();
            if (FadesOutAutomatically)
                _fadeOutTimer.Start();
        }

        private void StartFadeOutAnimation()
        {
            var storyBoard = (Storyboard) FindResource("FadeOutAnimation");
            storyBoard?.Begin(this);
        }
    }
}
