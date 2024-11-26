using DoAnCuoiKy_KTLT.Entiy;

namespace DoAnCuoiKy_KTLT.XyLyLuuTru
{
    public class LuuTruCategory
    {

        //Đọc danh sách Category
        public static Category[] ReadCategory()
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string filePath = Path.Combine(projectRoot, "resource", "category.txt");
            StreamReader file = new StreamReader(filePath);
            int numCategories = int.Parse(file.ReadLine());

            Category[] categories = new Category[numCategories];

            for (int i = 0; i < numCategories; i++)
            {
                string[] categoryInfo = file.ReadLine().Split(';');
                categories[i] = new Category(int.Parse(categoryInfo[0]), categoryInfo[1])
            }

            file.Close();

            return categories;
        }

        //Tìm kiếm Category theo ID
        public static Category? SearchCategoryById(int id) {
            Category[] categories = ReadCategory();

            foreach (Category category in categories)
            {
                if (category.Id == id)
                {
                    return category;
                }
            }

            return null;
        }

        //Tìm kiếm Category theo Name
        public static Category? SearchCategoryByName(string name)
        {
            Category[] categories = ReadCategory();

            foreach (Category category in categories)
            {
                if (category.Name.Contains(name))
                {
                    return category;
                }
            }

            return null;
        }

        //Thêm mới Category
        public static bool AddNewCategory(Category category)
        {
            Category[] categories = ReadCategory();

            Category[] newCateList = new Category[categories.Length+1];

            for (int i = 0; i < categories.Length; i++)
            {
                newCateList[i] = categories[i];
            }

            newCateList[newCateList.Length-1] = category;

            SaveNewListToDB(newCateList);

            return true;
        }


        //Lưu danh sách mới xuống DB
        public static void SaveNewListToDB(Category[] list )
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string filePath = Path.Combine(projectRoot, "resource", "category.txt");
           
            StreamWriter file = new StreamWriter(filePath);

            file.WriteLine(list.Length);

            for (int i = 0; i < list.Length; i++)
            {
                file.WriteLine($"{list[i].Id};{list[i].Name}");
            }
            file.Close();
        }

        //Sửa Category
        public static Category? EditCategory(Category cate)
        {
            Category[] categories = ReadCategory();
            
            for (int i = 0; i< categories.Length; i++)
            {
                if (categories[i].Id == cate.Id)
                {
                    categories[i].Name = cate.Name;
                    SaveNewListToDB(categories);
                    return categories[i];
                }
            }

            return null;
        }

        //Delete Category
        public static void DeleteCategory(int id) {

            Category[] categories = ReadCategory();
            Category[] newList = new Category[categories.Length-1];

            int j = 0;
            for (int i = 0; i < categories.Length; i++)
            {
                if (categories[i].Id != id)
                {
                    newList[j] = categories[i]; 
                    j++;
                }
            }
            SaveNewListToDB(newList);

           
        }
    }
}
