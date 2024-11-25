using DoAnCuoiKy_KTLT.Entity;
using DoAnCuoiKy_KTLT.Entiy;
using DoAnCuoiKy_KTLT.XyLyLuuTru;
using System.Globalization;

namespace DoAnCuoiKy_KTLT.XuLyNghiepVu
{
    public class XuLySaleBill
    {
        public static SaleBill[] ReadSaleBillList(string keyword = "")
        {
            SaleBill[] list = LuuTruSaleBill.ReadSaleBill();
            SaleBill[] newList;

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

            newList = new SaleBill[n];
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

        public static SaleBill? CreateSaleBill(int id, DateOnly date, int[] detailList)
        {
            if (id > 0 && date != DateOnly.MinValue && detailList != null && detailList.Length > 0)
            {
                SaleBill saleBill;
                saleBill.Id = id;
                saleBill.CreatedDate = date;
                saleBill.detailBillsID = detailList;

                return saleBill;
            }

            return null;
        }



        public static bool AddSaleBill(SaleBill sb)
        {
            return LuuTruSaleBill.AddNewSaleBill(sb);
        }

        public static SaleBill? SearchSaleBillById(int id)
        {
            return LuuTruSaleBill.SearchSaleBillById(id);
        }

        public static SaleBill? SearchSaleBillByCreatedDay(DateOnly date)
        {
            return LuuTruSaleBill.SearchSaleBillByCreatedDay(date);
        }

        public static SaleBill? EditSaleBill(int id, DateOnly date, int[] detailList)
        {
            SaleBill? sale = SearchSaleBillById(id);

            if (sale.HasValue)
            {
                SaleBill newSale = sale.Value;
                newSale.CreatedDate = date;
                newSale.detailBillsID = detailList;


                return LuuTruSaleBill.EditSaleBill(newSale);
            }

            return null;
        }

        

        // Phương thức sinh mã loại hàng tự động
        public static int GenerateSaleBillId()
        {
            SaleBill[] dsLoaiHang = XuLySaleBill.ReadSaleBillList();
            // Lấy danh sách mã loại hàng hiện có
            var existingIds = dsLoaiHang.Select(c => c.Id).ToList();

            // Tìm mã loại hàng lớn nhất
            int maxId = existingIds.Any() ? existingIds.Max() : 0;

            // Tạo mã loại hàng mới bằng cách tăng mã loại hàng lớn nhất lên 1
            return maxId + 1;
        }


        public static void DeleteSaleBill(int id)
        {

            LuuTruSaleBill.DeleteSaleBill(id);


        }


        
        


    }
}
