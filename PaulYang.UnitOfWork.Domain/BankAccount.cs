using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaulYang.UnitOfWork.Infrastrcture;

namespace PaulYang.UnitOfWork.Domain
{
    public class BankAccount:EntityBase
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

        public static BankAccount operator+(BankAccount leftBankAccount,BankAccount rightBankAccount)
        {
            var account = new BankAccount {Balance = leftBankAccount.Balance + rightBankAccount.Balance};
            return account;
        }
    }
}
