using Plugin.Maui.Biometric;
namespace MauiFinger;

public partial class FingerPrint : ContentPage
{
    int count = 0;
    // Constructor
    public FingerPrint()
    {
        InitializeComponent();
    }
    // Constructor with IBiometric parameter
    public FingerPrint(IBiometric biometric)
	{
		InitializeComponent();
        biometric.GetAuthenticationStatusAsync();
        BiometricAuthenticationService.Default.GetAuthenticationStatusAsync();
    }

    private async void OnAuthenticateClicked(object sender, EventArgs e)
    {
        var result = await BiometricAuthenticationService.Default.AuthenticateAsync(

          new AuthenticationRequest()
          {
              Title = "Biometric Request",
              NegativeText = "Cancel authentication"
          },
              CancellationToken.None
          );

        //// The following code block goes here
        ///

        if (result.Status == BiometricResponseStatus.Success)
        {
            count++;
            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        else
        {
            await DisplayAlert("Error", "Authentication failed.", "Ok");
        }

    }

}


