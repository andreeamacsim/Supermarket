using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Supermarket_Application.DataAccess;
using Supermarket_Application.Models;
using Supermarket_Application.Views;

namespace Supermarket_Application.ViewModels
{
    public class CashierReportViewModel : INotifyPropertyChanged
    {
        private SupermarketDbContext _context = new SupermarketDbContext();

        public ObservableCollection<User> Cashiers { get; set; }
        public ObservableCollection<int> Months { get; set; }
        public ObservableCollection<int> Years { get; set; }
        public ObservableCollection<DailyTotal> DailyTotals { get; set; }

        private User _selectedCashier;
        public User SelectedCashier
        {
            get => _selectedCashier;
            set
            {
                if (_selectedCashier != value)
                {
                    _selectedCashier = value;
                    OnPropertyChanged(nameof(SelectedCashier));
                    LoadData();
                }
            }
        }

        private int _selectedMonth; // Keep as int
        public int SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                if (_selectedMonth != value)
                {
                    _selectedMonth = value;
                    OnPropertyChanged(nameof(SelectedMonth));
                    LoadData();
                }
            }
        }

        private int _selectedYear; // Keep as int
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                    LoadData();
                }
            }
        }
        public ICommand ShowLargestReceiptCommand { get; private set; }
        public CashierReportViewModel()
        {
            Cashiers = new ObservableCollection<User>(_context.Users.Where(u => u.UserType == "Cashier").ToList());
            InitializeMonthsAndYears();
            DailyTotals = new ObservableCollection<DailyTotal>();
            ShowLargestReceiptCommand = new RelayCommand(ExecuteShowLargestReceipt);
        }

        private void ExecuteShowLargestReceipt(object parameter)
        {
             LargestReceiptView receiptWindow = new LargestReceiptView();
              receiptWindow.ShowDialog(); 
        }
        private void InitializeMonthsAndYears()
        {
            Months = new ObservableCollection<int>(Enumerable.Range(1, 12));
            Years = new ObservableCollection<int>(Enumerable.Range(DateTime.Now.Year - 10, 11));
        }

        private void LoadData()
        {
            if (SelectedCashier == null || SelectedMonth == 0 || SelectedYear == 0)
                return;

            DateTime startDate = new DateTime(SelectedYear, SelectedMonth, 1);
            DateTime endDate = startDate.AddMonths(1);

            var receipts = _context.Receipts
                                   .Where(r => r.CashierID == SelectedCashier.UserID &&
                                               r.DateIssued >= startDate &&
                                               r.DateIssued < endDate)
                                   .ToList();

            var dailyTotals = receipts.GroupBy(r => r.DateIssued.Date)
                                      .Select(g => new DailyTotal
                                      {
                                          Day = g.Key.Day,
                                          Total = g.Sum(x => x.TotalAmount)
                                      })
                                      .OrderBy(d => d.Day);

            DailyTotals.Clear();
            foreach (var total in dailyTotals)
            {
                DailyTotals.Add(total);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DailyTotal
    {
        public int Day { get; set; }
        public decimal Total { get; set; }
    }
}
