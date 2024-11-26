using DoAnCuoiKy_KTLT.Entity;

namespace DoAnCuoiKy_KTLT.Entity
{
    public class Bill
    {
        public int Id { get; set; }
        public DateOnly CreatedDate { get; set; }
        public int[] DetailBillsID { get; set; }

        public Bill(int id, DateOnly createdDate, int[] detailBillsID)
        {
            Id = id;
            CreatedDate = createdDate;
            DetailBillsID = detailBillsID;
        }
    }
}