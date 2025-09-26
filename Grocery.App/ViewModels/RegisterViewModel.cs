using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.Core.Exceptions;
using Grocery.App.Views;



namespace Grocery.App.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly GlobalViewModel _global;

        [ObservableProperty]
        private string email = "user3@mail.com";

        [ObservableProperty]
        private string password = "Useruser4";

        [ObservableProperty]
        private string name = "Bob Bladerdeeg";

        [ObservableProperty]
        private string errorMessage = "";

        public RegisterViewModel(IAuthService authService, GlobalViewModel global)
        {
            _authService = authService;
            _global = global;
        }

        [RelayCommand]
        private void Register()
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Email))
                errorMessage += "Voer een geldig emailadres in. ";

            if (string.IsNullOrWhiteSpace(Name))
                errorMessage += "Naam is een verplicht veld. ";

            if (string.IsNullOrWhiteSpace(Password))
                errorMessage += "Wachtwoord moet een waarde hebben. ";

            if (errorMessage != "")
                return;

            try
            {
                Client client = _authService.Register(Email, password, Name);

                _global.Client = client;
                Application.Current.MainPage = new AppShell();
            }
            catch (UsedEmailException _)
            {
                errorMessage = "Dit emailadres is ongelding of wordt al gebruikt.";
            }
            catch (InvalidEmailException _)
            {
                errorMessage = "Dit emailadres is ongelding of wordt al gebruikt.";
            }
            catch (InvalidPasswordException _)
            {
                errorMessage = "Wachtwoord voldoet niet aan de minimum eisen. Wachtwoord moet minimaal 8 tekens bevatten, waaronder een hoofdletter, een cijfer en een speciaal teken.";
            }
            catch (Exception)
            {
                errorMessage = "Er is misgegaan bij het registreren van uw account, probeer het later nog eens.";
            }
        }
    }
}