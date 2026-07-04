using System;
using System.Collections.Generic;

namespace BankingMVCApp
{
    // ========== MODEL ==========
    public class Account
    {
        public int AccountNumber { get; private set; }
        public string HolderName { get; set; }
        public decimal Balance { get; private set; }

        private static int _nextAccountNumber = 1001;

        public Account(string holderName, decimal initialBalance = 0)
        {
            AccountNumber = _nextAccountNumber++;
            HolderName = holderName;
            Balance = initialBalance >= 0 ? initialBalance : 0;
        }

        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
                return false;
            Balance += amount;
            return true;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0 || amount > Balance)
                return false;
            Balance -= amount;
            return true;
        }
    }

    // ========== VIEW ==========
    public class BankView
    {
        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("===== BANKING APPLICATION =====");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Show Balance");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");
        }

        public string GetInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public decimal GetDecimalInput(string prompt)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out decimal value))
                return value;
            return -1;
        }

        public int GetIntInput(string prompt)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value))
                return value;
            return -1;
        }

        public void ShowMessage(string message, bool isError = false)
        {
            if (isError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {message}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }

        public void ShowAccountDetails(Account account)
        {
            Console.WriteLine($"Account #{account.AccountNumber} | Holder: {account.HolderName} | Balance: {account.Balance:C}");
        }

        public void WaitForUser()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }

    // ========== CONTROLLER ==========
    public class BankController
    {
        private readonly BankView _view;
        private readonly Dictionary<int, Account> _accounts = new Dictionary<int, Account>();

        public BankController(BankView view)
        {
            _view = view;
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                _view.ShowMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": CreateAccount(); break;
                    case "2": Deposit(); break;
                    case "3": Withdraw(); break;
                    case "4": ShowBalance(); break;
                    case "5": exit = true; break;
                    default: _view.ShowMessage("Invalid option. Please try again.", true); break;
                }
                if (!exit)
                    _view.WaitForUser();
            }
        }

        private void CreateAccount()
        {
            string name = _view.GetInput("Enter holder name: ");
            if (string.IsNullOrWhiteSpace(name))
            {
                _view.ShowMessage("Name cannot be empty.", true);
                return;
            }
            decimal initial = _view.GetDecimalInput("Enter initial deposit (0 if none): ");
            if (initial < 0)
            {
                _view.ShowMessage("Invalid amount.", true);
                return;
            }
            var account = new Account(name, initial);
            _accounts[account.AccountNumber] = account;
            _view.ShowMessage($"Account created successfully! Account Number: {account.AccountNumber}");
        }

        private Account GetAccountByNumber()
        {
            int number = _view.GetIntInput("Enter account number: ");
            if (number == -1)
            {
                _view.ShowMessage("Invalid account number.", true);
                return null;
            }
            if (!_accounts.TryGetValue(number, out Account account))
            {
                _view.ShowMessage("Account not found.", true);
                return null;
            }
            return account;
        }

        private void Deposit()
        {
            Account account = GetAccountByNumber();
            if (account == null) return;

            decimal amount = _view.GetDecimalInput("Enter amount to deposit: ");
            if (amount <= 0)
            {
                _view.ShowMessage("Amount must be positive.", true);
                return;
            }

            if (account.Deposit(amount))
                _view.ShowMessage($"Deposited {amount:C}. New balance: {account.Balance:C}");
            else
                _view.ShowMessage("Deposit failed.", true);
        }

        private void Withdraw()
        {
            Account account = GetAccountByNumber();
            if (account == null) return;

            decimal amount = _view.GetDecimalInput("Enter amount to withdraw: ");
            if (amount <= 0)
            {
                _view.ShowMessage("Amount must be positive.", true);
                return;
            }

            if (account.Withdraw(amount))
                _view.ShowMessage($"Withdrew {amount:C}. New balance: {account.Balance:C}");
            else
                _view.ShowMessage("Insufficient balance or invalid amount.", true);
        }

        private void ShowBalance()
        {
            Account account = GetAccountByNumber();
            if (account == null) return;
            _view.ShowAccountDetails(account);
        }
    }

    // ========== PROGRAM ENTRY ==========
    class Program
    {
        static void Main(string[] args)
        {
            var view = new BankView();
            var controller = new BankController(view);
            controller.Run();
        }
    }
}
