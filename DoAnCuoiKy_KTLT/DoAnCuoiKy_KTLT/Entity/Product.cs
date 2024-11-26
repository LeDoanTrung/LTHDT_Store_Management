namespace DoAnCuoiKy_KTLT.Entiy
{
    public class Product
    {
        public int Id { get; set; } // Mã Sản phẩm
        public string Name { get; set; } // Tên Sản phẩm
        public DateOnly DueDate { get; set; } // Hạn sử dụng
        public string CompName { get; set; } // Tên công ty sản xuất
        public DateOnly DateManufacture { get; set; } // Ngày sản xuất
        public int CateId { get; set; } // Loại hàng của hàng hóa
        public int Price { get; set; }
        public int Quantity { get; set; }

        public Product(int id, string name, DateOnly dueDate, string compName, DateOnly dateManufacture, int cateId, int price, int quantity)
        {
            Id = id;
            Name = name;
            DueDate = dueDate;
            CompName = compName;
            DateManufacture = dateManufacture;
            CateId = cateId;
            Price = price;
            Quantity = quantity;
        }
    }
}
