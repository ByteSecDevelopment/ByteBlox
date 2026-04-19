using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Bloxstrap.Models;

namespace Bloxstrap.UI.ViewModels.Settings
{
    public class AccountsViewModel : NotifyPropertyChangedViewModel
    {
        public ICommand AddAccountCommand => new RelayCommand(AddAccount);
        public ICommand DeleteAccountCommand => new RelayCommand(DeleteAccount);

        private void AddAccount()
        {
            RobloxAccounts.Add(new RobloxAccount()
            {
                Name = "New Account"
            });

            SelectedAccountIndex = RobloxAccounts.Count - 1;

            OnPropertyChanged(nameof(SelectedAccountIndex));
            OnPropertyChanged(nameof(IsAccountSelected));
        }

        private void DeleteAccount()
        {
            if (SelectedAccount is null)
                return;

            RobloxAccounts.Remove(SelectedAccount);

            if (RobloxAccounts.Count > 0)
            {
                SelectedAccountIndex = RobloxAccounts.Count - 1;
                OnPropertyChanged(nameof(SelectedAccountIndex));
            }

            OnPropertyChanged(nameof(IsAccountSelected));
        }

        public ObservableCollection<RobloxAccount> RobloxAccounts
        {
            get => App.Settings.Prop.RobloxAccounts;
            set => App.Settings.Prop.RobloxAccounts = value;
        }

        public RobloxAccount? SelectedAccount { get; set; }
        public int SelectedAccountIndex { get; set; }
        public bool IsAccountSelected => SelectedAccount is not null;

        public void SetSelectedAccount(RobloxAccount account)
        {
            foreach (var acc in RobloxAccounts)
                acc.IsSelected = (acc == account);
            
            OnPropertyChanged(nameof(RobloxAccounts));
            OnPropertyChanged(nameof(IsAccountSelected));
            OnPropertyChanged(nameof(SelectedAccount));
        }
    }
}
