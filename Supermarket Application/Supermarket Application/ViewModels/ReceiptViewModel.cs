using Supermarket_Application.DataAccess;
using Supermarket_Application.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Data.Entity;

namespace Supermarket_Application.ViewModels
{
    public class ReceiptViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Receipt> _receipts;

        public ObservableCollection<Receipt> Receipts
        {
            get => _receipts;
            set
            {
                _receipts = value;
                OnPropertyChanged();
            }
        }

        public ReceiptViewModel()
        {
            LoadReceipts();
        }

        private void LoadReceipts()
        {
            using (var context = new SupermarketDbContext())
            {
                var receipts = context.Receipts
                                      .Include(r => r.ReceiptDetails) 
                                      .ToList();
                Receipts = new ObservableCollection<Receipt>(receipts);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
