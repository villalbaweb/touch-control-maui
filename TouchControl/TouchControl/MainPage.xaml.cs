namespace TouchControl;

public partial class MainPage : ContentPage
{
    double tempx = 0;
    double tempy = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void AddLabel_Clicked(object sender, EventArgs e)
    {
        var label = new Label()
        {
            Text = "This is a label",
            BackgroundColor = Colors.LightGray,
            Padding = 10
        };

        var panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += PanGestureRecognizer_PanUpdated;

        label.GestureRecognizers.Add(panGesture);

        ParentLayout.Children.Add(label);
    }

    private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
    {
        var label = sender as Label;
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
                {
                    tempx = label.TranslationX;
                    tempy = label.TranslationY;
                }

                break;

            case GestureStatus.Running:
                if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
                {
                    label.TranslationX = e.TotalX + tempx;
                    label.TranslationY = e.TotalY + tempy;
                }
                else if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                {
                    label.TranslationX += e.TotalX;
                    label.TranslationY += e.TotalY;
                }

                break;
            case GestureStatus.Completed:
                tempx = label.TranslationX;
                tempy = label.TranslationY;

                break;
        }
    }

}
