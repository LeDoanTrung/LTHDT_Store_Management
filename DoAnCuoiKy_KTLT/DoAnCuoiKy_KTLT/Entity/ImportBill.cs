using DoAnCuoiKy_KTLT.Entity;

namespace DoAnCuoiKy_KTLT.Entiy
{
    public class ImportBill : Bill
    {
        public ImportBill(int id, DateOnly createdDate, int[] detailBillsID)
            : base(id, createdDate, detailBillsID)
        {}
    }
}
