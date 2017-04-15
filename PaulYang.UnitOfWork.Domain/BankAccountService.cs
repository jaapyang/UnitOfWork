using PaulYang.UnitOfWork.Infrastrcture;

namespace PaulYang.UnitOfWork.Domain
{
    public class BankAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BankAccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public void TransferMoney(BankAccount from, BankAccount to, decimal balance)
        {
            if (from.Balance < balance) return;
            from.Balance = from.Balance - balance;
            to.Balance = to.Balance + balance;

            _accountRepository.Save(from);
            _accountRepository.Save(to);
            _unitOfWork.Commit();
        }
    }
}