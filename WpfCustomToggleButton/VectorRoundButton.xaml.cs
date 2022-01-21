using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfCustomToggleButton;

public partial class VectorRoundButton
{
    public bool IsCheckable
    {
        get => (bool)GetValue(IsCheckableProperty);
        set => SetValue(IsCheckableProperty, value);
    }

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    public Brush ActiveButtonColor
    {
        get => (Brush)GetValue(ActiveButtonColorProperty);
        set => SetValue(ActiveButtonColorProperty, value);
    }

    public Brush InactiveButtonColor
    {
        get => (Brush)GetValue(InactiveButtonColorProperty);
        set => SetValue(InactiveButtonColorProperty, value);
    }

    public Path ButtonIcon
    {
        get => (Path)GetValue(ButtonIconProperty);
        set => SetValue(ButtonIconProperty, value);
    }

    public static readonly DependencyProperty IsCheckableProperty = DependencyProperty.Register(
        "IsCheckable",
        typeof(bool),
        typeof(VectorRoundButton),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, IsCheckablePropertChanged));

    public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
        "IsChecked",
        typeof(bool),
        typeof(VectorRoundButton),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, IsCkeckedPropertChanged));

    public static readonly DependencyProperty InactiveButtonColorProperty = DependencyProperty.Register(
        "InactiveButtonColor",
        typeof(Brush),
        typeof(VectorRoundButton),
        new FrameworkPropertyMetadata(System.Windows.SystemColors.ControlBrush, FrameworkPropertyMetadataOptions.AffectsRender, InactiveButtonColorPropertyChanged));

    public static readonly DependencyProperty ActiveButtonColorProperty = DependencyProperty.Register(
        "ActiveButtonColor",
        typeof(Brush),
        typeof(VectorRoundButton),
        new FrameworkPropertyMetadata(System.Windows.SystemColors.ControlDarkBrush, FrameworkPropertyMetadataOptions.AffectsRender, ActiveButtonColorPropertyChanged));

    public static readonly DependencyProperty ButtonIconProperty = DependencyProperty.Register(
        "ButtonIcon",
        typeof(Path),
        typeof(VectorRoundButton),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, ButtonIconPropertyChanged));

    public VectorRoundButton()
    {
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        ButtonImage.Fill = ButtonIcon?.Fill;
        ButtonImage.Data = ButtonIcon?.Data;
        ButtonEllipse.Fill = InactiveButtonColor;
    }

    private static void ButtonIconPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
    {
        if (source is VectorRoundButton)
        {
            VectorRoundButton control = source as VectorRoundButton;
            control.ButtonIcon.Data = (e.NewValue as Path)?.Data;
            control.ButtonIcon.Fill = (e.NewValue as Path)?.Fill;
            control.ButtonImage.Data = control.ButtonIcon.Data;
            control.ButtonImage.Fill = control.ButtonIcon.Fill;
        }
    }

    private static void ActiveButtonColorPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
    {
        if (source is VectorRoundButton)
        {
            VectorRoundButton control = source as VectorRoundButton;
            control.ActiveButtonColor = (Brush)e.NewValue;
        }
    }

    private static void InactiveButtonColorPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
    {
        if (source is VectorRoundButton)
        {
            VectorRoundButton control = source as VectorRoundButton;
            control.InactiveButtonColor = (Brush)e.NewValue;
            control.ButtonEllipse.Fill = (Brush)e.NewValue;
        }
    }

    private static void IsCkeckedPropertChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
    {
        if (source is VectorRoundButton)
        {
            VectorRoundButton control = source as VectorRoundButton;
            if (control.IsCheckable)
            {
                control.IsChecked = (bool)e.NewValue;
                if (control.IsChecked)
                {
                    control.ButtonEllipse.Stroke = System.Windows.SystemColors.ControlDarkBrush;
                    control.ButtonEllipse.StrokeThickness = 2;
                }
                else
                {
                    control.ButtonEllipse.Stroke = null;
                    control.ButtonEllipse.StrokeThickness = 1;
                }
            }
        }
    }

    private static void IsCheckablePropertChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
    {
        if (source is VectorRoundButton)
        {
            VectorRoundButton control = source as VectorRoundButton;
            control.IsCheckable = (bool)e.NewValue;
        }
    }

    private void UserControl_MouseEnter(object sender, MouseEventArgs e)
    {
        ButtonEllipse.Fill = ActiveButtonColor;
    }

    private void UserControl_MouseLeave(object sender, MouseEventArgs e)
    {
        ButtonEllipse.Fill = InactiveButtonColor;
        if (!IsChecked)
            ButtonEllipse.Stroke = null;
    }

    private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ButtonEllipse.Stroke = System.Windows.SystemColors.ActiveCaptionBrush;
    }

    private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        ButtonEllipse.Fill = ActiveButtonColor;
        ButtonEllipse.Stroke = null;
        if (IsCheckable)
        {
            IsChecked = !IsChecked;
        }
    }
}