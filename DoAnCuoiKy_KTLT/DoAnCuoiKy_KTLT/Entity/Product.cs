namespace DoAnCuoiKy_KTLT.Entiy
{
    public struct Product
    {
        public int Id; // Mã Sản phẩm
        public string Name; // Tên Sản phẩm
        public DateOnly DueDate; // Hạn sử dụng
        public string CompName; // Tên công ty sản xuất
        public DateOnly DateManufacrure; //Ngày sản xuất
        public int CateId; // Loại hàng của hàng hóa
        public int Price;
        public int Quantity;
    }
}
