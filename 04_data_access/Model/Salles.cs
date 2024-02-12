namespace _04_data_access.Model
{
    public class Salles
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int EmployeeId { get; set; }
        public int ClientId { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
