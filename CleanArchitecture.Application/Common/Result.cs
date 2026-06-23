namespace CleanArchitecture.Application.Common
{
    public class Result
    {
        protected Result(bool isSuccess, string[] errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string[] Errors { get; }

        public static Result Success() => new(true, []);
        public static Result Failure(params string[] errors) => new(false, errors);
        public static Result Failure(IEnumerable<string> errors) => new(false, errors.ToArray());
    }

    public class Result<T> : Result
    {
        private Result(bool isSuccess, T? value, string[] errors)
            : base(isSuccess, errors)
        {
            Value = value;
        }

        public T? Value { get; }

        public static Result<T> Success(T value) => new(true, value, []);
        public static new Result<T> Failure(params string[] errors) => new(false, default, errors);
        public static new Result<T> Failure(IEnumerable<string> errors) => new(false, default, errors.ToArray());
    }
}
