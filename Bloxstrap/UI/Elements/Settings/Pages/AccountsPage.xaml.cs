using System.Windows;
using System.Windows.Controls;
using Bloxstrap.UI.ViewModels.Settings;
using Bloxstrap.Models;

namespace Bloxstrap.UI.Elements.Settings.Pages
{
    public partial class AccountsPage
    {
        public AccountsPage()
        {
            InitializeComponent();
        }

        private void AccountSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is AccountsViewModel viewModel)
            {
                viewModel.SelectedAccount = (RobloxAccount)AccountsListBox.SelectedItem;
                viewModel.OnPropertyChanged(nameof(viewModel.IsAccountSelected));
                viewModel.OnPropertyChanged(nameof(viewModel.SelectedAccount));
            }
        }

        private void UseAccount_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AccountsViewModel viewModel && viewModel.SelectedAccount != null)
            {
                viewModel.SetSelectedAccount(viewModel.SelectedAccount);
                // In a real scenario, you'd also want to write this cookie to Roblox's cookie file
                // or ensure it's used during the next launch.
                Frontend.ShowMessageBox($"Account '{viewModel.SelectedAccount.Name}' selected for next launch.", MessageBoxImage.Information);
            }
        }
    }
}
