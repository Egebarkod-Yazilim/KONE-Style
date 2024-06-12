using static KONE.Shared.Utils.Results.ComplexTypes.EnumVariables;

namespace KONE.Business.Utilities
{
    public static class EnumTranslater
    {
        public static string CurrentCardTypeEnumToTR(string currentCardTypeEnum)
        {
            switch (currentCardTypeEnum)
            {
                case nameof(CurrentCardTypeEnum.Grower):
                    return "Çiftçi";
                case nameof(CurrentCardTypeEnum.Merchant):
                    return "Tüccar";
                case nameof(CurrentCardTypeEnum.Logistician):
                    return "Lojistikçi";
                case nameof(CurrentCardTypeEnum.Customer):
                    return "Müşteri";
                case nameof(CurrentCardTypeEnum.ConsumablesSupplier):
                    return "Sarf Malzeme Tedarikçisi";
                default:
                    return "Hatalı Durum";
            }
        }

    }
}
