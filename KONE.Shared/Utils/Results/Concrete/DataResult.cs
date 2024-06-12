using KONE.Shared.Utilities.Results.Abstract;
using KONE.Shared.Utilities.Results.ComplexTypes;

namespace KONE.Shared.Utilities.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(ResultStatus resultStatus, T? data)
        {
            ResultStatus = resultStatus;
            Data = data;
        }
        public DataResult(ResultStatus resultStatus, string message, T? data)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
        }
        public DataResult(ResultStatus resultStatus, string message, T? data, Exception? exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
            Exception = exception;
        }
        public DataResult(ResultStatus resultStatus, string message, T? data, Exception? exception, List<string>? messages)
        {
            ResultStatus = resultStatus;
            Message = message;
            Messages = messages;
            Data = data;
            Exception = exception;
        }
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public List<string> Messages { get; } = new List<string>();
        public Exception Exception { get; }
        public T Data { get; }
    }
}
