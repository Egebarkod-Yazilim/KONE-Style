using Konfrut.Shared.Utilities.Results.ComplexTypes;

namespace Konfrut.Shared.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public List<string> Messages { get; }
        public Exception Exception { get; }
    }
}
