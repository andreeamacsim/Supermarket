using Supermarket_Application.DataAccess;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace Supermarket_Application.ViewModels
{
    public class LargestReceiptViewModel : INotifyPropertyChanged
    {
        private SupermarketDbContext _context = new SupermarketDbContext();
        private DateTime _selectedDate;
        private string _receiptDetails;

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                    LoadLargestReceipt();
                }
            }
        }

        public string ReceiptDetails
        {
            get => _receiptDetails;
            set
            {
                _receiptDetails = value;
                OnPropertyChanged(nameof(ReceiptDetails));
            }
        }

        public ICommand LoadLargestReceiptCommand { get; }

        public LargestReceiptViewModel()
        {
            LoadLargestReceiptCommand = new RelayCommand(param => LoadLargestReceipt());
        }

        private void LoadLargestReceipt()
        {
            var targetDate = SelectedDate.Date;

            var receipts = _context.Receipts
                .Where(r => DbFunctions.TruncateTime(r.DateIssued) == targetDate && r.IsActive)
                .OrderByDescending(r => r.TotalAmount)
                .FirstOrDefault();

            if (receipts != null)
            {
                ReceiptDetails = $"ReceiptID: {receipts.ReceiptID}\n" +
                                 $"DateIssued: {receipts.DateIssued}\n" +
                                 $"CashierID: {receipts.CashierID}\n" +
                                 $"TotalAmount: {receipts.TotalAmount:C}";
            }
            else
            {
                ReceiptDetails = "No receipt found for this date.";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
