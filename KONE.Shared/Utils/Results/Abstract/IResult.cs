using KONE.Shared.Utilities.Results.ComplexTypes;

namespace KONE.Shared.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public List<string> Messages { get; }
        public Exception Exception { get; }
    }
}
