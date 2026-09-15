using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfVisualValidationDemo;

public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private void CompareBaseline_Click(object sender, RoutedEventArgs e)
    {
        ShowFeedback("Baseline comparison requested — this is where CI would submit the current state to visual validation.", "#E8F4F8", "#005B75");
    }

    private void ActionButton_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        ShowFeedback($"{button.Tag} — click behavior executed successfully.", "#E7F6EF", "#087A4A");
    }

    private void PriorityPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!IsLoaded || PriorityPicker.SelectedItem is not ComboBoxItem item) return;
        ShowFeedback($"Priority changed to: {item.Content}.", "#E8F4F8", "#005B75");
    }

    private void ClinicalNote_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsLoaded) return;
        ShowFeedback($"Clinical note updated ({ClinicalNote.Text.Length} characters).", "#F9F5FF", "#6941C6");
    }

    private void ConfirmationCheck_Changed(object sender, RoutedEventArgs e)
    {
        if (!IsLoaded) return;
        ShowFeedback(ConfirmationCheck.IsChecked == true ? "Review confirmation selected." : "Review confirmation cleared.", "#E8F4F8", "#005B75");
    }

    private void AlertToggle_Changed(object sender, RoutedEventArgs e)
    {
        if (!IsLoaded) return;
        ShowFeedback(AlertToggle.IsChecked == true ? "Alert notifications enabled." : "Alert notifications disabled.", "#FFF4DE", "#A65E00");
    }

    private void ScenarioPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!IsLoaded) return;
        switch (ScenarioPicker.SelectedIndex)
        {
            case 1:
                ScenarioText.Text = "Needs attention — elevated observation detected";
                ScenarioText.Foreground = new SolidColorBrush(Color.FromRgb(180, 35, 24));
                ScenarioAction.Content = "Escalate";
                ScenarioAction.Background = new SolidColorBrush(Color.FromRgb(201, 65, 53));
                ScenarioAction.IsEnabled = true;
                ShowFeedback("Attention scenario loaded — alert action is available.", "#FFF1F0", "#B42318");
                break;
            case 2:
                ScenarioText.Text = "Data unavailable — action is disabled";
                ScenarioText.Foreground = new SolidColorBrush(Color.FromRgb(71, 84, 103));
                ScenarioAction.Content = "Review patient";
                ScenarioAction.IsEnabled = false;
                ShowFeedback("Unavailable scenario loaded — primary action is disabled.", "#F2F4F7", "#475467");
                break;
            default:
                ScenarioText.Text = "Stable patient — routine review ready";
                ScenarioText.Foreground = new SolidColorBrush(Color.FromRgb(19, 138, 91));
                ScenarioAction.Content = "Review patient";
                ScenarioAction.Background = (Brush)FindResource("BlueBrush");
                ScenarioAction.IsEnabled = true;
                ShowFeedback("Stable scenario loaded — routine action is ready.", "#E7F6EF", "#087A4A");
                break;
        }
    }

    private void ScenarioAction_Click(object sender, RoutedEventArgs e)
    {
        ShowFeedback($"{ScenarioAction.Content} action completed for the selected patient scenario.", "#E7F6EF", "#087A4A");
    }

    private void ShowFeedback(string message, string background, string foreground)
    {
        FeedbackText.Text = message;
        FeedbackText.Foreground = (Brush)new BrushConverter().ConvertFromString(foreground)!;
        FeedbackPanel.Background = (Brush)new BrushConverter().ConvertFromString(background)!;
    }
}
