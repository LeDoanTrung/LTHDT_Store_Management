using DoAnCuoiKy_KTLT.Entity;
using DoAnCuoiKy_KTLT.Entiy;
using DoAnCuoiKy_KTLT.XyLyLuuTru;

namespace DoAnCuoiKy_KTLT.XuLyNghiepVu
{
    public class XuLyDetail
    {
        public static DetailBill[] ReadDetailBillList()
        {
            DetailBill[] list = LuuTruDetail.ReadDetailBill();

            return list;
        }

        public static DetailBill? CreateDetailBill(int id, int proId, int quantity)
        {
            if (id > 0 )
            {
                DetailBill detail;
                detail.Id = id;
                detail.productId = proId;
                detail.quantity = quantity;

                return detail;
            }

            return null;
        }


        public static bool AddDetailBill(DetailBill detail)
        {
            return LuuTruDetail.AddNewDetailBill(detail);
        }

        public static DetailBill? SearchDetailBillById(int id)
        {
            return LuuTruDetail.SearchDetailBillById(id);
        }

       

        public static DetailBill? EditDetailBill(int id, int proId, int quantity)
        {
            DetailBill? DetailBill = SearchDetailBillById(id);

            if (DetailBill.HasValue)
            {
                DetailBill newDetailBill = DetailBill.Value;
                newDetailBill.productId = proId;
                newDetailBill.quantity = quantity;

                return LuuTruDetail.EditDetailBill(newDetailBill);
            }

            return null;
        }

        

        // Phương thức sinh mã loại hàng tự động
        public static int GenerateDetailBillId()
        {
            DetailBill[] dsLoaiHang = XuLyDetail.ReadDetailBillList();
            // Lấy danh sách mã loại hàng hiện có
            var existingIds = dsLoaiHang.Select(c => c.Id).ToList();

            // Tìm mã loại hàng lớn nhất
            int maxId = existingIds.Any() ? existingIds.Max() : 0;

            // Tạo mã loại hàng mới bằng cách tăng mã loại hàng lớn nhất lên 1
            return maxId + 1;
        }


        public static void DeleteDetailBillInSale(int id)
        {
            DetailBill? detailBill = SearchDetailBillById(id);           
            //Hoàn trả lại số lượng Product đã bán nếu bill bị xóa
            XuLyProduct.RestockQuantityWhileEditSaleBill(detailBill.Value.productId, detailBill.Value.quantity);
            LuuTruDetail.DeleteDetailBill(id);


        }

        public static void DeleteDetailBillInImport(int id)
        {
            DetailBill? detailBill = SearchDetailBillById(id);
            //Hoàn trả lại số lượng Product đã bán nếu bill bị xóa
            XuLyProduct.UpdateStockQuantityInDatabase(detailBill.Value.productId, detailBill.Value.quantity);
            LuuTruDetail.DeleteDetailBill(id);

        }

        public static void UpdateStockQuantities(DetailBill[] detailBills)
        {
            foreach (var detailBill in detailBills)
            {
                // Truy vấn cơ sở dữ liệu để lấy số lượng tồn kho hiện tại của sản phẩm

                int currentStockQuantity = XuLyProduct.GetQuantityInStock(detailBill.productId);

                // Tính toán số lượng mới sau khi bán
                int updatedStockQuantity = currentStockQuantity - detailBill.quantity;

                // Cập nhật số lượng tồn kho trong cơ sở dữ liệu
                LuuTruProduct.UpdateStockQuantityInDatabase(detailBill.productId, updatedStockQuantity);
            }
        }
    }
}
