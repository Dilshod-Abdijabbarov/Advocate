namespace Advocate.Dtos
{
    public class ResponeMode<T>
    {
        public int TotalItems { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
