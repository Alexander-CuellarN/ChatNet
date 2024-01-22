namespace Data.ModelsView
{
    public class ResponseGeneric<T>
    {
        public string Message { get; set; }
        public ICollection<T> Data { get; set; }
    }
}
