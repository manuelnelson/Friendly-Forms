using BusinessLogic.Contracts;
using Models.Helper;

namespace BusinessLogic
{
    public class PaymentService : IPaymentService
    {
        public double GetAmount(int paymentTier)
        {
            switch (paymentTier)
            {
                case (int)PaymentOptions.Bronze:
                    return 315;
                case (int)PaymentOptions.Silver:
                    return 500;
                case (int)PaymentOptions.Gold:
                    return 750;
                case (int)PaymentOptions.Premiere:
                    return 750;
                default:
                    return 315;
            }
        }

        public int GetMaxMonthlyUsers(int paymentTier)
        {
            switch(paymentTier)
            {
                case (int)PaymentOptions.Bronze:
                    return 7;
                case (int)PaymentOptions.Silver:
                    return 15;
                case (int)PaymentOptions.Gold:
                    return 30;
                case (int)PaymentOptions.Premiere:
                    return 30;
                default:
                    return 7;
            }
        }
    }
}
