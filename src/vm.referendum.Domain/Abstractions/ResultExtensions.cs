namespace vm.referendum.Domain.Abstractions;

public static class ResultExtensions
{
    /// <summary>
    ///     Maps the result value to a new value based on the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The mapping function.</param>
    /// <returns>
    ///     The success result with the mapped value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func)
    {
        return result.IsSuccess ? func(result.Value) : Result.Failure<TOut>(result.Error);
    }

    /// <summary>
    ///     Binds to the result of the function and returns it.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>
    ///     The success result with the bound value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static async Task<Result> Bind<TIn>(this Result<TIn> result, Func<TIn, Task<Result>> func)
    {
        return result.IsSuccess ? await func(result.Value!) : Result.Failure(result.Error);
    }

    // public static async Task<Result> Bind<TIn>(this Result<TIn> result, Func<TIn, Task<Result>> func)
    // {
    //    return result.IsSuccess ? await func(result.Value!) : Result.Failure(result.Error);
    // }
    //

    /// <summary>
    ///     Matches the success status of the result to the corresponding functions.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="onSuccess">The on-success function.</param>
    /// <param name="onFailure">The on-failure function.</param>
    /// <returns>
    ///     The result of the on-success function if the result is a success result, otherwise the result of the failure
    ///     result.
    /// </returns>
    public static async Task<T> Match<T>(
        this Task<Result> resultTask,
        Func<Result, T> onSuccess,
        Func<Error, T> onFailure)
    {
        var result = await resultTask;

        return result.IsSuccess ? onSuccess(result) : onFailure(result.Error);
    }


    public static async Task<T> Match<T>(
        this Task<Result> resultTask,
        Func<T> onSuccess,
        Func<Error, T> onFailure)
    {
        var result = await resultTask;

        return result.IsSuccess ? onSuccess() : onFailure(result.Error);
    }

    public static T Match<T>(
        this Result resultTask,
        Func<Result, T> onValue,
        Func<Error, T> onError)
    {
        return resultTask.IsFailure ? onError(resultTask.Error) : onValue(resultTask);
    }
}