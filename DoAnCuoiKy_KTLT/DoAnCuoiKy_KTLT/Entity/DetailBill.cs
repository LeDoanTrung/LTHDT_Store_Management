using DoAnCuoiKy_KTLT.Entity;

namespace DoAnCuoiKy_KTLT.Entity
{
    public class DetailBill
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public DetailBill(int id, int productId, int quantity)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
