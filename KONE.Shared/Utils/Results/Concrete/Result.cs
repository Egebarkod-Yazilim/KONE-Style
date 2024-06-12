using KONE.Shared.Utilities.Results.Abstract;
using KONE.Shared.Utilities.Results.ComplexTypes;

namespace KONE.Shared.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public Result(ResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
        }
        public Result(ResultStatus resultStatus, string message)
        {
            ResultStatus = resultStatus;
            Message = message;
        }
        public Result(ResultStatus resultStatus, string message, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Exception = exception;
        }

        public Result(ResultStatus resultStatus, string message, Exception exception, List<string> messages = null)
        {
            ResultStatus = resultStatus;
            Message = message;
            Exception = exception;
            Messages = messages;
        }
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
        public List<string> Messages { get; }
    }
}
