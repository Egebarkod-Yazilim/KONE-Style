using KONE.Entities.Concrete;

namespace KONE.Entities.Dtos.CurrentCard
{
    public class CurrentCardAddressList
    {
        public CurrentCardAddressList()
        {
            Addresses = new List<Address>();
        }
        public List<Address> Addresses { get; set; }
        public int CurrentCardId { get; set; }
    }
}
