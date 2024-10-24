namespace Application.Common.Http.Responses;

public interface IHasFactoryMethod<out TOutput, in TInput>
{
    static abstract TOutput CreateFrom(TInput input);
}