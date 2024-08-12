namespace SP_HW_09_07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank(10000);
            ATM atm1 = new ATM(bank);
            ATM atm2 = new ATM(bank);

            new Thread(() => atm1.Withdraw(5000)).Start();
            new Thread(() => atm2.Withdraw(6000)).Start();

            Console.ReadLine();
        }
    }

    public class Bank
    {
        private decimal balance;
        private static readonly object locker = new object();

        public Bank(decimal balance)
        {
            this.balance = balance;
        }

        public decimal Balance { get { return balance; } set {  balance = value; } }
        public void Withdraw(decimal money)
        {
            Monitor.Enter(locker);
            try
            {
                if (balance - money < 0)
                {
                    Console.WriteLine($"Not enough money! Current balance: {balance}");
                    return;
                }
                balance -= money;
                Console.WriteLine($"Withdraw {money}$ Current balance: {balance}");
            }
            finally
            {
                Monitor.Exit(locker);
            }
        }
    }

    public class ATM
    {
        private Bank bank;
        public ATM(Bank bank) 
        {
            this.bank = bank;
        }
        public void Withdraw(decimal money)
        {
            bank.Withdraw(money);
        }
    }
}
