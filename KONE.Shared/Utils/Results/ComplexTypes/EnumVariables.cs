
namespace KONE.Shared.Utils.Results.ComplexTypes
{
    public class EnumVariables
    {
        public enum CurrentCardTypeEnum
        {
            Grower = 10,
            Merchant = 20,
            Logistician = 30,
            ConsumablesSupplier = 40,
            Customer = 50,
        }

        public enum CompanyTypeEnum
        {
            PersonCompany = 10,
            LimitedCompany = 20,
            IncorporatedCompany = 30
        }

        public enum IntegrationEnum
        {
            InternalSystem = 10,
            ErpIntegration = 20
        }

        public enum CurrentCardStatusEnum
        {
            Waiting = 0,
            Approved = 10,
            Unapproved = 20
        }
        public enum ProductStatusEnum
        {
            Raw = 10,
            SemiProduct = 20,
            Product = 30
        }
    }
}
