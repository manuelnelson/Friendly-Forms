namespace BusinessLogic.Contracts
{
    public interface IPaymentService
    {
        double GetAmount(int amountId);
        int GetMaxMonthlyUsers(int value);
    }
}
