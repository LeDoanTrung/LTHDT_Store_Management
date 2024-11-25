using DoAnCuoiKy_KTLT.Entiy;
using DoAnCuoiKy_KTLT.XyLyLuuTru;

namespace DoAnCuoiKy_KTLT.XuLyNghiepVu
{
    public class XuLyCategory
    {
        public static Category[] ReadCategoryList (string keyword ="")
        {
            Category[] categories = LuuTruCategory.ReadCategory();

            Category[] newList;

            int n = 0;
            
            for (int i = 0; i < categories.Length ; i++)
            {
                if (categories[i].Name.Contains(keyword) || categories[i].Id.ToString() == keyword)
                {
                    n += 1;
                }
            }

            newList = new Category[n];

            int j = 0;

            for (int i = 0; i < categories.Length; i++)
            {
                if (categories[i].Name.Contains(keyword) || categories[i].Id.ToString() == keyword)
                {
                    newList[j] = categories[i];
                    j++;
                }
            }

            return newList;
        }

        public static Category? CreateCategory (int id, string name)
        {
            if (id > 0 && !string.IsNullOrEmpty(name)) 
            {
                Category cate;
                cate.Id = id;
                cate.Name = name;
               

                return cate;
            }

            return null;
        }


        public static bool AddCategory (Category cate)
        {
            return LuuTruCategory.AddNewCategory(cate);
        }

        public static Category? SearchCategoryById(int id)
        {
            return LuuTruCategory.SearchCategoryById(id);
        }

        public static Category? SearchCategoryByName(string name)
        {
            return LuuTruCategory.SearchCategoryByName(name);
        }

        public static Category? EditCategory (int id, string name)
        {
            Category? category = SearchCategoryById(id);

            if (category.HasValue)
            {
                Category newCategory = category.Value;
                newCategory.Name = name;
                

                return LuuTruCategory.EditCategory(newCategory);
            }

            return null;
        }

        public static bool IsCategoryExist(int categoryId, string categoryName)
        {
            Category[] categories = ReadCategoryList();

            // Kiểm tra xem mã loại hàng đã tồn tại chưa
            foreach (var category in categories)
            {
                if (category.Id == categoryId)
                {
                    return true;
                }
            }

            // Kiểm tra xem tên loại hàng đã tồn tại chưa
            foreach (var category in categories)
            {
                if (category.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        // Phương thức sinh mã loại hàng tự động
        public static int GenerateCategoryId()
        {
            Category[] dsLoaiHang = XuLyCategory.ReadCategoryList();
            // Lấy danh sách mã loại hàng hiện có
            var existingIds = dsLoaiHang.Select(c => c.Id).ToList();

            // Tìm mã loại hàng lớn nhất
            int maxId = existingIds.Any() ? existingIds.Max() : 0;

            // Tạo mã loại hàng mới bằng cách tăng mã loại hàng lớn nhất lên 1
            return maxId + 1;
        }


        public static void DeleteCategory(int id)
        {

            LuuTruCategory.DeleteCategory(id);


        }

    }
}
