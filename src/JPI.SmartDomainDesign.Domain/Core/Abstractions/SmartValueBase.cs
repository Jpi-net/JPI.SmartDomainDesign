namespace JPI.SmartDomainDesign.Domain.Core.Abstractions;

public abstract record SmartValueBase<TValue>
{
    protected SmartValueBase(TValue value)
        => Initialize(value);

    public TValue? Value { get; private set; }

    protected abstract void OnValidationError(TValue value);

    protected abstract bool OnValidate(TValue value);

    protected TValue Normalize(TValue value)
        => value;

    private void Initialize(TValue value)
    {
        if (!OnValidate(value))
        {
            OnValidationError(value);
        }
        Value = Normalize(value);
    }
}
