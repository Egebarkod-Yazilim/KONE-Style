
using KONE.Shared.Utilities.Results.ComplexTypes;

namespace KONE.Shared.Entities.Abstract
{
    public abstract class DtoGetBase
    {
        public DtoGetBase()
        {

        }
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string? Message { get; set; }
        public virtual List<string> Messages { get; set; }
    }
}
