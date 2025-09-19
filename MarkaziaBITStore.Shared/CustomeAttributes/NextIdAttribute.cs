namespace MarkaziaBITStore.Shared.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NextIdAttribute : Attribute
    {
        public string? IncrementBy { get; set; } = "1";
    }
}
