using DoAnCuoiKy_KTLT.Entity;

namespace DoAnCuoiKy_KTLT.Entiy
{
    public class SaleBill : Bill
    {
        public SaleBill(int id, DateOnly createdDate, int[] detailBillsID)
            : base(id, createdDate, detailBillsID)
        {}
    }
}
