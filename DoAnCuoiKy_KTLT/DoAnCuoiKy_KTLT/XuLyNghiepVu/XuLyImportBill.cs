using DoAnCuoiKy_KTLT.Entiy;
using DoAnCuoiKy_KTLT.Entity;
using DoAnCuoiKy_KTLT.XyLyLuuTru;

namespace DoAnCuoiKy_KTLT.XuLyNghiepVu
{
    public class XuLyImportBill
    {

        public static ImportBill[] ReadImportBillList(string keyword = "")
        {
            ImportBill[] list = LuuTruImportBill.ReadImportBill();
            ImportBill[] newList;

            int n = 0;

            DateOnly keywordDate = DateOnly.MinValue;
            bool isKeywordDate = false;

            if (!string.IsNullOrEmpty(keyword))
            {
                isKeywordDate = DateOnly.TryParse(keyword, out keywordDate);
            }

            for (int i = 0; i < list.Length; i++)
            {
                if (string.IsNullOrEmpty(keyword) ||
                    (isKeywordDate && list[i].CreatedDate == keywordDate) ||
                    (!isKeywordDate && list[i].Id == int.Parse(keyword)))
                {
                    n++;
                }
            }

            newList = new ImportBill[n];
            int j = 0;

            for (int i = 0; i < list.Length; i++)
            {
                if (string.IsNullOrEmpty(keyword) ||
                    (isKeywordDate && list[i].CreatedDate == keywordDate) ||
                    (!isKeywordDate && list[i].Id == int.Parse(keyword)))
                {
                    newList[j] = list[i];
                    j++;
                }
            }

            return newList;
        }

        public static ImportBill? CreateImportBill(int id, DateOnly date, int[] detailList)
        {
            if (id > 0 && date != DateOnly.MinValue && detailList != null && detailList.Length > 0)
            {
                ImportBill ip;
                ip.Id = id;
                ip.CreatedDate = date;
                ip.detailBillsID = detailList;

                return ip;
            }

            return null;
        }



        public static bool AddImportBill(ImportBill sb)
        {
            return LuuTruImportBill.AddNewImportBill(sb);
        }

        public static ImportBill? SearchImportBillById(int id)
        {
            return LuuTruImportBill.SearchImportBillById(id);
        }

        public static ImportBill? SearchImportBillByCreatedDay(DateOnly date)
        {
            return LuuTruImportBill.SearchImportBillByCreatedDay(date);
        }

        public static ImportBill? EditImportBill(int id, DateOnly date, int[] detailList)
        {
            ImportBill? import = SearchImportBillById(id);

            if (import.HasValue)
            {
                ImportBill newImport = import.Value;
                newImport.CreatedDate = date;
                newImport.detailBillsID = detailList;


                return LuuTruImportBill.EditImportBill(newImport);
            }

            return null;
        }



        // Phương thức sinh mã loại hàng tự động
        public static int GenerateImportBillId()
        {
            ImportBill[] dsLoaiHang = XuLyImportBill.ReadImportBillList();
            // Lấy danh sách mã loại hàng hiện có
            var existingIds = dsLoaiHang.Select(c => c.Id).ToList();

            // Tìm mã loại hàng lớn nhất
            int maxId = existingIds.Any() ? existingIds.Max() : 0;

            // Tạo mã loại hàng mới bằng cách tăng mã loại hàng lớn nhất lên 1
            return maxId + 1;
        }


        public static void DeleteImportBill(int id)
        {

            LuuTruImportBill.DeleteImportBill(id);


        }

    }
}
