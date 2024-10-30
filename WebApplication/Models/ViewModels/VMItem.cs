namespace WebAppToDo.Models.ViewModels
{
    public class VMItem
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public bool isCompleted { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? finishDate { get; set; }
    }
}
